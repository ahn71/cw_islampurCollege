using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DS.PropertyEntities.Model.ControlPanel
{
    public class UserAccount:IDisposable
    {
        public int UserId { get; set; }     
        public string FirstName { get; set; }       
        public string LastName { get; set; }      
        public string UserName { get; set; }
        public string UserPassword { get; set; }
        public string Email { get; set; }
        public string UserType { get; set; }
        public int CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public int ModifiedBy { get; set; }
        public DateTime ModifiedOn { get; set; }
        public bool Status { get; set; }
        public string CoockieInfo { get; set; }
        public int UserTypeId { get; set; }
        public int EID { get; set; }
        public Boolean IsAdviser { get; set; }

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
