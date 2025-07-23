namespace LMS.Models.ViewModels
{
    public class LeaveApplicationViewModel
    {
        public string empCode { get; set; }
        public string leaveType { get; set; }
        public DateTime startDate { get; set; }
        public DateTime endDate { get; set; }
        public TimeSpan? fromTime { get; set; }
        public TimeSpan? toTime { get; set; }
        public decimal? totalHours { get; set; }
        public string duration { get; set; }   
        public string reason { get; set; }
    }
}
