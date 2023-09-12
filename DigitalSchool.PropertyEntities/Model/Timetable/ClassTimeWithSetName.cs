using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DS.PropertyEntities.Model.Timetable
{
    public class ClassTimeWithSetName : IDisposable
    {
        public int Id { get; set; }
        public ClassTimeSpecificationEntities ClassTimeId { get; set; }
        public ClsTimeSetNameEntities ClsTimeSetNameId { get; set; }

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
