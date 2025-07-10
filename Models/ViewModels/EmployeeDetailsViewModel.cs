namespace LMS.Models.ViewModels
{
    public class EmployeeDetailsViewModel
    {
        public string employeeId { get; set; }
        public string fullName => $"{firstName} {lastName}".Trim();
        public string firstName { get; set; }
        public string lastName { get; set; }
        public DateTime? dob { get; set; }
        public string gender { get; set; }
        public string maritalStatus { get; set; }
        public string nationality { get; set; }
    }
}
