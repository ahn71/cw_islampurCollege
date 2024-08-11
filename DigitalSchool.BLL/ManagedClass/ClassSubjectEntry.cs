using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DS.DAL;
using DS.PropertyEntities.Model.ManagedClass;
using DS.PropertyEntities.Model.ManagedSubject;
using System.Data;
using System.Web.UI.WebControls;

namespace DS.BLL.ManagedClass
{
    public class ClassSubjectEntry : IDisposable
    {
        private ClassSubject _Entities;        
        string sql = string.Empty;
        bool result = true;
       
        public ClassSubject AddEntities
        {
            set
            {
                _Entities = value;
            }
        }

        public bool Insert_()
        {

           
                sql = string.Format("INSERT INTO [dbo].[ClassSubject] " +
               "([ClassID], [SubId],[CourseId],[Marks],[SubCode],[Ordering],[IsOptional],[BothType],[GroupId],[IsCommon][RelatedSubId]) VALUES (" +
               "'" + _Entities.ClassId + "'," +
               "'" + _Entities.Subject.SubjectId + "'," +
               "'" + _Entities.CourseID + "'," +
               "'" + _Entities.SubMarks + "'," +
               "'" + _Entities.SubjectCode + "'," +
               "'" + _Entities.OrderBy + "'," +
               "'" + _Entities.IsOptional + "'," +
                "'" + _Entities.BothType + "'," +
               "'" + _Entities.GroupId + "'," +
               "'" + _Entities.IsCommon + "','"+_Entities.RelatedSubId+"')");

              return result = CRUD.ExecuteQuery(sql);


        }

        public bool Insert()
        {


            sql = string.Format("INSERT INTO [dbo].[ClassSubject] " +
           "([ClassID], [SubId],[CourseId],[Marks],[SubCode],[Ordering],[IsOptional],[BothType],[GroupId],[IsCommon],[RelatedSubId]) VALUES (" +
           "'" + _Entities.ClassId + "'," +
           "'" + _Entities.Subject.SubjectId + "'," +
           "'" + _Entities.CourseID + "'," +
           "'" + _Entities.SubMarks + "'," +
           "'" + _Entities.SubjectCode + "'," +
           "'" + _Entities.OrderBy + "'," +
           "'" + _Entities.IsOptional + "'," +
            "'" + _Entities.BothType + "'," +
           "'" + _Entities.GroupId + "'," +
           "'" + _Entities.IsCommon + "','" + _Entities.RelatedSubId + "');SELECT SCOPE_IDENTITY()");

            int ClsSubId=  CRUD.GetMaxID(sql);
            if (ClsSubId > 0)
            {
                if(_Entities.RelatedSubId_CSID=="0")
                {
                    return true;
                }
                else
                   return RelatedSubIdUpdated(_Entities.RelatedSubId_CSID, _Entities.Subject.SubjectId.ToString());
            }
            return false;


        }




        public bool Update()
        {
            sql = string.Format("UPDATE [dbo].[ClassSubject] SET " +
                "[ClassID] = '" + _Entities.ClassId + "', " +
                "[SubId] = '" + _Entities.Subject.SubjectId + "'," +
                "[CourseId] = '" + _Entities.CourseID + "', " +
                "[Marks] = '" + _Entities.SubMarks + "', " +
                "[SubCode] = '" + _Entities.SubjectCode + "', " +
                "[Ordering] = '" + _Entities.OrderBy + "', " +
                "[IsOptional] = '" + _Entities.IsOptional + "', " +
                "[BothType] = '" + _Entities.BothType + "', " +
                "[GroupId] = '" + _Entities.GroupId + "', " +
                "[IsCommon] = '" + _Entities.IsCommon + "'," +
                 "[RelatedSubId] = '" + _Entities.RelatedSubId + "'" +
                "WHERE [CSId] = " + _Entities.ClassSubjectId + "");
            if (CRUD.ExecuteQuery(sql))
            {
                if (_Entities.RelatedSubId_CSID == "0")
                {
                    if (_Entities.RelatedSubId_CSID_Old != "0")
                        RelatedSubIdUpdated(_Entities.RelatedSubId_CSID_Old, "0");
                    
                }
                else
                     RelatedSubIdUpdated(_Entities.RelatedSubId_CSID, _Entities.Subject.SubjectId.ToString());
                return true;
            }
            else
                return false;
        }
        public bool RelatedSubIdUpdated(string CSID, string RelatedSubId)
        {
            sql = string.Format("UPDATE [dbo].[ClassSubject] SET " +
                 "[RelatedSubId] = '" + RelatedSubId + "'" +
                "WHERE [CSId] = " + CSID + "");
            return  CRUD.ExecuteQuery(sql);
        }






        public List<ClassSubject> GetEntitiesData
        {
            get
            {
                List<ClassSubject> ListEntities = new List<ClassSubject>();
                try
                {

                    ClassSubjectEntry ce = new ClassSubjectEntry();
                    ce.sql = string.Format("SELECT [cs].[CSId],[cs].[GroupId],[cs].[ClassID],[c].[ClassName],[c].[ClassOrder],[s].[SubId] as SubjectId,[s].[SubName] as SubjectName," +
                        "[s].[Ordering] as SubOrder,cs.CourseId,acs.CourseName," +
                        "[cs].[SubCode],cs.Marks,cs.Ordering,cs.IsOptional,cs.BothType FROM [dbo].[ClassSubject] cs INNER JOIN [dbo].[Classes] c ON [cs].[ClassID] = [c].[ClassID]" +
                        "INNER JOIN [dbo].[NewSubject] s ON [cs].[SubId] = [s].[SubId] Left Outer JOIN AddCourseWithSubject acs on " +
                        "cs.CourseId=acs.CourseId  Order by [c].[ClassOrder] ASC");
                    DataTable dt = new DataTable();

                    dt = CRUD.ReturnTableNull(ce.sql);
                    if (dt != null)
                    {
                        if (dt.Rows.Count > 0)
                        {
                            ListEntities = (from DataRow row in dt.Rows
                                            select new ClassSubject
                                            {
                                                ClassSubjectId = int.Parse(row["CSId"].ToString()),
                                                ClassId = int.Parse(row["ClassID"].ToString()),
                                                SubMarks = float.Parse(row["Marks"].ToString()),
                                                SubjectCode = row["SubCode"].ToString(),
                                                OrderBy = int.Parse(row["Ordering"].ToString()),
                                                IsOptional = bool.Parse(row["IsOptional"].ToString()),
                                                Mandatory = (bool.Parse(row["IsOptional"].ToString())) ? "No" : "Yes",
                                                BothType = bool.Parse(row["BothType"].ToString()),
                                                Both = (bool.Parse(row["BothType"].ToString())) ? "Yes" : "No",
                                                GroupId = int.Parse(row["GroupId"].ToString()),
                                                Class = new ClassEntities()
                                                {
                                                    ClassID = int.Parse(row["ClassID"].ToString()),
                                                    ClassName = row["ClassName"].ToString(),
                                                    ClassOrder = int.Parse(row["ClassOrder"].ToString())
                                                },
                                                Subject = new SubjectEntities()
                                                {
                                                    SubjectId = int.Parse(row["SubjectId"].ToString()),
                                                    SubjectName = row["SubjectName"].ToString()
                                                },
                                                Course = new CourseEntity()
                                                {
                                                    CourseId = int.Parse(row["CourseId"].ToString()),
                                                    CourseName = row["CourseName"].ToString()
                                                }
                                            }).ToList();
                            return ListEntities;
                        }
                    }

                }
                catch { return ListEntities = null; }

                return ListEntities = null;
            }

        }
        public void LoadMandatorySubjectList(string classID, string GroupID, CheckBoxList chkboxlist)
        {
            try
            {
                sql = string.Format("SELECT SubId,SubName FROM v_ClassSubjectList WHERE ClassID='" + classID + "' "
                + "And GroupId ='" + GroupID + "' and IsOptional='0' and BothType='0' UNION SELECT SubId,SubName FROM "
                + "v_ClassSubjectList WHERE ClassID='" + classID + "' And GroupId ='" + GroupID + "' and IsOptional='1' and BothType='1'");
                DataTable dt = new DataTable();
                dt = CRUD.ReturnTableNull(sql);
                chkboxlist.DataSource = dt;
                chkboxlist.DataTextField = "SubName";
                chkboxlist.DataValueField = "SubId";
                chkboxlist.DataBind();
            }
            catch
            {

            }
        }
        public void LoadMandatorySubjectListWithCommon(string classID, string GroupID, CheckBoxList chkboxlist)
        {
            try
            {
                sql = string.Format("SELECT SubId,SubName FROM v_ClassSubjectList WHERE ClassID='" + classID + "' "
                + "And GroupId ='" + GroupID + "' and IsOptional='0' and BothType='0' UNION SELECT SubId,SubName FROM "
                + "v_ClassSubjectList WHERE ClassID='" + classID + "' And (GroupId ='" + GroupID + "' or IsCommon=1) and IsOptional='1' and BothType='1'");
                DataTable dt = new DataTable();
                dt = CRUD.ReturnTableNull(sql);
                chkboxlist.DataSource = dt;
                chkboxlist.DataTextField = "SubName";
                chkboxlist.DataValueField = "SubId";
                chkboxlist.DataBind();
            }
            catch
            {

            }
        }
        public void LoadOptionalSubjectList(string classID, string GroupID, RadioButtonList rdblist)
        {
            try
            {
                string sql = string.Format("SELECT SubId,SubName FROM v_ClassSubjectList WHERE ClassID='" + classID + "' "
                + "And GroupId in('" + GroupID + "','0') and IsOptional='1' and BothType='0' UNION SELECT SubId,SubName FROM "
                + "v_ClassSubjectList WHERE ClassID='" + classID + "' And GroupId in('" + GroupID + "','0') and IsOptional='1' and BothType='1'");
                DataTable dt = new DataTable();
                dt = CRUD.ReturnTableNull(sql);
                rdblist.DataSource = dt;
                rdblist.DataTextField = "SubName";
                rdblist.DataValueField = "SubId";
                rdblist.DataBind();
            }
            catch
            {

            }
        }

        public static DataTable GetSubjec_Course_tList(int ClassId, string ClsGrpID)  
        {
            try
            {

                string sql = string.Format("select * from v_Class_Subject_Course_List where classId=" + ClassId + " AND  GroupId in (0,"+ClsGrpID+") order by SubjectId,CourseId");
               DataTable dt = CRUD.ReturnTableNull(sql);
               if (dt.Rows.Count > 0) return dt;
               else return null;
                
            }
            catch { return null; }
        }

        public static List<ClassEntities> OnlyClassList
        {
            get {
                  List<ClassEntities> getList=new List<ClassEntities>();
                  DataTable dt = CRUD.ReturnTableNull("select distinct Classid,ClassOrder,ClassName from Classes order by ClassOrder");
                  if (dt.Rows.Count > 0)
                  {
                      getList = (from DataRow dr in dt.Rows
                                 select new ClassEntities
                                 {
                                     ClassID = int.Parse(dr["Classid"].ToString()),
                                     ClassName = dr["CLassName"].ToString(),
                                     ClassOrder = int.Parse(dr["ClassOrder"].ToString())

                                 }).ToList();
                      return getList;

                  }
                  else return null;
            }
        }
        

        public static List<ClassSubject> GetClassSubjectListByFiltering(int ClassId)
        {
               try
                {
                    ClassSubjectEntry clssub = new ClassSubjectEntry();
                    List<ClassSubject> GetList = clssub.GetEntitiesData.FindAll(x => x.ClassId == ClassId).ToList();
                    return GetList;
                }
                catch { return null; }   
        }

        public static DataTable  getSubjectIdByClassSubject(string CSId)
        {
            try
            {
                DataTable dt = new DataTable();
                dt = CRUD.ReturnTableNull("select SubId,CourseId,Isnull(RelatedSubId,0) as RelatedSubId from ClassSubject where CSId=" + CSId+"");
                return dt;
            }
            catch { return null; }
        }
        public static string  getCSIdByClassSubject(string ClassId,string SubId)
        {
            try
            {
                DataTable dt = new DataTable();
                dt = CRUD.ReturnTableNull("select CSID from ClassSubject where classId="+ ClassId + " and subid="+ SubId);
                return dt.Rows[0]["CSID"].ToString();
            }
            catch { return null; }
        }
        //......................For Subject Pattern Report...............
        public DataTable GetDataTable(string ClassId)
        {
            DataTable dt = new DataTable();
            try
            {
                ClassSubjectEntry ce = new ClassSubjectEntry();
                ce.sql = string.Format("SELECT [cs].[CSId],[cs].[GroupId],[cs].[ClassID],[c].[ClassName],[c].[ClassOrder],[s].[SubId] as SubjectId,[s].[SubName] as SubjectName," +
                    "[s].[Ordering] as SubOrder,cs.CourseId,acs.CourseName," +
                    "[cs].[SubCode],cs.Marks,cs.Ordering,cs.IsOptional FROM [dbo].[ClassSubject] cs INNER JOIN [dbo].[Classes] c ON [cs].[ClassID] = [c].[ClassID]" +
                    "INNER JOIN [dbo].[NewSubject] s ON [cs].[SubId] = [s].[SubId] Left Outer JOIN AddCourseWithSubject acs on " +
                    "cs.CourseId=acs.CourseId" +
                     " where [cs].[ClassID]='" + ClassId + "'" +
                    "  Order by cs.Ordering ASC");

                dt = CRUD.ReturnTableNull(ce.sql);
                return dt;
            }
            catch { return dt; }
        }
        //.................................End....................................
        
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


        public string checkSubId() 
        {
            DataTable dt = new DataTable();
            dt = CRUD.ReturnTableNull("select SubId from ClassSubject where classId='221' and subid='" + _Entities.Subject.SubjectId + "'");

            return dt.Rows[0]["SubId"].ToString();
        
        }
    }
}

 //select new ClassSubject
 //                                   {
 //                                       ClassSubjectId = int.Parse(row["CSId"].ToString()),
 //                                       ClassId = int.Parse(row["ClassID"].ToString()),
 //                                       Class = new ClassEntities(){
 //                                           ClassID = int.Parse(row["ClassID"].ToString()),
 //                                           ClassName = row["ClassName"].ToString(),
 //                                           ClassOrder = int.Parse(row["ClassOrder"].ToString())
 //                                       },
 //                                       Subject = new SubjectEntities(){
 //                                           SubjectId = int.Parse(row["SubId"].ToString()),
 //                                           SubjectName = row["SubName"].ToString(),
 //                                           TotalMark = 0,
 //                                           SubjectUnit = int.Parse(row["SubUnit"].ToString()),
 //                                           IsOptional = bool.Parse(row["IsOptional"].ToString()),
 //                                           OrderBy = int.Parse(row["Ordering"].ToString()),
 //                                           Dependency = bool.Parse(row["Dependency"].ToString())
 //                                       },
 //                                       DependencySub = new SubjectEntities(){
 //                                           SubjectId = int.Parse(row["SubId"].ToString()),
 //                                           SubjectName = row["SubName"].ToString(),
 //                                           TotalMark = int.Parse(row["SubTotalMarks"].ToString()),
 //                                           SubjectUnit = int.Parse(row["SubUnit"].ToString()),
 //                                           IsOptional = bool.Parse(row["IsOptional"].ToString()),
 //                                           OrderBy = int.Parse(row["Ordering"].ToString()),
 //                                           Dependency = bool.Parse(row["Dependency"].ToString())
 //                                       },
 //                                       SubjectCode = row["SubCode"].ToString(),
 //                                       subjects = null                                       
 //                                   }).ToList();
