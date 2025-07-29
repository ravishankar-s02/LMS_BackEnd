namespace LMS.Models.ViewModels
{
    public class LoginResponse
    {
        public byte status { get; set; }
        public string errorMessage { get; set; }
        public LoginUserDetails result { get; set; }
    }

    public class LoginUserDetails
    {
        public string empCode { get; set; }
        public string fullName { get; set; }
        public string email { get; set; }
        public string designation { get; set; }
        public string loginStatus { get; set; }
    }
}
