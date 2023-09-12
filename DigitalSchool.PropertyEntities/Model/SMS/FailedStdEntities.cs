using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DS.PropertyEntities.Model.SMS
{
    public class FailedStdEntities : IDisposable
    {
        public int ID { get; set; }
        public StdEntities Student { get; set; }
        public ExamInfo ExmInfo { get; set; }
        public string SubjectName { get; set; }
        public decimal GetMark { get; set; }

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
