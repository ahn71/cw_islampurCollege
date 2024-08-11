using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DS.PropertyEntities.Model.TeacherEvaluation
{
    public class SessionEntities : IDisposable
    {
        public int SessionID { get; set; }
        public string Session { get; set; }
        public string StartDate { get; set; }
        public string EndDate { get; set; }
        public NumberPatternEntities NumPattern { get; set; }
        public CommitteeMemberEntities Member { get; set; }



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
    public class SessionDetailsEntities : IDisposable
    {
        public SessionEntities Session { get; set; }
        public SubCategoryEntities SubCetegory { get; set; }      
        public NumberPatternDetailsEntities NumPattern { get; set; }

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