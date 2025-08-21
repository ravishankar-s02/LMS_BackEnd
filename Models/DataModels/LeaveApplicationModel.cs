namespace LMS.Models.DataModels
{
    public class LeaveApplicationModel
    {
        public string EmpCode { get; set; }
        public string LeaveType { get; set; }
        public DateTime FromDate { get; set; }
        public DateTime ToDate { get; set; }
        public string FromTime { get; set; }
        public string ToTime { get; set; }
        public decimal? TotalHours { get; set; }
        public string Duration { get; set; }
        public string Reason { get; set; }
    }

}