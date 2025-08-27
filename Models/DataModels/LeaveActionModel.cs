namespace LMS.Models.DataModels
{
    public class LeaveActionModel
    {
        public string Full_Name { get; set; }
        public string Job_Title { get; set; }
        public int SS_Leave_PK { get; set; }
        public string Leave_Type { get; set; }
        public string Duration { get; set; }
        public string Reason { get; set; }
        public DateTime From_Date { get; set; }
        public DateTime To_Date { get; set; }
        public string Leave_Status { get; set; }
        public string Updated_User { get; set; } 
    }
}