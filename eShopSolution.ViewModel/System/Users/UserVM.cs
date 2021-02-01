using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace TShopSolution.ViewModels.System.Users
{
    public class UserVM
    {
        public Guid Id { get; set; }

        [Display(Name = "Tên")]
        public string FirstName { get; set; }

        [Display(Name = "Họ")]
        public string LastName { get; set; }

        [Display(Name = "Tài khoản")]
        public string UserName { get; set; }

        [Display(Name = "Ngày sinh")]
        public DateTime DoB { get; set; }

        [Display(Name = "Số điện thoại")]
        public string Phone { get; set; }

        [Display(Name = "Địa chỉ Email")]
        public string Email { get; set; }

        public IList<string> Roles { get; set; }
    }
}