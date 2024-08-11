using DS.DAL;
using DS.PropertyEntities.Model.Finance;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Web.UI.WebControls;

namespace DS.BLL.Finance
{
    public class TitleEntry : IDisposable
    {
        private TitleEntities _Entities;
        string sql = string.Empty;
        public TitleEntry() { }
        public TitleEntities AddEntities
        {
            set
            {
                _Entities = value;
            }
        }
        public bool Insert()
        {
            sql = string.Format("INSERT INTO [dbo].[Accounts_Title] " +
                "([Title],[Type]) VALUES (" +
                "'" + _Entities.Title + "','" + _Entities.Type + "')");
               bool result = CRUD.ExecuteQuery(sql);
               return result;
        }
        public bool Update()
        {
            sql = string.Format("UPDATE  [dbo].[Accounts_Title]  set " +
                "[Title]='" + _Entities.Title + "',[Type]='" + _Entities.Type + "' WHERE TitleID='" + _Entities.ID + "' ");
            bool result = CRUD.ExecuteQuery(sql);
            return result;
        }
        public List<TitleEntities> GetEntitiesData()
        {
            List<TitleEntities> ListEntities = new List<TitleEntities>();
            sql = string.Format("SELECT [TitleID],[Title],[Type] FROM [dbo].[Accounts_Title]");
            DataTable dt = new DataTable();

            dt = CRUD.ReturnTableNull(sql);
            if (dt != null)
            {
                if (dt.Rows.Count > 0)
                {
                    ListEntities = (from DataRow row in dt.Rows
                                    select new TitleEntities
                                    {
                                        ID = int.Parse(row["TitleID"].ToString()),
                                        Title = row["Title"].ToString() ,
                                        Type = bool.Parse(row["Type"].ToString())
                                    }).ToList();
                    return ListEntities;
                }

            }
            return ListEntities = null;
        }
        public List<TitleEntities> GetEntitiesData(string TitleType)
        {
            List<TitleEntities> ListEntities = new List<TitleEntities>();
            sql = string.Format("SELECT [TitleID],[Title],[Type] FROM [dbo].[Accounts_Title] where [Type]="+TitleType+"");
            DataTable dt = new DataTable();

            dt = CRUD.ReturnTableNull(sql);
            if (dt != null)
            {
                if (dt.Rows.Count > 0)
                {
                    ListEntities = (from DataRow row in dt.Rows
                                    select new TitleEntities
                                    {
                                        ID = int.Parse(row["TitleID"].ToString()),
                                        Title = row["Title"].ToString(),
                                        Type = bool.Parse(row["Type"].ToString())
                                    }).ToList();
                    return ListEntities;
                }

            }
            return ListEntities = null;
        }
        public void GetAccounts_Title(DropDownList ddl,string TitleType)
        {
            try
            {
                List<TitleEntities> ListEntities = new List<TitleEntities>();
                ListEntities = GetEntitiesData(TitleType);
                if (ListEntities != null)
                {
                    ddl.DataSource = ListEntities.ToList();
                    ddl.DataTextField = "Title";
                    ddl.DataValueField = "ID";
                    ddl.DataBind();
                    ddl.Items.Insert(0, new ListItem("...Select...", "0"));
                }
                else
                {
                    ddl.Items.Insert(0, new ListItem("...Select...", "0"));
                }
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
