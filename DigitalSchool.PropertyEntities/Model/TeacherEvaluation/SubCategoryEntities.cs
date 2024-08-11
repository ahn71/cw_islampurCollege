using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DS.PropertyEntities.Model.TeacherEvaluation
{
    public class SubCategoryEntities : IDisposable
    {   
        public int SubCategoryID{get;set;}
        public CategoryEntities Category { get; set; }
        public string SubCategory { get; set; }
        public int Ordering { get; set; }
        public bool Status { get; set; }


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