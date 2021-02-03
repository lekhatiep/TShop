namespace TShopSolution.ViewModels.System.Auth
{
    public class RefreshTokenRequest
    {
        public string JwtToken { get; set; }
        public string RefreshToken { get; set; }
    }
}