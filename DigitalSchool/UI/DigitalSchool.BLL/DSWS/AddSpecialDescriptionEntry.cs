using DS.DAL;
using DS.PropertyEntities.Model.DSWS;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace DS.BLL.DSWS
{
    public class AddSpecialDescriptionEntry
    {
        private AddSecialDescriptionEntities _Entities;
        bool result;
        string sql = "";
        int sL = 1;
        DataTable dt;
        public AddSecialDescriptionEntities AddEntities
        {
            set
            {
                _Entities = value;
            }
        }
        public bool Insert()
        {
            sql = string.Format("INSERT INTO [dbo].[WSSpecialDescription] " +
                "([DSubject],[Details],[EntryDate],[PublishDate],[Type]) VALUES ( " +
                "N'" + _Entities.Subject + "'," +
                "N'" + _Entities.Details + "','" + _Entities.EntryDate + "','" + _Entities.PublishedDate + "'," +
                "N'" + _Entities.Type + "')");
            return result = CRUD.ExecuteQuery(sql);
        }
        public bool Update()
        {
            sql = string.Format("UPDATE [dbo].[WSSpecialDescription] SET " +
                "[DSubject] = N'" + _Entities.Subject + "',[Details] = N'" + _Entities.Details + "'," +
                "[PublishDate]='" + _Entities.PublishedDate + "'," +
                "[Type]=N'" + _Entities.Type + "'" +
                " WHERE [DSL] = '" + _Entities.DSL + "'");
            return result = CRUD.ExecuteQuery(sql);
        }
        public bool Delete(string DSL)
        {
            sql = "delete from WSSpecialDescription where DSL=" + DSL + "";
            return result = CRUD.ExecuteQuery(sql);
        }
        public bool IsItExist() 
        {
            sql = " select DSL from WSSpecialDescription where Type= N'" + _Entities.Type + "'";
            dt = new DataTable();
            dt = CRUD.ReturnTableNull(sql);
            if (dt.Rows.Count > 0) return true;
            else return false;
        }
        public List<AddSecialDescriptionEntities> getEntitiesData()
        {
            sql = " select DSL, DSubject,Details," +
                  "PublishDate,Type" +
                  " from WSSpecialDescription order by DSL desc";
            dt = new DataTable();
            List<AddSecialDescriptionEntities> ListEntities = new List<AddSecialDescriptionEntities>();
            dt = CRUD.ReturnTableNull(sql);
            if (dt != null)
            {
                if (dt.Rows.Count > 0)
                {
                    ListEntities = (from DataRow row in dt.Rows
                                    select new AddSecialDescriptionEntities
                                    {
                                        SL = sL++,
                                        DSL = int.Parse(row["DSL"].ToString()),
                                        Type=row["Type"].ToString(),
                                        Subject = row["DSubject"].ToString(),
                                        Details = row["Details"].ToString(),
                                        // NEntryDate = DateTime.Parse(row["NEntryDate"].ToString()),
                                        PublishedDate = DateTime.Parse(row["PublishDate"].ToString())                                      
                                    }
                                    ).ToList();

                    return ListEntities;
                }

            }
            return ListEntities = null;

        }
        public List<AddSecialDescriptionEntities> getSecialDescriptionByType(string Type)
        {
            sql = " select DSubject,Details" +
                  " from WSSpecialDescription Where Type=N'" + Type + "'";
            dt = new DataTable();
            List<AddSecialDescriptionEntities> ListEntities = new List<AddSecialDescriptionEntities>();
            dt = CRUD.ReturnTableNull(sql);
            if (dt != null)
            {
                if (dt.Rows.Count > 0)
                {
                    ListEntities = (from DataRow row in dt.Rows
                                    select new AddSecialDescriptionEntities
                                    {                              
                                        Subject = row["DSubject"].ToString(),
                                        Details = row["Details"].ToString()
                                    }
                                    ).ToList();

                    return ListEntities;
                }

            }
            return ListEntities = null;

        }
    }
}
