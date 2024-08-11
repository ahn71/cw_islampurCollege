using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DS.PropertyEntities.Model.ControlPanel
{
    public class v_UserTypePageInfo
    {
        public int UserTypeId { get; set; }

        public int PageNameId { get; set; }
        public string PageTitle { get; set; }
        public string ModuleType { get; set; }
        public bool ViewAction { get; set; }
        public bool SaveAction { get; set; }
        public bool UpdateAction { get; set; }
        public bool DeleteAction { get; set; }
        public bool GenerateAction { get; set; }
        public bool AllAction { get; set; }
    }
}
