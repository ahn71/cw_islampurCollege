using DS.DAL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace DS.BLL.Examinition
{
    public class ExamRoutineEntry
    {
        private bool result=false;
        private string sql = string.Empty;
      

        public bool insert(string ExamDate,string ExamDay,string StartTime,string EndTime,string ExamID,string BatchID,string ClsGrpID,string SubID,string CourseID,string ShiftID)
        {
            try
            {
                sql = @"INSERT INTO [dbo].[Exam_ExamRoutine]
           ([ExamDate]
           ,[ExamDay]
           ,[StartTime]
           ,[EndTime]         
           ,[ExamID]
           ,[BatchID]
           ,[ClsGrpID]
           ,[SubID]
           ,[CourseID]
           ,[ShiftID])
     VALUES
           ('"+ ExamDate + "','"+ ExamDay + "','"+ StartTime + "','"+ EndTime + "',"+ ExamID + ","+ BatchID + ","+ ClsGrpID + ","+ SubID + ","+ CourseID + ","+ ShiftID + ")";
                return result = CRUD.ExecuteQuery(sql);
            }
            catch { return false; }
        }
        public bool update(string ExamRoutineID, string ExamDate, string ExamDay, string StartTime, string EndTime, string SubID, string CourseID)
        {
            try
            {
                sql = @"Update [dbo].[Exam_ExamRoutine] Set [ExamDate]='"+ ExamDate + "',[ExamDay]='"+ ExamDay + "',[StartTime]='"+ StartTime + "',[EndTime]='"+EndTime+"',[SubID]="+SubID+",[CourseID]="+ CourseID + " Where ExamRoutineID="+ ExamRoutineID;
                return result = CRUD.ExecuteQuery(sql);
            }
            catch { return false; }
        }
        public bool Delete(string ExamRoutineID)
        {
            try
            {
                sql = @"Delete [dbo].[Exam_ExamRoutine] Where ExamRoutineID="+ ExamRoutineID;
                return result = CRUD.ExecuteQuery(sql);
            }
            catch { return false; }
        }
        public DataTable getExamRoutine(string ExamID)
        {
            try {
                sql = @"select ExamRoutineID,ExamID, convert(varchar(10),ExamDate,105) as ExamDate,ExamDay, convert(varchar,StartTime,100) as StartTime, convert(varchar,EndTime,100) as EndTime,BatchID,er.SubID,er.CourseID,ShiftID,case when er.CourseID =0 then s.SubName else s.SubName +' '+cs.CourseName end as SubName,ClsGrpID  from Exam_ExamRoutine er inner join NewSubject s on er.SubID=s.SubId left join AddCourseWithSubject cs on er.SubID=cs.SubId and er.CourseID=er.CourseID
                 where ExamID=" + ExamID+ " order by  year(ExamDate),month(ExamDate),ExamDate";
                return CRUD.ReturnTableNull(sql);
                
            } catch(Exception ex) { return null; }
        }
    }
}
