using DS.DAL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace DS.BLL.Examinition
{
    public static  class ExamCommon
    {
        private static string sqlCmd="";
        
        public static DataTable getAttendanceSheetInExam(string BatchId,string ClassID,string ClsGrpID, string ClsSecID, string RollNo)
        {
            if (ClsSecID != "0")
                ClsSecID = " and si.ClsSecID=" + ClsSecID;
            else
                ClsSecID = "";
            if (RollNo != "")
                RollNo = "  and si.RollNo=" + RollNo;

            sqlCmd = "with cs as (" +
                "select SubName, CourseName, SubCode from  v_ClassSubjectList where  IsCommon = 1 and ClassID = "+ ClassID + " and IsOptional=0 )," +
                "scs as (select StudentId, SubName, CourseName, SubCode from CurrentStudentInfo si cross join cs  where  si.BatchID='" + BatchId + "' and si.ClsGrpID='" + ClsGrpID + "' " + ClsSecID + RollNo+
                " union all select StudentId, SubName, CourseName, SubCode  from v_StudentGroupSubSetupDetails) " +
                "select si.StudentId,RollNo,FullName,FathersName,MothersName,SubName as BusName,SubCode as BusID,SectionName,GroupName,BatchName from v_CurrentStudentInfo si inner join scs on si.StudentId = scs.StudentId  where  si.BatchID='" + BatchId + "' and si.ClsGrpID='" + ClsGrpID + "' "+ ClsSecID+RollNo;
          return  CRUD.ReturnTableNull(sqlCmd); 
        }

        public static DataTable getNumberSheetInExam(string BatchId, string ClassID,string ExamID, string ClsGrpID, string SubID, string ClsSecID)
        {
            if (ClsSecID != "0")
                ClsSecID = " and csi.ClsSecID=" + ClsSecID + " ";
            else
                ClsSecID = "";
            if (SubID != "0")
            {
                string[] s = SubID.Split('_');
                SubID = " and  er.subid=" + s[0]+ " and er.CourseID=" + s[1]+" ";
            }
                
            else
                SubID = "";

            //sqlCmd = "with cs as (" +
            //    "select SubName, CourseName, SubCode,SubID from  v_ClassSubjectList where  IsCommon = 1 and ClassID = " + ClassID + " and IsOptional=0 )," +
            //    "scs as (select StudentId, SubName, CourseName, SubCode,SubID from CurrentStudentInfo si cross join cs  where  si.BatchID='" + BatchId + "' and si.ClsGrpID='" + ClsGrpID + "' " + ClsSecID +
            //    " union all select StudentId, SubName, CourseName, SubCode,SubID  from v_StudentGroupSubSetupDetails) " +
            //    "select si.StudentId,RollNo,FullName,FathersName,MothersName,SubName as BusName,SubID as BusID,SectionName,GroupName,BatchName, isnull(ClsGrpID,0) as ClsGrpID,isnull(ClsSecID,0) as ClsSecID from v_CurrentStudentInfo si inner join scs on si.StudentId = scs.StudentId  where  si.BatchID='" + BatchId + "' and si.ClsGrpID='" + ClsGrpID + "' " + ClsSecID+SubID + " order by RollNo";

            sqlCmd = @"with gs as
(
select 0 as StudentID, SubId,CourseId from ClassSubject where ClassID=" + ClassID + @" and IsCommon=1 and IsOptional=0 union all 
select StudentID, SubId,CourseId from v_StudentGroupSubSetupDetails Where BatchId="+ BatchId + @"
)
, 
st as (
select ee.ExamID,ee.BatchID,ee.ClsGrpID,ee.ClsSecID,er.SubID,er.CourseID,convert(varchar(10), er.ExamDate,105) as ExamDate,er.ExamDate as ExamDate1,er.ExamDay,er.StartTime,er.EndTime,csi.StudentId,csi.FullName,csi.RollNo,csi.GroupName,csi.SectionName,csi.GuardianMobileNo,CONVERT(varchar(15),er.StartTime,100) as ExamStartTime, CONVERT(varchar(15),er.EndTime ,100) as ExamEndTime,csi.ClassName, case when  er.CourseID=0 then cs.SubName else cs.SubName +' '+ cs.CourseName end as Subject,ei.ExName+'-'+convert(varchar(4),Year(er.ExamDate)) as ExamName,FathersName,MothersName,cs.SubCode,BatchName  from ExamExaminee ee inner join  Exam_ExamRoutine er on ee.ExamID=er.ExamID inner join v_CurrentStudentInfo csi on csi.StudentId=ee.StudentID and csi.BatchID=ee.BatchID  inner join ExamInfo ei on ee.ExamID=ei.ExInSl  inner join v_ClassSubjectList cs  on er.SubID=cs.SubId and er.CourseID=cs.CourseId and cs.ClassID=" + ClassID + @"  where  ee.ExamID = " + ExamID + ClsSecID +SubID+ @")

select convert(varchar, st.SubID)+'_'+convert(varchar,st.CourseID) as SubCourseID,st.* from st inner join  gs on st.SubID=gs.SubId and( st.studentid=gs.studentid or gs.studentid=0 ) order by RollNo, year(ExamDate1),month(ExamDate1),ExamDate1";
            return CRUD.ReturnTableNull(sqlCmd);
        }
        public static DataTable getExamineeNumber(string BatchId, string ClassID, string ExamID, string ClsGrpID, string SubID)
        {
         
            if (SubID != "0")
            {
                string[] s = SubID.Split('_');
                SubID = " and  er.subid=" + s[0] + " and er.CourseID=" + s[1] + " ";
            }

            else
                SubID = "";

            //sqlCmd = "with cs as (" +
            //    "select SubName, CourseName, SubCode,SubID from  v_ClassSubjectList where  IsCommon = 1 and ClassID = " + ClassID + " and IsOptional=0 )," +
            //    "scs as (select StudentId, SubName, CourseName, SubCode,SubID from CurrentStudentInfo si cross join cs  where  si.BatchID='" + BatchId + "' and si.ClsGrpID='" + ClsGrpID + "' " + ClsSecID +
            //    " union all select StudentId, SubName, CourseName, SubCode,SubID  from v_StudentGroupSubSetupDetails) " +
            //    "select si.StudentId,RollNo,FullName,FathersName,MothersName,SubName as BusName,SubID as BusID,SectionName,GroupName,BatchName, isnull(ClsGrpID,0) as ClsGrpID,isnull(ClsSecID,0) as ClsSecID from v_CurrentStudentInfo si inner join scs on si.StudentId = scs.StudentId  where  si.BatchID='" + BatchId + "' and si.ClsGrpID='" + ClsGrpID + "' " + ClsSecID+SubID + " order by RollNo";

            sqlCmd = @"with gs as
(
select 0 as StudentID, SubId,CourseId from ClassSubject where ClassID=" + ClassID + @" and IsCommon=1 and IsOptional=0 union all 
select StudentID, SubId,CourseId from v_StudentGroupSubSetupDetails 
)
, 
st as (
select ee.ExamID,ee.BatchID,ee.ClsGrpID,ee.ClsSecID,er.SubID,er.CourseID,convert(varchar(10), er.ExamDate,105) as ExamDate,er.ExamDate as ExamDate1,er.ExamDay,er.StartTime,er.EndTime,csi.StudentId,csi.FullName,csi.RollNo,csi.GroupName,csi.SectionName,csi.GuardianMobileNo,CONVERT(varchar(15),er.StartTime,100) as ExamStartTime, CONVERT(varchar(15),er.EndTime ,100) as ExamEndTime,csi.ClassName, case when  er.CourseID=0 then cs.SubName else cs.SubName +' '+ cs.CourseName end as Subject,ei.ExName+'-'+convert(varchar(4),Year(er.ExamDate)) as ExamName,FathersName,MothersName,cs.SubCode,BatchName  from ExamExaminee ee inner join  Exam_ExamRoutine er on ee.ExamID=er.ExamID inner join v_CurrentStudentInfo csi on csi.StudentId=ee.StudentID and csi.BatchID=ee.BatchID  inner join ExamInfo ei on ee.ExamID=ei.ExInSl  inner join v_ClassSubjectList cs  on er.SubID=cs.SubId and er.CourseID=cs.CourseId and cs.ClassID=" + ClassID + @"  where  ee.ExamID = " + ExamID + SubID + @")

select st.ExamName,st.GroupName,st.ClsGrpID,  convert(varchar, st.SubID)+'_'+convert(varchar,st.CourseID) as SubCourseID,st.Subject,st.ExamDate,st.ExamDate1,st.ExamDay,count(st.RollNo) as RollNo from st inner join  gs on st.SubID=gs.SubId and( st.studentid=gs.studentid or gs.studentid=0 ) group by st.ExamName,st.GroupName,st.ClsGrpID,   convert(varchar, st.SubID)+'_'+convert(varchar,st.CourseID) ,st.Subject,st.ExamDate,st.ExamDate1,st.ExamDay order by year(ExamDate1),month(ExamDate1),ExamDate1";
            return CRUD.ReturnTableNull(sqlCmd);
        }
        public static DataTable getAdmitCard(string BatchId, string ClassID, string ExamID,string ClsSecID, string RollNo)
        {
            if (ClsSecID != "0")
                ClsSecID = " and ee.ClsSecID=" + ClsSecID;
            else
                ClsSecID = "";
            if (RollNo != "")                
            sqlCmd = @"with gs as
(
select 0 as StudentID, SubId,CourseId from ClassSubject where ClassID="+ ClassID + @"  and IsCommon=1 and IsOptional=0 union all 
select StudentID, SubId,CourseId from v_StudentGroupSubSetupDetails where BatchId=" + BatchId + " and RollNo=" + RollNo + @"
)
, 
st as (
select ee.ExamID,ee.BatchID,ee.ClsGrpID,ee.ClsSecID,er.SubID,er.CourseID,convert(varchar(10), er.ExamDate,105) as ExamDate,er.ExamDate as ExamDate1,er.ExamDay,er.StartTime,er.EndTime,csi.StudentId,csi.FullName,csi.RollNo,csi.GroupName,csi.SectionName,csi.GuardianMobileNo,CONVERT(varchar(15),er.StartTime,100) as ExamStartTime, CONVERT(varchar(15),er.EndTime ,100) as ExamEndTime,csi.ClassName, case when  er.CourseID=0 then cs.SubName else cs.SubName +' '+ cs.CourseName end as Subject,ei.ExName+'-'+convert(varchar(4),Year(er.ExamDate)) as ExamName,FathersName,MothersName,cs.SubCode,BatchName  from ExamExaminee ee inner join  Exam_ExamRoutine er on ee.ExamID=er.ExamID inner join v_CurrentStudentInfo csi on csi.StudentId=ee.StudentID and csi.BatchID=ee.BatchID  inner join ExamInfo ei on ee.ExamID=ei.ExInSl  inner join v_ClassSubjectList cs  on er.SubID=cs.SubId and er.CourseID=cs.CourseId and cs.ClassID=" + ClassID + @"  where  ee.ExamID = " + ExamID + ClsSecID+ @" and RollNo="+ RollNo + @")

select st.* from st inner join  gs on st.SubID=gs.SubId and st.CourseID=gs.CourseID and( st.studentid=gs.studentid or gs.studentid=0 ) order by RollNo, year(ExamDate1),month(ExamDate1),ExamDate1";
            else
            sqlCmd = @"with gs as
(
select 0 as StudentID, SubId,CourseId from ClassSubject where ClassID=" + ClassID + @" and IsCommon=1 and IsOptional=0 union all 
select StudentID, SubId,CourseId from v_StudentGroupSubSetupDetails 
)
, 
st as (
select ee.ExamID,ee.BatchID,ee.ClsGrpID,ee.ClsSecID,er.SubID,er.CourseID,convert(varchar(10), er.ExamDate,105) as ExamDate,er.ExamDate as ExamDate1,er.ExamDay,er.StartTime,er.EndTime,csi.StudentId,csi.FullName,csi.RollNo,csi.GroupName,csi.SectionName,csi.GuardianMobileNo,CONVERT(varchar(15),er.StartTime,100) as ExamStartTime, CONVERT(varchar(15),er.EndTime ,100) as ExamEndTime,csi.ClassName, case when  er.CourseID=0 then cs.SubName else cs.SubName +' '+ cs.CourseName end as Subject,ei.ExName+'-'+convert(varchar(4),Year(er.ExamDate)) as ExamName,FathersName,MothersName,cs.SubCode,BatchName  from ExamExaminee ee inner join  Exam_ExamRoutine er on ee.ExamID=er.ExamID inner join v_CurrentStudentInfo csi on csi.StudentId=ee.StudentID and csi.BatchID=ee.BatchID  inner join ExamInfo ei on ee.ExamID=ei.ExInSl  inner join v_ClassSubjectList cs  on er.SubID=cs.SubId and er.CourseID=cs.CourseId and cs.ClassID=" + ClassID + @"  where  ee.ExamID = " + ExamID + ClsSecID+ @")

select st.* from st inner join  gs on st.SubID=gs.SubId and st.CourseID=gs.CourseID and( st.studentid=gs.studentid or gs.studentid=0 ) order by RollNo, year(ExamDate1),month(ExamDate1),ExamDate1";
            return CRUD.ReturnTableNull(sqlCmd);
        }
        public static DataTable getSeatPlanStricker(string BatchId, string ClassID, string ExamID,string ClsSecID, string RollNo)
        {
            if (ClsSecID != "0")
                ClsSecID = " and ee.ClsSecID=" + ClsSecID;
            else
                ClsSecID = "";
            if (RollNo != "")                
            sqlCmd = @"select ee.ExamID,ee.BatchID,ee.ClsGrpID,csi.StudentId,csi.FullName,csi.RollNo,csi.GroupName,ei.ExName ,BatchName  from ExamExaminee ee  inner join v_CurrentStudentInfo csi on csi.StudentId=ee.StudentID and csi.BatchID=ee.BatchID  inner join ExamInfo ei on ee.ExamID=ei.ExInSl   and csi.ClassID="+ ClassID + "  where  ee.ExamID ="+ ExamID + " "+ ClsSecID + " and RollNo=" + RollNo;
            else
                sqlCmd = @"select ee.ExamID,ee.BatchID,ee.ClsGrpID,csi.StudentId,csi.FullName,csi.RollNo,csi.GroupName,ei.ExName ,BatchName  from ExamExaminee ee  inner join v_CurrentStudentInfo csi on csi.StudentId=ee.StudentID and csi.BatchID=ee.BatchID  inner join ExamInfo ei on ee.ExamID=ei.ExInSl   and csi.ClassID=" + ClassID + "  where  ee.ExamID =" + ExamID + " " + ClsSecID + " order by csi.RollNo";
            return CRUD.ReturnTableNull(sqlCmd);
        }

        public static DataTable getExamRoutine(string ExamID)
        {
             sqlCmd = @"select er.ExamID,er.BatchID,er.ClsGrpID,er.SubID,er.CourseID,convert(varchar(10), er.ExamDate,105) as ExamDate,er.ExamDate as ExamDate1,er.ExamDay,er.StartTime,er.EndTime,GroupName,CONVERT(varchar(15),er.StartTime,100) as ExamStartTime, CONVERT(varchar(15),er.EndTime ,100) as ExamEndTime,b.ClassName, case when  er.CourseID=0 then s.SubName else s.SubName +' '+ cs.CourseName end as Subject,ei.ExName+'-'+convert(varchar(4),Year(er.ExamDate)) as ExamName,cg.GroupName,ShiftID,ExamRoutineID  from    Exam_ExamRoutine er inner join ExamInfo ei on er.ExamID=ei.ExInSl inner join v_BatchInfo b on er.BatchID=b.BatchId inner join NewSubject s on er.SubID=s.SubId  inner join AddCourseWithSubject cs on er.subid=cs.subid and(  er.CourseID= cs.CourseId or er.CourseID=0) inner join v_Tbl_Class_Group cg on er.ClsGrpID=cg.ClsGrpID and er.BatchID= cg.BatchId 
                  where er.BatchID in (select BatchId from ExamInfo where ExInSl=" + ExamID + ") and er.ExamID=" + ExamID + " order by year(ExamDate),MONTH(ExamDate), ExamDate";
            return CRUD.ReturnTableNull(sqlCmd);
        }
    }
}
