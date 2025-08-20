namespace LMS.Models.ViewModels
{
    public class LeaveUpdateModel
    {
        public long leaveId { get; set; }
        public string empCode { get; set; }
        public string leaveType { get; set; }
        public DateTime fromDate { get; set; }
        public DateTime toDate { get; set; }
        public string fromTime { get; set; }   
        public string toTime { get; set; }     
        public string totalHours { get; set; }
        public string reason { get; set; }
        public string duration { get; set; }
    }

}