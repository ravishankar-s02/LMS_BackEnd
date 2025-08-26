namespace LMS.Models.ViewModels
{
    public class EmployeeFullProfileModel
    {   
        public int? SS_Emp_PK { get; set; }
        public string SS_Emp_Code { get; set; }
        public string Emp_Status { get; set; }
        public string First_Name { get; set; }
        public string Last_Name { get; set; }
        public string Full_Name { get; set; }
        public DateTime? Date_Of_Birth { get; set; }
        public string Gender { get; set; }
        public string Marital_Status { get; set; }
        public string Nationality { get; set; }

        public string Phone_Number { get; set; }
        public string? Alternate_Number { get; set; }
        public string Email { get; set; }
        public string Street_Address { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Zip_Code { get; set; }
        public string Country { get; set; }

        public string Designation { get; set; }
        public string Team_HR_Head { get; set; }
        public string Project_Manager { get; set; }
        public string Team_Lead { get; set; }

        public string Job_Title { get; set; }
        public string Employment_Status { get; set; }
        public DateTime? Joined_Date { get; set; }
        public string Skillset { get; set; }

        public string Pay_Grade { get; set; }
        public string Currency { get; set; }
        public decimal Basic_Salary { get; set; }
        public string Pay_Frequency { get; set; }
    }
}