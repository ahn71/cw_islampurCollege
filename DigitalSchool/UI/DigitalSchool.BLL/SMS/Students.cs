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
        public List<StdEntities> GetEntitiesData(string shift)
        {
            List<StdEntities> ListEntities = new List<StdEntities>();
            sql = string.Format("SELECT cs.[StudentId],cs.[FullName],cs.[RollNo],cs.[ClassName],cs.[SectionName],cs.[Shift],cs.[GuardianMobileNo] " +
                                "FROM [dbo].[CurrentStudentInfo] cs JOIN [dbo].[Classes] c ON " +                               
                                "(cs.[ClassName] = c.[ClassName]) WHERE cs.[Shift] = '" + shift + "' ORDER BY c.[ClassOrder]");
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
                                        Shift = row["Shift"].ToString(),
                                        GuardiantMobile = row["GuardianMobileNo"].ToString()
                                    }).ToList();
                    return ListEntities;
                }

            }
            return ListEntities = null;
        }
        public List<StdEntities> GetEntitiesData(string batch, string section, string shift)
        {
            List<StdEntities> ListEntities = new List<StdEntities>();
            sql = string.Format("SELECT cs.[StudentId],cs.[FullName],cs.[RollNo],cs.[ClassName],cs.[SectionName],cs.[Shift],cs.[GuardianMobileNo] " +
                                "FROM [dbo].[CurrentStudentInfo] cs JOIN [dbo].[Classes] c ON " +
                                "(cs.[ClassName] = c.[ClassName]) WHERE " +
                                "cs.[BatchName] = '" + batch + "' AND cs.[SectionName] = '" + section + "' AND " +
                                "cs.[Shift] = '" + shift + "' ORDER BY c.[ClassOrder]");
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
                                        Shift = row["Shift"].ToString(),
                                        GuardiantMobile = row["GuardianMobileNo"].ToString()
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
