namespace LMS.Models.ViewModels
{
    public class UsersLeaveHistoryViewModel
    {
        public string empCode { get; set; }
        public string leaveType { get; set; }
        public DateTime fromDate { get; set; }
        public DateTime toDate { get; set; }
        public string duration { get; set; }
        public string leaveStatus { get; set; }
        public string reason { get; set; }
        public string fullName { get; set; }
    }
}