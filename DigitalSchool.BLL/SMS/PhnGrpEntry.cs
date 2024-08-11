using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using DS.PropertyEntities.Model.SMS;
using DS.DAL;
using System.Web.UI.WebControls;

namespace DS.BLL.SMS
{
    public class PhnGrpEntry : IDisposable
    {
        private PhoneGroup _Entities;
        private static List<PhoneGroup> PhnGrptList;
        string sql = string.Empty;
        bool result = true;
        public PhnGrpEntry()
        { }
        public PhoneGroup AddEntities
        {
            set
            {
                _Entities = value;
            }
        }
        public bool Insert()
        {
            sql = string.Format("INSERT INTO [dbo].[Tbl_Phone_Group] " +
                "([GrpName],[Details]) VALUES ( " +
                "N'" + _Entities.GrpName + "'," +
                "N'" + _Entities.Details + "')");
            return result = CRUD.ExecuteQuery(sql);
        }
        public List<PhoneGroup> GetAllPhnGrp()
        {
            List<PhoneGroup> ListEntities = new List<PhoneGroup>();
            sql = string.Format("SELECT [GrpID],[GrpName],[Details] " +
                                "FROM [dbo].[Tbl_Phone_Group] " +
                                "ORDER BY [GrpID] ASC");
            DataTable dt = new DataTable();

            dt = CRUD.ReturnTableNull(sql);
            if (dt != null)
            {
                if (dt.Rows.Count > 0)
                {
                    ListEntities = (from DataRow row in dt.Rows
                                    select new PhoneGroup
                                    {
                                        GrpID = int.Parse(row["GrpID"].ToString()),
                                        GrpName = row["GrpName"].ToString(),
                                        Details = row["Details"].ToString()
                                    }).ToList();
                    return ListEntities;
                }

            }
            return ListEntities = null;
        }
        public static void GetDropdownlist(DropDownList dl)
        {
            PhnGrpEntry phnGrp = new PhnGrpEntry();
            //if (PhnGrptList == null)
            //{
            //    PhnGrptList = phnGrp.GetAllPhnGrp();
            //}
            PhnGrptList = phnGrp.GetAllPhnGrp();
            dl.DataValueField = "GrpID";
            dl.DataTextField = "GrpName";
            dl.DataSource = PhnGrptList;
            dl.DataBind();
            dl.Items.Insert(0, new ListItem("...Select...", "0"));            
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
