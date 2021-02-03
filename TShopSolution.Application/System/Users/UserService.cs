using TShopSolution.Application.System.Auth;
using TShopSolution.Data.EF;
using TShopSolution.Data.Entities;
using TShopSolution.ViewModels.Common;
using TShopSolution.ViewModels.System.Auth;
using TShopSolution.ViewModels.System.Users;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace TShopSolution.Application.System.Users
{
    public class UserService : IUserService
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly RoleManager<AppRole> _roleManager;
        private readonly IConfiguration _config;
        private readonly IRefreshTokenGenerate _refreshToken;
        private readonly EShopDbContext _context;

        public UserService(UserManager<AppUser> userManager,
            SignInManager<AppUser> signInManager,
            RoleManager<AppRole> roleManager,
            IConfiguration config,
            IRefreshTokenGenerate refreshToken,
            EShopDbContext context
            )
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
            _config = config;
            _refreshToken = refreshToken;
            _context = context;
        }

        public async Task<ApiResult<AuthenticateResponse>> Authenticate(LoginRequest request)
        {
            var user = await _userManager.FindByNameAsync(request.UserName);
            if (user == null) return new ApiErrorResult<AuthenticateResponse>("Tài khoản không tồn tại"); ;

            var result = await _signInManager.PasswordSignInAsync(user, request.Password, request.RememberMe, true);
            if (!result.Succeeded)
            {
                return new ApiErrorResult<AuthenticateResponse>("Tên đăng nhập hoặc mật khẩu không đúng");
            }
            return await GenerateTokenAsync(user);
        }

        public async Task<ApiResult<bool>> Delete(Guid Id)
        {
            var user = await _userManager.FindByIdAsync(Id.ToString());
            if (user == null)
                return new ApiErrorResult<bool>("Không tìm thấy tài khoản");

            await _userManager.DeleteAsync(user);
            return new ApiSuccessResult<bool>();
        }

        public async Task<ApiResult<UserVM>> GetById(Guid id)
        {
            var user = await _userManager.FindByIdAsync(id.ToString());
            if (user == null)
                return new ApiErrorResult<UserVM>("User không tồn tại");
            var roles = await _userManager.GetRolesAsync(user);
            var result = new UserVM()
            {
                Id = user.Id,
                FirstName = user.FirstName,
                LastName = user.LastName,
                DoB = user.DoB,
                UserName = user.UserName,
                Email = user.Email,
                Phone = user.PhoneNumber,
                Roles = roles
            };
            return new ApiSuccessResult<UserVM>(result);
        }

        public async Task<ApiResult<PagedResult<UserVM>>> GetUsersPaging(GetUserPagingRequest request)
        {
            var query = _userManager.Users;
            if (!string.IsNullOrEmpty(request.Keyword))
            {
                query = query.Where(x => x.UserName.Contains(request.Keyword) || x.PhoneNumber.Contains(request.Keyword));
            }
            var totalRow = await query.CountAsync();

            var data = await query.Skip((request.PageIndex - 1) * request.PageSize).Take(request.PageSize)
                .Select(x => new UserVM()
                {
                    Id = x.Id,
                    FirstName = x.FirstName,
                    LastName = x.LastName,
                    DoB = x.DoB,
                    UserName = x.UserName,
                    Email = x.Email
                }).ToListAsync();
            ;
            var pagedResult = new PagedResult<UserVM>
            {
                PageIndex = request.PageIndex,
                PageSize = request.PageSize,
                Items = data,
                TotalRecords = totalRow
            };
            return new ApiSuccessResult<PagedResult<UserVM>>(pagedResult);
        }

        public async Task<ApiResult<bool>> Register(RegisterRequest request)
        {
            var user = await _userManager.FindByNameAsync(request.UserName);
            if (user != null)
                return new ApiErrorResult<bool>("Tài khoản đã tồn tại");
            if (await _userManager.FindByEmailAsync(request.Email) != null)
            {
                return new ApiErrorResult<bool>("Email đã tồn tại");
            }
            user = new AppUser()
            {
                UserName = request.UserName,
                FirstName = request.FirstName,
                LastName = request.LastName,
                Email = request.Email,
                DoB = request.DoB,
                PhoneNumber = request.PhoneNumber,
            };
            var result = await _userManager.CreateAsync(user, request.Password);
            if (result.Succeeded)
            {
                return new ApiSuccessResult<bool>();
            }
            return new ApiErrorResult<bool>("Đăng ký không thành công"); ;
        }

        public async Task<ApiResult<bool>> Update(Guid Id, UserUpdateRequest request)
        {
            if (await _userManager.Users.AnyAsync(x => x.Email == request.Email && x.Id != Id))
                return new ApiErrorResult<bool>("Email đã tồn tại");

            var user = await _userManager.FindByIdAsync(Id.ToString());
            if (user != null)
            {
                user.FirstName = request.FirstName;
                user.LastName = request.LastName;
                user.Email = request.Email;
                user.DoB = request.DoB;
                user.PhoneNumber = request.PhoneNumber;
            }
            var result = await _userManager.UpdateAsync(user);
            if (result.Succeeded)
            {
                return new ApiSuccessResult<bool>();
            }
            return new ApiErrorResult<bool>("Đăng ký không thành công"); ;
        }

        public async Task<ApiResult<bool>> RoleAssign(Guid Id, RoleAssignRequest request)
        {
            var user = await _userManager.FindByIdAsync(Id.ToString());
            if (user == null)
                return new ApiErrorResult<bool>("Tài khoản đã tồn tại");
            // remove role not check from request
            var removeRoles = request.Roles.Where(x => x.Selected == false).Select(x => x.Name).ToList();
            foreach (var roleName in removeRoles)
            {
                if (await _userManager.IsInRoleAsync(user, roleName) == true)
                {
                    await _userManager.RemoveFromRoleAsync(user, roleName);
                }
            }

            // add role is checked from request
            var addRoles = request.Roles.Where(x => x.Selected == true).Select(x => x.Name).ToList();
            foreach (var roleName in addRoles)
            {
                //check role already assigned user?
                if (await _userManager.IsInRoleAsync(user, roleName) == false)
                {
                    await _userManager.AddToRoleAsync(user, roleName);
                }
            }
            return new ApiSuccessResult<bool>();
        }

        private async Task<ApiResult<AuthenticateResponse>> GenerateTokenAsync(AppUser user)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            //Create TOKEN:

            //Claim chua thong tin muon duoc ma hoa thanh 1 chuoi token

            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.Email),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Email, user.Email),
                new Claim("Id", user.Id.ToString()),
            };
            //Su dung 1 key chuoi ngau nghien de tao ra string token
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Tokens:Key"]));
            //Tao string chung chi dang ky cho jwt dua vao key
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            //Token desciption
            var tokenDesrciptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.Now.AddSeconds(45),
                SigningCredentials = credentials
            };

            var token = tokenHandler.CreateToken(tokenDesrciptor);

            var refreshToken = new RefreshToken
            {
                Token = Guid.NewGuid().ToString(),
                JwtId = token.Id,
                UserId = user.Id,
                CreationDate = DateTime.Now,
                ExpiryDate = DateTime.Now.AddMonths(6)
            };

            await _context.RefreshTokens.AddAsync(refreshToken);
            await _context.SaveChangesAsync();

            return new ApiSuccessResult<AuthenticateResponse>(new AuthenticateResponse
            {
                JwtToken = tokenHandler.WriteToken(token),
                RefreshToken = refreshToken.Token
            });
        }

        public async Task<ApiResult<AuthenticateResponse>> Authenticate(AppUser user)
        {
            return await GenerateTokenAsync(user);
        }
    }
}