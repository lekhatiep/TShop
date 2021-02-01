using TShopSolution.Data.Entities;
using TShopSolution.ViewModels.Common;
using TShopSolution.ViewModels.System.Roles;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TShopSolution.Application.System.Roles
{
    public class RoleService : IRoleService
    {
        private readonly RoleManager<AppRole> _roleManager;

        public RoleService(
            RoleManager<AppRole> roleManager

            )
        {
            _roleManager = roleManager;
        }

        public async Task<ApiResult<List<RoleVm>>> GetAll()
        {
            var roles = await _roleManager.Roles
                .Select(x => new RoleVm()
                {
                    Id = x.Id,
                    Name = x.Name,
                    Description = x.Description
                }).ToListAsync();
            return new ApiSuccessResult<List<RoleVm>>(roles);
        }
    }
}