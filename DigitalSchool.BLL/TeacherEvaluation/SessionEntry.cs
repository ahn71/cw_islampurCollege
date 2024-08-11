using DS.DAL;
using DS.PropertyEntities.Model.TeacherEvaluation;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Web.UI.WebControls;

namespace DS.BLL.TeacherEvaluation
{
    public class SessionEntry : IDisposable
    {
        private SessionEntities _Entities;
        private CommitteeMemberEntities _EntitiesCM;
        string sql = string.Empty;
        bool result = false;
        DataTable dt;
        public SessionEntities AddEntities
        {
            set
            {
                _Entities = value;
            }
        }
        public CommitteeMemberEntities AddEntitiesCM
        {
            set
            {
                _EntitiesCM = value;
            }
        }
        public bool Insert()
        {
            sql = string.Format("INSERT INTO [dbo].[TE_Session] " +
                 "([Session],[NumPatternID],[StartDate],[EndDate])"
                 + " VALUES ( '" + _Entities.Session + "'," + _Entities.NumPattern.NumPatternID + ",'" + _Entities.StartDate + "','" + _Entities.EndDate + "'); SELECT CAST(scope_identity() AS int)");
            int result=CRUD.GetMaxID(sql);
            
            if(result==0)
            return false;
            else
             _Entities.SessionID=result;
            return true;
        }
        public bool Update()
        {
            sql = string.Format("UPDATE  [dbo].[TE_Session] SET [Session]='" + _Entities.Session + "', " +
                 "  [NumPatternID]=" + _Entities.NumPattern.NumPatternID + ",[StartDate]='" + _Entities.StartDate + "',[EndDate]='" + _Entities.EndDate + "' where [SessionID]=" + _Entities.SessionID + " ; delete TE_Committee where [SessionID]=" + _Entities.SessionID + "");
            //CRUD.ExecuteQuery(sql);
            //sql = "SELECT NumPattern FROM TE_NumberPattern";
            return result = CRUD.ExecuteQuery(sql);
        }
        public bool InsertCM()
        {
            sql = string.Format("INSERT INTO [dbo].[TE_Committee] " +
                 "([SessionID],[MemberID])"
                 + " VALUES ( " + _Entities.SessionID + "," + _EntitiesCM.MemberID + ")");
            return result = CRUD.ExecuteQuery(sql);
            
        }
        //public bool Update()
        //{
        //    sql = string.Format("UPDATE [dbo].[TE_Category] " +
        //         " SET [Category]='" + _Entities.Category + "',[Ordering]=" + _Entities.Ordering + ""
        //         + " WHERE [CategoryID]=" + _Entities.CategoryID + "");
        //    return result = CRUD.ExecuteQuery(sql);
        //}
        public List<SessionEntities> GetEntitiesData()
        {
          
            List<SessionEntities> ListEntities = new List<SessionEntities>();
            sql = string.Format("SELECT [SessionID],[Session],s.[NumPatternID],n.[NumPattern],convert(varchar(10),StartDate,105) StartDate ,convert(varchar(10),EndDate,105) EndDate FROM [dbo].[TE_Session] s inner join [dbo].[TE_NumberPattern] n on s.NumPatternID=n.NumPatternID  ORDER BY [StartDate] Desc");
            DataTable dt = new DataTable();

            dt = CRUD.ReturnTableNull(sql);
            if (dt != null)
            {
                if (dt.Rows.Count > 0)
                {
                    ListEntities = (from DataRow row in dt.Rows
                                    select new SessionEntities
                                    {
                                        SessionID = int.Parse(row["SessionID"].ToString()),
                                        NumPattern = new NumberPatternEntities
                                        {
                                            NumPatternID = int.Parse(row["NumPatternID"].ToString()),
                                            NumPattern = row["NumPattern"].ToString()
                                        },
                                        Session = row["Session"].ToString(),
                                        StartDate = row["StartDate"].ToString(),
                                        EndDate = row["EndDate"].ToString()

                                    }).ToList();
                    return ListEntities;
                }

            }
            return ListEntities = null;
        }
         public List<SessionEntities> GetEntitiesData(string SessionID )
        {
            List<SessionEntities> ListEntities = new List<SessionEntities>();
            sql = string.Format("   SELECT s.[SessionID],[Session],s.[NumPatternID],n.[NumPattern],convert(varchar(10),StartDate,105) StartDate ,convert(varchar(10),EndDate,105) EndDate,c.MemberID FROM [dbo].[TE_Session] s inner join [dbo].[TE_NumberPattern] n on s.NumPatternID=n.NumPatternID inner join TE_Committee c on s.SessionID=c.SessionID where s.SessionID="+SessionID+"");
            DataTable dt = new DataTable();

            dt = CRUD.ReturnTableNull(sql);
            if (dt != null)
            {
                if (dt.Rows.Count > 0)
                {
                    ListEntities = (from DataRow row in dt.Rows
                                    select new SessionEntities
                                    {
                                        SessionID = int.Parse(row["SessionID"].ToString()),
                                        NumPattern = new NumberPatternEntities
                                        {
                                            NumPatternID = int.Parse(row["NumPatternID"].ToString()),
                                            NumPattern = row["NumPattern"].ToString()
                                        },
                                        Member = new CommitteeMemberEntities
                                        {
                                            MemberID = int.Parse(row["MemberID"].ToString())
                                        },
                                        Session = row["Session"].ToString(),
                                        StartDate = row["StartDate"].ToString(),
                                        EndDate = row["EndDate"].ToString()

                                    }).ToList();
                    return ListEntities;
                }

            }
            return ListEntities = null;
        }
         public List<SessionDetailsEntities> GetSessionDetailsBySession(string SessionID)
         {
             List<SessionDetailsEntities> ListEntities = new List<SessionDetailsEntities>();
             sql = string.Format("select s.SessionID,s.Session,c.CategoryID,c.Category,sc.SubCategoryID,sc.SubCategory,nd.NumPatternID,nd.FullNumber,nd.Excellent,"+
                 "nd.Good,nd.Medium,nd.Weak,nd.SoWeak from TE_Session s inner join TE_NumberPatternDetails nd on s.NumPatternID=nd.NumPatternID inner join TE_SubCategory sc"+
                 " on sc.SubCategoryID=nd.SubCategoryID inner join TE_Category c on sc.CategoryID=c.CategoryID where s.SessionID=" + SessionID + " order by c.Ordering,sc.Ordering");
             DataTable dt = new DataTable();
             dt = CRUD.ReturnTableNull(sql);
             if (dt != null)
             {
                 if (dt.Rows.Count > 0)
                 {
                     ListEntities = (from DataRow row in dt.Rows
                                     select new SessionDetailsEntities
                                     {
                                         Session = new SessionEntities
                                         {
                                             
                                             SessionID = int.Parse(row["SessionID"].ToString()),
                                             Session = row["Session"].ToString(),
                                             NumPattern =new NumberPatternEntities
                                             {
                                                 NumPatternID = int.Parse(row["NumPatternID"].ToString())
                                             }
                                            

                                         },
                                         SubCetegory = new SubCategoryEntities
                                         {
                                             SubCategoryID = int.Parse(row["SubCategoryID"].ToString()),
                                             SubCategory = row["SubCategory"].ToString(),
                                             Category = new CategoryEntities 
                                             {
                                                 CategoryID = int.Parse(row["CategoryID"].ToString()),
                                                 Category = row["Category"].ToString()
                                             }
                                         },
                                         NumPattern = new NumberPatternDetailsEntities
                                         {
                                             
                                             FullNumber = float.Parse(row["FullNumber"].ToString()),
                                             Excellent = float.Parse(row["Excellent"].ToString()),
                                             Good = float.Parse(row["Good"].ToString()),
                                             Medium = float.Parse(row["Medium"].ToString()),
                                             Weak = float.Parse(row["Weak"].ToString()),
                                             SoWeak = float.Parse(row["SoWeak"].ToString())
                                         }

                                     }).ToList();
                     return ListEntities;
                 }

             }
             return ListEntities = null;
         }
        public static void GetdataInGridview(GridView gv) 
        {
            DataTable dt = new DataTable();
            dt = CRUD.ReturnTableNull("SELECT [SessionID],[Session],s.[NumPatternID],n.[NumPattern],convert(varchar(10),StartDate,105) StartDate ,convert(varchar(10),EndDate,105) EndDate FROM [dbo].[TE_Session] s inner join [dbo].[TE_NumberPattern] n on s.NumPatternID=n.NumPatternID ORDER BY [StartDate] Desc");
            gv.DataSource = dt;
            gv.DataBind();
        }

        public static void GetDropdownlist(DropDownList dl)
        {

            
            DataTable dt = new DataTable();

            dt = CRUD.ReturnTableNull("SELECT [SessionID],[Session] FROM [dbo].[TE_Session] ORDER BY [StartDate] Desc");
            dl.DataSource = dt;
            dl.DataValueField = "SessionID";
            dl.DataTextField = "Session";
            dl.DataBind();
            dl.Items.Insert(0, new ListItem("...Select...", "0"));

        }
        public static void GetCommitteeMemberDropdownlist(DropDownList dl,string SessionID)
        {

            DataTable dt = new DataTable();
            dt = CRUD.ReturnTableNull("select MemberID,FirstName+' '+LastName as Name from TE_Committee c inner join UserAccount ua on c.MemberID=ua.UserId where c.SessionID=" + SessionID + "");
            dl.DataSource = dt;
            dl.DataValueField = "MemberID";
            dl.DataTextField = "Name";
            dl.DataBind();
            dl.Items.Insert(0, new ListItem("...Select...", "0"));

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