namespace LMS.Models.ViewModels
{
    public class LeaveApplicationViewModel
    {
        public long empFK { get; set; }
        public string empCode { get; set; }
        public string leaveType { get; set; }
        public DateTime startDate { get; set; }
        public DateTime endDate { get; set; }
        public string reason { get; set; }
    }
}