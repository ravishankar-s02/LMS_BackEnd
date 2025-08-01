namespace LMS.Models.ViewModels
{
    public class LeaveActionViewModel
    {
        public string fullName { get; set; }
        public string designation { get; set; }
        public int LeavePK { get; set; }
        public string leaveType { get; set; }
        public string duration { get; set; }
        public string Reason { get; set; }
        public DateTime fromDate { get; set; }
        public DateTime toDate { get; set; }
    }
}