using eShopSolution.Application.System.Users;
using eShopSolution.Data.EF;
using eShopSolution.Data.Entities;
using eShopSolution.ViewModels.Common;
using eShopSolution.ViewModels.System.Auth;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Globalization;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace eShopSolution.Application.System.Auth
{
    public class TokenRefresh : ITokenRefresh
    {
        private readonly IUserService _userService;
        private readonly IConfiguration _config;
        private readonly TokenValidationParameters _tokenValidationParameters;
        private readonly EShopDbContext _context;
        private readonly UserManager<AppUser> _userManager;

        public TokenRefresh(IUserService userService,
            IConfiguration config,
            TokenValidationParameters tokenValidationParameters,
            EShopDbContext context,
            UserManager<AppUser> userManager)
        {
            _userService = userService;
            _config = config;
            _tokenValidationParameters = tokenValidationParameters;
            _context = context;
            _userManager = userManager;
        }

        public async Task<ApiResult<AuthenticateResponse>> RefreshTokenAsync(RefreshTokenRequest request)
        {
            var validatedToken = GetClaimsPrincipalFromToken(request.JwtToken);

            if (validatedToken == null)
            {
                return new ApiErrorResult<AuthenticateResponse>("Invalid Token");
            }
            var exp = validatedToken.Claims.Single(x => x.Type == JwtRegisteredClaimNames.Exp).Value;

            var expiryDateUnix = long.Parse(exp);

            var cultureinfo = CultureInfo.CreateSpecificCulture("vi-VN");

            var expiryDate = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Local).AddSeconds(expiryDateUnix + 25200);

            if (expiryDate > DateTime.Now)
            {
                return new ApiErrorResult<AuthenticateResponse>("This token hasn't expired yet");
            }

            var jwtId = validatedToken.Claims.Single(x => x.Type == JwtRegisteredClaimNames.Jti).Value;

            var storedRefreshToken = _context.RefreshTokens.SingleOrDefault(x => x.Token == request.RefreshToken);

            if (storedRefreshToken == null)
            {
                return new ApiErrorResult<AuthenticateResponse>("This refresh token does not exists");
            }

            if (DateTime.Now > storedRefreshToken.ExpiryDate)
            {
                return new ApiErrorResult<AuthenticateResponse>("This refresh token has expired");
            }

            if (storedRefreshToken.Invalidated)
            {
                return new ApiErrorResult<AuthenticateResponse>("This refresh token has been invalidated");
            }

            if (storedRefreshToken.Used)
            {
                return new ApiErrorResult<AuthenticateResponse>("This refresh token has been used");
            }

            if (storedRefreshToken.JwtId != jwtId)
            {
                return new ApiErrorResult<AuthenticateResponse>("This refresh token does not match this JWT");
            }

            storedRefreshToken.Used = true;
            _context.RefreshTokens.Update(storedRefreshToken);
            await _context.SaveChangesAsync();

            var user = await _userManager.FindByIdAsync(validatedToken.Claims.Single(x => x.Type == "Id").Value);

            return await _userService.Authenticate(user);
        }

        private ClaimsPrincipal GetClaimsPrincipalFromToken(string token)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Tokens:Key"]));
            SecurityToken validatedToken;
            try
            {
                var principal = tokenHandler.ValidateToken(token, new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = key,
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ValidateLifetime = false //here we are saying that we don't care about the token's expiration date
                }, out validatedToken);
                if (!IsJwtWithSercurityAgorithm(validatedToken))
                {
                    return null;
                }
                return principal;
            }
            catch (Exception)
            {
                return null;
            }
        }

        private bool IsJwtWithSercurityAgorithm(SecurityToken validatedToken)
        {
            var jwtToken = validatedToken as JwtSecurityToken;
            return (jwtToken != null) && jwtToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256, StringComparison.InvariantCultureIgnoreCase);
        }
    }
}