using DS.DAL;
using DS.PropertyEntities.Model.ControlPanel;
using DS.PropertyEntities.Model.ManageUser;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Web.UI.WebControls;

namespace DS.BLL.ManageUser
{
    public class StudentUserAccountEntry:IDisposable
    {
         private StudentUserAccountEntities _Entities;
        private static List<StudentUserAccountEntities> batchList;
        string sql = string.Empty;
        bool result = true;
        public StudentUserAccountEntry() { }
        public StudentUserAccountEntities AddEntities
        {
            set
            {
                _Entities = value;
            }
        }

        public bool Insert()
        {
            sql = string.Format("INSERT INTO [dbo].[UserAccont_Student] " +
                "([StudentId], [UserName],[UserPassword],[CreatedBy],[CreatedOn],[Status]) "
                +"VALUES ( '" + _Entities.StudentId + "','"+_Entities.UserName+"', " +
                " '" + _Entities.UserPassword + "','" + _Entities.CreatedBy + "','" 
                + _Entities.CreatedOn + "','"+_Entities.Status+"')");
            return result = CRUD.ExecuteQuery(sql);
        }
        public bool Update()
        {
            sql = string.Format("UPDATE [dbo].[UserAccont_Student] SET " +
                "[UserPassword] = '" + _Entities.UserPassword + "', " +
                "[CreatedBy] = '" + _Entities.CreatedBy + "', " +
                "[CreatedOn] = '" + _Entities.CreatedOn + "', " +
                "[Status] = '" + _Entities.Status + "' " +
                "WHERE [StudentAccId] = '" + _Entities.StudentAccId + "'");
            return result = CRUD.ExecuteQuery(sql);
        }
        public bool Chagepassword()
        {
            sql = string.Format("UPDATE [dbo].[UserAccont_Student] SET " +
               "[UserPassword] = '" + _Entities.UserPassword + "' " +
               "WHERE [StudentId] = '" + _Entities.StudentId + "'");
            return result = CRUD.ExecuteQuery(sql);
        }
        public List<StudentUserAccountEntities> GetEntitiesData()
        {
            List<StudentUserAccountEntities> ListEntities = new List<StudentUserAccountEntities>();
            sql = string.Format("SELECT [StudentAccId],[StudentId],[UserName],[UserPassword],[CreatedBy] "
            + " FORMAT(CreatedOn,'dd-MM-yyyy') as CreatedOn ,Status FROM [dbo].[UserAccont_Student] ORDER BY [UserName]");
            DataTable dt = new DataTable();

            dt = CRUD.ReturnTableNull(sql);
            if (dt != null)
            {
                if (dt.Rows.Count > 0)
                {
                    ListEntities = (from DataRow row in dt.Rows
                                    select new StudentUserAccountEntities
                                    {
                                        StudentAccId = int.Parse(row["StudentAccId"].ToString()),
                                        StudentId =int.Parse(row["StudentId"].ToString()),
                                        UserName = row["UserName"].ToString(),
                                        UserPassword = row["UserPassword"].ToString(),
                                        CreatedBy =int.Parse(row["CreatedBy"].ToString()),
                                        CreatedOn = DateTime.Parse(row["CreatedOn"].ToString()),
                                        Status = bool.Parse(row["Status"].ToString())

                                    }).ToList();
                    return ListEntities;
                }

            }
            return ListEntities = null;
        }
        public static void getStudentAccountList(GridView gv)
        {

            try
            {
                DataTable dt = CRUD.ReturnTableNull(@"SELECT s.AdmissionNo,s.FullName,s.BatchId,b.BatchName, u.UserId,u.UserName,u.Password,u.CreatedBy,u.CreatedAt,u.UpdatedBy,u.UpdatedAt,u.IsActive
FROM UsersForPortal u
INNER JOIN CurrentStudentInfo s ON  ISNUMERIC(u.UserName) = 1 and u.UserName = s.AdmissionNo left join BatchInfo b on s.BatchId=b.BatchId");
                gv.DataSource = dt;
                gv.DataBind();
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
