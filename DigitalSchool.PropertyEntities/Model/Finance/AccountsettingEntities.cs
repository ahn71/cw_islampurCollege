using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DS.PropertyEntities.Model.Finance
{
    public class AccountsettingEntities : IDisposable
    {
        public int id { get; set; }
        public bool feescount { get; set; }
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
