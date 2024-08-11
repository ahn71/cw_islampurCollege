using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DS.PropertyEntities.Model.Finance
{
    public class AdmFeesCategoriesEntities : IDisposable
    {
        public int AdmFeeCatId { get; set; }
        public int ClassID { get; set; }
        public string ClassName { get; set; }
        public DateTime DateOfCreation { get; set; }
        public double FeeAmount { get; set; }
        public string FeeCatName { get; set; }
        public DateTime  DateOfStart { get; set; }
        public DateTime DateOfEnd { get; set; }
        public bool IsActive { get; set; }

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
