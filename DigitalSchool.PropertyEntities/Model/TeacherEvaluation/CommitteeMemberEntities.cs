using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DS.PropertyEntities.Model.TeacherEvaluation
{
    public class CommitteeMemberEntities : IDisposable
    {
        public int SessionID { get; set; }
        public int MemberID { get; set; }        

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