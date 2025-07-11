namespace LMS.Models.DataModels
{
    public class ContactDetailsModel
    {
        public long SS_Contact_Details_PK { get; set; }
        public long SS_Emp_FK { get; set; }
        public string SS_Emp_Code { get; set; }
        public string Phone_Number { get; set; }
        public string Alternate_Number { get; set; }
        public string Email { get; set; }
        public string Street_Address { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Zip_Code { get; set; }
        public string Country { get; set; }
    }
}
