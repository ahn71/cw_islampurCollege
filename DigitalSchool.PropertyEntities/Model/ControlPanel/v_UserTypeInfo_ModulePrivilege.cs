using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DS.PropertyEntities.Model.ControlPanel
{
    public class v_UserTypeInfo_ModulePrivilege:IDisposable
    {
        public int UserTypeDId { get; set; }
        public int UserTypeId { get; set; }
        public string UserType { get; set; }
        public bool AcademicModule { get; set; }
        public bool AdministrationModule { get; set; }
        public bool NotificationModule { get; set; }
        public bool ReportsModule { get; set; }

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
