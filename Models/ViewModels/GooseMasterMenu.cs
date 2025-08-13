namespace LMS.Models.ViewModels
{
    public class GooseMasterMenu
    {
        public string LinkTitle { get; set; }
        public string LinkPath { get; set; }
        public string Icon { get; set; }
        public bool Disabled { get; set; }
        public bool Hidden { get; set; }
        public List<GooseChildMenu> Children { get; set; } = new List<GooseChildMenu>();
    }
}
