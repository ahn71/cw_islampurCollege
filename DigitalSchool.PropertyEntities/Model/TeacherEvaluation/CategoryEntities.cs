using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DS.PropertyEntities.Model.TeacherEvaluation
{
    public class CategoryEntities : IDisposable
    {
        public int CategoryID { get; set; }
        public string Category { get; set; }
        public int Ordering { get; set; }


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