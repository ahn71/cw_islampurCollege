using DS.DAL;
using DS.PropertyEntities.Model.ManagedClass;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace DS.BLL.ManagedClass
{
    public class ClassDepedencySubPassMarksEntry:IDisposable
    {
        private ClassDependencySubPassMarksEntities _Entities;
        string sql = string.Empty;
        bool result = true;

        public ClassDependencySubPassMarksEntities AddEntities
        {
            set
            {
                _Entities = value;
            }
        }       
        public bool Insert(List<ClassDependencySubPassMarksEntities> SubPassMarksList)
        {
            foreach (var subpassMList in SubPassMarksList)
            {
                sql = string.Format("DELETE FROM [dbo].[Class_DependencyPassMarks]"
                + " WHERE [ClassID]='" + subpassMList.Class.ClassID + "'"
                + " AND [SubId]='" + subpassMList.Subject.SubjectId + "'");
                result = CRUD.ExecuteQuery(sql);
                if (result == true)
                {
                    sql = string.Format("INSERT INTO [dbo].[Class_DependencyPassMarks] " +
                    "([ClassID], [SubId],[PassMarks]) VALUES (" +
                    "'" + subpassMList.Class.ClassID + "'," +
                     "'" + subpassMList.Subject.SubjectId + "'," +
                    "'" + subpassMList.PassMarks + "')");
                    result = CRUD.ExecuteQuery(sql);
                }
            }
            return result;
        }
        public List<ClassDependencySubPassMarksEntities> GetDependencySubEntitiesData(string clsId)
        {
            List<ClassDependencySubPassMarksEntities> ListEntities = new List<ClassDependencySubPassMarksEntities>();
            sql = string.Format("SELECT  cs.SubId,ns.SubName,cdp.PassMarks "
            + "FROM ClassSubject cs INNER JOIN NewSubject ns ON cs.SubId="
            + "ns.SubId LEFT OUTER JOIN Class_DependencyPassMarks cdp ON "
            + "cs.ClassId=cdp.ClassId WHERE cs.ClassID='"+clsId+"' GROUP BY cs.ClassID,"
            + "cs.SubId,ns.SubName,cdp.PassMarks HAVING COUNT(cs.SubId)>1");
            DataTable dt = new DataTable();

            dt = CRUD.ReturnTableNull(sql);
            if (dt != null)
            {
                if (dt.Rows.Count > 0)
                {
                    ListEntities = (from DataRow row in dt.Rows
                                    select new ClassDependencySubPassMarksEntities
                                    {
                                        Subject = new PropertyEntities.Model.ManagedSubject.SubjectEntities()
                                        {
                                            SubjectId = int.Parse(row["SubId"].ToString()),
                                            SubjectName = row["SubName"].ToString()
                                        },
                                        PassMarks = row["PassMarks"].ToString() == "" ? 0 : int.Parse(row["PassMarks"].ToString())
                                    }).ToList();
                    return ListEntities;
                }

            }
            return ListEntities = null;
        }
        public List<ClassDependencySubPassMarksEntities> DependencySubEntitiesData(string clsId)
        {
            List<ClassDependencySubPassMarksEntities> ListEntities = new List<ClassDependencySubPassMarksEntities>();
            sql = string.Format("SELECT DISTINCT cs.SubId,ns.SubName,cdp.PassMarks FROM "
            +"Class_DependencyPassMarks cdp INNER JOIN ClassSubject cs ON cdp.ClassID=cs."
            +"ClassID AND cdp.SubId=cs.SubId INNER JOIN NewSubject ns ON cs.SubId=ns.SubId "
            +"WHERE cs.ClassID='"+clsId+"'");
            DataTable dt = new DataTable();

            dt = CRUD.ReturnTableNull(sql);
            if (dt != null)
            {
                if (dt.Rows.Count > 0)
                {
                    ListEntities = (from DataRow row in dt.Rows
                                    select new ClassDependencySubPassMarksEntities
                                    {
                                        Subject = new PropertyEntities.Model.ManagedSubject.SubjectEntities()
                                        {
                                            SubjectId = int.Parse(row["SubId"].ToString()),
                                            SubjectName = row["SubName"].ToString()
                                        },
                                        PassMarks = row["PassMarks"].ToString() == "" ? 0 : int.Parse(row["PassMarks"].ToString())
                                    }).ToList();
                    return ListEntities;
                }

            }
            return ListEntities = null;
        }  
        //public bool Update()
        //{
        //    sql = string.Format("UPDATE [dbo].[Class_DependencyPassMarks] SET " +
        //        ""+
        //        "'" + _Entities.Class.ClassID + "'," +
        //         "'" + _Entities.Subject.SubjectId + "'," +
        //        "'" + _Entities.PassMarks + "')");
        //    return result = CRUD.ExecuteQuery(sql);
        //}
        //public bool Delete()
        //{
        //    sql = string.Format("Delete From [dbo].[SubjectQuestionPattern]" +
        //        "WHERE [ExId] =' " + _Entities.ExamType.ExId + "'"
        //    + " and [BatchId] =' " + _Entities.Batch.BatchId + "'"
        //    + " and [GroupID] =' " + _Entities.Group.GroupID + "'");
        //    return result = CRUD.ExecuteQuery(sql);
        //}

        //public List<ClassDependencySubPassMarksEntities> GetEntitiesData()
        //{
        //    List<ClassDependencySubPassMarksEntities> ListEntities = new List<ClassDependencySubPassMarksEntities>();
        //    try
        //    {
        //        sql = string.Format("SELECT sqp.SubQPId,et.ExId,"
        //            + "grp.GroupID,grp.GroupName,"
        //            + "et.ExName,bi.BatchId,bi.BatchName,"
        //            + "ns.SubId,ns.SubName,acws.CourseId,"
        //            + "acws.CourseName,qp.QPId,qp.QPName,sqp.QMarks,"
        //            + "sqp.PassMarks,sqp.ConvertTo,sqp.IsOptional FROM "
        //            + "SubjectQuestionPattern sqp INNER"
        //            + " JOIN ExamType et ON sqp.ExId=et.ExId INNER JOIN"
        //            + " BatchInfo bi ON sqp.BatchId=bi.BatchId"
        //            + " INNER JOIN NewSubject ns ON sqp.SubId=ns.SubId"
        //            + " LEFT OUTER JOIN Tbl_Group grp ON  sqp.GroupID="
        //            + "grp.GroupID LEFT OUTER JOIN AddCourseWithSubject acws"
        //            + " ON sqp.CourseId=acws.CourseId INNER JOIN "
        //            + "QuestionPattern qp ON sqp.QPId=qp.QPId");
        //        DataTable dt = new DataTable();
        //        dt = CRUD.ReturnTableNull(sql);
        //        if (dt != null)
        //        {
        //            if (dt.Rows.Count > 0)
        //            {
        //                ListEntities = (from DataRow row in dt.Rows
        //                                select new ClassDependencySubPassMarksEntities
        //                                {
        //                                    SubQPId = int.Parse(row["SubQPId"].ToString()),
        //                                    ExamType = new ExamTypeEntities()
        //                                    {
        //                                        ExId = int.Parse(row["ExId"].ToString()),
        //                                        ExName = row["ExName"].ToString()
        //                                    },
        //                                    Batch = new BatchEntities()
        //                                    {
        //                                        BatchId = int.Parse(row["BatchId"].ToString()),
        //                                        BatchName = row["BatchName"].ToString()
        //                                    },
        //                                    Group = new GroupEntities()
        //                                    {
        //                                        GroupID = row["GroupID"].ToString() == "" ? 0 : int.Parse(row["GroupID"].ToString()),
        //                                        GroupName = row["GroupName"].ToString()
        //                                    },
        //                                    Subject = new SubjectEntities()
        //                                    {
        //                                        SubjectId = int.Parse(row["SubId"].ToString()),
        //                                        SubjectName = row["SubName"].ToString()
        //                                    },
        //                                    Course = new CourseEntity()
        //                                    {
        //                                        CourseId = row["CourseId"].ToString() == "" ? 0 : int.Parse(row["CourseId"].ToString()),
        //                                        CourseName = row["CourseName"].ToString()
        //                                    },
        //                                    QPattern = new QuestionPatternEntities()
        //                                    {
        //                                        QPId = int.Parse(row["QPId"].ToString()),
        //                                        QPName = row["QPName"].ToString()
        //                                    },
        //                                    QMarks = float.Parse(row["QMarks"].ToString()),
        //                                    PassMarks = float.Parse(row["PassMarks"].ToString()),
        //                                    ConvertTo = float.Parse(row["ConvertTo"].ToString()),
        //                                    IsOptional = bool.Parse(row["IsOptional"].ToString())
        //                                }).ToList();
        //                return ListEntities;
        //            }
        //        }

        //    }
        //    catch { return ListEntities = null; }

        //    return ListEntities = null;
        //}
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
