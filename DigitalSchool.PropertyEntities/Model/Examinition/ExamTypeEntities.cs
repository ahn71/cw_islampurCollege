using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DS.PropertyEntities.Model.Examinition
{
    public class ExamTypeEntities :IDisposable
    {
        public int ExId { get; set; }
        public string ExName { get; set; }
        public int Ordering { get; set; }
        public bool? semesterexam { get; set; }
        public bool IsActive { get; set; }
       

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
