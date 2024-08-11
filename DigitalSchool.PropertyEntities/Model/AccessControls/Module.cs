using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DS.PropertyEntities.Model.AccessControls
{
    public class Module
    {
        public int ID { get; set; }
        public string ModuleName { get; set; }
        public int ParentID { get; set; }
        public string Url { get; set; }
        public string PhysicalLocation { get; set; }
        public int Ordering { get; set; }
        public string IconClass { get; set; }
        public bool Status { get; set; }
        public DateTime CreatedAt { get; set; }
        public int CreatedBy { get; set; }
    }
}
