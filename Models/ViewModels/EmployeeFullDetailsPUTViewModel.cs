namespace LMS.Models.ViewModels
{
    public class EmployeeFullDetailsPUTViewModel
    {
        public string EmployeeCode { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string Gender { get; set; }
        public string MaritalStatus { get; set; }
        public string Nationality { get; set; }

        public string PhoneNumber { get; set; }
        public string AlternateNumber { get; set; }
        public string Email { get; set; }
        public string StreetAddress { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string ZipCode { get; set; }
        public string Country { get; set; }

        public string Designation { get; set; }
        public string TeamHRHead { get; set; }
        public string ProjectManager { get; set; }
        public string TeamLead { get; set; }

        public string JobTitle { get; set; }
        public string EmploymentStatus { get; set; }
        public DateTime JoinDate { get; set; }
        public string Skillset { get; set; }

        public string PayGrade { get; set; }
        public string Currency { get; set; }
        public decimal BasicSalary { get; set; }
        public string PayFrequency { get; set; }

        public string UpdatedUser { get; set; }
    }
}