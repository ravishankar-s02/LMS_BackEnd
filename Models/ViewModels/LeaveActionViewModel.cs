namespace LMS.Models.ViewModels
{
    public class LeaveActionViewModel
    {
        public string fullName { get; set; }
        public string designation { get; set; }
        public int leavePK { get; set; }
        public string leaveType { get; set; }
        public string duration { get; set; }
        public string reason { get; set; }
        public DateTime fromDate { get; set; }
        public DateTime toDate { get; set; }
        public string leaveStatus { get; set; }
        public string updatedUser { get; set; } 
    }
}