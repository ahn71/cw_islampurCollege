using DS.DAL;
using DS.PropertyEntities.Model.Examinition;
using DS.PropertyEntities.Model.ManagedBatch;
using DS.PropertyEntities.Model.ManagedClass;
using DS.PropertyEntities.Model.ManagedSubject;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Web.UI.WebControls;

namespace DS.BLL.Examinition
{
    public class SubQuestionPatternEntry:IDisposable
    {
        private SubQuestionPatternEntities _Entities;
        string sql = string.Empty;
        bool result = true;

        public SubQuestionPatternEntities AddEntities
        {
            set
            {
                _Entities = value;
            }
        }

        public bool Insert()
        {
            sql = string.Format("INSERT INTO [dbo].[SubjectQuestionPattern] " +
                "([ExId], [BatchId],[ClsGrpID],[SubId],[CourseId],[QPId],[QMarks]," +
                "[PassMarks],[ConvertTo],[IsOptional]) VALUES (" +
                "'" + _Entities.ExamType.ExId + "'," +
                "'" + _Entities.Batch.BatchId + "'," +
                "'" + _Entities.Group.ClsGrpID + "'," +
                "'" + _Entities.Subject.SubjectId + "'," +
                "'" + _Entities.Course.CourseId + "'," +
                "'" + _Entities.QPattern.QPId + "'," +
                "'" + _Entities.QMarks + "'," +
                "'" + _Entities.PassMarks + "'," +
                "'" + _Entities.ConvertTo + "'," +                   
                "'" + _Entities.IsOptional + "')");
            return result = CRUD.ExecuteQuery(sql);
        }
        public bool Update()
        {
            sql = string.Format("UPDATE [dbo].[SubjectQuestionPattern] SET " +
                "[ExId] = '" + _Entities.ExamType.ExId + "', " +
                "[BatchId] = '" + _Entities.Batch.BatchId + "'," +
                "[ClsGrpID] = '" + _Entities.Group.ClsGrpID + "', " +
                "[SubId] = '" + _Entities.Subject.SubjectId + "', " +
                "[CourseId] = '" + _Entities.Course.CourseId + "', " +
                "[QPId] = '" + _Entities.QPattern.QPId + "', " +
                "[QMarks] = '" + _Entities.QMarks + "', " +
                "[PassMarks] = '" + _Entities.PassMarks + "', " +
                "[ConvertTo] = '" + _Entities.ConvertTo + "', " +
                "[IsOptional] = '" + _Entities.IsOptional + "' " +
                "WHERE [SubQPId] = " + _Entities.SubQPId + "");
            return result = CRUD.ExecuteQuery(sql);
        }
        public bool Delete()
        {
            sql = string.Format("Delete From [dbo].[SubjectQuestionPattern]"+
                "WHERE [ExId] =' " + _Entities.ExamType.ExId + "'"
            + " and [BatchId] =' " + _Entities.Batch.BatchId + "'"
            + " and [ClsGrpID] =' " + _Entities.Group.ClsGrpID + "'");
            return result = CRUD.ExecuteQuery(sql);
        }
        public DataTable GetDataTable(string ExId,string BatchId,string GroupId)
        {
            DataTable dt = new DataTable();
            try
            {
                sql = string.Format("SELECT sqp.SubQPId,sqp.ClsGrpID, et.ExId, grp.GroupName, et.ExName, bi.BatchId," 
                                               +"bi.BatchName, ns.SubId, ns.SubName,acws.CourseId, acws.CourseName, qp.QPId,"
	                                           +" qp.QPName, sqp.QMarks, sqp.PassMarks,sqp.ConvertTo, sqp.IsOptional"
                                       +" FROM   dbo.AddCourseWithSubject AS acws RIGHT OUTER JOIN "
                                       +"dbo.SubjectQuestionPattern AS sqp INNER JOIN "
                                       +"dbo.ExamType AS et ON sqp.ExId = et.ExId INNER JOIN "
                                       +"dbo.BatchInfo AS bi ON sqp.BatchId = bi.BatchId INNER JOIN "
                                       +"dbo.NewSubject AS ns ON sqp.SubId = ns.SubId INNER JOIN "
                                       +" dbo.QuestionPattern AS qp ON sqp.QPId = qp.QPId LEFT OUTER JOIN "
                                       +"dbo.Tbl_Class_Group INNER JOIN "
                                       +"dbo.Tbl_Group AS grp ON dbo.Tbl_Class_Group.GroupID = grp.GroupID "
                                       +"ON sqp.ClsGrpID = dbo.Tbl_Class_Group.ClsGrpID ON acws.CourseId = sqp.CourseId "
                                       + " where et.ExId='" + ExId + "' and bi.BatchId='" + BatchId + "' and sqp.ClsGrpID " + GroupId + ""
                    + " order by ns.SubId,acws.CourseId Asc ");                
                dt = CRUD.ReturnTableNull(sql);
                return dt;
            }
            catch 
            {
                return dt;
            }
        }
        public DataTable GetGroupWyseSubject(string ClassId,string GroupId) 
        {
            DataTable dt = new DataTable();
            try
            {

                sql = "SELECT distinct [s].[SubId] as SubjectId,[s].[SubName] as SubjectName  "
                            + " FROM [dbo].[ClassSubject] cs INNER JOIN [dbo].[Classes] c ON [cs].[ClassID] = [c].[ClassID]"
                            + " INNER JOIN [dbo].[NewSubject] s ON [cs].[SubId] = [s].[SubId] Left Outer JOIN AddCourseWithSubject acs on "
                            + " cs.CourseId=acs.CourseId"
                            + " where  cs.ClassID='" + ClassId + "' and cs.GroupId in('" + GroupId + "','0')";                            
                dt = CRUD.ReturnTableNull(sql);
                return dt;
            }
            catch { return dt; }
        }
        public List<SubQuestionPatternEntities> GetEntitiesData()
        {           
                List<SubQuestionPatternEntities> ListEntities = new List<SubQuestionPatternEntities>();
                try
                {
                    sql = string.Format("SELECT sqp.SubQPId,sqp.ClsGrpID, et.ExId, grp.GroupName, et.ExName, bi.BatchId," 
                                               +"bi.BatchName, ns.SubId, ns.SubName,acws.CourseId, acws.CourseName, qp.QPId,"
	                                           +" qp.QPName, sqp.QMarks, sqp.PassMarks,sqp.ConvertTo, sqp.IsOptional"
                                       +" FROM   dbo.AddCourseWithSubject AS acws RIGHT OUTER JOIN "
                                       +"dbo.SubjectQuestionPattern AS sqp INNER JOIN "
                                       +"dbo.ExamType AS et ON sqp.ExId = et.ExId INNER JOIN "
                                       +"dbo.BatchInfo AS bi ON sqp.BatchId = bi.BatchId INNER JOIN "
                                       +"dbo.NewSubject AS ns ON sqp.SubId = ns.SubId INNER JOIN "
                                       +" dbo.QuestionPattern AS qp ON sqp.QPId = qp.QPId LEFT OUTER JOIN "
                                       +"dbo.Tbl_Class_Group INNER JOIN "
                                       +"dbo.Tbl_Group AS grp ON dbo.Tbl_Class_Group.GroupID = grp.GroupID "
                                       +"ON sqp.ClsGrpID = dbo.Tbl_Class_Group.ClsGrpID ON acws.CourseId = sqp.CourseId "
                                       +"ORDER BY ns.SubId, acws.CourseId");                       
                    DataTable dt = new DataTable();
                    dt = CRUD.ReturnTableNull(sql);
                    if (dt != null)
                    {
                        if (dt.Rows.Count > 0)
                        {
                            ListEntities = (from DataRow row in dt.Rows
                                            select new SubQuestionPatternEntities
                                            {
                                                SubQPId = int.Parse(row["SubQPId"].ToString()), 
                                                ExamType=new ExamTypeEntities()
                                                {
                                                     ExId = int.Parse(row["ExId"].ToString()),
                                                    ExName = row["ExName"].ToString()      
                                                },
                                                Batch = new BatchEntities()
                                                {
                                                    BatchId = int.Parse(row["BatchId"].ToString()),
                                                    BatchName = row["BatchName"].ToString()                                                    
                                                }, 
                                                Group=new ClassGroupEntities()
                                                {
                                                    ClsGrpID = row["ClsGrpID"].ToString() == "" ? 0 : int.Parse(row["ClsGrpID"].ToString()),
                                                    GroupName = row["GroupName"].ToString() 
                                                },
                                                //Group=new GroupEntities()
                                                //{
                                                //    GroupID= row["GroupID"].ToString()==""?0:int.Parse(row["GroupID"].ToString()),
                                                //    GroupName = row["GroupName"].ToString() 
                                                //},
                                                Subject = new SubjectEntities()
                                                {
                                                    SubjectId = int.Parse(row["SubId"].ToString()),
                                                    SubjectName = row["SubName"].ToString()
                                                },
                                                Course = new CourseEntity()
                                                {
                                                    CourseId =row["CourseId"].ToString()==""? 0: int.Parse(row["CourseId"].ToString()),
                                                    CourseName = row["CourseName"].ToString()
                                                },
                                                QPattern=new QuestionPatternEntities()
                                                {
                                                    QPId = int.Parse(row["QPId"].ToString()),
                                                    QPName = row["QPName"].ToString()
                                                },
                                                QMarks=float.Parse(row["QMarks"].ToString()),
                                                PassMarks=float.Parse(row["PassMarks"].ToString()),
                                                ConvertTo=float.Parse(row["ConvertTo"].ToString()),
                                                IsOptional=bool.Parse(row["IsOptional"].ToString())
                                            }).ToList();
                            return ListEntities;
                        }
                    }

                }
                catch { return ListEntities = null; }

                return ListEntities = null;
        }
        public float GetConvertMarks(string batchId, string GroupID, string subId, string courseId, bool optional)
        {
            float Marks = 0;
            sql = string.Format("SELECT SUM(ConvertTo) as ConvertTo "
            + "FROM SubjectQuestionPattern WHERE BatchId='" + batchId + "' AND ClsGrpID='" + GroupID + "' "
            + "AND SubId='" + subId + "'  AND CourseId='" + courseId + "' AND IsOptional='"+optional+"'");
            DataTable dt = new DataTable();
            dt = CRUD.ReturnTableNull(sql);
            if (dt.Rows[0]["ConvertTo"].ToString() == "")
            {
                Marks = 0;
            }
            else
            {
                Marks = float.Parse(dt.Rows[0]["ConvertTo"].ToString());
            }
            return Marks;
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
