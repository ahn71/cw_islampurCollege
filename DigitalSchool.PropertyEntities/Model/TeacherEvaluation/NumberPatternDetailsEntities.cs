using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DS.PropertyEntities.Model.TeacherEvaluation
{
   public class NumberPatternDetailsEntities : IDisposable
    {
        public int SL { get; set; }       
        public int SubCategoryID { get; set; }
        public float FullNumber { get; set; }
        public float Excellent { get; set; }
        public float Good { get; set; }
        public float Medium { get; set; }
        public float Weak { get; set; }
        public float SoWeak { get; set; }
        

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