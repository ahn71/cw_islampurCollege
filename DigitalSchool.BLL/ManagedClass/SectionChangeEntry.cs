using DS.DAL;
using DS.PropertyEntities.Model.ManagedClass;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace DS.BLL.ManagedClass
{
    public class SectionChangeEntry: IDisposable
    {
        string sql = string.Empty;
        bool result = false;
       
        public bool SectionChange(List<SectionChangeEntities> stdPrmtnList)
        {
            foreach (var stdPrmtn in stdPrmtnList)
            {


                sql = string.Format("Update [dbo].[CurrentStudentInfo] SET " +
                                   "ClsSecID = '" + stdPrmtn.NewClsSecID + "'" +
                                   " WHERE [StudentID] = '" + stdPrmtn.Student.StudentID + "'");
                result = CRUD.ExecuteQuery(sql);
                sql = "";
                
                if (result == true)
                {
                    sql = string.Format("insert into Tbl_SectionChange_Log  values(" + stdPrmtn.Student.StudentID + "," + stdPrmtn.PreClsSecID +
                       "," + stdPrmtn.NewClsSecID + ",'" + stdPrmtn.ChangeDate.ToString("yyyy-MM-dd") + "')");
                    result = CRUD.ExecuteQuery(sql);
                    sql = "";
                    
                }               
                if (result) 
                {
                    sql = string.Format("select * from DailyAttendanceRecord where StudentId='" + stdPrmtn.Student.StudentID + "' and AttDate>='" + stdPrmtn.ChangeDate.ToString("yyyy-MM-dd") + "' and format(AttDate,'MM-yyyy')='" + stdPrmtn.ChangeDate.ToString("MM-yyyy") + "'");
                    DataTable dt = new DataTable();
                    dt = CRUD.ReturnTableNull(sql);
                    if (dt.Rows.Count > 0)
                    {
                        sql = string.Format("Update [dbo].[DailyAttendanceRecord] SET " +
                                   "ClsSecID = '" + stdPrmtn.NewClsSecID + "'" +
                                   " WHERE [StudentID] = '" + stdPrmtn.Student.StudentID + "' and AttDate>='" + stdPrmtn.ChangeDate.ToString("yyyy-MM-dd") + "' and format(AttDate,'MM-yyyy')='" + stdPrmtn.ChangeDate.ToString("MM-yyyy") + "'");
                        result = CRUD.ExecuteQuery(sql);
                    }
                }
                
            }
            return result;
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
