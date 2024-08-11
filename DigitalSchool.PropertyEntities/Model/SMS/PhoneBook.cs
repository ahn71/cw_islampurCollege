using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DS.PropertyEntities.Model.SMS
{
    public class PhoneBook : IDisposable
    {
        public int NumID { get; set; }
        public string Number { get; set; }
        public PhoneGroup Group { get; set; }
        public string Name { get; set; }
        public string Details { get; set; }

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
