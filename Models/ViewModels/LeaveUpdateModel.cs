namespace LMS.Models.ViewModels
{
    public class LeaveUpdateModel
    {
        public long LeavePK { get; set; }
        public string EmpCode { get; set; }
        public string LeaveType { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string Reason { get; set; }
        public string UpdatedUser { get; set; }
    }

}