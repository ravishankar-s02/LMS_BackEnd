namespace LMS.Models.ViewModels
{
    public class LeaveUpdateModel
    {
        public long LeavePK { get; set; }
        public string empCode { get; set; }
        public string LeaveType { get; set; }
        public DateTime fromDate { get; set; }
        public DateTime toDate { get; set; }
        public string Reason { get; set; }
    }

}