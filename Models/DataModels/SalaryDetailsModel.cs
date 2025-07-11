namespace LMS.Models.DataModels
{
    public class SalaryDetailsModel
    {
        public long SS_Salary_Details_PK { get; set; }
        public long SS_Emp_FK { get; set; }
        public string SS_Emp_Code { get; set; }
        public string Pay_Grade { get; set; }
        public string Currency { get; set; }
        public string Basic_Salary { get; set; }
        public string Pay_Frequency { get; set; }
    }
}
