using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.UI.WebControls;

namespace DS.PropertyEntities.Model.Examinition
{
    public class ExamDependencyInfoEntity:IDisposable
    {
        public int DeId { get; set; }
        public string ParentExInId { get; set; }
        public CheckBoxList DependencyIExamId { get; set; }
        public bool IsFinal { get; set; }
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
