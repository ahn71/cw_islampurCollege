using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DS.PropertyEntities.Model.Examinition
{
   public class ExamResultFailSubject : IDisposable
    {        
     
        public string StudentID { get; set; }
        public string SubID { get; set; }
        public string CourseID { get; set; }
        public string IsAbsent { get; set; }
        public string IsOptionalSub { get; set; }
      

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