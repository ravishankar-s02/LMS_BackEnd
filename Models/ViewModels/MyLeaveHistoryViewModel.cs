namespace LMS.Models.ViewModels
{
    public class MyLeaveHistoryViewModel
    {
        public string EmpCode { get; set; }
        public string LeaveType { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string Duration { get; set; }
        public string LeaveStatus { get; set; }
        public string Reason { get; set; }
    }
}