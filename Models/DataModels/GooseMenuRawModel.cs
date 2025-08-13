namespace LMS.Models.DataModels
{
    public class GooseMenuRawModel
    {
        public long MenuMasterPK { get; set; }
        public string MasterLinkTitle { get; set; }
        public string MasterLinkPath { get; set; }
        public string MasterIcon { get; set; }
        public bool MasterIsDisabled { get; set; }
        public bool MasterIsHidden { get; set; }

        public string ChildLinkCode { get; set; }
        public string ChildLinkTitle { get; set; }
        public string ChildLinkPath { get; set; }
        public string ChildIcon { get; set; }
        public bool? ChildIsDisabled { get; set; }
        public bool? ChildIsHidden { get; set; }
    }
}
