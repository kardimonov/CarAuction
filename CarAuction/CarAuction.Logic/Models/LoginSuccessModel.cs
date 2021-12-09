namespace CarAuction.Logic.Models
{
    public class LoginSuccessModel
    {
        public string AccessToken { get; set; }
        public string UserName { get; set; }
        public string Role { get; set; }
        public string UserId { get; set; }
        public bool Success { get; set; }
    }
}
