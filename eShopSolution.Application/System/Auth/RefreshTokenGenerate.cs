using System;
using System.Security.Cryptography;

namespace eShopSolution.Application.System.Auth
{
    public class RefreshTokenGenerate : IRefreshTokenGenerate
    {
        public string GenerateToken()
        {
            var randomNumber = new byte[32];
            using (var randomNumberGenerator = RandomNumberGenerator.Create())
            {
                randomNumberGenerator.GetBytes(randomNumber);
                return Convert.ToBase64String(randomNumber);
            }
        }
    }
}