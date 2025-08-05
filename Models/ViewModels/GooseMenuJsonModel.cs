using System.Collections.Generic;

namespace LMS.Models.ViewModels
{
    public class GooseMenuJsonModel
    {
        public string LinkTitle { get; set; }
        public string LinkPath { get; set; }
        public bool Disabled { get; set; }
        public bool Hidden { get; set; }
        public List<GooseMenuJsonModel> Children { get; set; }
    }

    public class GooseMenuGroupedJsonModel
    {
        public List<GooseMenuJsonModel> MAINMENU { get; set; }
        public List<GooseMenuJsonModel> BOTTOMMENU { get; set; }
    }
}
