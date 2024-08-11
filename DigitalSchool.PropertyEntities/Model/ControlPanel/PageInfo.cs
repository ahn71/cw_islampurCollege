using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DS.PropertyEntities.Model.ControlPanel
{
    public class PageInfo:IDisposable
    {
        public int PageNameId { get; set; }
        public string PageName { get; set; }
        public string PageTitle { get; set; }
        public string ModuleType { get; set; }
        public bool Chosen { get; set; }
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
