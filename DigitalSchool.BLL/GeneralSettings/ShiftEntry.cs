using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DS.DAL;
using DS.PropertyEntities.Model.GeneralSettings;
using System.Data;
using System.Web.UI.WebControls;

namespace DS.BLL.GeneralSettings
{
    public class ShiftEntry : IDisposable
    {
        private ShiftEntities _Entities;
        string sql = string.Empty;
        bool result = true;
        static List<ShiftEntities> grpList;
        public ShiftEntry() { }
        public ShiftEntities AddEntities
        {
            set
            {
                _Entities = value;
            }
        }

        public bool Insert()
        {
            sql = string.Format("INSERT INTO [dbo].[ShiftConfiguration] " +
                "([ShiftName],[StartTime],[CloseTime],[LateTime]) VALUES (" + 
                "'" + _Entities.ShiftName + "' " +
                "'" + _Entities.StartTime + "' " +
                "'" + _Entities.EndTime + "' " +
                "'" + _Entities.LateTime + "' " +
                ")");
            return result = CRUD.ExecuteQuery(sql);
        }
        public bool Update()
        {
            sql = string.Format("UPDATE [dbo].[ShiftConfiguration] SET " +
                "[ShiftName] = '" + _Entities.ShiftName + "' " +
                "[StartTime] = '" + _Entities.StartTime + "' " +
                "[CloseTime] = '" + _Entities.EndTime + "' " +
                "[LateTime] = '" + _Entities.LateTime + "' " +                
                "WHERE [ConfigId] = '" + _Entities.ShiftConfigId + "'");
            return result = CRUD.ExecuteQuery(sql);
        }
        public List<ShiftEntities> GetEntitiesData()
        {
            List<ShiftEntities> ListEntities = new List<ShiftEntities>();
            sql = string.Format("SELECT [ConfigId],[ShiftName],[StartTime],[CloseTime]," +
                                "[LateTime] FROM [dbo].[ShiftConfiguration]");
            DataTable dt = new DataTable();

            dt = CRUD.ReturnTableNull(sql);
            if (dt != null)
            {
                if (dt.Rows.Count > 0)
                {
                    ListEntities = (from DataRow row in dt.Rows
                                    select new ShiftEntities
                                    {
                                        ShiftConfigId = int.Parse(row["ConfigId"].ToString()),
                                        ShiftName = row["ShiftName"].ToString(),
                                        StartTime = row["StartTime"].ToString(),
                                        EndTime = row["CloseTime"].ToString(),
                                        LateTime = row["LateTime"].ToString()                                       
                                    }).ToList();
                    return ListEntities;
                }
            }
            return ListEntities = null;
        }
        public static void GetDropDownList(DropDownList dl)
        {
            ShiftEntry clsGrp = new ShiftEntry();         
            grpList = clsGrp.GetEntitiesData();       
            if (grpList != null)
            {
                dl.DataTextField = "ShiftName";
                dl.DataValueField = "ShiftConfigId";
                dl.DataSource = grpList.ToList();
                dl.DataBind();
                dl.Items.Insert(0, new ListItem("...Select...", "0"));
            }
        }
        public static void GetShiftList(DropDownList dl)
        {
            ShiftEntry clsGrp = new ShiftEntry();
            grpList = clsGrp.GetEntitiesData();
            if (grpList != null)
            {
                dl.DataTextField = "ShiftName";
                dl.DataValueField = "ShiftConfigId";
                dl.DataSource = grpList.ToList();
                dl.DataBind();
                if(dl.Items.Count>1)
                dl.Items.Insert(0, new ListItem("...Select...", "0"));
            }
        }
        public static void GetShiftListWithAll(DropDownList dl)
        {
            ShiftEntry clsGrp = new ShiftEntry();
            grpList = clsGrp.GetEntitiesData();
            if (grpList != null)
            {
                dl.DataTextField = "ShiftName";
                dl.DataValueField = "ShiftConfigId";
                dl.DataSource = grpList.ToList();
                dl.DataBind();
                if (dl.Items.Count > 1)
                    dl.Items.Insert(0, new ListItem("All", "00"));
            }
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
