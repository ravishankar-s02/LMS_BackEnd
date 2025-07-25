namespace LMS.Models.ViewModels
{
    public class MyLeaveHistoryViewModel
    {
        public string leaveId { get; set; }
        public string empCode { get; set; }
        public string leaveType { get; set; }
        public DateTime fromDate { get; set; }
        public DateTime toDate { get; set; }
        public string duration { get; set; }
        public string leaveStatus { get; set; }
        public string reason { get; set; }
    }
}