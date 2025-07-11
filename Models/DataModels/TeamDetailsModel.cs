namespace LMS.Models.DataModels
{
    public class TeamDetailsModel
    {
        public long SS_Team_Details_PK { get; set; }
        public long SS_Emp_FK { get; set; }
        public string SS_Emp_Code { get; set; }
        public string Designation { get; set; }
        public string Team_HR_Head { get; set; }
        public string Project_Manager { get; set; }
        public string Team_Lead { get; set; }
    }
}
