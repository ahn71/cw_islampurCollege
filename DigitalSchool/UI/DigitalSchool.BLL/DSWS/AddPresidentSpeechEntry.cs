using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DS.PropertyEntities.Model.DSWS;
using DS.DAL;
using System.Data;

namespace DS.BLL.DSWS
{
    public  class AddPresidentSpeechEntry
    {
        private string sql="";
        private bool result;
        private DataTable dt;
        private AddPresidentEntities _Entities;
        int sL = 1;
        
        public AddPresidentEntities AddEntitis 
        {
            set
            {
                _Entities = value;
            }
        }
        public bool Insert()
        {
            sql = string.Format("INSERT INTO [dbo].[WSPresidentSpeech] " +
                "([Pspeech],[PresidentName],[ImagePath]) VALUES ( " +
                "N'" + _Entities.Speech + "'," +
                "N'" + _Entities.PresidentName + "','"+_Entities.ImgPath+"')");
            return result = CRUD.ExecuteQuery(sql);
        }
        public bool Update()
        {
            sql = string.Format("UPDATE [dbo].[WSPresidentSpeech] SET " +
                "[Pspeech] = N'" + _Entities.Speech + "', " +
                "[PresidentName] = N'" + _Entities.PresidentName + "',[ImagePath]='"+_Entities.ImgPath+"' " +
                " WHERE [PSL] = '" + _Entities.SPId + "'");
            return result = CRUD.ExecuteQuery(sql);
        }
        public bool Delete(string PSL)
        {
            sql = "delete from WSPresidentSpeech where PSL=" + PSL + "";
            return result = CRUD.ExecuteQuery(sql);
        }
        public List<AddPresidentEntities> getEntitiesData() 
        {
            sql = "select * from WSPresidentSpeech order by PSL desc";
            dt =new DataTable();
            List<AddPresidentEntities> ListEntities = new List<AddPresidentEntities>();
            dt = CRUD.ReturnTableNull(sql);
            if (dt != null)
            {
                if (dt.Rows.Count > 0)
                {
                    ListEntities = (from DataRow row in dt.Rows
                                    select new AddPresidentEntities
                                    {
                                        Sl =sL++,
                                        SPId = int.Parse(row["PSL"].ToString()),
                                        Speech = row["Pspeech"].ToString(),
                                        PresidentName = row["PresidentName"].ToString(),
                                        ImgPath = row["ImagePath"].ToString()
                                    }
                                    ).ToList();
                   
               return ListEntities;
                }

            }
            return ListEntities = null;

        }
        public List<AddPresidentEntities> getPresidentSpeechData()
        {
            sql = " select TOP(1) *  from WSPresidentSpeech order by PSL desc";
            dt = new DataTable();
            List<AddPresidentEntities> ListEntities = new List<AddPresidentEntities>();
            dt = CRUD.ReturnTableNull(sql);
            if (dt != null)
            {
                if (dt.Rows.Count > 0)
                {
                    ListEntities = (from DataRow row in dt.Rows
                                    select new AddPresidentEntities
                                    {                                     
                                        SPId = int.Parse(row["PSL"].ToString()),
                                        Speech = row["Pspeech"].ToString(),
                                        PresidentName = row["PresidentName"].ToString(),
                                        ImgPath = row["ImagePath"].ToString()
                                    }
                                    ).ToList();

                    return ListEntities;
                }

            }
            return ListEntities = null;

        }
         //---------------------------------------------------------------------------------------------------------------
        //---------------------------------------------------------------------------------------------------------------
        public bool InsertPr()
        {
            sql = string.Format("INSERT INTO [dbo].[WSPrincipalSpeech] " +
                "([Prinspeech],[PrincipalName],[ImagePath]) VALUES ( " +
                "N'" + _Entities.Speech + "'," +
                "N'" + _Entities.PresidentName + "','" + _Entities.ImgPath + "')");
            return result = CRUD.ExecuteQuery(sql);
        }
        public bool UpdatePr()
        {
            sql = string.Format("UPDATE [dbo].[WSPrincipalSpeech] SET " +
                "[Prinspeech] = N'" + _Entities.Speech + "', " +
                "[PrincipalName] = N'" + _Entities.PresidentName + "',[ImagePath]='" + _Entities.ImgPath + "' " +
                " WHERE [PrinSL] = '" + _Entities.SPId + "'");
            return result = CRUD.ExecuteQuery(sql);
        }
        public bool DeletePr(string PSL)
        {
            sql = "delete from WSPrincipalSpeech where PrinSL=" + PSL + "";
            return result = CRUD.ExecuteQuery(sql);
        }
        public List<AddPresidentEntities> getEntitiesDataPr()
        {
            sql = "select * from WSPrincipalSpeech order by PrinSL desc";
            dt = new DataTable();
            List<AddPresidentEntities> ListEntities = new List<AddPresidentEntities>();
            dt = CRUD.ReturnTableNull(sql);
            if (dt != null)
            {
                if (dt.Rows.Count > 0)
                {
                    ListEntities = (from DataRow row in dt.Rows
                                    select new AddPresidentEntities
                                    {
                                        Sl = sL++,
                                        SPId = int.Parse(row["PrinSL"].ToString()),
                                        Speech = row["Prinspeech"].ToString(),
                                        PresidentName = row["PrincipalName"].ToString(),
                                        ImgPath = row["ImagePath"].ToString()
                                    }
                                    ).ToList();

                    return ListEntities;
                }

            }
            return ListEntities = null;

        }

        public List<AddPresidentEntities> getPrincipalSpeechData()
        {
            sql = "select TOP(1) * from WSPrincipalSpeech order by PrinSL desc";
            dt = new DataTable();
            List<AddPresidentEntities> ListEntities = new List<AddPresidentEntities>();
            dt = CRUD.ReturnTableNull(sql);
            if (dt != null)
            {
                if (dt.Rows.Count > 0)
                {
                    ListEntities = (from DataRow row in dt.Rows
                                    select new AddPresidentEntities
                                    {
                                        Sl = sL++,
                                        SPId = int.Parse(row["PrinSL"].ToString()),
                                        Speech = row["Prinspeech"].ToString(),
                                        PresidentName = row["PrincipalName"].ToString(),
                                        ImgPath = row["ImagePath"].ToString()
                                    }
                                    ).ToList();

                    return ListEntities;
                }

            }
            return ListEntities = null;

        }
    }
}
