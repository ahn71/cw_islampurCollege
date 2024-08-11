using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DS.PropertyEntities.Model.SchoolInfo
{
    public class SchoolInfoEntities : IDisposable
    {
        public int SchoolInfoId { get; set; }
        public string SchoolName { get; set; }
        public string Address { get; set; }
        public string Country { get; set; }
        public string Telephone { get; set; }
        public string Fax { get; set; }
        public string RegistrationNo { get; set; }
        public string Email { get; set; }
        public string EmailPassword { get; set; }
        public string Logo { get; set; }

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
