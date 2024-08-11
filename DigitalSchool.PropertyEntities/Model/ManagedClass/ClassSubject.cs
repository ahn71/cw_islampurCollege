using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DS.PropertyEntities.Model.ManagedSubject;

namespace DS.PropertyEntities.Model.ManagedClass
{
    public class ClassSubject:IDisposable
    {       
        public int ClassSubjectId { get; set; }
        public int ClassId { get; set; }
        public ClassEntities Class { get; set; }
        public SubjectEntities Subject { get; set; }

        public int CourseID { get; set; }
        public CourseEntity Course { get; set; }
        public float SubMarks { get; set; }
        public int OrderBy { get; set; }
        public bool IsOptional { get; set; }
        public bool BothType { get; set; }
        public string Both { get; set; }  
        public string SubjectCode { get; set; }
        public int GroupId { get; set; }
        public string Mandatory { get; set; }
        public bool IsCommon { get; set; }

        public string RelatedSubId { get; set; }
        public string RelatedSubId_CSID { get; set; }
        public string RelatedSubId_CSID_Old { get; set; }

        bool disposed = false;
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposed)
                return;
            disposed = true;
        }
    }
}
