using eShopSolution.ApiIntegration.Interface;
using eShopSolution.ViewModels.System.Users;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Threading.Tasks;

namespace eShopSolution.WebApp.Controllers
{
    public class AduserController : Controller
    {
        private readonly IUserApiClient _userApiClient;
        private readonly IRoleApiClient _roleApiClient;
        private readonly IConfiguration _configuration;

        public AduserController(IUserApiClient userApiClient,
            IRoleApiClient roleApiClient,
            IConfiguration configuration)
        {
            _userApiClient = userApiClient;
            _roleApiClient = roleApiClient;
            _configuration = configuration;
        }

        public async Task<IActionResult> Index(string keyword, int pageIndex = 1, int pageSize = 10)
        {
            var result = await _userApiClient.GetUsersPaging(new GetUserPagingRequest()
            {
                PageIndex = pageIndex,
                PageSize = pageSize,
                Keyword = keyword
            });
            ViewBag.Keyword = keyword;
            if (TempData["success"] != null)
            {
                ViewBag.SuccessMsg = TempData["success"];
            }

            return View(result.ResultObject);
        }

        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            HttpContext.Session.Remove("Token");
            return RedirectToAction("Index", "Login");
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(RegisterRequest request)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }
            var result = await _userApiClient.RegisterUser(request);
            if (result.IsSuccessed)
            {
                TempData["success"] = "Thêm thành công";
                return RedirectToAction("Index", "User");
            }
            ModelState.AddModelError("", result.Message);
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> Edit(Guid Id)
        {
            var result = await _userApiClient.GetById(Id);
            if (result.IsSuccessed)
            {
                var user = result.ResultObject;
                var userUpdate = new UserUpdateRequest()
                {
                    Id = user.Id,
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    Email = user.Email,
                    PhoneNumber = user.Phone,
                    DoB = user.DoB,
                };
                return View(userUpdate);
            }
            return RedirectToAction("Error", "Home");
        }

        [HttpPost]
        public async Task<IActionResult> Edit(UserUpdateRequest request)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }
            var result = await _userApiClient.UpdateUser(request.Id, request);
            if (result.IsSuccessed)
            {
                TempData["success"] = "Cập nhật thành công";
                return RedirectToAction("Index");
            }
            ModelState.AddModelError("", result.Message);
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> Details(Guid Id)
        {
            var result = await _userApiClient.GetById(Id);
            if (result.IsSuccessed)
            {
                var user = result.ResultObject;

                return View(user);
            }
            return RedirectToAction("Error", "Home");
        }

        [HttpGet]
        public async Task<IActionResult> Delete(Guid Id)
        {
            var result = await _userApiClient.GetById(Id);
            if (result.IsSuccessed)
            {
                var user = result.ResultObject;
                var userUpdate = new UserDeleteRequest()
                {
                    Id = user.Id,
                };
                return View(userUpdate);
            }
            return RedirectToAction("Error", "Home");
        }

        [HttpPost]
        public async Task<IActionResult> Delete(UserDeleteRequest request)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }
            var result = await _userApiClient.Delete(request);
            if (result.IsSuccessed)
            {
                TempData["success"] = "Xóa thành công";
                return RedirectToAction("Index");
            }
            ModelState.AddModelError("", result.Message);
            return View();
        }

        //http:localhost/api/users/paging?pageIndex=1&pageSize=10&keyword=
        [HttpGet("paging")]
        public async Task<IActionResult> GetAllPaging([FromQuery] GetUserPagingRequest request)
        {
            var user = await _userApiClient.GetUsersPaging(request);
            return View(user);
        }

        [HttpGet]
        public async Task<IActionResult> RoleAssign(Guid Id)
        {
            var roleAssignRequest = await GetRoleAssignRequest(Id);

            return View(roleAssignRequest);
        }

        [HttpPost]
        public async Task<IActionResult> RoleAssign(RoleAssignRequest request)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }
            var result = await _userApiClient.RoleAssign(request.Id, request);
            if (result.IsSuccessed)
            {
                TempData["success"] = "Gán quyền thành công";
                return RedirectToAction("Index");
            }

            ModelState.AddModelError("", result.Message);
            var roleAssignRequest = await GetRoleAssignRequest(request.Id);
            return View(roleAssignRequest);
        }

        private async Task<RoleAssignRequest> GetRoleAssignRequest(Guid Id)
        {
            var userObj = await _userApiClient.GetById(Id);
            var roleObj = await _roleApiClient.GetAll();
            var roleAssignRequest = new RoleAssignRequest();
            if (roleObj.ResultObject != null || userObj.ResultObject != null)
            {
                foreach (var role in roleObj.ResultObject)
                {
                    roleAssignRequest.Roles.Add(new SelectedItem()
                    {
                        Id = role.Id.ToString(),
                        Name = role.Name,
                        Selected = userObj.ResultObject.Roles.Contains(role.Name)
                    });
                }
            }
            return roleAssignRequest;
        }
    }
}