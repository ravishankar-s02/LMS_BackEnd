namespace LMS.Models.DataModels
{
    public class LoginDM
    {
        // Request fields
        public string EmpCode { get; set; }
        public string Password { get; set; }

        // Response fields
        public byte Status { get; set; }
        public string ErrorMessage { get; set; }

        // User details
        public string FullName { get; set; }
        public string Email { get; set; }
        public string Designation { get; set; }
        public string LoginStatus { get; set; }
    }
}