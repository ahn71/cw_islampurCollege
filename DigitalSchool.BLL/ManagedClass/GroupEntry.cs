using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DS.PropertyEntities.Model.ManagedClass;
using DS.DAL;
using System.Data;
using System.Web.UI.WebControls;

namespace DS.BLL.ManagedClass
{
    public class GroupEntry : IDisposable
    {
        private GroupEntities _Entities;
        string sql = string.Empty;
        bool result = true;
        public GroupEntry() { }
        public GroupEntities AddEntities
        {
            set
            {
                _Entities = value;
            }
        }

        public bool Insert()
        {
            sql = string.Format("INSERT INTO [dbo].[Tbl_Group] " +
                "([GroupName]) VALUES ( '" + _Entities.GroupName + "')");

            return result = CRUD.ExecuteQuery(sql);
        }
        public bool Update()
        {
            sql = string.Format("UPDATE [dbo].[Tbl_Group] SET " +
                "[GroupName] = '" + _Entities.GroupName + "' " +
                "WHERE [GroupID] = '" + _Entities.GroupID + "'");
            return result = CRUD.ExecuteQuery(sql);
        }
        public List<GroupEntities> GetEntitiesData()
        {
            List<GroupEntities> ListEntities = new List<GroupEntities>();
            sql = string.Format("SELECT [GroupID],[GroupName] FROM [dbo].[Tbl_Group]");
            DataTable dt = new DataTable();

            dt = CRUD.ReturnTableNull(sql);
            if (dt != null)
            {
                if (dt.Rows.Count > 0)
                {
                    ListEntities = (from DataRow row in dt.Rows
                                    select new GroupEntities
                                    {
                                        GroupID = int.Parse(row["GroupID"].ToString()),
                                        GroupName = row["GroupName"].ToString(),

                                    }).ToList();
                    return ListEntities;
                }

            }
            return ListEntities = null;
        }

        public List<GroupEntities> GetEntitiesData(string GroupId)
        {
            List<GroupEntities> ListEntities = new List<GroupEntities>();
            sql = string.Format("SELECT [GroupID],[GroupName] FROM [dbo].[Tbl_Group] WHERE [GroupID] = '" + GroupId + "'");
            DataTable dt = new DataTable();

            dt = CRUD.ReturnTableNull(sql);
            if (dt != null)
            {
                if (dt.Rows.Count > 0)
                {
                    ListEntities = (from DataRow row in dt.Rows
                                    select new GroupEntities
                                    {
                                        GroupID = int.Parse(row["GroupID"].ToString()),
                                        GroupName = row["GroupName"].ToString()                                       
                                    }).ToList();
                    return ListEntities;
                }

            }
            return ListEntities = null;
        }
        public static void GetEntitiesData(DropDownList dl)
        {
            try
            {
                GroupEntry GroupName = new GroupEntry();
                List<GroupEntities> GroupNameList = GroupName.GetEntitiesData();
                dl.DataTextField = "GroupName";
                dl.DataValueField = "GroupID";
                dl.DataSource = GroupNameList;
                dl.DataBind();               
                dl.Items.Insert(0, new ListItem("...No Group...", "0"));
            }
            catch { }
        }

        public static void GetGroupByClass(RadioButtonList radioButtonList, string ClassId)
        {
            try
            {
             //   radioButtonList.Items.Clear();
                 DataTable dt = new DataTable();
                 dt= CRUD.ReturnTableNull("select cg.GroupID,GroupName from Tbl_Class_Group cg inner join Tbl_Group g on cg.GroupID=g.GroupID where ClassID="+ ClassId + " order by GroupID");
                 radioButtonList.DataValueField = "GroupID";
                 radioButtonList.DataTextField = "GroupName";
                 radioButtonList.DataSource = dt;
                 radioButtonList.DataBind();
                 radioButtonList.Items.Insert(0, new ListItem("Common", "0"));
                radioButtonList.SelectedValue = "0";


            }
            catch { }
        
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
