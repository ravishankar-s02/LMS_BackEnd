namespace LMS.Models.ViewModels
{
    public class EmployeeFullDetailsViewModel
    {   
        public string empFk { get; set; }
        public string empCode { get; set; }
        public string firstName { get; set; }
        public string lastName { get; set; }
        public string fullName { get; set; }
        public string dateOfBirth { get; set; }
        public string gender { get; set; }
        public string maritalStatus { get; set; }
        public string nationality { get; set; }

        public string phoneNumber { get; set; }
        public string alternateNumber { get; set; }
        public string email { get; set; }
        public string streetAddress { get; set; }
        public string city { get; set; }
        public string state { get; set; }
        public string zipCode { get; set; }
        public string country { get; set; }

        public string designation { get; set; }
        public string teamHRHead { get; set; }
        public string projectManager { get; set; }
        public string teamLead { get; set; }

        public string jobTitle { get; set; }
        public string employmentStatus { get; set; }
        public string joinedDate { get; set; }
        public string skillset { get; set; }

        public string payGrade { get; set; }
        public string currency { get; set; }
        public decimal basicSalary { get; set; }
        public string payFrequency { get; set; }
        public string empStatus { get; set; }
    }
}