using DS.DAL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Web.UI.WebControls;

namespace DS.BLL.Timetable
{
  
    public class Tbl_Exam_Time_SetNameEntry
    {
        public bool result = false;
        DataTable dt = new DataTable();
        Tbl_Exam_Time_SetName _Entities;
        string sql;
        public Tbl_Exam_Time_SetName AddEntities
        {
            set {
                _Entities = value;
            }
        }

        public bool Insert()
        {
            sql = string.Format("INSERT INTO [dbo].[Tbl_Exam_Time_SetName] " +
                "([Name]) VALUES ( '" + _Entities.Name + "')");
            return result = CRUD.ExecuteQuery(sql);
        }

        public bool Update()
        {
            sql = string.Format("Update [dbo].[Tbl_Exam_Time_SetName] Set " +
                        " [Name] = '" + _Entities.Name + "' " +
                        " WHERE [ExamTimeSetNameId] = '" + _Entities.ExamTimeSetNameId + "'");
            return result = CRUD.ExecuteQuery(sql);
        }

        public List<Tbl_Exam_Time_SetName> GetEntitiesData()
        {
            List<Tbl_Exam_Time_SetName> ListEntities = new List<Tbl_Exam_Time_SetName>();
            sql = string.Format("SELECT [ExamTimeSetNameId],[Name] FROM [dbo].[Tbl_Exam_Time_SetName]");
            DataTable dt = new DataTable();

            dt = CRUD.ReturnTableNull(sql);
            if (dt != null)
            {
                if (dt.Rows.Count > 0)
                {
                    ListEntities = (from DataRow row in dt.Rows

                                    select new Tbl_Exam_Time_SetName
                                    {
                                        ExamTimeSetNameId = int.Parse(row["ExamTimeSetNameId"].ToString()),
                                        Name = row["Name"].ToString()
                                    }).ToList();
                    return ListEntities;

                }

            }
            return ListEntities = null;
        }

        public static void getSubject_CourseList(string BatchId,DropDownList ddl,string GetGrpId)
        {
            try
            {
                ddl.Items.Clear();
                // DataTable dt = CRUD.ReturnTableNull("select distinct convert(varchar,SubId)+'_'+CONVERT(varchar,CourseId)  as SubCourseId,SubName,CourseName  as SubCourseName ,SubId,CourseId from  v_SubjectQuestionPattern where BatchId=" + BatchId + " order by SubId,CourseId ");
                DataTable dt = CRUD.ReturnTableNull("select distinct SubName,CourseName,SubId,CourseId from v_ClassSubjectList Where ClassId=(select ClassId from BatchInfo where BatchId=" + BatchId + ") AND GroupId in(0,"+GetGrpId+") order by SubId,CourseId ");
                if (dt.Rows.Count > 0)
                {
                    byte b=0;
                    foreach (DataRow dr in dt.Rows)
                    {
                        string courseName = (dr["CourseName"].ToString() != "") ? "_" + dr["CourseName"].ToString() : " ";
                        string SubId_CourId = (dr["CourseId"].ToString().Trim().Length == 0) ? "0" : dr["CourseId"].ToString().Trim();

                        ddl.Items.Insert(b, new ListItem(dr["SubName"].ToString() + courseName,dr["SubId"]+"_"+SubId_CourId));
                        b++;
                    }
                    ddl.Items.Insert(b, new ListItem("Select Subject","0"));
                    ddl.SelectedIndex = ddl.Items.Count-1;
                }
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
