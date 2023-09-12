using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DS.DAL;
using DS.PropertyEntities.Model.SchoolInfo;
using System.Data;

namespace DS.BLL.SchoolInfo
{
    public class SchoolInfoEntry
    {
        private SchoolInfoEntities _Entities;
        string sql = string.Empty;
        bool result = true;
        public SchoolInfoEntry() { }
        public SchoolInfoEntities AddEntities
        {
            set
            {
                _Entities = value;
            }
        }

        public bool Insert()
        {
            sql = string.Format("INSERT INTO [dbo].[School_Setup] " +
                "([SchoolName],[Address],[Country],[Telephone],[Fax]," +
                "[RegistrationNo],[Email],[LogoName],[EmailPassword]) VALUES ( " +
                "'" + _Entities.SchoolName + "'," +
                "'" + _Entities.Address + "'," +
                "'" + _Entities.Country + "'," +
                "'" + _Entities.Telephone + "'," +
                "'" + _Entities.Fax + "'," +
                "'" + _Entities.RegistrationNo + "'," +
                "'" + _Entities.Email + "'," +
                "'" + _Entities.Logo + "'," +
                "'" + _Entities.EmailPassword + "')");
            return result = CRUD.ExecuteQuery(sql);
        }
        public bool Update()
        {
            sql = string.Format("UPDATE [dbo].[School_Setup] SET " +
                "[SchoolName] = '" + _Entities.SchoolName + "'," +
                "[Address] = '" + _Entities.Address + "'," +
                "[Country] = '" + _Entities.Country + "'," +
                "[Telephone] = '" + _Entities.Telephone + "'," +
                "[Fax] = '" + _Entities.Fax + "'," +
                "[RegistrationNo] = '" + _Entities.RegistrationNo + "'," +
                "[Email] = '" + _Entities.Email + "'," +
                "[LogoName] = '" + _Entities.Logo + "'," +
                "[EmailPassword] = '" + _Entities.EmailPassword + "'," +
                "WHERE [DSId] = '" + _Entities.SchoolInfoId + "'");
            return result = CRUD.ExecuteQuery(sql);
        }
        public List<SchoolInfoEntities> GetEntitiesData()
        {
            List<SchoolInfoEntities> ListEntities = new List<SchoolInfoEntities>();
            sql = string.Format("SELECT [DSId],[SchoolName],[Address],[Country],[Telephone]," +
                                "[Fax],[RegistrationNo],[Email],[LogoName],[EmailPassword] FROM [dbo].[School_Setup]");
            DataTable dt = new DataTable();

            dt = CRUD.ReturnTableNull(sql);
            if (dt != null)
            {
                if (dt.Rows.Count > 0)
                {
                    ListEntities = (from DataRow row in dt.Rows
                                    select new SchoolInfoEntities
                                    {
                                        SchoolInfoId = int.Parse(row["DSId"].ToString()),
                                        SchoolName = row["SchoolName"].ToString(),
                                        Address = row["Address"].ToString(),
                                        Country = row["Country"].ToString(),
                                        Telephone = row["Telephone"].ToString(),
                                        Fax = row["Fax"].ToString(),
                                        RegistrationNo = row["RegistrationNo"].ToString(),
                                        Email = row["Email"].ToString(),
                                        Logo = row["LogoName"].ToString(),
                                        EmailPassword = row["EmailPassword"].ToString()
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
