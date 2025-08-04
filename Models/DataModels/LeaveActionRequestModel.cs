namespace LMS.Models.DataModels
{
    public class LeaveActionRequestModel
    {
        public long LeavePK { get; set; }
        public string Action { get; set; } // "APPROVE" or "CANCEL"
    }
}