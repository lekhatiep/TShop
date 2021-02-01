namespace eShopSolution.ViewModels.System.Auth
{
    public class AuthenticateResponse
    {
        public string JwtToken { get; set; }
        public string RefreshToken { get; set; }
    }
}