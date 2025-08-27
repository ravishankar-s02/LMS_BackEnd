namespace LMS.Models.DataModels
{
    public class UsersLeaveHistoryModel
    {
        public string SS_Emp_Code { get; set; }
        public string LeaveType { get; set; }
        public DateTime From_Date { get; set; }
        public DateTime To_Date { get; set; }
        public string Duration { get; set; }
        public string Leave_Status { get; set; }
        public string Reason { get; set; }
        public string Full_Name { get; set; }
    }
}