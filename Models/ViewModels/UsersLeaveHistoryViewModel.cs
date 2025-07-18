namespace LMS.Models.ViewModels
{
    public class UsersLeaveHistoryViewModel
    {
        public string empCode { get; set; }
        public string leaveType { get; set; }
        public DateTime startDate { get; set; }
        public DateTime endDate { get; set; }
        public string duration { get; set; }
        public string leaveStatus { get; set; }
        public string reason { get; set; }
    }
}