namespace LMS.Models.DataModels
{
    public class LeaveUpdateModel
    {
        public long SS_Leave_PK { get; set; }
        public string SS_Emp_Code { get; set; }
        public string Leave_Type { get; set; }
        public DateTime From_Date { get; set; }
        public DateTime To_Date { get; set; }
        public string? From_Time { get; set; }   
        public string? To_Time { get; set; }     
        public decimal? Total_Hours { get; set; }
        public string Reason { get; set; }
        public string Duration { get; set; }
    }

}