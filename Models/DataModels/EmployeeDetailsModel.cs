namespace LMS.Models.DataModels
{
    public class EmployeeDetailsModel
    {
        public long SS_Employee_Details_PK { get; set; }
        public long SS_Emp_FK { get; set; }
        public string SS_Emp_Code { get; set; }
        public string First_Name { get; set; }
        public string? Middle_Name { get; set; }
        public string Last_Name { get; set; }
        public DateTime? Date_Of_Birth { get; set; }
        public string Gender { get; set; }
        public string Marital_Status { get; set; }
        public string Nationality { get; set; }
    }

}
