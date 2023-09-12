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
        private ExamTypeEntry et;
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
                "[PassMarks],[ConvertTo],[IsOptional],[SubQPMarks]) VALUES (" +
                "'" + _Entities.ExamType.ExId + "'," +
                "'" + _Entities.Batch.BatchId + "'," +
                "'" + _Entities.Group.ClsGrpID + "'," +
                "'" + _Entities.Subject.SubjectId + "'," +
                "'" + _Entities.Course.CourseId + "'," +
                "'" + _Entities.QPattern.QPId + "'," +
                "'" + _Entities.QMarks + "'," +
                "'" + _Entities.PassMarks + "'," +
                "'" + _Entities.ConvertTo + "'," +
                "'" + _Entities.IsOptional + "',"+
                "'" + _Entities.SubQPMarks + "')");
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
                "[IsOptional] = '" + _Entities.IsOptional + "', " +
                "[IsOptional] = '" + _Entities.SubQPMarks + "' " +
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
                sql = string.Format("SELECT  sqp.SubQPId, et.ExId, grp.GroupName,sqp.ClsGrpID, et.ExName, bi.BatchId,"
                    + "bi.BatchName, ns.SubId, ns.SubName, acws.CourseId, acws.CourseName, qp.QPId, qp.QPName, "
                    + "sqp.QMarks, sqp.PassMarks,sqp.ConvertTo, sqp.IsOptional, dbo.ClassSubject.Ordering FROM "
                    + "dbo.ClassSubject INNER JOIN dbo.SubjectQuestionPattern AS sqp INNER JOIN  dbo.ExamType "
                    + "AS et ON sqp.ExId = et.ExId INNER JOIN dbo.BatchInfo AS bi ON sqp.BatchId = bi.BatchId "
                    + "INNER JOIN dbo.NewSubject AS ns ON sqp.SubId = ns.SubId INNER JOIN dbo.QuestionPattern "
                    + "AS qp ON sqp.QPId = qp.QPId ON dbo.ClassSubject.ClassID = bi.ClassID AND dbo.ClassSubject.SubId "
                    + "= sqp.SubId AND dbo.ClassSubject.CourseId = sqp.CourseId LEFT OUTER JOIN dbo.Tbl_Class_Group INNER "
                    + "JOIN dbo.Tbl_Group AS grp ON dbo.Tbl_Class_Group.GroupID = grp.GroupID ON sqp.ClsGrpID = "
                    + "dbo.Tbl_Class_Group.ClsGrpID LEFT OUTER JOIN dbo.AddCourseWithSubject AS acws ON "
                    + "sqp.CourseId = acws.CourseId "
                    + " where et.ExId='" + ExId + "' and bi.BatchId='" + BatchId + "' and sqp.ClsGrpID=" + GroupId + ""
                    + " ORDER BY ClassSubject.Ordering ");                
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
        public List<SubQuestionPatternEntities> GetEntitiesData(string BatchId, string ExId, string ClsGrpID)
        {
            List<SubQuestionPatternEntities> ListEntities = new List<SubQuestionPatternEntities>();
            try
            {
                if (ClsGrpID == "0")
                    ClsGrpID = "";
                else
                    ClsGrpID = " and sqp.ClsGrpID =" + ClsGrpID;
                sql = string.Format("select  SubQPId,et.SemesterExam,sqp.ExId,g.GroupName, sqp.ClsGrpID,et.ExName,sqp.BatchId,b.BatchName,sqp.SubId,s.SubName,sqp.CourseId,acs.CourseName,sqp.QPId,qp.QPName,sqp.QMarks,sqp.SubQPMarks,sqp.PassMarks,sqp.ConvertTo,sqp.IsOptional,cs.Ordering from SubjectQuestionPattern sqp inner join ExamType et on sqp.ExId=et.ExId  inner  join BatchInfo b on sqp.BatchId=b.BatchId inner join NewSubject s on sqp.SubId = s.SubId inner join Tbl_Class_Group cg on sqp.ClsGrpID = cg.ClsGrpID inner join Tbl_Group g on cg.GroupID = g.GroupID left join AddCourseWithSubject acs on sqp.SubId = acs.SubId and sqp.CourseId = acs.CourseId inner join QuestionPattern qp on sqp.QPId = qp.QPId inner join(select ClassID, SubId, min(Ordering) Ordering from ClassSubject   group by SubId, ClassID) as cs on sqp.SubId = cs.SubId and b.ClassID = cs.ClassID where sqp.BatchId = " + BatchId + " and et.ExId = " + ExId + " " + ClsGrpID + " order by cs.Ordering");
                DataTable dt = new DataTable();
                dt = CRUD.ReturnTableNull(sql);
                if (dt != null)
                {


                    if (dt.Rows.Count > 0)
                    {
                        if (et == null)
                            et = new ExamTypeEntry();
                        ListEntities = (from DataRow row in dt.Rows
                                        select new SubQuestionPatternEntities
                                        {

                                            SubQPId = int.Parse(row["SubQPId"].ToString()),
                                            ExamType = new ExamTypeEntities()
                                            {
                                                ExId = int.Parse(row["ExId"].ToString()),
                                                ExName = row["ExName"].ToString(),
                                                semesterexam = et.ParseTryReturnNull(row["SemesterExam"].ToString())
                                            },
                                            Batch = new BatchEntities()
                                            {
                                                BatchId = int.Parse(row["BatchId"].ToString()),
                                                BatchName = row["BatchName"].ToString()
                                            },
                                            Group = new ClassGroupEntities()
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
                                                CourseId = row["CourseId"].ToString() == "" ? 0 : int.Parse(row["CourseId"].ToString()),
                                                CourseName = row["CourseName"].ToString()
                                            },
                                            QPattern = new QuestionPatternEntities()
                                            {
                                                QPId = int.Parse(row["QPId"].ToString()),
                                                QPName = row["QPName"].ToString()
                                            },
                                            QMarks = float.Parse(row["QMarks"].ToString()),
                                            PassMarks = float.Parse(row["PassMarks"].ToString()),
                                            ConvertTo = float.Parse(row["ConvertTo"].ToString()),
                                            IsOptional = bool.Parse(row["IsOptional"].ToString()),
                                            SubQPMarks = float.Parse(row["SubQPMarks"].ToString())
                                        }).ToList();
                        return ListEntities;
                    }
                }

            }
            catch { return ListEntities = null; }

            return ListEntities = null;
        }
        public List<SubQuestionPatternEntities> GetEntitiesData()
        {           
                List<SubQuestionPatternEntities> ListEntities = new List<SubQuestionPatternEntities>();
                try
                {
                    sql = string.Format("SELECT  sqp.SubQPId,et.SemesterExam, et.ExId, grp.GroupName,sqp.ClsGrpID, et.ExName, bi.BatchId,"
                    +"bi.BatchName, ns.SubId, ns.SubName, acws.CourseId, acws.CourseName, qp.QPId, qp.QPName, "
                    + "sqp.QMarks,sqp.SubQPMarks, sqp.PassMarks,sqp.ConvertTo, sqp.IsOptional, dbo.ClassSubject.Ordering FROM "
                    +"dbo.ClassSubject INNER JOIN dbo.SubjectQuestionPattern AS sqp INNER JOIN  dbo.ExamType "
                    +"AS et ON sqp.ExId = et.ExId INNER JOIN dbo.BatchInfo AS bi ON sqp.BatchId = bi.BatchId "
                    +"INNER JOIN dbo.NewSubject AS ns ON sqp.SubId = ns.SubId INNER JOIN dbo.QuestionPattern "
                    +"AS qp ON sqp.QPId = qp.QPId ON dbo.ClassSubject.ClassID = bi.ClassID AND dbo.ClassSubject.SubId "
                    +"= sqp.SubId AND dbo.ClassSubject.CourseId = sqp.CourseId LEFT OUTER JOIN dbo.Tbl_Class_Group INNER "
                    +"JOIN dbo.Tbl_Group AS grp ON dbo.Tbl_Class_Group.GroupID = grp.GroupID ON sqp.ClsGrpID = "
                    +"dbo.Tbl_Class_Group.ClsGrpID LEFT OUTER JOIN dbo.AddCourseWithSubject AS acws ON "
                    +"sqp.CourseId = acws.CourseId ORDER BY ClassSubject.Ordering");                       
                    DataTable dt = new DataTable();
                    dt = CRUD.ReturnTableNull(sql);
                    if (dt != null)
                    {
                        
                           
                        if (dt.Rows.Count > 0)
                        {
                            if (et == null)
                                et = new ExamTypeEntry();
                            ListEntities = (from DataRow row in dt.Rows
                                            select new SubQuestionPatternEntities
                                            {
                                                
                                                SubQPId = int.Parse(row["SubQPId"].ToString()), 
                                                ExamType=new ExamTypeEntities()
                                                {
                                                     ExId = int.Parse(row["ExId"].ToString()),
                                                    ExName = row["ExName"].ToString() ,
                                                     semesterexam = et.ParseTryReturnNull(row["SemesterExam"].ToString())
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
                                                IsOptional=bool.Parse(row["IsOptional"].ToString()),
                                                SubQPMarks = float.Parse(row["SubQPMarks"].ToString())
                                            }).ToList();
                            return ListEntities;
                        }
                    }

                }
                catch { return ListEntities = null; }

                return ListEntities = null;
        }
        public float GetConvertMarks(string batchId, string GroupID)
        {
            float Marks = 0;
            sql = string.Format("SELECT Distinct ExId,ConvertTo FROM SubjectQuestionPattern"
            +" WHERE BatchId='" + batchId + "' AND ClsGrpID='" + GroupID + "'");
            DataTable dt = new DataTable();
            dt = CRUD.ReturnTableNull(sql);
            if (dt.Rows.Count > 0)
            {
                object m = dt.Compute("SUM(ConvertTo)", "");
                Marks = float.Parse(m.ToString());                
            }
            return Marks;
        }
        public DataTable GetConvertTotalMarks(string batchId, string GroupID)
        {
            sql = string.Format("SELECT Distinct ExName,ConvertTo FROM v_SubjectQuestionPattern"
            + " WHERE BatchId='" + batchId + "' AND ClsGrpID='" + GroupID + "'");
            DataTable dt = new DataTable();
            dt = CRUD.ReturnTableNull(sql);
            return dt;
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
