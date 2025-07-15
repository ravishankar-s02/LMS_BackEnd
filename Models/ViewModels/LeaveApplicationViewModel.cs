namespace LMS.Models.ViewModels
{
    public class LeaveApplicationViewModel
    {
        public long EmpFK { get; set; }
        public string EmpCode { get; set; }
        public string LeaveType { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string Reason { get; set; }
        public string InsertedUser { get; set; }
    }
}