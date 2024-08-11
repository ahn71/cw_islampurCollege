using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DS.PropertyEntities.Model.SMS;
using System.Data;
using DS.DAL;

namespace DS.BLL.SMS
{
    public class Students : IDisposable
    {       
        string sql = string.Empty;        
        public Students() { }       
        public List<StdEntities> GetEntitiesData(string condition)
        {
            List<StdEntities> ListEntities = new List<StdEntities>();
            sql = string.Format("SELECT [StudentId],[FullName],[RollNo],[ClassName],[SectionName],[ShiftName],[GuardianMobileNo],[Mobile] " +
                                "FROM v_CurrentStudentInfo " + condition + "");
            DataTable dt = new DataTable();

            dt = CRUD.ReturnTableNull(sql);
            if (dt != null)
            {
                if (dt.Rows.Count > 0)
                {
                    ListEntities = (from DataRow row in dt.Rows
                                    select new StdEntities
                                    {
                                        StudentID = int.Parse(row["StudentId"].ToString()),
                                        StudentName = row["FullName"].ToString(),
                                        Roll = int.Parse(row["RollNo"].ToString()),
                                        ClassName = row["ClassName"].ToString(),
                                        Section = row["SectionName"].ToString(),
                                        Shift = row["ShiftName"].ToString(),
                                        GuardiantMobile = row["GuardianMobileNo"].ToString(),
                                        Mobile = row["Mobile"].ToString()
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
