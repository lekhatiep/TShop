using System;
using System.Collections.Generic;

namespace eShopSolution.ViewModels.System.Users
{
    public class RoleAssignRequest
    {
        public Guid Id { get; set; }
        public List<SelectedItem> Roles { get; set; } = new List<SelectedItem>();
    }
}