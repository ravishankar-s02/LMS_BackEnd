namespace LMS.Models.DataModels
{
    public class JobDetailsModel
    {
        public long SS_Job_Details_PK { get; set; }
        public long SS_Emp_FK { get; set; }
        public string SS_Emp_Code { get; set; }
        public string Job_Title { get; set; }
        public string Employment_Status { get; set; }
        public string Joined_Date { get; set; }
        public string Skillset { get; set; }
    }
}
