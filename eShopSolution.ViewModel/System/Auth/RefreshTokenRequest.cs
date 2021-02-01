using System;
using System.Collections.Generic;
using System.Text;

namespace eShopSolution.ViewModels.System.Auth
{
    public class RefreshTokenRequest
    {
        public string JwtToken { get; set; }
        public string RefreshToken { get; set; }
    }
}