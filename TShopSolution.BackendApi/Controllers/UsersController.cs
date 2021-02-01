using System;
using System.Threading.Tasks;
using TShopSolution.Application.System.Users;
using TShopSolution.ViewModels.System.Users;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace TShopSolution.BackendApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _userService;

        public UsersController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost("authenticate")]
        [AllowAnonymous]
        public async Task<IActionResult> Authenticate([FromBody] LoginRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var result = await _userService.Authenticate(request);
            if (string.IsNullOrEmpty(result.ResultObject))
            {
                return BadRequest(result);
            }

            return Ok(result);
        }

        [HttpPost("register")]
        [AllowAnonymous]
        public async Task<IActionResult> Register([FromBody] RegisterRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var result = await _userService.Register(request);
            if (!result.IsSuccessed)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }

        // GET http:local/api/users/id
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid Id)
        {
            var user = await _userService.GetById(Id);
            return Ok(user);
        }

        // PUT http:local/api/users/id
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Guid Id, [FromBody] UserUpdateRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var result = await _userService.Update(Id, request);
            if (!result.IsSuccessed)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }

        //http:local/api/users/paging?pageIndex=1&pageSixe=1&keyWord=
        [HttpGet("paging")]
        public async Task<IActionResult> GetUserpaging([FromQuery] GetUserPagingRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var user = await _userService.GetUsersPaging(request);
            if (user == null)
            {
                return BadRequest();
            }
            return Ok(user);
        }

        // Delete http:local/api/users/id
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid Id)
        {
            var user = await _userService.Delete(Id);
            return Ok(user);
        }

        [HttpPut("{id}/roles")]
        public async Task<IActionResult> RoleAssign(Guid Id, [FromBody] RoleAssignRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var result = await _userService.RoleAssign(Id, request);
            if (!result.IsSuccessed)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }
    }
}