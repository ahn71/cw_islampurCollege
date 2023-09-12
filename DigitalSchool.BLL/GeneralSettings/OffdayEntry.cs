using DS.DAL;
using DS.DAL.ComplexScripting;
using DS.PropertyEntities.Model.GeneralSettings;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace DS.BLL.GeneralSettings
{
    public class OffdayEntry: IDisposable
    {
        private OffdayEntities _Entities;
        string sql = string.Empty;
        bool result = true;
        static List<ShiftEntities> grpList;
        public OffdayEntry() { }
        public OffdayEntities AddEntities
        {
            set
            {
                _Entities = value;
            }
        }

       
        public List<OffdayEntities> GetEntitiesData()
        {
            List<OffdayEntities> ListEntities = new List<OffdayEntities>();
            sql = string.Format("SELECT [OffDateId],CONVERT(VARCHAR(11), OffDate, 105) as OffDate,[Purpose],[DayName]," +
                                "[Month] FROM [dbo].[OffdaySettings]");
            DataTable dt = new DataTable();

            dt = CRUD.ReturnTableNull(sql);
            if (dt != null)
            {
                if (dt.Rows.Count > 0)
                {
                    ListEntities = (from DataRow row in dt.Rows
                                    select new OffdayEntities
                                    {
                                        OffDateId = int.Parse(row["OffDateId"].ToString()),
                                        OffDate = row["OffDate"].ToString() == "" ? (DateTime?)null : convertDateTime.getCertainCulture(row["OffDate"].ToString()),
                                        Purpose = row["Purpose"].ToString(),
                                        DayName = row["DayName"].ToString(),
                                        Month = row["Month"].ToString()                                       
                                    }).ToList();
                    return ListEntities;
                }
            }
            return ListEntities = null;
        }       

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
