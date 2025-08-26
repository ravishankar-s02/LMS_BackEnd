namespace LMS.Models.DataModels
{
    public class MyLeaveHistoryModel
    {
        public string SS_Leave_PK { get; set; }
        public string SS_Emp_Code { get; set; }
        public string Leave_Type { get; set; }
        public DateTime From_Date { get; set; }
        public DateTime To_Date { get; set; }
        public string From_Time { get; set; }
        public string To_Time { get; set; }
        public string Duration { get; set; }
        public string Leave_Status { get; set; }
        public string Reason { get; set; }
    }
}