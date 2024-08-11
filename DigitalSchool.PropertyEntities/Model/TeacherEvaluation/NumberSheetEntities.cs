using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DS.PropertyEntities.Model.TeacherEvaluation
{
    public class NumberSheetEntities : IDisposable
    {
        public int SessionID { get; set; }
        public int MemberID { get; set; }
        public int SubCategoryID { get; set; }
        public int EID { get; set; }
        public float ObtainNumber { get; set; }


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