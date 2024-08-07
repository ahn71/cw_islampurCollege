using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DS.Classes
{
    public class StudentInfoDto
    {
        public int AdmissionFormNo { get; set; }
        public string FullName { get; set; }
        public string FathersName { get; set; }
        public int ClassId { get; set; }
        public string ClassName { get; set; }
        public int? AdmissionYear { get; set; }
        public string GroupName { get; set; }
        public string ImageName { get; set; }
        public string Mobile { get; set; } 
    }
}