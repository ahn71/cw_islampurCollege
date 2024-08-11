using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DS.PropertyEntities.Model.Attendance
{
    public class SheetInfoEntities : IDisposable
    {
        public int sheetInfoID { get; set; }
        public string ASName { get; set; }
        public string Batch { get; set; }
        public string Class { get; set; }
        public string Section { get; set; }
        public string Month { get; set; }
        public int Year { get; set; }

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
