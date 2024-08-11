using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DS.DAL;
using DS.PropertyEntities.Model.HR;
using System.Data;

namespace DS.BLL.HR
{
    public class EmpDepartmentEntry
    {
        private EmpDepartment _Entities;
        string sql = string.Empty;
        bool result = true;
        public EmpDepartmentEntry() { }
        public EmpDepartment AddEntities
        {
            set
            {
                _Entities = value;
            }
        }

        public bool Insert()
        {
            return result = CRUD.ExecuteQuery(sql);
        }
        public bool Update()
        {            
            return result = CRUD.ExecuteQuery(sql);
        }
        public List<EmpDepartment> GetEntitiesData()
        {
            List<EmpDepartment> ListEntities = new List<EmpDepartment>();
            sql = string.Format("SELECT  *, (SELECT COUNT(*) FROM [dbo].[Tbl_Subject_Teacher] WHERE EId = [e].[EID]) as WorkLoad " +
                                "FROM [dbo].[Departments_HR] d INNER JOIN " +
                                "[dbo].[EmployeeInfo] e ON " +
                                "[d].[DId] = [e].[DId] " +
                                "WHERE [d].[IsTeacher] = 'true' ORDER BY [d].[DId] ASC");
            DataTable dt = new DataTable();

            dt = CRUD.ReturnTableNull(sql);
            if (dt != null)
            {
                if (dt.Rows.Count > 0)
                {
                    ListEntities = (from DataRow row in dt.Rows
                                    select new EmpDepartment
                                    {
                                        Department = new DepartmentEntities
                                        {
                                            DepartmentId = int.Parse(row["DId"].ToString()),
                                            DepartmentName = row["DName"].ToString(),
                                            IsTeacher = bool.Parse(row["DStatus"].ToString()),
                                            Status = bool.Parse(row["IsTeacher"].ToString())
                                        },
                                        Employee = new Employee
                                        {
                                            EmployeeId = int.Parse(row["EID"].ToString()),
                                            EmpName = row["EName"].ToString()
                                        },
                                        WorkLoad = int.Parse((row["WorkLoad"].ToString() == string.Empty) ? "0" : row["WorkLoad"].ToString())                                                                              
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
