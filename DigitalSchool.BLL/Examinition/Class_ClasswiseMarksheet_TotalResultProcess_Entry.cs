using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DS.PropertyEntities.Model.Examinition;
using System.Data;
using DS.DAL;
using DS.PropertyEntities.Model.ManagedSubject;
using System.Web.UI.WebControls;

namespace DS.BLL.Examinition
{
    
    public class Class_ClasswiseMarksheet_TotalResultProcess_Entry
    {
        private Class_ClasswiseMarksheet_TotalResultProcess Entities;       
        bool result;
        DataTable dt;
        string sql;
        public Class_ClasswiseMarksheet_TotalResultProcess SetValues
        {
            set
            {
                Entities = value;
            }           
        }

        public bool Insert(string getTableName)
        {
            try
            {
                sql = "insert into " + getTableName + " (ExInId,StudentId,RollNo,BatchId,ClsSecId,ShiftId,SubId,IsOptional,CourseId,QPId,Marks,ClsGrpId,"
                + "MarksOfAllPatternBySCId,GradeOfAllPatternBySCId,PointOfAllPatternBySCId,MarksOfSubject_WithAllDependencySub,GradeOfSubject_WithAllDependencySub,PointOfSubject_WithAllDependencySub)"
                +"values('"+Entities.ExInId+"',"+Entities.StudentId+","+Entities.RollNo+","+Entities.BatchId+","+Entities.ClsSecId+","
                +""+Entities.ShiftId+","+Entities.SubId+",'"+Entities.IsOptional+"',"+Entities.CourseId+","+Entities.QPId+","+Entities.Marks+", "+Entities.ClsGrpID+","
                +""+Entities.MarksOfAllPatternBySCId+",'"+Entities.GradeOfAllPatternBySCId+"',"+Entities.PointOfAllPatternBySCId+","
                +""+Entities.MarksOfSubject_WithAllDependencySub+",'"+Entities.GradeOfSubject_WithAllDependencySub+"',"+Entities.PointOfSubject_WithAllDependencySub+")";

                result = CRUD.ExecuteQuery(sql);
                return result;


            }
            catch { return false; }
        }
        public  List<Class_ClasswiseMarksheet_TotalResultProcess> GetEntitiesData(string tableName)    //Get Subjec Name
        {
             List<Class_ClasswiseMarksheet_TotalResultProcess> ListEntities = new List<Class_ClasswiseMarksheet_TotalResultProcess>();

                DataTable dt = new DataTable();

                dt = CRUD.ReturnTableNull("SELECT DISTINCT dbo.NewSubject.SubId, dbo.NewSubject.SubName "
                     + ",ctrp.ExInId,ctrp.BatchId,ctrp.ClsSecID,ctrp.ShiftId,ac.CourseId,ac.CourseName"
                     +" FROM  dbo.Class_"+tableName+"MarksSheet_TotalResultProcess ctrp INNER JOIN dbo.NewSubject ON "
                     +"ctrp.SubId = dbo.NewSubject.SubId LEFT OUTER JOIN "
                     + "dbo.AddCourseWithSubject ac ON ctrp.CourseId =ac.CourseId ");
                if (dt != null)
                {
                    if (dt.Rows.Count > 0)
                    {
                        ListEntities = (from DataRow row in dt.Rows
                                        select new Class_ClasswiseMarksheet_TotalResultProcess
                                        { 
                                            SubId = int.Parse(row["SubId"].ToString()),
                                            SubName = row["SubName"].ToString(),                                           
                                            ExInId = row["ExInId"].ToString(),
                                            BatchId = int.Parse(row["BatchId"].ToString()),
                                            ShiftId = int.Parse(row["ShiftId"].ToString()),
                                            ClsSecId = int.Parse(row["ClsSecID"].ToString()),
                                            CourseId =row["CourseId"].ToString()==""?0:int.Parse(row["CourseId"].ToString()),
                                            CourseName = row["CourseName"].ToString()
                                        }).ToList();
                        return ListEntities;
                    }

                }
                return ListEntities = null;            
        }
        public DataTable GETSUBJECTWISEMARKSLIST(string tableName, string shiftId,
          string batchId, string groupId, string sectionId, string examinId, string subId, string courseId)  //Subject Wise Mark Sheet
        {
            DataTable dt = new DataTable();
            try
            {
                sql = string.Format("SELECT ctrp.RollNo, dbo.BatchInfo.BatchName, FORMAT(dbo.ExamInfo.ExStartDate,"
                +"'yyyy') AS ExInDate, dbo.ExamType.ExName, dbo.Sections.SectionName, dbo.ShiftConfiguration.ShiftName,"
                +"dbo.CurrentStudentInfo.FullName, CASE WHEN ctrp.CourseID = 0 THEN NewSubject.SubName ELSE CASE WHEN "
                +"dbo.NewSubject.SubName LIKE  dbo.AddCourseWithSubject.CourseName THEN dbo.AddCourseWithSubject.CourseName "
                +"ELSE dbo.NewSubject.SubName+dbo.AddCourseWithSubject.CourseName END END AS SubName, ctrp.MarksOfAllPatternBySCId,"
                +"ctrp.GradeOfAllPatternBySCId, ctrp.PointOfAllPatternBySCId, dbo.Tbl_Group.GroupName FROM dbo.Tbl_Group RIGHT OUTER "
                +"JOIN dbo.Tbl_Class_Group ON dbo.Tbl_Group.GroupID = dbo.Tbl_Class_Group.GroupID RIGHT OUTER JOIN dbo."
                +"CurrentStudentInfo INNER JOIN dbo.Class_"+tableName+"MarksSheet_TotalResultProcess AS ctrp INNER JOIN dbo.NewSubject "
                +"ON ctrp.SubId = dbo.NewSubject.SubId INNER JOIN dbo.BatchInfo ON ctrp.BatchId = dbo.BatchInfo.BatchId INNER "
                +"JOIN dbo.ShiftConfiguration ON ctrp.ShiftId = dbo.ShiftConfiguration.ConfigId ON dbo.CurrentStudentInfo.StudentId "
                +"= ctrp.StudentId INNER JOIN dbo.Tbl_Class_Section ON ctrp.ClsSecId = dbo.Tbl_Class_Section.ClsSecID INNER JOIN "
                +"dbo.Sections ON dbo.Tbl_Class_Section.SectionID = dbo.Sections.SectionID ON dbo.Tbl_Class_Group.ClsGrpID = "
                +"ctrp.ClsGrpID LEFT OUTER JOIN dbo.AddCourseWithSubject ON ctrp.CourseId = dbo.AddCourseWithSubject.CourseId "
                +"LEFT OUTER JOIN dbo.ExamType INNER JOIN dbo.ExamInfo ON dbo.ExamType.ExId = dbo.ExamInfo.ExId ON ctrp.ExInId "
                +"= dbo.ExamInfo.ExInId WHERE (ctrp.BatchId = '"+batchId+"') AND (ctrp.ShiftId = '"+shiftId+"') AND "
                +"(ctrp.SubId = '"+subId+"') AND (ctrp.ExInId = '"+examinId+"') AND (ctrp.CourseId = '"+courseId+"')"
                + " AND (ctrp.ClsGrpID = '" + groupId + "') AND (ctrp.ClsSecId = '" + sectionId + "')");
                dt = CRUD.ReturnTableNull(sql);
                return dt;
            }
            catch (Exception ex)
            {
                ex.Message.ToString();
                return dt = null;
            }
        }
        public DataTable GetExamResultDetails(string tableName, string shiftId,
          string batchId, string groupId, string sectionId, string examinId)    //Exam Result Detais(Batch wise Student Result)
        {
            DataTable dt = new DataTable();
            try
            {
                sql = string.Format("SELECT dbo.CurrentStudentInfo.FullName, msheet.RollNo, dbo.NewSubject.SubName "
                +"AS MainSubName, CASE WHEN dbo.AddCourseWithSubject.CourseName IS NULL THEN dbo.NewSubject.SubName "
                +"ELSE CASE WHEN dbo.NewSubject.SubName LIKE dbo.AddCourseWithSubject.CourseName THEN dbo.AddCourseWithSubject"
                +".CourseName ELSE dbo.NewSubject.SubName + dbo.AddCourseWithSubject.CourseName END END AS SubName, "
                +"dbo.QuestionPattern.QPName, msheet.Marks, msheet.MarksOfAllPatternBySCId, msheet.GradeOfAllPatternBySCId, "
                +"msheet.PointOfAllPatternBySCId, msheet.MarksOfSubject_WithAllDependencySub, msheet.GradeOfSubject_"
                +"WithAllDependencySub, msheet.PointOfSubject_WithAllDependencySub, dbo.BatchInfo.BatchName, "
                + "dbo.ShiftConfiguration.ShiftName, dbo.Sections.SectionName, msheet.ExInId, Format(dbo.ExamInfo.ExStartDate, "
                +"'yyyy') AS ExInDate, dbo.ExamType.ExName, dbo.Tbl_Group.GroupName, msheet.ClsGrpID FROM dbo.Tbl_Group "
                +"RIGHT OUTER JOIN dbo.Tbl_Class_Group ON dbo.Tbl_Group.GroupID = dbo.Tbl_Class_Group.GroupID RIGHT OUTER "
                +"JOIN dbo.Class_"+tableName+"MarksSheet_TotalResultProcess AS msheet INNER JOIN dbo.NewSubject ON msheet.SubId = "
                +"dbo.NewSubject.SubId INNER JOIN dbo.QuestionPattern ON msheet.QPId = dbo.QuestionPattern.QPId INNER JOIN dbo."
                +"CurrentStudentInfo ON msheet.StudentId = dbo.CurrentStudentInfo.StudentId INNER JOIN dbo.BatchInfo ON msheet."
                +"BatchId = dbo.BatchInfo.BatchId INNER JOIN dbo.ShiftConfiguration ON msheet.ShiftId = dbo.ShiftConfiguration."
                +"ConfigId INNER JOIN dbo.ExamInfo ON msheet.ExInId = dbo.ExamInfo.ExInId INNER JOIN dbo.ExamType ON dbo.ExamInfo"
                +".ExId = dbo.ExamType.ExId INNER JOIN dbo.Tbl_Class_Section ON msheet.ClsSecId = dbo.Tbl_Class_Section.ClsSecID "
                +"INNER JOIN dbo.Sections ON dbo.Tbl_Class_Section.SectionID = dbo.Sections.SectionID ON dbo.Tbl_Class_Group.ClsGrpID "
                +"= msheet.ClsGrpID LEFT OUTER JOIN dbo.AddCourseWithSubject ON msheet.CourseId = dbo.AddCourseWithSubject.CourseId "
                +"WHERE (msheet.BatchId = '"+batchId+"') AND (msheet.ShiftId = '"+shiftId+"') AND(msheet.ClsGrpID='"+groupId+"') "
                +"AND (msheet.ClsSecId='"+sectionId+"') AND (msheet.ExInId = '"+examinId+"')");
                dt = CRUD.ReturnTableNull(sql);
                return dt;
            }
            catch (Exception ex)
            {
                ex.Message.ToString();
                return dt = null;
            }
        }
        public DataTable GetExamResultDetails(string tableName, string stdID, string examinId)    //Exam Result Detais(Batch wise Student Result)
        {
            DataTable dt = new DataTable();
            try
            {
                sql = string.Format("SELECT dbo.CurrentStudentInfo.FullName, msheet.RollNo, dbo.NewSubject.SubName "
                + "AS MainSubName, CASE WHEN dbo.AddCourseWithSubject.CourseName IS NULL THEN dbo.NewSubject.SubName "
                + "ELSE CASE WHEN dbo.NewSubject.SubName LIKE dbo.AddCourseWithSubject.CourseName THEN dbo.AddCourseWithSubject"
                + ".CourseName ELSE dbo.NewSubject.SubName + dbo.AddCourseWithSubject.CourseName END END AS SubName, "
                + "dbo.QuestionPattern.QPName, msheet.Marks, msheet.MarksOfAllPatternBySCId, msheet.GradeOfAllPatternBySCId, "
                + "msheet.PointOfAllPatternBySCId, msheet.MarksOfSubject_WithAllDependencySub, msheet.GradeOfSubject_"
                + "WithAllDependencySub, msheet.PointOfSubject_WithAllDependencySub, dbo.BatchInfo.BatchName, "
                + "dbo.ShiftConfiguration.ShiftName, dbo.Sections.SectionName, msheet.ExInId, Format(dbo.ExamInfo.ExStartDate, "
                + "'yyyy') AS ExInDate, dbo.ExamType.ExName, dbo.Tbl_Group.GroupName, msheet.ClsGrpID FROM dbo.Tbl_Group "
                + "RIGHT OUTER JOIN dbo.Tbl_Class_Group ON dbo.Tbl_Group.GroupID = dbo.Tbl_Class_Group.GroupID RIGHT OUTER "
                + "JOIN dbo.Class_" + tableName + "MarksSheet_TotalResultProcess AS msheet INNER JOIN dbo.NewSubject ON msheet.SubId = "
                + "dbo.NewSubject.SubId INNER JOIN dbo.QuestionPattern ON msheet.QPId = dbo.QuestionPattern.QPId INNER JOIN dbo."
                + "CurrentStudentInfo ON msheet.StudentId = dbo.CurrentStudentInfo.StudentId INNER JOIN dbo.BatchInfo ON msheet."
                + "BatchId = dbo.BatchInfo.BatchId INNER JOIN dbo.ShiftConfiguration ON msheet.ShiftId = dbo.ShiftConfiguration."
                + "ConfigId INNER JOIN dbo.ExamInfo ON msheet.ExInId = dbo.ExamInfo.ExInId INNER JOIN dbo.ExamType ON dbo.ExamInfo"
                + ".ExId = dbo.ExamType.ExId INNER JOIN dbo.Tbl_Class_Section ON msheet.ClsSecId = dbo.Tbl_Class_Section.ClsSecID "
                + "INNER JOIN dbo.Sections ON dbo.Tbl_Class_Section.SectionID = dbo.Sections.SectionID ON dbo.Tbl_Class_Group.ClsGrpID "
                + "= msheet.ClsGrpID LEFT OUTER JOIN dbo.AddCourseWithSubject ON msheet.CourseId = dbo.AddCourseWithSubject.CourseId "
                + "WHERE msheet.StudentId='"+stdID+"' AND (msheet.ExInId = '" + examinId + "')");
                dt = CRUD.ReturnTableNull(sql);
                return dt;
            }
            catch (Exception ex)
            {
                ex.Message.ToString();
                return dt = null;
            }
        }
        public DataTable GetGuideStudentResultDetails(string tableName,string EID, string examinId)    //Exam Result Detais(Batch wise Student Result)
        {
            DataTable dt = new DataTable();
            try
            {
                sql = string.Format("SELECT dbo.CurrentStudentInfo.FullName, msheet.RollNo, dbo.NewSubject.SubName "
                + "AS MainSubName, CASE WHEN dbo.AddCourseWithSubject.CourseName IS NULL THEN dbo.NewSubject.SubName "
                + "ELSE CASE WHEN dbo.NewSubject.SubName LIKE dbo.AddCourseWithSubject.CourseName THEN dbo.AddCourseWithSubject"
                + ".CourseName ELSE dbo.NewSubject.SubName + dbo.AddCourseWithSubject.CourseName END END AS SubName, "
                + "dbo.QuestionPattern.QPName, msheet.Marks, msheet.MarksOfAllPatternBySCId, msheet.GradeOfAllPatternBySCId, "
                + "msheet.PointOfAllPatternBySCId, msheet.MarksOfSubject_WithAllDependencySub, msheet.GradeOfSubject_"
                + "WithAllDependencySub, msheet.PointOfSubject_WithAllDependencySub, dbo.BatchInfo.BatchName, "
                + "dbo.ShiftConfiguration.ShiftName, dbo.Sections.SectionName, msheet.ExInId, Format(dbo.ExamInfo.ExStartDate, "
                + "'yyyy') AS ExInDate, dbo.ExamType.ExName, dbo.Tbl_Group.GroupName, msheet.ClsGrpID FROM dbo.Tbl_Group "
                + "RIGHT OUTER JOIN dbo.Tbl_Class_Group ON dbo.Tbl_Group.GroupID = dbo.Tbl_Class_Group.GroupID RIGHT OUTER "
                + "JOIN dbo.Class_" + tableName + "MarksSheet_TotalResultProcess AS msheet INNER JOIN dbo.NewSubject ON msheet.SubId = "
                + "dbo.NewSubject.SubId INNER JOIN dbo.QuestionPattern ON msheet.QPId = dbo.QuestionPattern.QPId INNER JOIN dbo."
                + "CurrentStudentInfo ON msheet.StudentId = dbo.CurrentStudentInfo.StudentId INNER JOIN dbo.BatchInfo ON msheet."
                + "BatchId = dbo.BatchInfo.BatchId INNER JOIN dbo.ShiftConfiguration ON msheet.ShiftId = dbo.ShiftConfiguration."
                + "ConfigId INNER JOIN dbo.ExamInfo ON msheet.ExInId = dbo.ExamInfo.ExInId INNER JOIN dbo.ExamType ON dbo.ExamInfo"
                + ".ExId = dbo.ExamType.ExId INNER JOIN dbo.Tbl_Class_Section ON msheet.ClsSecId = dbo.Tbl_Class_Section.ClsSecID "
                + "INNER JOIN dbo.Sections ON dbo.Tbl_Class_Section.SectionID = dbo.Sections.SectionID ON dbo.Tbl_Class_Group.ClsGrpID "
                + "= msheet.ClsGrpID LEFT OUTER JOIN dbo.AddCourseWithSubject ON msheet.CourseId = dbo.AddCourseWithSubject.CourseId "
                + "WHERE msheet.StudentId in (SELECT StudentId FROM tbl_Guide_Teacher WHERE EID='"+EID+"') AND (msheet.ExInId = '" + examinId + "')");
                dt = CRUD.ReturnTableNull(sql);
                return dt;
            }
            catch (Exception ex)
            {
                ex.Message.ToString();
                return dt = null;
            }
        }
        public DataTable LoadRoll(string tableName, string shiftId,
          string batchId, string groupId, string sectionId, string examinId)  //Pass Student Roll Number
        {
            try
            {
                DataTable dt = new DataTable();
                sql = string.Format("SELECT Distinct  msheet.RollNo,msheet.StudentId,"
                +"Tbl_Class_Group.GroupID FROM dbo.Class_"+tableName+"MarksSheet msheet Left "
                +"Outer Join Tbl_Class_Group ON msheet.ClsGrpID=Tbl_Class_Group.ClsGrpID  "
                +"WHERE msheet.batchId='"+batchId+"' AND msheet.shiftId='"+shiftId+"' AND msheet.ExInId"
                + "='" + examinId + "' AND msheet.ClsSecId='" + sectionId + "' AND  ISNULL(msheet.ClsGrpID,'')='" + groupId + "'");
                dt = CRUD.ReturnTableNull(sql);
                return dt;
            }
            catch
            {
                return dt = null;
            }
        }
        public DataTable LoadResultSummary(string shiftId,  //Result Summary(Total Student ,Pass Studnet Fail Student)
          string batchId, string groupId, string sectionId, string examinId)       
        {
            try
            {
                dt = new DataTable();
                sql = string.Format("SELECT  SUM(CASE WHEN fr.FinalGrad_OfExam = 'F' THEN 1 ELSE 0 END) AS "
                +"FailStd, SUM(CASE WHEN fr.FinalGrad_OfExam != 'F' THEN 1 ELSE 0 END) AS PassStd,SUM(CASE "
                +"WHEN fr.FinalGrad_OfExam = 'F' THEN 1 ELSE 0 END) + SUM(CASE WHEN fr.FinalGrad_OfExam != 'F' "
                +"THEN 1 ELSE 0 END) AS Total, dbo.Tbl_Class_Section.SectionID, dbo.BatchInfo.BatchName, "
                + "dbo.Tbl_Group.GroupName, dbo.Sections.SectionName, FORMAT(dbo.ExamInfo.ExStartDate, 'yyyy') "
                +"AS ExInDate, dbo.ExamType.ExName FROM dbo.Tbl_Group RIGHT OUTER JOIN dbo.Tbl_Class_Group "
                +"ON dbo.Tbl_Group.GroupID = dbo.Tbl_Class_Group.GroupID RIGHT OUTER JOIN dbo.ExamInfo "
                +"INNER JOIN dbo.Exam_Final_Result_Stock_Of_All_Batch AS fr INNER JOIN dbo.BatchInfo ON "
                +"fr.BatchId = dbo.BatchInfo.BatchId ON dbo.ExamInfo.ExInId = fr.ExInId INNER JOIN dbo."
                +"ExamType ON dbo.ExamInfo.ExId = dbo.ExamType.ExId INNER JOIN dbo.Sections INNER JOIN "
                +"dbo.Tbl_Class_Section ON dbo.Sections.SectionID = dbo.Tbl_Class_Section.SectionID ON "
                +"fr.ClsSecId = dbo.Tbl_Class_Section.ClsSecID ON dbo.Tbl_Class_Group.ClsGrpID = "
                +"fr.ClsGrpID WHERE (fr.BatchId = '"+batchId+"') AND (fr.ExInId = '"+examinId+"') AND "
                +"(fr.ShiftId = '"+shiftId+"') AND (fr.ClsSecId = '"+sectionId+"') AND (ISNULL(fr.ClsGrpID,"
                +"'') = '"+groupId+"') GROUP BY dbo.Tbl_Class_Section.SectionID, dbo.BatchInfo.BatchName, "
                + "dbo.Tbl_Group.GroupName, dbo.Sections.SectionName, FORMAT(dbo.ExamInfo.ExStartDate, 'yyyy'), dbo.ExamType.ExName");
                dt = CRUD.ReturnTableNull(sql);
                return dt;
            }
            catch
            { return dt = null; }
        }
        public DataTable LoadFailSubject(string tableName, string shiftId,
         string batchId, string groupId, string sectionId, string examinId)             //BatchWise Fail Subject List
        {
            try
            {
                dt = new DataTable();
                sql = string.Format("SELECT DISTINCT dbo.CurrentStudentInfo.FullName, "
                + "rp.RollNo, rp.ExInId,dbo.NewSubject.SubName,dbo.BatchInfo.BatchName, "
                + "dbo.Tbl_Group.GroupName, dbo.ExamType.ExName, FORMAT(dbo.ExamInfo.ExStartDate, 'yyyy') "
                +"AS ExInDate, dbo.Sections.SectionName FROM dbo.Sections INNER JOIN dbo.Tbl_Class_Section "
                +"ON dbo.Sections.SectionID = dbo.Tbl_Class_Section.SectionID INNER JOIN dbo.ExamInfo INNER "
                +"JOIN dbo.Class_"+tableName+"MarksSheet_TotalResultProcess AS rp INNER JOIN dbo.CurrentStudentInfo "
                +"ON rp.StudentId = dbo.CurrentStudentInfo.StudentId INNER JOIN dbo.NewSubject ON rp.SubId = "
                +"dbo.NewSubject.SubId INNER JOIN dbo.BatchInfo ON rp.BatchId = dbo.BatchInfo.BatchId INNER "
                +"JOIN dbo.Class_"+tableName+"MarksSheet AS ms ON rp.ExInId = ms.ExInId AND rp.StudentId = "
                +"ms.StudentId AND rp.BatchId = ms.BatchId AND rp.ShiftId = ms.ShiftId ON dbo.ExamInfo.ExInId "
                +"= rp.ExInId INNER JOIN dbo.ExamType ON dbo.ExamInfo.ExId = dbo.ExamType.ExId ON dbo.Tbl_"
                +"Class_Section.ClsSecID = rp.ClsSecId LEFT OUTER JOIN dbo.AddCourseWithSubject ON rp.CourseId "
                +"= dbo.AddCourseWithSubject.CourseId LEFT OUTER JOIN dbo.Tbl_Group RIGHT OUTER JOIN dbo.Tbl_"
                +"Class_Group ON dbo.Tbl_Group.GroupID = dbo.Tbl_Class_Group.GroupID ON rp.ClsGrpID = dbo.Tbl"
                +"_Class_Group.ClsGrpID WHERE (rp.BatchId = '"+batchId+"') AND (rp.ExInId = '"+examinId+"') "
                +"AND (rp.ShiftId = '"+shiftId+"') AND (rp.GradeOfSubject_WithAllDependencySub = 'F') AND "
                +"(ISNULL(rp.ClsGrpID, '')='"+groupId+"') AND (rp.ClsSecId = '"+sectionId+"') ORDER BY rp.RollNo");
                dt = CRUD.ReturnTableNull(sql);
                return dt;
            }
            catch
            { return dt = null; }
        }
        public DataTable LoadFailSubject(string tableName, string examinId,string stdID)             //BatchWise Fail Subject List
        {
            try
            {
                dt = new DataTable();
                sql = string.Format("SELECT DISTINCT dbo.CurrentStudentInfo.FullName, "
                + "rp.RollNo, rp.ExInId,dbo.NewSubject.SubName,dbo.BatchInfo.BatchName, "
                + "dbo.Tbl_Group.GroupName, dbo.ExamType.ExName, FORMAT(dbo.ExamInfo.ExStartDate, 'yyyy') "
                + "AS ExInDate, dbo.Sections.SectionName FROM dbo.Sections INNER JOIN dbo.Tbl_Class_Section "
                + "ON dbo.Sections.SectionID = dbo.Tbl_Class_Section.SectionID INNER JOIN dbo.ExamInfo INNER "
                + "JOIN dbo.Class_" + tableName + "MarksSheet_TotalResultProcess AS rp INNER JOIN dbo.CurrentStudentInfo "
                + "ON rp.StudentId = dbo.CurrentStudentInfo.StudentId INNER JOIN dbo.NewSubject ON rp.SubId = "
                + "dbo.NewSubject.SubId INNER JOIN dbo.BatchInfo ON rp.BatchId = dbo.BatchInfo.BatchId INNER "
                + "JOIN dbo.Class_" + tableName + "MarksSheet AS ms ON rp.ExInId = ms.ExInId AND rp.StudentId = "
                + "ms.StudentId AND rp.BatchId = ms.BatchId AND rp.ShiftId = ms.ShiftId ON dbo.ExamInfo.ExInId "
                + "= rp.ExInId INNER JOIN dbo.ExamType ON dbo.ExamInfo.ExId = dbo.ExamType.ExId ON dbo.Tbl_"
                + "Class_Section.ClsSecID = rp.ClsSecId LEFT OUTER JOIN dbo.AddCourseWithSubject ON rp.CourseId "
                + "= dbo.AddCourseWithSubject.CourseId LEFT OUTER JOIN dbo.Tbl_Group RIGHT OUTER JOIN dbo.Tbl_"
                + "Class_Group ON dbo.Tbl_Group.GroupID = dbo.Tbl_Class_Group.GroupID ON rp.ClsGrpID = dbo.Tbl"
                + "_Class_Group.ClsGrpID WHERE  (rp.ExInId = '" + examinId + "')  AND (rp.GradeOfSubject_"
                +"WithAllDependencySub = 'F') AND rp.StudentId='"+stdID+"'  ORDER BY rp.RollNo");
                dt = CRUD.ReturnTableNull(sql);
                return dt;
            }
            catch
            { return dt = null; }
        }
        public DataTable LoadGuideStudentFailSubject(string tableName, string examinId, string EID)             //BatchWise Fail Subject List
        {
            try
            {
                dt = new DataTable();
                sql = string.Format("SELECT DISTINCT dbo.CurrentStudentInfo.FullName, "
                + "rp.RollNo, rp.ExInId,dbo.NewSubject.SubName,dbo.BatchInfo.BatchName, "
                + "dbo.Tbl_Group.GroupName, dbo.ExamType.ExName, FORMAT(dbo.ExamInfo.ExStartDate, 'yyyy') "
                + "AS ExInDate, dbo.Sections.SectionName FROM dbo.Sections INNER JOIN dbo.Tbl_Class_Section "
                + "ON dbo.Sections.SectionID = dbo.Tbl_Class_Section.SectionID INNER JOIN dbo.ExamInfo INNER "
                + "JOIN dbo.Class_" + tableName + "MarksSheet_TotalResultProcess AS rp INNER JOIN dbo.CurrentStudentInfo "
                + "ON rp.StudentId = dbo.CurrentStudentInfo.StudentId INNER JOIN dbo.NewSubject ON rp.SubId = "
                + "dbo.NewSubject.SubId INNER JOIN dbo.BatchInfo ON rp.BatchId = dbo.BatchInfo.BatchId INNER "
                + "JOIN dbo.Class_" + tableName + "MarksSheet AS ms ON rp.ExInId = ms.ExInId AND rp.StudentId = "
                + "ms.StudentId AND rp.BatchId = ms.BatchId AND rp.ShiftId = ms.ShiftId ON dbo.ExamInfo.ExInId "
                + "= rp.ExInId INNER JOIN dbo.ExamType ON dbo.ExamInfo.ExId = dbo.ExamType.ExId ON dbo.Tbl_"
                + "Class_Section.ClsSecID = rp.ClsSecId LEFT OUTER JOIN dbo.AddCourseWithSubject ON rp.CourseId "
                + "= dbo.AddCourseWithSubject.CourseId LEFT OUTER JOIN dbo.Tbl_Group RIGHT OUTER JOIN dbo.Tbl_"
                + "Class_Group ON dbo.Tbl_Group.GroupID = dbo.Tbl_Class_Group.GroupID ON rp.ClsGrpID = dbo.Tbl"
                + "_Class_Group.ClsGrpID WHERE  (rp.ExInId = '" + examinId + "')  AND (rp.GradeOfSubject_"
                + "WithAllDependencySub = 'F') AND rp.StudentId in (SELECT StudentId FROM tbl_Guide_Teacher WHERE EID='"+EID+"')  ORDER BY rp.RollNo");
                dt = CRUD.ReturnTableNull(sql);
                return dt;
            }
            catch
            { return dt = null; }
        }
        public DataTable LoadDependencyCnvtMarksList(string tableName,string condition )
        {
            try
            {
                sql = string.Format("SELECT Distinct clsMS.ExId,clsMS.CourseId, dbo.ExamType.ExName, clsMS.ExInId, clsMS.StudentId, "
                +"dbo.CurrentStudentInfo.FullName, clsMS.RollNo, clsMS.BatchId, dbo.BatchInfo.BatchName, "
                +"clsMS.ClsSecId, dbo.Sections.SectionName, clsMS.ShiftId, dbo.ShiftConfiguration.ShiftName, "
                +"clsMS.SubId, dbo.QuestionPattern.QPName, dbo.SubjectQuestionPattern.QMarks, clsMS.Marks, "
                + "clsMS.ConvertToPercentage, clsMS.ConvertMarks,clsMS.TotalConvertMarksOfSub, clsMS.ClsGrpID, dbo.Tbl_Group.GroupName, "
                +"CASE WHEN clsMS.CourseID = 0 THEN NewSubject.SubName ELSE CASE WHEN dbo.NewSubject.SubName "
                +"LIKE dbo.AddCourseWithSubject.CourseName THEN dbo.AddCourseWithSubject.CourseName ELSE "
                +"dbo.NewSubject.SubName + dbo.AddCourseWithSubject.CourseName END END AS SubName FROM "
                +"dbo.QuestionPattern INNER JOIN dbo.Class_"+tableName+"MarksSheet clsMS INNER JOIN dbo.ExamType "
                +"ON clsMS.ExId = dbo.ExamType.ExId INNER JOIN dbo.CurrentStudentInfo ON clsMS.StudentId "
                +"= dbo.CurrentStudentInfo.StudentId INNER JOIN dbo.BatchInfo ON clsMS.BatchId = dbo."
                +"BatchInfo.BatchId INNER JOIN dbo.Tbl_Class_Section ON clsMS.ClsSecId = dbo.Tbl_Class"
                +"_Section.ClsSecID INNER JOIN dbo.Sections ON dbo.Tbl_Class_Section.SectionID = "
                +"dbo.Sections.SectionID INNER JOIN dbo.ShiftConfiguration ON clsMS.ShiftId = "
                +"dbo.ShiftConfiguration.ConfigId INNER JOIN dbo.NewSubject ON clsMS.SubId = "
                +"dbo.NewSubject.SubId INNER JOIN dbo.SubjectQuestionPattern ON clsMS.SubQPId = "
                +"dbo.SubjectQuestionPattern.SubQPId ON dbo.QuestionPattern.QPId = dbo.SubjectQuestionPattern."
                +"QPId LEFT OUTER JOIN dbo.AddCourseWithSubject ON clsMS.CourseId = dbo.AddCourseWithSubject."
                +"CourseId LEFT OUTER JOIN dbo.Tbl_Group INNER JOIN dbo.Tbl_Class_Group ON dbo.Tbl_Group."
                + "GroupID = dbo.Tbl_Class_Group.GroupID ON clsMS.ClsGrpID = dbo.Tbl_Class_Group.ClsGrpID " + condition + " ORDER BY SubId,clsMS.CourseId");
                dt = CRUD.ReturnTableNull(sql);
                return dt;
            }
            catch { return dt; }
        }
        public DataTable LoadDependencyExamResult(string tableName, string condition)
        {
            try
            {
                sql = string.Format("SELECT DISTINCT  clsMS.ExId, clsMS.CourseId, dbo.ExamType.ExName, clsMS.ExInId, "
                +"clsMS.StudentId, dbo.CurrentStudentInfo.FullName, clsMS.RollNo, clsMS.BatchId, dbo.BatchInfo.BatchName, "
                +"clsMS.ClsSecId, dbo.Sections.SectionName, clsMS.ShiftId, dbo.ShiftConfiguration.ShiftName, clsMS.SubId, "
                +"dbo.QuestionPattern.QPName, dbo.SubjectQuestionPattern.QMarks, clsMS.Marks, clsMS.ConvertToPercentage, "
                +"clsMS.ConvertMarks, clsMS.TotalConvertMarksOfSub, clsMS.ClsGrpID, dbo.Tbl_Group.GroupName, CASE WHEN "
                +"clsMS.CourseID = 0 THEN NewSubject.SubName ELSE CASE WHEN dbo.NewSubject.SubName LIKE dbo.AddCourseWithSubject."
                +"CourseName THEN dbo.AddCourseWithSubject.CourseName ELSE dbo.NewSubject.SubName + dbo.AddCourseWithSubject."
                +"CourseName END END AS SubName, dbo.Class_NineMarksSheet_TotalResultProcess.MarksOfSubject_WithAllDependencySub,"
                +"dbo.Class_"+tableName+"MarksSheet_TotalResultProcess.GradeOfSubject_WithAllDependencySub, dbo."
                +"Class_"+tableName+"MarksSheet_TotalResultProcess.PointOfSubject_WithAllDependencySub, "
                +"dbo.Exam_Final_Result_Stock_Of_All_Batch.FinalGPA_OfExam_WithOptionalSub, dbo.Exam_Final_Result_Stock_"
                +"Of_All_Batch.FinalGrade_OfExam_WithOptionalSub, dbo.Exam_Final_Result_Stock_Of_All_Batch.FinalTotalMarks_"
                +"OfExam_WithOptionalSub, dbo.Exam_Final_Result_Stock_Of_All_Batch.FinalGPA_OfExam, dbo.Exam_Final_Result_"
                +"Stock_Of_All_Batch.FinalGrad_OfExam, dbo.Exam_Final_Result_Stock_Of_All_Batch.FinalTotalMarks_OfExam, "
                +"dbo.Exam_Final_Result_Stock_Of_All_Batch.PublishDate, dbo.Exam_Final_Result_Stock_Of_All_Batch.IsFinalExam "
                +"FROM dbo.QuestionPattern INNER JOIN dbo.Class_NineMarksSheet AS clsMS INNER JOIN dbo.ExamType ON clsMS.ExId "
                +"= dbo.ExamType.ExId INNER JOIN dbo.CurrentStudentInfo ON clsMS.StudentId = dbo.CurrentStudentInfo.StudentId "
                +"INNER JOIN dbo.BatchInfo ON clsMS.BatchId = dbo.BatchInfo.BatchId INNER JOIN dbo.Tbl_Class_Section ON clsMS.ClsSecId "
                +"= dbo.Tbl_Class_Section.ClsSecID INNER JOIN dbo.Sections ON dbo.Tbl_Class_Section.SectionID = dbo.Sections.SectionID "
                +"INNER JOIN dbo.ShiftConfiguration ON clsMS.ShiftId = dbo.ShiftConfiguration.ConfigId INNER JOIN dbo.NewSubject "
                +"ON clsMS.SubId = dbo.NewSubject.SubId INNER JOIN dbo.SubjectQuestionPattern ON clsMS.SubQPId = "
                +"dbo.SubjectQuestionPattern.SubQPId ON dbo.QuestionPattern.QPId = dbo.SubjectQuestionPattern.QPId INNER "
                +"JOIN dbo.Class_"+tableName+"MarksSheet_TotalResultProcess ON clsMS.ExInId = dbo.Class_"+tableName+"MarksSheet_"
                +"TotalResultProcess.ExInId AND clsMS.StudentId = dbo.Class_NineMarksSheet_TotalResultProcess.StudentId "
                +"AND clsMS.BatchId = dbo.Class_"+tableName+"MarksSheet_TotalResultProcess.BatchId AND clsMS.SubId = "
                +"dbo.Class_"+tableName+"MarksSheet_TotalResultProcess.SubId AND clsMS.CourseId = dbo.Class_"+tableName+"MarksSheet"
                +"_TotalResultProcess.CourseId INNER JOIN dbo.Exam_Final_Result_Stock_Of_All_Batch ON clsMS.StudentId = "
                +"dbo.Exam_Final_Result_Stock_Of_All_Batch.StudentId AND clsMS.ExInId = dbo.Exam_Final_Result_Stock_Of_"
                +"All_Batch.ExInId AND clsMS.BatchId = dbo.Exam_Final_Result_Stock_Of_All_Batch.BatchId LEFT OUTER JOIN "
                +"dbo.AddCourseWithSubject ON clsMS.CourseId = dbo.AddCourseWithSubject.CourseId LEFT OUTER JOIN dbo.Tbl_Group "
                +"INNER JOIN dbo.Tbl_Class_Group ON dbo.Tbl_Group.GroupID = dbo.Tbl_Class_Group.GroupID ON clsMS.ClsGrpID = "
                +"dbo.Tbl_Class_Group.ClsGrpID  "+condition+" ORDER BY clsMS.RollNo, clsMS.SubId, clsMS.CourseId, clsMS.ExInId");
                dt = CRUD.ReturnTableNull(sql);
                return dt;
            }
            catch { return dt; }
        }
        public DataTable LoadProgressReport(string tableName, string shiftId,
         string batchId, string groupId, string sectionId, string examinId)
        {
            try
            {
                string[] examsplit = examinId.Split('_');
                string[] dateyear = examsplit[1].Split('-');
                string date = dateyear[2] + "-" + dateyear[0] + "-" + dateyear[1];
                sql = ";with attendance as (Select sum(case  when AttStatus='p' or AttStatus= 'l'   then 1 else 0 end) as Present,sum(case  when AttStatus='a'  then 1 else 0 end) as Absent,sum(case  when AttStatus='p' or AttStatus= 'l'   then 1 else 0 end)+sum(case  when AttStatus='a'  then 1 else 0 end) as Total,StudentId  from DailyAttendanceRecord where ShiftId='" + shiftId + "' and BatchId='" + batchId + "' and ClsGrpId='" + groupId + "' and ClsSecId='" + sectionId + "' and AttDate Between (Select isnull(Max(ExStartDate),convert(varchar(4),Year(GetDate()))+'-01'+'-01') AttDate  From ExamInfo inner join ExamType on ExamInfo.ExId=ExamType.ExId where BatchId='" + batchId + "' and SemesterExam=1 and ExStartDate<'" + date + "') and  '" + date + "' group by StudentId),  a as (SELECT StudentId, SUM(Obtainmarks) Obtainmarks,Sum(Patternmarks) Patternmarks FROM v_Tbl_Exam_MonthlyTest where BatchId='" + batchId + "' and ShiftId='" + shiftId + "' and ClsGrpID='" + groupId + "' and ClsSecId='" + sectionId + "' and ExStartDate Between (Select isnull(Max(ExStartDate),convert(varchar(4),Year(GetDate()))+'-01'+'-01') ExStartDate  From ExamInfo inner join ExamType on ExamInfo.ExId=ExamType.ExId where BatchId='" + batchId + "' and SemesterExam=1 and ExStartDate<'" + date + "') and  '" + date + "' Group by StudentId),b as (SELECT v_Tbl_Exam_MonthlyTest.StudentId,Sum(v_Tbl_Exam_MonthlyTest.Patternmarks) mPatternmarks,  SUM(v_Tbl_Exam_MonthlyTest.Obtainmarks) mObtainmarks,(Select Max(Obtainmarks) From a) mHigestmarks FROM v_Tbl_Exam_MonthlyTest inner join a on v_Tbl_Exam_MonthlyTest.StudentId=a.StudentId where BatchId='" + batchId + "' and ShiftId='" + shiftId + "' and ClsGrpID='" + groupId + "' and ClsSecId='" + sectionId + "' and ExStartDate Between (Select isnull(Max(ExStartDate),convert(varchar(4),Year(GetDate()))+'-01'+'-01') ExStartDate  From ExamInfo inner join ExamType on ExamInfo.ExId=ExamType.ExId where BatchId='" + batchId + "' and SemesterExam=1 and ExStartDate<'" + date + "') and  '" + date + "' Group by v_Tbl_Exam_MonthlyTest.StudentId),lettergpa as(Select cms.StudentId,RollNo,ExinId, (SUM(Marks+a.Obtainmarks)*100)/SUM(SubjectQuestionPattern.QMarks+a.Patternmarks) Perobmarks,dbo.FindletterGrade((SUM(Marks+a.Obtainmarks)*100)/SUM(SubjectQuestionPattern.QMarks+a.Patternmarks)) LG,dbo.FINDGPA((SUM(Marks+a.Obtainmarks)*100)/SUM(SubjectQuestionPattern.QMarks+a.Patternmarks)) GPA,dbo.FindComments((SUM(Marks+a.Obtainmarks)*100)/SUM(SubjectQuestionPattern.QMarks+a.Patternmarks)) Comments FROM Class_" + tableName + "MarksSheet cms INNER JOIN SubjectQuestionPattern  ON cms.SubQPId=SubjectQuestionPattern.SubQPId left join a on cms.StudentId=a.StudentId  where  ExInId='" + examinId + "' and ShiftId='" + shiftId + "' and cms.ClsGrpID='" + groupId + "' and cms.ClsSecId='" + sectionId + "' GROUP BY cms.studentid,RollNo,ExinId), k as (Select studentid,RollNo,ExinId,cms.SubId,SUM(Marks) as Marks FROM Class_" + tableName + "MarksSheet cms INNER JOIN SubjectQuestionPattern  ON cms.SubQPId=SubjectQuestionPattern.SubQPId  where  ExInId='" + examinId + "' and ShiftId='" + shiftId + "' and cms.ClsGrpID='" + groupId + "' and cms.ClsSecId='" + sectionId + "' GROUP BY studentid,RollNo,ExinId,cms.SubId),l as(SELECT distinct s1.SubId, s2.higestmarks FROM k s1, (SELECT s.SubId, max(s.marks) as higestmarks from k s group by s.SubId ) s2 WHERE s1.SubId =s2.SubId and s1.marks= s2.higestmarks), p as(Select cms.StudentId,RollNo,DENSE_RANK() OVER (ORDER BY SUM(cms.Marks)+a.Obtainmarks DESC) AS 'position' FROM Class_"+tableName+"MarksSheet cms INNER JOIN SubjectQuestionPattern  ON cms.SubQPId=SubjectQuestionPattern.SubQPId left join a on cms.StudentId=a.StudentId  where  ExInId='" + examinId + "' and ShiftId='" + shiftId + "' and cms.ClsGrpID='" + groupId + "' and cms.ClsSecId='" + sectionId + "' GROUP BY cms.studentid,RollNo,a.Obtainmarks) SELECT        dbo.CurrentStudentInfo.FullName, dbo.BatchInfo.BatchName, dbo.ShiftConfiguration.ShiftName, dbo.Tbl_Group.GroupName, dbo.Sections.SectionName, cms.RollNo, case when dbo.AddCourseWithSubject.CourseName is null then dbo.NewSubject.SubName else dbo.NewSubject.SubName +' '+dbo.AddCourseWithSubject.CourseName end SubName, dbo.AddCourseWithSubject.CourseName, dbo.NewSubject.Ordering,cms.ExInId,ExamType.ExName,convert(varchar(11),ExamInfo.ExStartDate,105) ExStartDate,year(ExamInfo.ExStartDate) Year,DATENAME(MONTH,ExamInfo.ExStartDate) Month,SUM(dbo.SubjectQuestionPattern.QMarks) FullMarks,SUM(dbo.SubjectQuestionPattern.PassMarks) as PassMarks, SUM(cms.Marks)MarksObtained,lettergpa.Perobmarks,lettergpa.LG,lettergpa.GPA,l.higestmarks,lettergpa.Comments,b.mPatternmarks,b.mObtainmarks,b.mHigestmarks,p.position,Present,Absent,Total FROM            dbo.NewSubject INNER JOIN dbo.Class_" + tableName + "MarksSheet cms INNER JOIN dbo.SubjectQuestionPattern ON cms.SubQPId = dbo.SubjectQuestionPattern.SubQPId INNER JOIN dbo.CurrentStudentInfo ON cms.StudentId = dbo.CurrentStudentInfo.StudentId INNER JOIN dbo.BatchInfo ON cms.BatchId = dbo.BatchInfo.BatchId INNER JOIN dbo.ShiftConfiguration ON cms.ShiftId = dbo.ShiftConfiguration.ConfigId INNER JOIN dbo.Tbl_Class_Section ON cms.ClsSecId = dbo.Tbl_Class_Section.ClsSecID INNER JOIN dbo.Sections ON dbo.Tbl_Class_Section.SectionID = dbo.Sections.SectionID ON dbo.NewSubject.SubId = cms.SubId LEFT OUTER JOIN dbo.AddCourseWithSubject ON cms.CourseId = dbo.AddCourseWithSubject.CourseId LEFT OUTER JOIN dbo.Tbl_Group INNER JOIN dbo.Tbl_Class_Group ON dbo.Tbl_Group.GroupID = dbo.Tbl_Class_Group.GroupID ON cms.ClsGrpID = dbo.Tbl_Class_Group.ClsGrpID inner join lettergpa on cms.StudentId=lettergpa.StudentId inner join l on cms.SubId=l.SubId INNER JOIN ExamInfo on cms.ExInId=ExamInfo.ExInId inner join ExamType on ExamInfo.ExId=ExamType.ExId inner join b on cms.StudentId=b.StudentId inner join p on cms.StudentId=p.StudentId left join attendance on cms.StudentId=attendance.StudentId WHERE  cms.ExInId='" + examinId + "' and cms.ShiftId='" + shiftId + "' and cms.ClsGrpID='" + groupId + "' and cms.ClsSecId='" + sectionId + "' GROUP BY   dbo.CurrentStudentInfo.FullName, dbo.BatchInfo.BatchName, dbo.ShiftConfiguration.ShiftName, dbo.Tbl_Group.GroupName, dbo.Sections.SectionName,cms.RollNo, dbo.NewSubject.SubName, dbo.AddCourseWithSubject.CourseName, dbo.NewSubject.Ordering,cms.ExInId,lettergpa.Perobmarks,lettergpa.LG,lettergpa.GPA,l.higestmarks,lettergpa.Comments,ExamType.ExName,ExamInfo.ExStartDate,b.mPatternmarks,b.mObtainmarks,b.mHigestmarks,p.position,Present,Absent,Total      ORDER BY RollNo,Ordering";

                dt = CRUD.ReturnTableNull(sql);
                return dt;
            }
            catch { return dt; }
        }
        public DataTable LoadSemesterProgressReportIndividual(string year, string className,
            string examinId,string rollNo)
        {
            try
            {
                
                //string[] examsplit = examinId.Split('_');
                //string[] dateyear = examsplit[1].Split('-');
                //string date = dateyear[2] + "-" + dateyear[0] + "-" + dateyear[1];
                
                        sql = @"with ss as  (
                            select 0 as StudentId,SubId,1 as MSStatus,Ordering from ClassSubject  where ClassID in(select ClassID from BatchInfo where BatchName='" + className+ year + @"') and IsCommon=1 and IsOptional=0 union all

                            SELECT StudentId, SubId,MSStatus,Ordering FROM v_StudentGroupSubSetup WHERE   BatchId=(select BatchId from BatchInfo where BatchName='" + className + year + @"')"+ @")
                           , ms as(select m.* from  Class_"+ className + "MarksSheet m inner join ss on (m.StudentId=ss.StudentId or ss.StudentId=0) and ss.SubId=m.SubId and m.RollNo="+rollNo+ "  and ExamID='" + examinId + "'), Mxm as(" +
                            "select et.semesterexam,ms.BatchId, ms.StudentId,ms.RollNo,ms.ShiftId,ms.SubId,ms.CourseId, case when ms.IsPassed=1 then 0 else 1 end As IsFailed,qp.qpName, sqp.QpId,ms.marks ,sqp.QMarks from ms inner join ExamType et on ms.exid=et.exid " +
                            "inner join SubjectQuestionPattern sqp on ms.subqpid=sqp.subqpid inner join QuestionPattern qp on sqp.QPid=qp.QpId ), MxmFM as(" +
                            "SELECT  semesterexam, BatchId, StudentId,RollNo,ShiftId,SubId,CourseId, MCQ ,   CQ  ,Practical  FROM   Mxm PIVOT ( SUM(QMarks) FOR [qpName] IN ( MCQ,CQ,Practical)) AS P group by semesterexam, BatchId, StudentId,RollNo,ShiftId,SubId,CourseId,p.mcq,p.cq,p.Practical)" +
                            ",MxmFM_T as(select BatchId,StudentId,RollNo,ShiftId,SubId, CourseId, sum(isnull(mcq,0)) MCQ,sum(isnull(cq,0)) CQF ,sum(isnull(Practical,0)) Practical from MxmFM group by semesterexam,BatchId,StudentId,RollNo,ShiftId,SubId, CourseId)" +
                            ",Mxm1 as(SELECT  semesterexam, BatchId, StudentId,RollNo,ShiftId,SubId,CourseId, IsFailed, MCQ,   CQ,Practical FROM   Mxm PIVOT ( SUM(Marks) FOR [qpName] IN ( MCQ,CQ,Practical)) AS P group by semesterexam, BatchId,StudentId,RollNo,ShiftId,SubId,CourseId, IsFailed,p.mcq,p.cq,p.Practical )" +
                            ",Mxm1_T as(select BatchId,StudentId,RollNo,ShiftId,SubId, CourseId, sum(IsFailed) as IsFailed,case when CQ is null and  MCQ is null and Practical is null then 'AB' else '' end as AB,     sum(isnull(mcq,0)) MCQ,sum(isnull(cq,0)) CQ ,sum(isnull(Practical,0)) Practical from Mxm1 group by semesterexam,BatchId,StudentId,RollNo,ShiftId,SubId, CourseId, case when CQ is null and  MCQ is null and Practical is null then 'AB' else '' end)" +
                            ",MaxN as(select subid,courseid,max(Cq) MaxCQ,max(mcq)MaxMCQ from  Mxm1_T  group by subid,courseid), " +
                            "psn as (select Mxm1_T.StudentId,Mxm1_T.ShiftId,csi.ClsSecId,csi.ClsGrpId, sum(round((Mxm1_T.cq+Mxm1_T.mcq+Mxm1_T.Practical),0)) TotalMark,ROW_NUMBER() OVER (order by  sum(round((Mxm1_T.cq+Mxm1_T.mcq+Mxm1_T.Practical),0)) desc) as PositionInGrp,ROW_NUMBER() OVER(PARTITION BY csi.ClsSecid order by   sum(round((Mxm1_T.cq+Mxm1_T.mcq+Mxm1_T.Practical),0)) desc) as PositionInSec   from  Mxm1_T inner join v_CurrentStudentInfo csi on Mxm1_T.batchid=csi.batchid and Mxm1_T.StudentId=csi.StudentId group by Mxm1_T.StudentId,Mxm1_T.ShiftId ,csi.ClsSecId,csi.ClsGrpId) " +
                            " select '1' as IsIndependent, ClassName,Sxm2.batchid,Sxm2.studentid,Sxm2.rollno,Sxm2.subid,Sxm2.courseid,Sxm2.cq TotalCQ, Sxm2.mcq,Sxm2.Practical,(Sxm2.cq +Sxm2.mcq+Sxm2.Practical) TotalMark,MaxN.MaxCQ,MaxN.MaxMCQ,ns.SubName,acs.CourseName,cs.SubCode,cs.Marks,IsFailed,csi.FullName,csi.FathersName,csi.MothersName,csi.Session,convert(varchar(10), csi.DateOfBirth,105) as DateOfBirth,csi.ShiftName ,csi.GroupName,csi.SectionName,csi.RollNo,psn.PositionInSec,psn.PositionInGrp, case when gs.MSStatus is null then 1 else gs.MSStatus end as MSStatus,ers.IsPassed,ers.GPA,ers.Grade,ers.withoutOpGPA,ers.withoutOpGrade,AB   from Mxm1_T Sxm2  inner join NewSubject ns on Sxm2.subid=ns.subid inner join AddCourseWithSubject acs on Sxm2.subid=acs.subid and (Sxm2.courseid=acs.courseid or sxm2.courseid=0) " +
                            "inner join BatchInfo on sxm2.BatchId=BatchInfo.BatchId inner join ClassSubject cs on BatchInfo.ClassId=cs.ClassId and Sxm2.subid=cs.subid and Sxm2.courseid=cs.courseid inner join v_CurrentStudentInfo csi on Sxm2.batchid=csi.batchid and Sxm2.StudentId=csi.StudentId  inner join MaxN on Sxm2.subid=MaxN.subid and Sxm2.Courseid=MaxN.Courseid inner join psn on Sxm2.StudentId=psn.StudentId left join v_StudentGroupSubSetupDetails gs on Sxm2.Batchid=gs.Batchid and Sxm2.StudentId=gs.StudentId and Sxm2.Subid=gs.Subid left join Exam_ResultSheet ers on Sxm2.StudentId = ers.StudentID and ers.ExamID = '" + examinId + "' order by MSStatus desc ";
                 
                dt = CRUD.ReturnTableNull(sql);

                return dt;
            }
            catch { return dt; }
        }


        //   public DataTable LoadSemesterProgressReport(string tableName, string shiftId,
        //string batchId, string groupId, string sectionId, string examinId,string RollNo, bool isFinal)
        //   {
        //       try
        //       {
        //           RollNo = (RollNo == "") ? "" : " and RollNo="+ RollNo;
        //           string[] examsplit = examinId.Split('_');
        //           string[] dateyear = examsplit[1].Split('-');
        //           string date = dateyear[2] + "-" + dateyear[0] + "-" + dateyear[1];
        //           if (!isFinal) {
        //               string examList = getDependencyExInId(examinId, "False");
        //               if (examList.Contains(','))
        //               {
        //                   sql = @"with ms as(select * from Class_" + tableName + "MarksSheet where Marks is not null and isPassed is not null and BatchId='" + batchId + "' and ShiftId='" + shiftId + "'  and ExInId in(" + examList + ")  "+ RollNo + "), " +
        //                  "Qxm as(select et.semesterexam,ms.BatchId, StudentId,RollNo,ShiftId,ms.SubId,ms.CourseId,qp.qpName, sqp.QpId,round((sum(ms.marks)/count(ms.ExInId)),0) marks,sqp.ConvertTo,case when qp.QPName='CQ' then( ((round( (sum(ms.marks)/count(ms.ExInId)),0))/100)* sqp.ConvertTo) else 0 end as ConvertMarks,sqp.QMarks from ms inner join ExamType et on ms.exid=et.exid inner join SubjectQuestionPattern sqp on ms.subqpid=sqp.subqpid inner join QuestionPattern qp on sqp.QPid=qp.QpId where et.semesterexam is null group by  et.semesterexam,ms.BatchId,StudentId,RollNo,ShiftId,ms.SubId,ms.CourseId,qp.qpName,sqp.QpId,sqp.ConvertTo,sqp.QMarks)" +
        //                  ",QxmFM as(SELECT  semesterexam, BatchId, StudentId,RollNo,ShiftId,SubId,CourseId,ConvertTo,sum(ConvertMarks) as ConvertMarks, MCQ,   CQ FROM   Qxm PIVOT(SUM(QMarks)FOR [qpName] IN ( MCQ,CQ)) AS P group by semesterexam, BatchId, StudentId,RollNo,ShiftId,SubId,CourseId,ConvertTo,p.mcq,p.cq )," +
        //                  "Qxm1 as(SELECT  semesterexam, BatchId, StudentId,RollNo,ShiftId,SubId,CourseId,ConvertTo,sum(ConvertMarks) as ConvertMarks, MCQ,   CQ FROM   Qxm PIVOT(SUM(Marks)FOR [qpName] IN ( MCQ,CQ)) AS P group by semesterexam, BatchId, StudentId,RollNo,ShiftId,SubId,CourseId,ConvertTo,p.mcq,p.cq )," +
        //                  "QxmFM_T as(select BatchId,StudentId,RollNo,ShiftId,SubId, CourseId,sum(ConvertTo) ConvertTo, sum(isnull(mcq,0)) MCQ,sum(isnull(cq,0)) CQF , sum(ConvertMarks) [10 % of CQ]from QxmFM group by semesterexam,BatchId,StudentId,RollNo,ShiftId,SubId, CourseId)," +
        //                  "Qxm1_T as(select BatchId,StudentId,RollNo,ShiftId,SubId, CourseId,sum(ConvertTo) ConvertTo, sum(isnull(mcq,0)) MCQ,sum(isnull(cq,0)) CQ , sum(ConvertMarks) [10 % of CQ]from Qxm1 group by semesterexam,BatchId,StudentId,RollNo,ShiftId,SubId, CourseId) ,  " +
        //                  "Qxm2 as (select Qxm1_T.BatchId,Qxm1_T.StudentId,Qxm1_T.RollNo,Qxm1_T.ShiftId,Qxm1_T.SubId, Qxm1_T.CourseId, Qxm1_T.ConvertTo,  Qxm1_T.MCQ,Qxm1_T. CQ , CQF ,Qxm1_T.[10 % of CQ]from Qxm1_T inner join QxmFM_T on Qxm1_T.StudentId=QxmFM_T.StudentId and Qxm1_T.SubId=QxmFM_T.SubId and Qxm1_T.CourseId=QxmFM_T.CourseId)," +
        //                  " Sxm as( select et.semesterexam,ms.BatchId, ms.StudentId,ms.RollNo,ms.ShiftId,ms.SubId,ms.CourseId,qp.qpName, sqp.QpId,(sum(ms.marks)/count(ms.ExInId)) marks,case when Qxm.ConvertTo>0 then  (100-Qxm.ConvertTo) else 0 end as ConvertTo,case when qp.qpName='CQ' then((sum(ms.marks)/count(ms.ExInId))/100)*(case when Qxm.ConvertTo>0 then  (100-Qxm.ConvertTo) else 0 end) else 0 end as ConvertMarks ,sqp.QMarks " +
        //                  "from ms inner join ExamType et on ms.exid=et.exid inner join SubjectQuestionPattern sqp on ms.subqpid=sqp.subqpid inner join QuestionPattern qp on sqp.QPid=qp.QpId left JOIN Qxm on ms.subid=Qxm.subid and ms.courseid=Qxm.courseid and sqp.qpid=Qxm.qpid where et.semesterexam=1 group by  et.semesterexam,ms.BatchId,ms.StudentId,ms.RollNo,ms.ShiftId,ms.SubId,ms.CourseId,qp.qpName,sqp.QpId,Qxm.ConvertTo,sqp.QMarks  ) , " +
        //                  "SxmFM as(SELECT  semesterexam, BatchId, StudentId,RollNo,ShiftId,SubId,CourseId,ConvertTo,sum(ConvertMarks) as ConvertMarks, MCQ ,   CQ  ,Practical  FROM   Sxm PIVOT ( SUM(QMarks) FOR [qpName] IN ( MCQ,CQ,Practical)) AS P group by semesterexam, BatchId, StudentId,RollNo,ShiftId,SubId,CourseId,ConvertTo,p.mcq,p.cq,p.Practical)," +
        //                  "Sxm1 as(SELECT  semesterexam, BatchId, StudentId,RollNo,ShiftId,SubId,CourseId,ConvertTo,sum(ConvertMarks) as ConvertMarks, MCQ,   CQ,Practical FROM   Sxm PIVOT ( SUM(Marks) FOR [qpName] IN ( MCQ,CQ,Practical)) AS P group by semesterexam, BatchId, StudentId,RollNo,ShiftId,SubId,CourseId,ConvertTo,p.mcq,p.cq,p.Practical ), " +
        //                  "SxmFM_T as (select BatchId,StudentId,RollNo,ShiftId,SubId, CourseId, sum(isnull(mcq,0)) MCQ,sum(isnull(cq,0)) CQF ,sum(isnull(Practical,0)) Practical, sum(ConvertMarks) [90 % of CQ]from SxmFM group by semesterexam,BatchId,StudentId,RollNo,ShiftId,SubId, CourseId) , " +
        //                  "Sxm1_T as (select BatchId,StudentId,RollNo,ShiftId,SubId, CourseId, sum(isnull(mcq,0)) MCQ,sum(isnull(cq,0)) CQ ,sum(isnull(Practical,0)) Practical, sum(ConvertMarks) [90 % of CQ]from Sxm1 group by semesterexam,BatchId,StudentId,RollNo,ShiftId,SubId, CourseId)," +
        //                  "Sxm2 as (select Sxm1_T.BatchId,Sxm1_T.StudentId,Sxm1_T.RollNo,Sxm1_T.ShiftId,Sxm1_T.SubId, Sxm1_T.CourseId,Sxm1_T.MCQ,Sxm1_T.CQ,Sxm1_T.Practical,Sxm1_T.[90 % of CQ],CQF from Sxm1_T inner join SxmFM_T on Sxm1_T.StudentId=SxmFM_T.StudentId and Sxm1_T.SubId=SxmFM_T.SubId and Sxm1_T.CourseId=SxmFM_T.CourseId), ar as (Select sum(case  when AttStatus='p' or AttStatus= 'l'   then 1 else 0 end) as Present,sum(case  when AttStatus='a'  then 1 else 0 end) as Absent,sum(case  when AttStatus='p' or AttStatus= 'l'   then 1 else 0 end)+sum(case  when AttStatus='a'  then 1 else 0 end) as Total,StudentId  from DailyAttendanceRecord where StudentId in(select distinct StudentId from Sxm2 ) and AttDate<'2018-05-01' and BatchId='3017' group by StudentId), MaxN as(select Sxm2.subid,Sxm2.courseid,max( round((Qxm2.[10 % of CQ]+Sxm2.[90 % of CQ]) ,0)) MaxCQ,max( Sxm2.mcq) MaxMCQ from  Sxm2 inner join Qxm2 on Sxm2.StudentId=Qxm2.StudentId and  Sxm2.subid=Qxm2.subid and Sxm2.CourseId=Qxm2.CourseId group by Sxm2.subid,Sxm2.courseid) , psn as(select Sxm2.StudentId,Sxm2.ShiftId,csi.ClsSecId,csi.ClsGrpId, sum( (round((Qxm2.[10 % of CQ]+Sxm2.[90 % of CQ]) ,0)+Sxm2.mcq+Sxm2.Practical)) TotalMark,ROW_NUMBER() OVER (order by  sum( (round((Qxm2.[10 % of CQ]+Sxm2.[90 % of CQ]) ,0)+Sxm2.mcq+Sxm2.Practical)) desc) as PositionInGrp,ROW_NUMBER() OVER(PARTITION BY csi.ClsSecid order by   sum( (round((Qxm2.[10 % of CQ]+Sxm2.[90 % of CQ]) ,0)+Sxm2.mcq+Sxm2.Practical)) desc) as PositionInSec   from  Sxm2 inner join Qxm2 on Sxm2.StudentId=Qxm2.StudentId and  Sxm2.subid=Qxm2.subid and Sxm2.CourseId=Qxm2.CourseId inner join v_CurrentStudentInfo csi on Sxm2.batchid=csi.batchid and Sxm2.StudentId=csi.StudentId group by Sxm2.StudentId,Sxm2.ShiftId ,csi.ClsSecId,csi.ClsGrpId) " +
        //                  " select '0' as IsIndependent,  ClassName,Sxm2.batchid,Sxm2.studentid,Sxm2.rollno,Sxm2.subid,Sxm2.courseid,Qxm2.mcq Qmcq,Qxm2.cq Qcq,Qxm2.[10 % of CQ] as [10 % of CQ_1],((Sxm2.CQF* Qxm2.[10 % of CQ])/Qxm2.CQF) [10 % of CQ] ,Sxm2.cq,Sxm2.[90 % of CQ],round((((Sxm2.CQF* Qxm2.[10 % of CQ])/Qxm2.CQF)+Sxm2.[90 % of CQ]) ,0)TotalCQ, Sxm2.mcq,Sxm2.Practical,(round((((Sxm2.CQF* Qxm2.[10 % of CQ])/Qxm2.CQF)+Sxm2.[90 % of CQ]) ,0)+Sxm2.mcq+Sxm2.Practical) TotalMark,MaxN.MaxCQ,MaxN.MaxMCQ,ns.SubName,acs.CourseName,cs.SubCode,cs.Marks,csi.FullName,csi.FathersName,csi.MothersName,csi.Session,convert(varchar(10), csi.DateOfBirth,105) as DateOfBirth,csi.ShiftName ,csi.GroupName,csi.SectionName,csi.RollNo ,ar.Absent,ar.Present,ar.Total,psn.PositionInSec,psn.PositionInGrp, case when gs.MSStatus is null then 1 else gs.MSStatus end as MSStatus from Sxm2 inner join Qxm2 on Sxm2.StudentId=Qxm2.StudentId and  Sxm2.subid=Qxm2.subid and Sxm2.CourseId=Qxm2.CourseId inner join NewSubject ns on Sxm2.subid=ns.subid inner join AddCourseWithSubject acs on Sxm2.subid=acs.subid and Sxm2.courseid=acs.courseid inner join BatchInfo on sxm2.BatchId=BatchInfo.BatchId inner join ClassSubject cs on BatchInfo.ClassId=cs.ClassId and Sxm2.subid=cs.subid and Sxm2.courseid=cs.courseid inner join v_CurrentStudentInfo csi on Sxm2.batchid=csi.batchid and Sxm2.StudentId=csi.StudentId left join ar on Sxm2.StudentId=ar.StudentId inner join MaxN on Sxm2.subid=MaxN.subid and Sxm2.Courseid=MaxN.Courseid inner join psn on Sxm2.StudentId=psn.StudentId left join v_StudentGroupSubSetupDetails gs on Sxm2.Batchid=gs.Batchid and Sxm2.StudentId=gs.StudentId and Sxm2.Subid=gs.Subid" +
        //                  " where csi.ClsSecId=" + sectionId + "";
        //               }
        //               else
        //               {
        //                   //
        //                   sql = @"with ms as(select * from Class_" + tableName + "MarksSheet where Marks is not null and isPassed is not null and BatchId='" + batchId + "' and ShiftId='" + shiftId + "'  and ExInId in(" + examList + ")  " + RollNo + "), Mxm as(" +
        //                       "select et.semesterexam,ms.BatchId, ms.StudentId,ms.RollNo,ms.ShiftId,ms.SubId,ms.CourseId,qp.qpName, sqp.QpId,ms.marks ,sqp.QMarks from ms inner join ExamType et on ms.exid=et.exid " +
        //                       "inner join SubjectQuestionPattern sqp on ms.subqpid=sqp.subqpid inner join QuestionPattern qp on sqp.QPid=qp.QpId ), MxmFM as(" +
        //                       "SELECT  semesterexam, BatchId, StudentId,RollNo,ShiftId,SubId,CourseId, MCQ ,   CQ  ,Practical  FROM   Mxm PIVOT ( SUM(QMarks) FOR [qpName] IN ( MCQ,CQ,Practical)) AS P group by semesterexam, BatchId, StudentId,RollNo,ShiftId,SubId,CourseId,p.mcq,p.cq,p.Practical)" +
        //                       ",MxmFM_T as(select BatchId,StudentId,RollNo,ShiftId,SubId, CourseId, sum(isnull(mcq,0)) MCQ,sum(isnull(cq,0)) CQF ,sum(isnull(Practical,0)) Practical from MxmFM group by semesterexam,BatchId,StudentId,RollNo,ShiftId,SubId, CourseId)" +
        //                       ",Mxm1 as(SELECT  semesterexam, BatchId, StudentId,RollNo,ShiftId,SubId,CourseId, MCQ,   CQ,Practical FROM   Mxm PIVOT ( SUM(Marks) FOR [qpName] IN ( MCQ,CQ,Practical)) AS P group by semesterexam, BatchId,StudentId,RollNo,ShiftId,SubId,CourseId,p.mcq,p.cq,p.Practical )" +
        //                       ",Mxm1_T as(select BatchId,StudentId,RollNo,ShiftId,SubId, CourseId, sum(isnull(mcq,0)) MCQ,sum(isnull(cq,0)) CQ ,sum(isnull(Practical,0)) Practical from Mxm1 group by semesterexam,BatchId,StudentId,RollNo,ShiftId,SubId, CourseId)" +
        //                       ",MaxN as(select subid,courseid,max(Cq) MaxCQ,max(mcq)MaxMCQ from  Mxm1_T  group by subid,courseid), " +
        //                       "psn as (select Mxm1_T.StudentId,Mxm1_T.ShiftId,csi.ClsSecId,csi.ClsGrpId, sum(round((Mxm1_T.cq+Mxm1_T.mcq+Mxm1_T.Practical),0)) TotalMark,ROW_NUMBER() OVER (order by  sum(round((Mxm1_T.cq+Mxm1_T.mcq+Mxm1_T.Practical),0)) desc) as PositionInGrp,ROW_NUMBER() OVER(PARTITION BY csi.ClsSecid order by   sum(round((Mxm1_T.cq+Mxm1_T.mcq+Mxm1_T.Practical),0)) desc) as PositionInSec   from  Mxm1_T inner join v_CurrentStudentInfo csi on Mxm1_T.batchid=csi.batchid and Mxm1_T.StudentId=csi.StudentId group by Mxm1_T.StudentId,Mxm1_T.ShiftId ,csi.ClsSecId,csi.ClsGrpId) " +
        //                       " select '1' as IsIndependent, ClassName,Sxm2.batchid,Sxm2.studentid,Sxm2.rollno,Sxm2.subid,Sxm2.courseid,Sxm2.cq TotalCQ, Sxm2.mcq,Sxm2.Practical,(Sxm2.cq +Sxm2.mcq+Sxm2.Practical) TotalMark,MaxN.MaxCQ,MaxN.MaxMCQ,ns.SubName,acs.CourseName,cs.SubCode,cs.Marks,csi.FullName,csi.FathersName,csi.MothersName,csi.Session,convert(varchar(10), csi.DateOfBirth,105) as DateOfBirth,csi.ShiftName ,csi.GroupName,csi.SectionName,csi.RollNo,psn.PositionInSec,psn.PositionInGrp, case when gs.MSStatus is null then 1 else gs.MSStatus end as MSStatus from Mxm1_T Sxm2  inner join NewSubject ns on Sxm2.subid=ns.subid inner join AddCourseWithSubject acs on Sxm2.subid=acs.subid and Sxm2.courseid=acs.courseid " +
        //                       "inner join BatchInfo on sxm2.BatchId=BatchInfo.BatchId inner join ClassSubject cs on BatchInfo.ClassId=cs.ClassId and Sxm2.subid=cs.subid and Sxm2.courseid=cs.courseid inner join v_CurrentStudentInfo csi on Sxm2.batchid=csi.batchid and Sxm2.StudentId=csi.StudentId  inner join MaxN on Sxm2.subid=MaxN.subid and Sxm2.Courseid=MaxN.Courseid inner join psn on Sxm2.StudentId=psn.StudentId left join v_StudentGroupSubSetupDetails gs on Sxm2.Batchid=gs.Batchid and Sxm2.StudentId=gs.StudentId and Sxm2.Subid=gs.Subid where csi.ClsSecId=" + sectionId + "";
        //               }


        //               //sql = "with ms as(select * from Class_" + tableName + "MarksSheet where Marks is not null and BatchId='" + batchId + "' and ShiftId='" + shiftId + "'  and ExInId in(" + getDependencyExInId(examinId, "False") + "))," +
        //               //    " Qxm as(" +
        //               //    "select et.semesterexam,ms.BatchId, StudentId,RollNo,ShiftId,ms.SubId,ms.CourseId,qp.qpName, sqp.QpId,(sum(ms.marks)/count(ms.ExInId)) marks,sqp.ConvertTo,((sum(ms.marks)/count(ms.ExInId))/100)* sqp.ConvertTo as ConvertMarks from ms inner join ExamType et on ms.exid=et.exid inner join SubjectQuestionPattern sqp on ms.subqpid=sqp.subqpid inner join QuestionPattern qp on sqp.QPid=qp.QpId where et.semesterexam is null " +
        //               //    "group by  et.semesterexam,ms.BatchId,StudentId,RollNo,ShiftId,ms.SubId,ms.CourseId,qp.qpName,sqp.QpId,sqp.ConvertTo) ," +
        //               //    " Qxm1 as(SELECT  semesterexam, BatchId, StudentId,RollNo,ShiftId,SubId,CourseId,ConvertTo,sum(ConvertMarks) as ConvertMarks, MCQ,   CQ FROM   Qxm " +
        //               //    "PIVOT" +
        //               //    "(SUM(Marks)FOR [qpName] IN ( MCQ,CQ)) AS P group by semesterexam, BatchId, StudentId,RollNo,ShiftId,SubId,CourseId,ConvertTo,p.mcq,p.cq )," +
        //               //    " Qxm2 as (select BatchId,StudentId,RollNo,ShiftId,SubId, CourseId,sum(ConvertTo) ConvertTo, sum(isnull(mcq,0)) MCQ,sum(isnull(cq,0)) CQ , sum(ConvertMarks) [10 % of CQ]from Qxm1 group by semesterexam,BatchId,StudentId,RollNo,ShiftId,SubId, CourseId)," +
        //               //    "Sxm as(select et.semesterexam,ms.BatchId, ms.StudentId,ms.RollNo,ms.ShiftId,ms.SubId,ms.CourseId,qp.qpName, sqp.QpId,(sum(ms.marks)/count(ms.ExInId)) marks,case when Qxm.ConvertTo>0 then  (100-Qxm.ConvertTo) else 0 end as ConvertTo,((sum(ms.marks)/count(ms.ExInId))/100)*(case when Qxm.ConvertTo>0 then  (100-Qxm.ConvertTo) else 0 end) as ConvertMarks from ms inner join ExamType et on ms.exid=et.exid inner join SubjectQuestionPattern sqp on ms.subqpid=sqp.subqpid inner join QuestionPattern qp on sqp.QPid=qp.QpId left JOIN Qxm on ms.subid=Qxm.subid and ms.courseid=Qxm.courseid and sqp.qpid=Qxm.qpid where et.semesterexam=1 " +
        //               //    "group by  et.semesterexam,ms.BatchId,ms.StudentId,ms.RollNo,ms.ShiftId,ms.SubId,ms.CourseId,qp.qpName,sqp.QpId,Qxm.ConvertTo ) , " +
        //               //    "Sxm1 as(SELECT  semesterexam, BatchId, StudentId,RollNo,ShiftId,SubId,CourseId,ConvertTo,sum(ConvertMarks) as ConvertMarks, MCQ,   CQ,Practical FROM   Sxm " +
        //               //    "PIVOT " +
        //               //    "( SUM(Marks) FOR [qpName] IN ( MCQ,CQ,Practical)) AS P " +
        //               //    "group by semesterexam, BatchId, StudentId,RollNo,ShiftId,SubId,CourseId,ConvertTo,p.mcq,p.cq,p.Practical )," +
        //               //    "Sxm2 as (select BatchId,StudentId,RollNo,ShiftId,SubId, CourseId, sum(isnull(mcq,0)) MCQ,sum(isnull(cq,0)) CQ ,sum(isnull(Practical,0)) Practical, sum(ConvertMarks) [90 % of CQ]from Sxm1 group by semesterexam,BatchId,StudentId,RollNo,ShiftId,SubId, CourseId), " +
        //               //    "ar as (Select sum(case  when AttStatus='p' or AttStatus= 'l'   then 1 else 0 end) as Present,sum(case  when AttStatus='a'  then 1 else 0 end) as Absent,sum(case  when AttStatus='p' or AttStatus= 'l'   then 1 else 0 end)+sum(case  when AttStatus='a'  then 1 else 0 end) as Total,StudentId  from DailyAttendanceRecord where StudentId in(select distinct StudentId from Sxm2 ) and AttDate<'" + date + "' and BatchId='" + batchId + "' group by StudentId)" +
        //               //    ", MaxN as(select Sxm2.subid,Sxm2.courseid,max( round((Qxm2.[10 % of CQ]+Sxm2.[90 % of CQ]) ,0)) MaxCQ,max( Sxm2.mcq) MaxMCQ from  Sxm2 inner join Qxm2 on Sxm2.StudentId=Qxm2.StudentId and  Sxm2.subid=Qxm2.subid and Sxm2.CourseId=Qxm2.CourseId group by Sxm2.subid,Sxm2.courseid) " +
        //               //    ", psn as(select Sxm2.StudentId,Sxm2.ShiftId,csi.ClsSecId,csi.ClsGrpId, sum( (round((Qxm2.[10 % of CQ]+Sxm2.[90 % of CQ]) ,0)+Sxm2.mcq+Sxm2.Practical)) TotalMark,ROW_NUMBER() OVER (order by  sum( (round((Qxm2.[10 % of CQ]+Sxm2.[90 % of CQ]) ,0)+Sxm2.mcq+Sxm2.Practical)) desc) as PositionInGrp,ROW_NUMBER() OVER(PARTITION BY csi.ClsSecid order by   sum( (round((Qxm2.[10 % of CQ]+Sxm2.[90 % of CQ]) ,0)+Sxm2.mcq+Sxm2.Practical)) desc) as PositionInSec " +
        //               //    "  from  Sxm2 inner join Qxm2 on Sxm2.StudentId=Qxm2.StudentId and  Sxm2.subid=Qxm2.subid and Sxm2.CourseId=Qxm2.CourseId " +
        //               //    "inner join v_CurrentStudentInfo csi on Sxm2.batchid=csi.batchid and Sxm2.StudentId=csi.StudentId group by Sxm2.StudentId,Sxm2.ShiftId ,csi.ClsSecId,csi.ClsGrpId) " +
        //               //    "select ClassName,Sxm2.batchid,Sxm2.studentid,Sxm2.rollno,Sxm2.subid,Sxm2.courseid,Qxm2.mcq Qmcq,Qxm2.cq Qcq,Qxm2.[10 % of CQ],Sxm2.cq,Sxm2.[90 % of CQ],round((Qxm2.[10 % of CQ]+Sxm2.[90 % of CQ]) ,0)TotalCQ, Sxm2.mcq,Sxm2.Practical,(round((Qxm2.[10 % of CQ]+Sxm2.[90 % of CQ]) ,0)+Sxm2.mcq+Sxm2.Practical) TotalMark,MaxN.MaxCQ,MaxN.MaxMCQ,ns.SubName,acs.CourseName,cs.SubCode,cs.Marks,csi.FullName,csi.ShiftName ,csi.GroupName,csi.SectionName,csi.RollNo ,ar.Absent,ar.Present,ar.Total,psn.PositionInSec,psn.PositionInGrp, case when gs.MSStatus is null then 1 else gs.MSStatus end as MSStatus " +
        //               //    "from Sxm2 inner join Qxm2 on Sxm2.StudentId=Qxm2.StudentId and  Sxm2.subid=Qxm2.subid and Sxm2.CourseId=Qxm2.CourseId inner join NewSubject ns on Sxm2.subid=ns.subid " +
        //               //    "inner join AddCourseWithSubject acs on Sxm2.subid=acs.subid and Sxm2.courseid=acs.courseid inner join BatchInfo on sxm2.BatchId=BatchInfo.BatchId inner join ClassSubject cs on BatchInfo.ClassId=cs.ClassId and Sxm2.subid=cs.subid and Sxm2.courseid=cs.courseid inner join v_CurrentStudentInfo csi on Sxm2.batchid=csi.batchid and Sxm2.StudentId=csi.StudentId left join ar on Sxm2.StudentId=ar.StudentId inner join MaxN on Sxm2.subid=MaxN.subid and Sxm2.Courseid=MaxN.Courseid inner join psn on Sxm2.StudentId=psn.StudentId left join v_StudentGroupSubSetupDetails gs on Sxm2.Batchid=gs.Batchid and Sxm2.StudentId=gs.StudentId and Sxm2.Subid=gs.Subid " +
        //               //    "where csi.ClsSecId=" + sectionId + "";
        //       }
        //           else
        //           {
        //               string[] ExInId = getDependencyExInId(examinId, "True").Split(',');

        //               sql = "with "+
        //                   "F_ms as(select * from Class_" + tableName + "MarksSheet where Marks is not null and isPassed is not null and BatchId='" + batchId + "' and ShiftId='" + shiftId + "'  and ExInId in(" + getDependencyExInId(ExInId[2].Replace("'",string.Empty), "False") + ")), F_Qxm as(select et.semesterexam,F_ms.BatchId, StudentId,RollNo,ShiftId,F_ms.SubId,F_ms.CourseId,qp.qpName, sqp.QpId,(sum(F_ms.marks)/count(F_ms.ExInId)) marks,sqp.ConvertTo,((sum(F_ms.marks)/count(F_ms.ExInId))/100)* sqp.ConvertTo as ConvertMarks from F_ms inner join ExamType et on F_ms.exid=et.exid inner join SubjectQuestionPattern sqp on F_ms.subqpid=sqp.subqpid inner join QuestionPattern qp on sqp.QPid=qp.QpId where et.semesterexam is null group by  et.semesterexam,F_ms.BatchId,StudentId,RollNo,ShiftId,F_ms.SubId,F_ms.CourseId,qp.qpName,sqp.QpId,sqp.ConvertTo) , F_Qxm1 as(SELECT  semesterexam,BatchId, StudentId,RollNo,ShiftId,SubId,CourseId,ConvertTo,sum(ConvertMarks) as ConvertMarks, MCQ,   CQ FROM   F_Qxm PIVOT(SUM(Marks)FOR [qpName] IN ( MCQ,CQ)) AS P group by semesterexam, BatchId, StudentId,RollNo,ShiftId,SubId,CourseId,ConvertTo,p.mcq,p.cq ), F_Qxm2 as (select BatchId,StudentId,RollNo,ShiftId,SubId, CourseId,sum(ConvertTo) ConvertTo, sum(isnull(mcq,0)) MCQ,sum(isnull(cq,0)) CQ , sum(ConvertMarks) [10 % of CQ]from F_Qxm1 group by semesterexam,BatchId,StudentId,RollNo,ShiftId,SubId, CourseId)," +
        //                   "F_Sxm as(select et.semesterexam,F_ms.BatchId, F_ms.StudentId,F_ms.RollNo,F_ms.ShiftId,F_ms.SubId,F_ms.CourseId,qp.qpName, sqp.QpId,(sum(F_ms.marks)/count(F_ms.ExInId)) marks,case when F_Qxm.ConvertTo>0 then  (100-F_Qxm.ConvertTo) else 0 end as ConvertTo,((sum(F_ms.marks)/count(F_ms.ExInId))/100)*(case when F_Qxm.ConvertTo>0 then  (100-F_Qxm.ConvertTo) else 0 end) as ConvertMarks from F_ms inner join ExamType et on F_ms.exid=et.exid inner join SubjectQuestionPattern sqp on F_ms.subqpid=sqp.subqpid inner join QuestionPattern qp on sqp.QPid=qp.QpId left JOIN F_Qxm on F_ms.subid=F_Qxm.subid and F_ms.courseid=F_Qxm.courseid and sqp.qpid=F_Qxm.qpid where et.semesterexam=1 group by  et.semesterexam,F_ms.BatchId,F_ms.StudentId,F_ms.RollNo,F_ms.ShiftId,F_ms.SubId,F_ms.CourseId,qp.qpName,sqp.QpId,F_Qxm.ConvertTo ) , F_Sxm1 as(SELECT  semesterexam, BatchId, StudentId,RollNo,ShiftId,SubId,CourseId,ConvertTo,sum(ConvertMarks) as ConvertMarks, MCQ,   CQ,Practical FROM   F_Sxm PIVOT ( SUM(Marks) FOR [qpName] IN ( MCQ,CQ,Practical)) AS P group by semesterexam, BatchId, StudentId,RollNo,ShiftId,SubId,CourseId,ConvertTo,p.mcq,p.cq,p.Practical ),F_Sxm2 as (select BatchId,StudentId,RollNo,ShiftId,SubId, CourseId, sum(isnull(mcq,0)) MCQ,sum(isnull(cq,0)) CQ ,sum(isnull(Practical,0)) Practical, sum(ConvertMarks) [90 % of CQ]from F_Sxm1 group by semesterexam,BatchId,StudentId,RollNo,ShiftId,SubId, CourseId),"+
        //                   "F_tbl as(select F_Sxm2.batchid,F_Sxm2.studentid,F_Sxm2.subid,F_Sxm2.courseid,round((F_Qxm2.[10 % of CQ]+F_Sxm2.[90 % of CQ]) ,0) *.3 TotalCQ, F_Sxm2.mcq *.3 mcq,F_Sxm2.Practical*.3 Practical from F_Sxm2 inner join F_Qxm2 on F_Sxm2.StudentId=F_Qxm2.StudentId and  F_Sxm2.subid=F_Qxm2.subid and F_Sxm2.CourseId=F_Qxm2.CourseId inner join NewSubject ns on F_Sxm2.subid=ns.subid inner join AddCourseWithSubject acs on F_Sxm2.subid=acs.subid and F_Sxm2.courseid=acs.courseid inner join BatchInfo on F_Sxm2.BatchId=BatchInfo.BatchId inner join ClassSubject cs on BatchInfo.ClassId=cs.ClassId and F_Sxm2.subid=cs.subid and F_Sxm2.courseid=cs.courseid ),"+

        //                   "S_ms as(select * from Class_" + tableName + "MarksSheet where Marks is not null and BatchId='" + batchId + "' and ShiftId='" + shiftId + "'  and ExInId in(" + getDependencyExInId(ExInId[1].Replace("'", string.Empty), "False") + ")), S_Qxm as(select et.semesterexam,S_ms.BatchId, StudentId,RollNo,ShiftId,S_ms.SubId,S_ms.CourseId,qp.qpName, sqp.QpId,(sum(S_ms.marks)/count(S_ms.ExInId)) marks,sqp.ConvertTo,((sum(S_ms.marks)/count(S_ms.ExInId))/100)* sqp.ConvertTo as ConvertMarks from S_ms inner join ExamType et on S_ms.exid=et.exid inner join SubjectQuestionPattern sqp on S_ms.subqpid=sqp.subqpid inner join QuestionPattern qp on sqp.QPid=qp.QpId where et.semesterexam is null group by  et.semesterexam,S_ms.BatchId,StudentId,RollNo,ShiftId,S_ms.SubId,S_ms.CourseId,qp.qpName,sqp.QpId,sqp.ConvertTo) , S_Qxm1 as(SELECT  semesterexam, BatchId, StudentId,RollNo,ShiftId,SubId,CourseId,ConvertTo,sum(ConvertMarks) as ConvertMarks, MCQ,   CQ FROM   S_Qxm PIVOT(SUM(Marks)FOR [qpName] IN ( MCQ,CQ)) AS P group by semesterexam, BatchId, StudentId,RollNo,ShiftId,SubId,CourseId,ConvertTo,p.mcq,p.cq ), S_Qxm2 as (select BatchId,StudentId,RollNo,ShiftId,SubId, CourseId,sum(ConvertTo) ConvertTo, sum(isnull(mcq,0)) MCQ,sum(isnull(cq,0)) CQ , sum(ConvertMarks) [10 % of CQ]from S_Qxm1 group by semesterexam,BatchId,StudentId,RollNo,ShiftId,SubId, CourseId)," +
        //                   "S_Sxm as(select et.semesterexam,S_ms.BatchId, S_ms.StudentId,S_ms.RollNo,S_ms.ShiftId,S_ms.SubId,S_ms.CourseId,qp.qpName, sqp.QpId,(sum(S_ms.marks)/count(S_ms.ExInId)) marks,case when S_Qxm.ConvertTo>0 then  (100-S_Qxm.ConvertTo) else 0 end as ConvertTo,((sum(S_ms.marks)/count(S_ms.ExInId))/100)*(case when S_Qxm.ConvertTo>0 then  (100-S_Qxm.ConvertTo) else 0 end) as ConvertMarks from S_ms inner join ExamType et on S_ms.exid=et.exid inner join SubjectQuestionPattern sqp on S_ms.subqpid=sqp.subqpid inner join QuestionPattern qp on sqp.QPid=qp.QpId left JOIN S_Qxm on S_ms.subid=S_Qxm.subid and S_ms.courseid=S_Qxm.courseid and sqp.qpid=S_Qxm.qpid where et.semesterexam=1 group by  et.semesterexam,S_ms.BatchId,S_ms.StudentId,S_ms.RollNo,S_ms.ShiftId,S_ms.SubId,S_ms.CourseId,qp.qpName,sqp.QpId,S_Qxm.ConvertTo ) , S_Sxm1 as(SELECT  semesterexam, BatchId, StudentId,RollNo,ShiftId,SubId,CourseId,ConvertTo,sum(ConvertMarks) as ConvertMarks, MCQ,   CQ,Practical FROM   S_Sxm PIVOT ( SUM(Marks) FOR [qpName] IN ( MCQ,CQ,Practical)) AS P group by semesterexam, BatchId, StudentId,RollNo,ShiftId,SubId,CourseId,ConvertTo,p.mcq,p.cq,p.Practical ),S_Sxm2 as (select BatchId,StudentId,RollNo,ShiftId,SubId, CourseId, sum(isnull(mcq,0)) MCQ,sum(isnull(cq,0)) CQ ,sum(isnull(Practical,0)) Practical, sum(ConvertMarks) [90 % of CQ]from S_Sxm1 group by semesterexam,BatchId,StudentId,RollNo,ShiftId,SubId, CourseId),"+
        //                   "S_tbl as(select S_Sxm2.batchid,S_Sxm2.studentid,S_Sxm2.subid,S_Sxm2.courseid,round((S_Qxm2.[10 % of CQ]+S_Sxm2.[90 % of CQ]) ,0) *.3 TotalCQ, S_Sxm2.mcq *.3 mcq,S_Sxm2.Practical*.3 Practical from S_Sxm2 inner join S_Qxm2 on S_Sxm2.StudentId=S_Qxm2.StudentId and  S_Sxm2.subid=S_Qxm2.subid and S_Sxm2.CourseId=S_Qxm2.CourseId inner join NewSubject ns on S_Sxm2.subid=ns.subid inner join AddCourseWithSubject acs on S_Sxm2.subid=acs.subid and S_Sxm2.courseid=acs.courseid inner join BatchInfo on S_Sxm2.BatchId=BatchInfo.BatchId inner join ClassSubject cs on BatchInfo.ClassId=cs.ClassId and S_Sxm2.subid=cs.subid and S_Sxm2.courseid=cs.courseid ),"+

        //                   "T_ms as(select * from Class_" + tableName + "MarksSheet where Marks is not null and BatchId='" + batchId + "' and ShiftId='" + shiftId + "'  and ExInId in(" + getDependencyExInId(ExInId[0].Replace("'", string.Empty), "False") + ")), T_Qxm as(select et.semesterexam,T_ms.BatchId, StudentId,RollNo,ShiftId,T_ms.SubId,T_ms.CourseId,qp.qpName, sqp.QpId,(sum(T_ms.marks)/count(T_ms.ExInId)) marks,sqp.ConvertTo,((sum(T_ms.marks)/count(T_ms.ExInId))/100)* sqp.ConvertTo as ConvertMarks from T_ms inner join ExamType et on T_ms.exid=et.exid inner join SubjectQuestionPattern sqp on T_ms.subqpid=sqp.subqpid inner join QuestionPattern qp on sqp.QPid=qp.QpId where et.semesterexam is null group by  et.semesterexam,T_ms.BatchId,StudentId,RollNo,ShiftId,T_ms.SubId,T_ms.CourseId,qp.qpName,sqp.QpId,sqp.ConvertTo) , T_Qxm1 as(SELECT  semesterexam, BatchId, StudentId,RollNo,ShiftId,SubId,CourseId,ConvertTo,sum(ConvertMarks) as ConvertMarks, MCQ,   CQ FROM   T_Qxm PIVOT(SUM(Marks)FOR [qpName] IN ( MCQ,CQ)) AS P group by semesterexam, BatchId, StudentId,RollNo,ShiftId,SubId,CourseId,ConvertTo,p.mcq,p.cq ), T_Qxm2 as (select BatchId,StudentId,RollNo,ShiftId,SubId, CourseId,sum(ConvertTo) ConvertTo, sum(isnull(mcq,0)) MCQ,sum(isnull(cq,0)) CQ , sum(ConvertMarks) [10 % of CQ]from T_Qxm1 group by semesterexam,BatchId,StudentId,RollNo,ShiftId,SubId, CourseId)," +
        //                   "T_Sxm as(select et.semesterexam,T_ms.BatchId, T_ms.StudentId,T_ms.RollNo,T_ms.ShiftId,T_ms.SubId,T_ms.CourseId,qp.qpName, sqp.QpId,(sum(T_ms.marks)/count(T_ms.ExInId)) marks,case when T_Qxm.ConvertTo>0 then  (100-T_Qxm.ConvertTo) else 0 end as ConvertTo,((sum(T_ms.marks)/count(T_ms.ExInId))/100)*(case when T_Qxm.ConvertTo>0 then  (100-T_Qxm.ConvertTo) else 0 end) as ConvertMarks from T_ms inner join ExamType et on T_ms.exid=et.exid inner join SubjectQuestionPattern sqp on T_ms.subqpid=sqp.subqpid inner join QuestionPattern qp on sqp.QPid=qp.QpId left JOIN T_Qxm on T_ms.subid=T_Qxm.subid and T_ms.courseid=T_Qxm.courseid and sqp.qpid=T_Qxm.qpid where et.semesterexam=1 group by  et.semesterexam,T_ms.BatchId,T_ms.StudentId,T_ms.RollNo,T_ms.ShiftId,T_ms.SubId,T_ms.CourseId,qp.qpName,sqp.QpId,T_Qxm.ConvertTo ) , T_Sxm1 as(SELECT  semesterexam, BatchId, StudentId,RollNo,ShiftId,SubId,CourseId,ConvertTo,sum(ConvertMarks) as ConvertMarks, MCQ,   CQ,Practical FROM   T_Sxm PIVOT ( SUM(Marks) FOR [qpName] IN ( MCQ,CQ,Practical)) AS P group by semesterexam, BatchId, StudentId,RollNo,ShiftId,SubId,CourseId,ConvertTo,p.mcq,p.cq,p.Practical ),T_Sxm2 as (select BatchId,StudentId,RollNo,ShiftId,SubId, CourseId, sum(isnull(mcq,0)) MCQ,sum(isnull(cq,0)) CQ ,sum(isnull(Practical,0)) Practical, sum(ConvertMarks) [90 % of CQ]from T_Sxm1 group by semesterexam,BatchId,StudentId,RollNo,ShiftId,SubId, CourseId),"+
        //                   "T_tbl as(select T_Sxm2.batchid,T_Sxm2.studentid,T_Sxm2.subid,T_Sxm2.courseid,round((T_Qxm2.[10 % of CQ]+T_Sxm2.[90 % of CQ]) ,0) *.4 TotalCQ, T_Sxm2.mcq *.4 mcq,T_Sxm2.Practical*.4 Practical from T_Sxm2 inner join T_Qxm2 on T_Sxm2.StudentId=T_Qxm2.StudentId and  T_Sxm2.subid=T_Qxm2.subid and T_Sxm2.CourseId=T_Qxm2.CourseId inner join NewSubject ns on T_Sxm2.subid=ns.subid inner join AddCourseWithSubject acs on T_Sxm2.subid=acs.subid and T_Sxm2.courseid=acs.courseid inner join BatchInfo on T_Sxm2.BatchId=BatchInfo.BatchId inner join ClassSubject cs on BatchInfo.ClassId=cs.ClassId and T_Sxm2.subid=cs.subid and T_Sxm2.courseid=cs.courseid ), "+

        //                   "FST as(select T_tbl.BatchId,T_tbl.StudentId,T_tbl.SubId,T_tbl.Courseid,F_tbl.TotalCQ as CQ1,F_tbl.mcq as MCQ1,F_tbl.Practical as Prac1,S_tbl.TotalCQ as CQ2,S_tbl.mcq as MCQ2,S_tbl.Practical as Prac2,T_tbl.TotalCQ as CQ3,T_tbl.mcq as MCQ3,T_tbl.Practical as Prac3 , (F_tbl.TotalCQ+S_tbl.TotalCQ+T_tbl.TotalCQ) CQ, (F_tbl.mcq+S_tbl.mcq+T_tbl.mcq) MCQ,(F_tbl.Practical+S_tbl.Practical+T_tbl.Practical) Practical,round((F_tbl.TotalCQ+S_tbl.TotalCQ+T_tbl.TotalCQ)+ (F_tbl.mcq+S_tbl.mcq+T_tbl.mcq)+(F_tbl.Practical+S_tbl.Practical+T_tbl.Practical),0) TotalOM from T_tbl inner join S_tbl on T_tbl.studentId=S_tbl.studentid and T_tbl.subId=S_tbl.subId and T_tbl.courseid=S_tbl.courseid inner join F_tbl on T_tbl.studentId=F_tbl.studentid and T_tbl.subId=F_tbl.subId and T_tbl.courseid=F_tbl.courseid)," +

        //                   "ar as (Select sum(case  when AttStatus='p' or AttStatus= 'l'   then 1 else 0 end) as Present,sum(case  when AttStatus='a'  then 1 else 0 end) as Absent,sum(case  when AttStatus='p' or AttStatus= 'l'   then 1 else 0 end)+sum(case  when AttStatus='a'  then 1 else 0 end) as Total,StudentId  from DailyAttendanceRecord where StudentId in(select distinct StudentId from FST ) "+
        //                   "and AttDate<'" + date + "' and BatchId='" + batchId + "' group by StudentId),"+
        //                   "psn as(select FST.StudentId,csi.ShiftId,csi.ClsSecId,csi.ClsGrpId, sum(FST.TotalOM) TotalMark,ROW_NUMBER() OVER (order by  sum(FST.TotalOM) desc) as PositionInGrp,ROW_NUMBER() OVER(PARTITION BY csi.ClsSecid order by   sum(FST.TotalOM) desc) as PositionInSec   from  FST inner join v_CurrentStudentInfo csi on FST.batchid=csi.batchid and FST.StudentId=csi.StudentId group by FST.StudentId,csi.ShiftId ,csi.ClsSecId,csi.ClsGrpId) " +

        //                   "select '0' as IsIndependent, ClassName,FST.batchid,FST.studentid,FST.subid,FST.courseid,ns.SubName,acs.CourseName,cs.SubCode,cs.Marks,CQ1,MCQ1,Prac1,CQ2,MCQ2,Prac2,CQ3,MCQ3,Prac3,CQ,MCQ,Practical,TotalOM,csi.FullName,csi.FathersName,csi.MothersName,csi.Session,convert(varchar(10), csi.DateOfBirth,105) as DateOfBirth,csi.ShiftName ,csi.GroupName,csi.SectionName,csi.RollNo ,ar.Absent,ar.Present,ar.Total,psn.PositionInSec,psn.PositionInGrp, case when gs.MSStatus is null then 1 else gs.MSStatus end as MSStatus from FST  inner join NewSubject ns on FST.subid=ns.subid inner join AddCourseWithSubject acs on FST.subid=acs.subid and FST.courseid=acs.courseid inner join BatchInfo on FST.BatchId=BatchInfo.BatchId inner join ClassSubject cs on BatchInfo.ClassId=cs.ClassId and FST.subid=cs.subid and FST.courseid=cs.courseid inner join v_CurrentStudentInfo csi on FST.batchid=csi.batchid and FST.StudentId=csi.StudentId left join ar on FST.StudentId=ar.StudentId inner join psn on FST.StudentId=psn.StudentId left join v_StudentGroupSubSetupDetails gs on FST.Batchid=gs.Batchid and FST.StudentId=gs.StudentId and FST.Subid=gs.Subid " +
        //                   "where csi.ClsSecId=" + sectionId + "";
        //           }
        //           dt = CRUD.ReturnTableNull(sql);

        //           return dt;
        //       }
        //       catch { return dt; }
        //   }
        public DataTable LoadSemesterProgressReport(string tableName, string shiftId,
     string batchId, string groupId, string sectionId, string examinId,string ExamID, string RollNo, bool isFinal)
        {
            try
            {
                RollNo = (RollNo == "") ? "" : " and RollNo=" + RollNo;
                sectionId = (sectionId == "0") ? "" : " and ClsSecID="+sectionId;
                string[] examsplit = examinId.Split('_');
                string[] dateyear = examsplit[1].Split('-');
                string date = dateyear[2] + "-" + dateyear[0] + "-" + dateyear[1];
                
                    
                if (!isFinal)
                {
                    //    string examList = getDependencyExInId(examinId, "False");
                    string examList = "'" + examinId + "'";
                    if (examList.Contains(','))
                    {
                        sql = @"with ms as(select * from Class_" + tableName + "MarksSheet where Marks is not null and isPassed is not null and BatchId='" + batchId + "' and ShiftId='" + shiftId + "'  and ExInId in(" + examList + ")  " + RollNo + "), " +
                       "Qxm as(select et.semesterexam,ms.BatchId, StudentId,RollNo,ShiftId,ms.SubId,ms.CourseId,qp.qpName, sqp.QpId,round((sum(ms.marks)/count(ms.ExInId)),0) marks,sqp.ConvertTo,case when qp.QPName='CQ' then( ((round( (sum(ms.marks)/count(ms.ExInId)),0))/100)* sqp.ConvertTo) else 0 end as ConvertMarks,sqp.QMarks from ms inner join ExamType et on ms.exid=et.exid inner join SubjectQuestionPattern sqp on ms.subqpid=sqp.subqpid inner join QuestionPattern qp on sqp.QPid=qp.QpId where et.semesterexam is null group by  et.semesterexam,ms.BatchId,StudentId,RollNo,ShiftId,ms.SubId,ms.CourseId,qp.qpName,sqp.QpId,sqp.ConvertTo,sqp.QMarks)" +
                       ",QxmFM as(SELECT  semesterexam, BatchId, StudentId,RollNo,ShiftId,SubId,CourseId,ConvertTo,sum(ConvertMarks) as ConvertMarks, MCQ,   CQ FROM   Qxm PIVOT(SUM(QMarks)FOR [qpName] IN ( MCQ,CQ)) AS P group by semesterexam, BatchId, StudentId,RollNo,ShiftId,SubId,CourseId,ConvertTo,p.mcq,p.cq )," +
                       "Qxm1 as(SELECT  semesterexam, BatchId, StudentId,RollNo,ShiftId,SubId,CourseId,ConvertTo,sum(ConvertMarks) as ConvertMarks, MCQ,   CQ FROM   Qxm PIVOT(SUM(Marks)FOR [qpName] IN ( MCQ,CQ)) AS P group by semesterexam, BatchId, StudentId,RollNo,ShiftId,SubId,CourseId,ConvertTo,p.mcq,p.cq )," +
                       "QxmFM_T as(select BatchId,StudentId,RollNo,ShiftId,SubId, CourseId,sum(ConvertTo) ConvertTo, sum(isnull(mcq,0)) MCQ,sum(isnull(cq,0)) CQF , sum(ConvertMarks) [10 % of CQ]from QxmFM group by semesterexam,BatchId,StudentId,RollNo,ShiftId,SubId, CourseId)," +
                       "Qxm1_T as(select BatchId,StudentId,RollNo,ShiftId,SubId, CourseId,sum(ConvertTo) ConvertTo, sum(isnull(mcq,0)) MCQ,sum(isnull(cq,0)) CQ , sum(ConvertMarks) [10 % of CQ]from Qxm1 group by semesterexam,BatchId,StudentId,RollNo,ShiftId,SubId, CourseId) ,  " +
                       "Qxm2 as (select Qxm1_T.BatchId,Qxm1_T.StudentId,Qxm1_T.RollNo,Qxm1_T.ShiftId,Qxm1_T.SubId, Qxm1_T.CourseId, Qxm1_T.ConvertTo,  Qxm1_T.MCQ,Qxm1_T. CQ , CQF ,Qxm1_T.[10 % of CQ]from Qxm1_T inner join QxmFM_T on Qxm1_T.StudentId=QxmFM_T.StudentId and Qxm1_T.SubId=QxmFM_T.SubId and Qxm1_T.CourseId=QxmFM_T.CourseId)," +
                       " Sxm as( select et.semesterexam,ms.BatchId, ms.StudentId,ms.RollNo,ms.ShiftId,ms.SubId,ms.CourseId,qp.qpName, sqp.QpId,(sum(ms.marks)/count(ms.ExInId)) marks,case when Qxm.ConvertTo>0 then  (100-Qxm.ConvertTo) else 0 end as ConvertTo,case when qp.qpName='CQ' then((sum(ms.marks)/count(ms.ExInId))/100)*(case when Qxm.ConvertTo>0 then  (100-Qxm.ConvertTo) else 0 end) else 0 end as ConvertMarks ,sqp.QMarks " +
                       "from ms inner join ExamType et on ms.exid=et.exid inner join SubjectQuestionPattern sqp on ms.subqpid=sqp.subqpid inner join QuestionPattern qp on sqp.QPid=qp.QpId left JOIN Qxm on ms.subid=Qxm.subid and ms.courseid=Qxm.courseid and sqp.qpid=Qxm.qpid where et.semesterexam=1 group by  et.semesterexam,ms.BatchId,ms.StudentId,ms.RollNo,ms.ShiftId,ms.SubId,ms.CourseId,qp.qpName,sqp.QpId,Qxm.ConvertTo,sqp.QMarks  ) , " +
                       "SxmFM as(SELECT  semesterexam, BatchId, StudentId,RollNo,ShiftId,SubId,CourseId,ConvertTo,sum(ConvertMarks) as ConvertMarks, MCQ ,   CQ  ,Practical  FROM   Sxm PIVOT ( SUM(QMarks) FOR [qpName] IN ( MCQ,CQ,Practical)) AS P group by semesterexam, BatchId, StudentId,RollNo,ShiftId,SubId,CourseId,ConvertTo,p.mcq,p.cq,p.Practical)," +
                       "Sxm1 as(SELECT  semesterexam, BatchId, StudentId,RollNo,ShiftId,SubId,CourseId,ConvertTo,sum(ConvertMarks) as ConvertMarks, MCQ,   CQ,Practical FROM   Sxm PIVOT ( SUM(Marks) FOR [qpName] IN ( MCQ,CQ,Practical)) AS P group by semesterexam, BatchId, StudentId,RollNo,ShiftId,SubId,CourseId,ConvertTo,p.mcq,p.cq,p.Practical ), " +
                       "SxmFM_T as (select BatchId,StudentId,RollNo,ShiftId,SubId, CourseId, sum(isnull(mcq,0)) MCQ,sum(isnull(cq,0)) CQF ,sum(isnull(Practical,0)) Practical, sum(ConvertMarks) [90 % of CQ]from SxmFM group by semesterexam,BatchId,StudentId,RollNo,ShiftId,SubId, CourseId) , " +
                       "Sxm1_T as (select BatchId,StudentId,RollNo,ShiftId,SubId, CourseId, sum(isnull(mcq,0)) MCQ,sum(isnull(cq,0)) CQ ,sum(isnull(Practical,0)) Practical, sum(ConvertMarks) [90 % of CQ]from Sxm1 group by semesterexam,BatchId,StudentId,RollNo,ShiftId,SubId, CourseId)," +
                       "Sxm2 as (select Sxm1_T.BatchId,Sxm1_T.StudentId,Sxm1_T.RollNo,Sxm1_T.ShiftId,Sxm1_T.SubId, Sxm1_T.CourseId,Sxm1_T.MCQ,Sxm1_T.CQ,Sxm1_T.Practical,Sxm1_T.[90 % of CQ],CQF from Sxm1_T inner join SxmFM_T on Sxm1_T.StudentId=SxmFM_T.StudentId and Sxm1_T.SubId=SxmFM_T.SubId and Sxm1_T.CourseId=SxmFM_T.CourseId), ar as (Select sum(case  when AttStatus='p' or AttStatus= 'l'   then 1 else 0 end) as Present,sum(case  when AttStatus='a'  then 1 else 0 end) as Absent,sum(case  when AttStatus='p' or AttStatus= 'l'   then 1 else 0 end)+sum(case  when AttStatus='a'  then 1 else 0 end) as Total,StudentId  from DailyAttendanceRecord where StudentId in(select distinct StudentId from Sxm2 ) and AttDate<'2018-05-01' and BatchId='3017' group by StudentId), MaxN as(select Sxm2.subid,Sxm2.courseid,max( round((Qxm2.[10 % of CQ]+Sxm2.[90 % of CQ]) ,0)) MaxCQ,max( Sxm2.mcq) MaxMCQ from  Sxm2 inner join Qxm2 on Sxm2.StudentId=Qxm2.StudentId and  Sxm2.subid=Qxm2.subid and Sxm2.CourseId=Qxm2.CourseId group by Sxm2.subid,Sxm2.courseid) , psn as(select Sxm2.StudentId,Sxm2.ShiftId,csi.ClsSecId,csi.ClsGrpId, sum( (round((Qxm2.[10 % of CQ]+Sxm2.[90 % of CQ]) ,0)+Sxm2.mcq+Sxm2.Practical)) TotalMark,ROW_NUMBER() OVER (order by  sum( (round((Qxm2.[10 % of CQ]+Sxm2.[90 % of CQ]) ,0)+Sxm2.mcq+Sxm2.Practical)) desc) as PositionInGrp,ROW_NUMBER() OVER(PARTITION BY csi.ClsSecid order by   sum( (round((Qxm2.[10 % of CQ]+Sxm2.[90 % of CQ]) ,0)+Sxm2.mcq+Sxm2.Practical)) desc) as PositionInSec   from  Sxm2 inner join Qxm2 on Sxm2.StudentId=Qxm2.StudentId and  Sxm2.subid=Qxm2.subid and Sxm2.CourseId=Qxm2.CourseId inner join v_CurrentStudentInfo csi on Sxm2.batchid=csi.batchid and Sxm2.StudentId=csi.StudentId group by Sxm2.StudentId,Sxm2.ShiftId ,csi.ClsSecId,csi.ClsGrpId) " +
                       " select '0' as IsIndependent,  ClassName,Sxm2.batchid,Sxm2.studentid,Sxm2.rollno,Sxm2.subid,Sxm2.courseid,Qxm2.mcq Qmcq,Qxm2.cq Qcq,Qxm2.[10 % of CQ] as [10 % of CQ_1],((Sxm2.CQF* Qxm2.[10 % of CQ])/Qxm2.CQF) [10 % of CQ] ,Sxm2.cq,Sxm2.[90 % of CQ],round((((Sxm2.CQF* Qxm2.[10 % of CQ])/Qxm2.CQF)+Sxm2.[90 % of CQ]) ,0)TotalCQ, Sxm2.mcq,Sxm2.Practical,(round((((Sxm2.CQF* Qxm2.[10 % of CQ])/Qxm2.CQF)+Sxm2.[90 % of CQ]) ,0)+Sxm2.mcq+Sxm2.Practical) TotalMark,MaxN.MaxCQ,MaxN.MaxMCQ,ns.SubName,acs.CourseName,cs.SubCode,cs.Marks,csi.FullName,csi.FathersName,csi.MothersName,csi.Session,convert(varchar(10), csi.DateOfBirth,105) as DateOfBirth,csi.ShiftName ,csi.GroupName,csi.SectionName,csi.RollNo ,ar.Absent,ar.Present,ar.Total,psn.PositionInSec,psn.PositionInGrp, case when gs.MSStatus is null then 1 else gs.MSStatus end as MSStatus from Sxm2 inner join Qxm2 on Sxm2.StudentId=Qxm2.StudentId and  Sxm2.subid=Qxm2.subid and Sxm2.CourseId=Qxm2.CourseId inner join NewSubject ns on Sxm2.subid=ns.subid inner join AddCourseWithSubject acs on Sxm2.subid=acs.subid and Sxm2.courseid=acs.courseid inner join BatchInfo on sxm2.BatchId=BatchInfo.BatchId inner join ClassSubject cs on BatchInfo.ClassId=cs.ClassId and Sxm2.subid=cs.subid and Sxm2.courseid=cs.courseid inner join v_CurrentStudentInfo csi on Sxm2.batchid=csi.batchid and Sxm2.StudentId=csi.StudentId left join ar on Sxm2.StudentId=ar.StudentId inner join MaxN on Sxm2.subid=MaxN.subid and Sxm2.Courseid=MaxN.Courseid inner join psn on Sxm2.StudentId=psn.StudentId left join v_StudentGroupSubSetupDetails gs on Sxm2.Batchid=gs.Batchid and Sxm2.StudentId=gs.StudentId and Sxm2.Subid=gs.Subid " +
                       " where csi.ClsSecId=" + sectionId + "";
                    }
                    else
                    {


//                        sql = @"with ss as  (
//                            select 0 as StudentId,SubId,1 as MSStatus,Ordering from ClassSubject  where ClassID in(select ClassID from BatchInfo where BatchId="+ batchId + @") and IsCommon=1 and IsOptional=0 union all
//   SELECT StudentId, SubId,MSStatus,Ordering FROM v_StudentGroupSubSetup WHERE   BatchId="+batchId+ @"),
//ms as(select m.* from  Class_ElevenMarksSheet m inner join ss on (m.StudentId=ss.StudentId or ss.StudentId=0) and ss.SubId=m.SubId   and ExInId='"+examinId+"' "+RollNo+"), Mxm as(select et.semesterexam,ms.BatchId, ms.StudentId,ms.RollNo,ms.ShiftId,ms.SubId,ms.CourseId,qp.qpName, sqp.QpId,ms.marks ,sqp.QMarks, case when ms.IsPassed=1 then 0 else 1 end As IsFailed from ms inner join ExamType et on ms.exid=et.exid inner join SubjectQuestionPattern sqp on ms.subqpid=sqp.subqpid inner join QuestionPattern qp on sqp.QPid=qp.QpId  ), MxmFM as (SELECT semesterexam, BatchId, StudentId, RollNo, ShiftId, SubId, CourseId, IsFailed, MCQ, CQ, Practical FROM   Mxm PIVOT(SUM(QMarks) FOR [qpName] IN (MCQ, CQ, Practical)) AS P group by semesterexam, BatchId, StudentId,RollNo,ShiftId,SubId,CourseId,IsFailed,p.mcq,p.cq,p.Practical),MxmFM_T as (select BatchId, StudentId, RollNo, ShiftId, SubId, CourseId, sum(IsFailed) as IsFailed, sum(isnull(mcq, 0)) MCQ,sum(isnull(cq, 0)) CQF ,sum(isnull(Practical, 0)) Practical from MxmFM group by semesterexam, BatchId, StudentId, RollNo, ShiftId, SubId, CourseId),Mxm1 as (SELECT semesterexam, BatchId, StudentId, RollNo, ShiftId, SubId, CourseId, IsFailed, MCQ, CQ, Practical FROM   Mxm PIVOT(SUM(Marks) FOR [qpName] IN (MCQ, CQ, Practical)) AS P group by semesterexam, BatchId,StudentId,RollNo,ShiftId,SubId,CourseId,IsFailed,p.mcq,p.cq,p.Practical),Mxm1_T as (select BatchId, StudentId, RollNo, ShiftId, SubId, CourseId, sum(IsFailed) as IsFailed,case when CQ is null and  MCQ is null and Practical is null then 'AB' else '' end as AB, sum(isnull(mcq, 0)) MCQ,sum(isnull(cq, 0)) CQ ,sum(isnull(Practical, 0)) Practical from Mxm1 group by semesterexam, BatchId, StudentId, RollNo, ShiftId, SubId, CourseId,case when CQ is null and  MCQ is null and Practical is null then 'AB' else '' end),MaxN as (select subid, courseid, max(Cq)MaxCQ,max(mcq)MaxMCQ from  Mxm1_T group by subid, courseid), psn as (select Mxm1_T.StudentId,Mxm1_T.ShiftId,csi.ClsSecId,csi.ClsGrpId, sum(round((Mxm1_T.cq + Mxm1_T.mcq + Mxm1_T.Practical), 0)) TotalMark,ROW_NUMBER() OVER(order by  sum(round((Mxm1_T.cq + Mxm1_T.mcq + Mxm1_T.Practical), 0)) desc) as PositionInGrp,ROW_NUMBER() OVER(PARTITION BY csi.ClsSecid order by   sum(round((Mxm1_T.cq + Mxm1_T.mcq + Mxm1_T.Practical), 0)) desc) as PositionInSec   from Mxm1_T inner join v_CurrentStudentInfo csi on Mxm1_T.batchid = csi.batchid and Mxm1_T.StudentId = csi.StudentId group by Mxm1_T.StudentId,Mxm1_T.ShiftId ,csi.ClsSecId,csi.ClsGrpId) , cs as (select sqp.SubId,sqp.CourseId,sqp.Marks,cs.SubCode from(select SubId, CourseId, max(SubQPMarks) as Marks from SubjectQuestionPattern where BatchId = '" + batchId + "' and ClsGrpID = " + groupId + " group by SubId, CourseId) as sqp left join(select* from ClassSubject where ClassID in (select ClassID from BatchInfo where BatchId= '" + batchId + "') )as cs on sqp.SubId = cs.SubId and sqp.CourseId = cs.CourseId) select '1' as IsIndependent, ClassName,Sxm2.batchid,Sxm2.studentid,Sxm2.rollno,Sxm2.subid,Sxm2.courseid,Sxm2.cq TotalCQ, Sxm2.mcq,Sxm2.Practical,(Sxm2.cq + Sxm2.mcq + Sxm2.Practical) TotalMark,Sxm2.IsFailed,MaxN.MaxCQ,MaxN.MaxMCQ,ns.SubName + ' ' + isnull(acs.CourseName, '') as SubName,acs.CourseName,cs.SubCode,cs.Marks,csi.FullName,csi.FathersName,csi.MothersName,csi.Session,convert(varchar(10), csi.DateOfBirth, 105) as DateOfBirth,csi.ShiftName ,csi.GroupName,csi.SectionName,csi.RollNo,psn.PositionInSec,psn.PositionInGrp, case when gs.MSStatus is null then 1 else gs.MSStatus end as MSStatus,ers.IsPassed,ers.GPA,ers.Grade,ers.withoutOpGPA,ers.withoutOpGrade,AB from Mxm1_T Sxm2  inner join NewSubject ns on Sxm2.subid = ns.subid left join AddCourseWithSubject acs on Sxm2.subid = acs.subid and Sxm2.courseid = acs.courseid inner join BatchInfo on sxm2.BatchId = BatchInfo.BatchId inner join  cs on  Sxm2.subid = cs.subid and Sxm2.courseid = cs.courseid inner join v_CurrentStudentInfo csi on Sxm2.batchid = csi.batchid and Sxm2.StudentId = csi.StudentId  inner join MaxN on Sxm2.subid = MaxN.subid and Sxm2.Courseid = MaxN.Courseid inner join psn on Sxm2.StudentId = psn.StudentId left join v_StudentGroupSubSetupDetails gs on Sxm2.Batchid = gs.Batchid and Sxm2.StudentId = gs.StudentId and Sxm2.Subid = gs.Subid left join Exam_ResultSheet ers on Sxm2.StudentId = ers.StudentID and ers.ExInId = " + examList + " where csi.ClsSecId = " + sectionId;

sql = @"with 
ms as(select * from  Class_" + tableName + "MarksSheet where ExamID=" + ExamID + sectionId + RollNo + "), Mxm as(select et.semesterexam,ms.BatchId, ms.StudentId,ms.RollNo,ms.ShiftId,ms.SubId,ms.CourseId,qp.qpName, sqp.QpId,ms.marks ,sqp.QMarks, case when ms.IsPassed=1 then 0 else 1 end As IsFailed from ms inner join ExamType et on ms.exid=et.exid inner join SubjectQuestionPattern sqp on ms.subqpid=sqp.subqpid inner join QuestionPattern qp on sqp.QPid=qp.QpId  ), MxmFM as (SELECT semesterexam, BatchId, StudentId, RollNo, ShiftId, SubId, CourseId, IsFailed, MCQ, CQ, Practical FROM   Mxm PIVOT(SUM(QMarks) FOR [qpName] IN (MCQ, CQ, Practical)) AS P group by semesterexam, BatchId, StudentId,RollNo,ShiftId,SubId,CourseId,IsFailed,p.mcq,p.cq,p.Practical),MxmFM_T as (select BatchId, StudentId, RollNo, ShiftId, SubId, CourseId, sum(IsFailed) as IsFailed, sum(isnull(mcq, 0)) MCQ,sum(isnull(cq, 0)) CQF ,sum(isnull(Practical, 0)) Practical from MxmFM group by semesterexam, BatchId, StudentId, RollNo, ShiftId, SubId, CourseId),Mxm1 as (SELECT semesterexam, BatchId, StudentId, RollNo, ShiftId, SubId, CourseId, IsFailed, MCQ, CQ, Practical FROM   Mxm PIVOT(SUM(Marks) FOR [qpName] IN (MCQ, CQ, Practical)) AS P group by semesterexam, BatchId,StudentId,RollNo,ShiftId,SubId,CourseId,IsFailed,p.mcq,p.cq,p.Practical),Mxm1_T as (select BatchId, StudentId, RollNo, ShiftId, SubId, CourseId, sum(IsFailed) as IsFailed,case when CQ is null and  MCQ is null and Practical is null then 'AB' else '' end as AB, sum(isnull(mcq, 0)) MCQ,sum(isnull(cq, 0)) CQ ,sum(isnull(Practical, 0)) Practical from Mxm1 group by semesterexam, BatchId, StudentId, RollNo, ShiftId, SubId, CourseId,case when CQ is null and  MCQ is null and Practical is null then 'AB' else '' end),MaxN as (select subid, courseid, max(Cq)MaxCQ,max(mcq)MaxMCQ from  Mxm1_T group by subid, courseid),  cs as (select sqp.SubId,sqp.CourseId,sqp.Marks,cs.SubCode from(select SubId, CourseId, max(SubQPMarks) as Marks from SubjectQuestionPattern where BatchId = '" + batchId + "' and ClsGrpID = " + groupId + " group by SubId, CourseId) as sqp left join(select* from ClassSubject where ClassID in (select ClassID from BatchInfo where BatchId= '" + batchId + "') )as cs on sqp.SubId = cs.SubId and sqp.CourseId = cs.CourseId) select '1' as IsIndependent, ClassName,Sxm2.batchid,Sxm2.studentid,Sxm2.rollno,Sxm2.subid,Sxm2.courseid,Sxm2.cq TotalCQ, Sxm2.mcq,Sxm2.Practical,(Sxm2.cq + Sxm2.mcq + Sxm2.Practical) TotalMark,Sxm2.IsFailed,MaxN.MaxCQ,MaxN.MaxMCQ,ns.SubName + ' ' + isnull(acs.CourseName, '') as SubName,acs.CourseName,cs.SubCode,cs.Marks,csi.FullName,csi.FathersName,csi.MothersName,csi.Session,convert(varchar(10), csi.DateOfBirth, 105) as DateOfBirth,csi.ShiftName ,csi.GroupName,csi.SectionName,csi.RollNo, case when gs.MSStatus is null then 1 else gs.MSStatus end as MSStatus,ers.IsPassed,ers.GPA,ers.Grade,ers.withoutOpGPA,ers.withoutOpGrade,AB,ei.ExName,ei.ExName,mr.GrpRank as PositionInGrp,mr.SecRank as PositionInSec from Mxm1_T Sxm2  inner join NewSubject ns on Sxm2.subid = ns.subid left join AddCourseWithSubject acs on Sxm2.subid = acs.subid and Sxm2.courseid = acs.courseid inner join BatchInfo on sxm2.BatchId = BatchInfo.BatchId inner join  cs on  Sxm2.subid = cs.subid and Sxm2.courseid = cs.courseid inner join v_CurrentStudentInfo csi on Sxm2.batchid = csi.batchid and Sxm2.StudentId = csi.StudentId  inner join MaxN on Sxm2.subid = MaxN.subid and Sxm2.Courseid = MaxN.Courseid left join v_StudentGroupSubSetupDetails gs on Sxm2.Batchid = gs.Batchid and Sxm2.StudentId = gs.StudentId and Sxm2.Subid = gs.Subid inner join Exam_ResultSheet ers on Sxm2.StudentId = ers.StudentID and ers.ExamID=" + ExamID + " inner join ExamInfo ei on ei.ExInSl=" + ExamID + " inner join Exam_ResultMeritList mr on ers.SL=mr.ResultID"  ;
                    }


                    //sql = "with ms as(select * from Class_" + tableName + "MarksSheet where Marks is not null and BatchId='" + batchId + "' and ShiftId='" + shiftId + "'  and ExInId in(" + getDependencyExInId(examinId, "False") + "))," +
                    //    " Qxm as(" +
                    //    "select et.semesterexam,ms.BatchId, StudentId,RollNo,ShiftId,ms.SubId,ms.CourseId,qp.qpName, sqp.QpId,(sum(ms.marks)/count(ms.ExInId)) marks,sqp.ConvertTo,((sum(ms.marks)/count(ms.ExInId))/100)* sqp.ConvertTo as ConvertMarks from ms inner join ExamType et on ms.exid=et.exid inner join SubjectQuestionPattern sqp on ms.subqpid=sqp.subqpid inner join QuestionPattern qp on sqp.QPid=qp.QpId where et.semesterexam is null " +
                    //    "group by  et.semesterexam,ms.BatchId,StudentId,RollNo,ShiftId,ms.SubId,ms.CourseId,qp.qpName,sqp.QpId,sqp.ConvertTo) ," +
                    //    " Qxm1 as(SELECT  semesterexam, BatchId, StudentId,RollNo,ShiftId,SubId,CourseId,ConvertTo,sum(ConvertMarks) as ConvertMarks, MCQ,   CQ FROM   Qxm " +
                    //    "PIVOT" +
                    //    "(SUM(Marks)FOR [qpName] IN ( MCQ,CQ)) AS P group by semesterexam, BatchId, StudentId,RollNo,ShiftId,SubId,CourseId,ConvertTo,p.mcq,p.cq )," +
                    //    " Qxm2 as (select BatchId,StudentId,RollNo,ShiftId,SubId, CourseId,sum(ConvertTo) ConvertTo, sum(isnull(mcq,0)) MCQ,sum(isnull(cq,0)) CQ , sum(ConvertMarks) [10 % of CQ]from Qxm1 group by semesterexam,BatchId,StudentId,RollNo,ShiftId,SubId, CourseId)," +
                    //    "Sxm as(select et.semesterexam,ms.BatchId, ms.StudentId,ms.RollNo,ms.ShiftId,ms.SubId,ms.CourseId,qp.qpName, sqp.QpId,(sum(ms.marks)/count(ms.ExInId)) marks,case when Qxm.ConvertTo>0 then  (100-Qxm.ConvertTo) else 0 end as ConvertTo,((sum(ms.marks)/count(ms.ExInId))/100)*(case when Qxm.ConvertTo>0 then  (100-Qxm.ConvertTo) else 0 end) as ConvertMarks from ms inner join ExamType et on ms.exid=et.exid inner join SubjectQuestionPattern sqp on ms.subqpid=sqp.subqpid inner join QuestionPattern qp on sqp.QPid=qp.QpId left JOIN Qxm on ms.subid=Qxm.subid and ms.courseid=Qxm.courseid and sqp.qpid=Qxm.qpid where et.semesterexam=1 " +
                    //    "group by  et.semesterexam,ms.BatchId,ms.StudentId,ms.RollNo,ms.ShiftId,ms.SubId,ms.CourseId,qp.qpName,sqp.QpId,Qxm.ConvertTo ) , " +
                    //    "Sxm1 as(SELECT  semesterexam, BatchId, StudentId,RollNo,ShiftId,SubId,CourseId,ConvertTo,sum(ConvertMarks) as ConvertMarks, MCQ,   CQ,Practical FROM   Sxm " +
                    //    "PIVOT " +
                    //    "( SUM(Marks) FOR [qpName] IN ( MCQ,CQ,Practical)) AS P " +
                    //    "group by semesterexam, BatchId, StudentId,RollNo,ShiftId,SubId,CourseId,ConvertTo,p.mcq,p.cq,p.Practical )," +
                    //    "Sxm2 as (select BatchId,StudentId,RollNo,ShiftId,SubId, CourseId, sum(isnull(mcq,0)) MCQ,sum(isnull(cq,0)) CQ ,sum(isnull(Practical,0)) Practical, sum(ConvertMarks) [90 % of CQ]from Sxm1 group by semesterexam,BatchId,StudentId,RollNo,ShiftId,SubId, CourseId), " +
                    //    "ar as (Select sum(case  when AttStatus='p' or AttStatus= 'l'   then 1 else 0 end) as Present,sum(case  when AttStatus='a'  then 1 else 0 end) as Absent,sum(case  when AttStatus='p' or AttStatus= 'l'   then 1 else 0 end)+sum(case  when AttStatus='a'  then 1 else 0 end) as Total,StudentId  from DailyAttendanceRecord where StudentId in(select distinct StudentId from Sxm2 ) and AttDate<'" + date + "' and BatchId='" + batchId + "' group by StudentId)" +
                    //    ", MaxN as(select Sxm2.subid,Sxm2.courseid,max( round((Qxm2.[10 % of CQ]+Sxm2.[90 % of CQ]) ,0)) MaxCQ,max( Sxm2.mcq) MaxMCQ from  Sxm2 inner join Qxm2 on Sxm2.StudentId=Qxm2.StudentId and  Sxm2.subid=Qxm2.subid and Sxm2.CourseId=Qxm2.CourseId group by Sxm2.subid,Sxm2.courseid) " +
                    //    ", psn as(select Sxm2.StudentId,Sxm2.ShiftId,csi.ClsSecId,csi.ClsGrpId, sum( (round((Qxm2.[10 % of CQ]+Sxm2.[90 % of CQ]) ,0)+Sxm2.mcq+Sxm2.Practical)) TotalMark,ROW_NUMBER() OVER (order by  sum( (round((Qxm2.[10 % of CQ]+Sxm2.[90 % of CQ]) ,0)+Sxm2.mcq+Sxm2.Practical)) desc) as PositionInGrp,ROW_NUMBER() OVER(PARTITION BY csi.ClsSecid order by   sum( (round((Qxm2.[10 % of CQ]+Sxm2.[90 % of CQ]) ,0)+Sxm2.mcq+Sxm2.Practical)) desc) as PositionInSec " +
                    //    "  from  Sxm2 inner join Qxm2 on Sxm2.StudentId=Qxm2.StudentId and  Sxm2.subid=Qxm2.subid and Sxm2.CourseId=Qxm2.CourseId " +
                    //    "inner join v_CurrentStudentInfo csi on Sxm2.batchid=csi.batchid and Sxm2.StudentId=csi.StudentId group by Sxm2.StudentId,Sxm2.ShiftId ,csi.ClsSecId,csi.ClsGrpId) " +
                    //    "select ClassName,Sxm2.batchid,Sxm2.studentid,Sxm2.rollno,Sxm2.subid,Sxm2.courseid,Qxm2.mcq Qmcq,Qxm2.cq Qcq,Qxm2.[10 % of CQ],Sxm2.cq,Sxm2.[90 % of CQ],round((Qxm2.[10 % of CQ]+Sxm2.[90 % of CQ]) ,0)TotalCQ, Sxm2.mcq,Sxm2.Practical,(round((Qxm2.[10 % of CQ]+Sxm2.[90 % of CQ]) ,0)+Sxm2.mcq+Sxm2.Practical) TotalMark,MaxN.MaxCQ,MaxN.MaxMCQ,ns.SubName,acs.CourseName,cs.SubCode,cs.Marks,csi.FullName,csi.ShiftName ,csi.GroupName,csi.SectionName,csi.RollNo ,ar.Absent,ar.Present,ar.Total,psn.PositionInSec,psn.PositionInGrp, case when gs.MSStatus is null then 1 else gs.MSStatus end as MSStatus " +
                    //    "from Sxm2 inner join Qxm2 on Sxm2.StudentId=Qxm2.StudentId and  Sxm2.subid=Qxm2.subid and Sxm2.CourseId=Qxm2.CourseId inner join NewSubject ns on Sxm2.subid=ns.subid " +
                    //    "inner join AddCourseWithSubject acs on Sxm2.subid=acs.subid and Sxm2.courseid=acs.courseid inner join BatchInfo on sxm2.BatchId=BatchInfo.BatchId inner join ClassSubject cs on BatchInfo.ClassId=cs.ClassId and Sxm2.subid=cs.subid and Sxm2.courseid=cs.courseid inner join v_CurrentStudentInfo csi on Sxm2.batchid=csi.batchid and Sxm2.StudentId=csi.StudentId left join ar on Sxm2.StudentId=ar.StudentId inner join MaxN on Sxm2.subid=MaxN.subid and Sxm2.Courseid=MaxN.Courseid inner join psn on Sxm2.StudentId=psn.StudentId left join v_StudentGroupSubSetupDetails gs on Sxm2.Batchid=gs.Batchid and Sxm2.StudentId=gs.StudentId and Sxm2.Subid=gs.Subid " +
                    //    "where csi.ClsSecId=" + sectionId + "";
                }
                else
                {
                    string[] ExInId = getDependencyExInId(examinId, "True").Split(',');

                    sql = "with " +
                        "F_ms as(select * from Class_" + tableName + "MarksSheet where Marks is not null and isPassed is not null and BatchId='" + batchId + "' and ShiftId='" + shiftId + "'  and ExInId in(" + getDependencyExInId(ExInId[2].Replace("'", string.Empty), "False") + ")), F_Qxm as(select et.semesterexam,F_ms.BatchId, StudentId,RollNo,ShiftId,F_ms.SubId,F_ms.CourseId,qp.qpName, sqp.QpId,(sum(F_ms.marks)/count(F_ms.ExInId)) marks,sqp.ConvertTo,((sum(F_ms.marks)/count(F_ms.ExInId))/100)* sqp.ConvertTo as ConvertMarks from F_ms inner join ExamType et on F_ms.exid=et.exid inner join SubjectQuestionPattern sqp on F_ms.subqpid=sqp.subqpid inner join QuestionPattern qp on sqp.QPid=qp.QpId where et.semesterexam is null group by  et.semesterexam,F_ms.BatchId,StudentId,RollNo,ShiftId,F_ms.SubId,F_ms.CourseId,qp.qpName,sqp.QpId,sqp.ConvertTo) , F_Qxm1 as(SELECT  semesterexam,BatchId, StudentId,RollNo,ShiftId,SubId,CourseId,ConvertTo,sum(ConvertMarks) as ConvertMarks, MCQ,   CQ FROM   F_Qxm PIVOT(SUM(Marks)FOR [qpName] IN ( MCQ,CQ)) AS P group by semesterexam, BatchId, StudentId,RollNo,ShiftId,SubId,CourseId,ConvertTo,p.mcq,p.cq ), F_Qxm2 as (select BatchId,StudentId,RollNo,ShiftId,SubId, CourseId,sum(ConvertTo) ConvertTo, sum(isnull(mcq,0)) MCQ,sum(isnull(cq,0)) CQ , sum(ConvertMarks) [10 % of CQ]from F_Qxm1 group by semesterexam,BatchId,StudentId,RollNo,ShiftId,SubId, CourseId)," +
                        "F_Sxm as(select et.semesterexam,F_ms.BatchId, F_ms.StudentId,F_ms.RollNo,F_ms.ShiftId,F_ms.SubId,F_ms.CourseId,qp.qpName, sqp.QpId,(sum(F_ms.marks)/count(F_ms.ExInId)) marks,case when F_Qxm.ConvertTo>0 then  (100-F_Qxm.ConvertTo) else 0 end as ConvertTo,((sum(F_ms.marks)/count(F_ms.ExInId))/100)*(case when F_Qxm.ConvertTo>0 then  (100-F_Qxm.ConvertTo) else 0 end) as ConvertMarks from F_ms inner join ExamType et on F_ms.exid=et.exid inner join SubjectQuestionPattern sqp on F_ms.subqpid=sqp.subqpid inner join QuestionPattern qp on sqp.QPid=qp.QpId left JOIN F_Qxm on F_ms.subid=F_Qxm.subid and F_ms.courseid=F_Qxm.courseid and sqp.qpid=F_Qxm.qpid where et.semesterexam=1 group by  et.semesterexam,F_ms.BatchId,F_ms.StudentId,F_ms.RollNo,F_ms.ShiftId,F_ms.SubId,F_ms.CourseId,qp.qpName,sqp.QpId,F_Qxm.ConvertTo ) , F_Sxm1 as(SELECT  semesterexam, BatchId, StudentId,RollNo,ShiftId,SubId,CourseId,ConvertTo,sum(ConvertMarks) as ConvertMarks, MCQ,   CQ,Practical FROM   F_Sxm PIVOT ( SUM(Marks) FOR [qpName] IN ( MCQ,CQ,Practical)) AS P group by semesterexam, BatchId, StudentId,RollNo,ShiftId,SubId,CourseId,ConvertTo,p.mcq,p.cq,p.Practical ),F_Sxm2 as (select BatchId,StudentId,RollNo,ShiftId,SubId, CourseId, sum(isnull(mcq,0)) MCQ,sum(isnull(cq,0)) CQ ,sum(isnull(Practical,0)) Practical, sum(ConvertMarks) [90 % of CQ]from F_Sxm1 group by semesterexam,BatchId,StudentId,RollNo,ShiftId,SubId, CourseId)," +
                        "F_tbl as(select F_Sxm2.batchid,F_Sxm2.studentid,F_Sxm2.subid,F_Sxm2.courseid,round((F_Qxm2.[10 % of CQ]+F_Sxm2.[90 % of CQ]) ,0) *.3 TotalCQ, F_Sxm2.mcq *.3 mcq,F_Sxm2.Practical*.3 Practical from F_Sxm2 inner join F_Qxm2 on F_Sxm2.StudentId=F_Qxm2.StudentId and  F_Sxm2.subid=F_Qxm2.subid and F_Sxm2.CourseId=F_Qxm2.CourseId inner join NewSubject ns on F_Sxm2.subid=ns.subid inner join AddCourseWithSubject acs on F_Sxm2.subid=acs.subid and F_Sxm2.courseid=acs.courseid inner join BatchInfo on F_Sxm2.BatchId=BatchInfo.BatchId inner join ClassSubject cs on BatchInfo.ClassId=cs.ClassId and F_Sxm2.subid=cs.subid and F_Sxm2.courseid=cs.courseid )," +

                        "S_ms as(select * from Class_" + tableName + "MarksSheet where Marks is not null and BatchId='" + batchId + "' and ShiftId='" + shiftId + "'  and ExInId in(" + getDependencyExInId(ExInId[1].Replace("'", string.Empty), "False") + ")), S_Qxm as(select et.semesterexam,S_ms.BatchId, StudentId,RollNo,ShiftId,S_ms.SubId,S_ms.CourseId,qp.qpName, sqp.QpId,(sum(S_ms.marks)/count(S_ms.ExInId)) marks,sqp.ConvertTo,((sum(S_ms.marks)/count(S_ms.ExInId))/100)* sqp.ConvertTo as ConvertMarks from S_ms inner join ExamType et on S_ms.exid=et.exid inner join SubjectQuestionPattern sqp on S_ms.subqpid=sqp.subqpid inner join QuestionPattern qp on sqp.QPid=qp.QpId where et.semesterexam is null group by  et.semesterexam,S_ms.BatchId,StudentId,RollNo,ShiftId,S_ms.SubId,S_ms.CourseId,qp.qpName,sqp.QpId,sqp.ConvertTo) , S_Qxm1 as(SELECT  semesterexam, BatchId, StudentId,RollNo,ShiftId,SubId,CourseId,ConvertTo,sum(ConvertMarks) as ConvertMarks, MCQ,   CQ FROM   S_Qxm PIVOT(SUM(Marks)FOR [qpName] IN ( MCQ,CQ)) AS P group by semesterexam, BatchId, StudentId,RollNo,ShiftId,SubId,CourseId,ConvertTo,p.mcq,p.cq ), S_Qxm2 as (select BatchId,StudentId,RollNo,ShiftId,SubId, CourseId,sum(ConvertTo) ConvertTo, sum(isnull(mcq,0)) MCQ,sum(isnull(cq,0)) CQ , sum(ConvertMarks) [10 % of CQ]from S_Qxm1 group by semesterexam,BatchId,StudentId,RollNo,ShiftId,SubId, CourseId)," +
                        "S_Sxm as(select et.semesterexam,S_ms.BatchId, S_ms.StudentId,S_ms.RollNo,S_ms.ShiftId,S_ms.SubId,S_ms.CourseId,qp.qpName, sqp.QpId,(sum(S_ms.marks)/count(S_ms.ExInId)) marks,case when S_Qxm.ConvertTo>0 then  (100-S_Qxm.ConvertTo) else 0 end as ConvertTo,((sum(S_ms.marks)/count(S_ms.ExInId))/100)*(case when S_Qxm.ConvertTo>0 then  (100-S_Qxm.ConvertTo) else 0 end) as ConvertMarks from S_ms inner join ExamType et on S_ms.exid=et.exid inner join SubjectQuestionPattern sqp on S_ms.subqpid=sqp.subqpid inner join QuestionPattern qp on sqp.QPid=qp.QpId left JOIN S_Qxm on S_ms.subid=S_Qxm.subid and S_ms.courseid=S_Qxm.courseid and sqp.qpid=S_Qxm.qpid where et.semesterexam=1 group by  et.semesterexam,S_ms.BatchId,S_ms.StudentId,S_ms.RollNo,S_ms.ShiftId,S_ms.SubId,S_ms.CourseId,qp.qpName,sqp.QpId,S_Qxm.ConvertTo ) , S_Sxm1 as(SELECT  semesterexam, BatchId, StudentId,RollNo,ShiftId,SubId,CourseId,ConvertTo,sum(ConvertMarks) as ConvertMarks, MCQ,   CQ,Practical FROM   S_Sxm PIVOT ( SUM(Marks) FOR [qpName] IN ( MCQ,CQ,Practical)) AS P group by semesterexam, BatchId, StudentId,RollNo,ShiftId,SubId,CourseId,ConvertTo,p.mcq,p.cq,p.Practical ),S_Sxm2 as (select BatchId,StudentId,RollNo,ShiftId,SubId, CourseId, sum(isnull(mcq,0)) MCQ,sum(isnull(cq,0)) CQ ,sum(isnull(Practical,0)) Practical, sum(ConvertMarks) [90 % of CQ]from S_Sxm1 group by semesterexam,BatchId,StudentId,RollNo,ShiftId,SubId, CourseId)," +
                        "S_tbl as(select S_Sxm2.batchid,S_Sxm2.studentid,S_Sxm2.subid,S_Sxm2.courseid,round((S_Qxm2.[10 % of CQ]+S_Sxm2.[90 % of CQ]) ,0) *.3 TotalCQ, S_Sxm2.mcq *.3 mcq,S_Sxm2.Practical*.3 Practical from S_Sxm2 inner join S_Qxm2 on S_Sxm2.StudentId=S_Qxm2.StudentId and  S_Sxm2.subid=S_Qxm2.subid and S_Sxm2.CourseId=S_Qxm2.CourseId inner join NewSubject ns on S_Sxm2.subid=ns.subid inner join AddCourseWithSubject acs on S_Sxm2.subid=acs.subid and S_Sxm2.courseid=acs.courseid inner join BatchInfo on S_Sxm2.BatchId=BatchInfo.BatchId inner join ClassSubject cs on BatchInfo.ClassId=cs.ClassId and S_Sxm2.subid=cs.subid and S_Sxm2.courseid=cs.courseid )," +

                        "T_ms as(select * from Class_" + tableName + "MarksSheet where Marks is not null and BatchId='" + batchId + "' and ShiftId='" + shiftId + "'  and ExInId in(" + getDependencyExInId(ExInId[0].Replace("'", string.Empty), "False") + ")), T_Qxm as(select et.semesterexam,T_ms.BatchId, StudentId,RollNo,ShiftId,T_ms.SubId,T_ms.CourseId,qp.qpName, sqp.QpId,(sum(T_ms.marks)/count(T_ms.ExInId)) marks,sqp.ConvertTo,((sum(T_ms.marks)/count(T_ms.ExInId))/100)* sqp.ConvertTo as ConvertMarks from T_ms inner join ExamType et on T_ms.exid=et.exid inner join SubjectQuestionPattern sqp on T_ms.subqpid=sqp.subqpid inner join QuestionPattern qp on sqp.QPid=qp.QpId where et.semesterexam is null group by  et.semesterexam,T_ms.BatchId,StudentId,RollNo,ShiftId,T_ms.SubId,T_ms.CourseId,qp.qpName,sqp.QpId,sqp.ConvertTo) , T_Qxm1 as(SELECT  semesterexam, BatchId, StudentId,RollNo,ShiftId,SubId,CourseId,ConvertTo,sum(ConvertMarks) as ConvertMarks, MCQ,   CQ FROM   T_Qxm PIVOT(SUM(Marks)FOR [qpName] IN ( MCQ,CQ)) AS P group by semesterexam, BatchId, StudentId,RollNo,ShiftId,SubId,CourseId,ConvertTo,p.mcq,p.cq ), T_Qxm2 as (select BatchId,StudentId,RollNo,ShiftId,SubId, CourseId,sum(ConvertTo) ConvertTo, sum(isnull(mcq,0)) MCQ,sum(isnull(cq,0)) CQ , sum(ConvertMarks) [10 % of CQ]from T_Qxm1 group by semesterexam,BatchId,StudentId,RollNo,ShiftId,SubId, CourseId)," +
                        "T_Sxm as(select et.semesterexam,T_ms.BatchId, T_ms.StudentId,T_ms.RollNo,T_ms.ShiftId,T_ms.SubId,T_ms.CourseId,qp.qpName, sqp.QpId,(sum(T_ms.marks)/count(T_ms.ExInId)) marks,case when T_Qxm.ConvertTo>0 then  (100-T_Qxm.ConvertTo) else 0 end as ConvertTo,((sum(T_ms.marks)/count(T_ms.ExInId))/100)*(case when T_Qxm.ConvertTo>0 then  (100-T_Qxm.ConvertTo) else 0 end) as ConvertMarks from T_ms inner join ExamType et on T_ms.exid=et.exid inner join SubjectQuestionPattern sqp on T_ms.subqpid=sqp.subqpid inner join QuestionPattern qp on sqp.QPid=qp.QpId left JOIN T_Qxm on T_ms.subid=T_Qxm.subid and T_ms.courseid=T_Qxm.courseid and sqp.qpid=T_Qxm.qpid where et.semesterexam=1 group by  et.semesterexam,T_ms.BatchId,T_ms.StudentId,T_ms.RollNo,T_ms.ShiftId,T_ms.SubId,T_ms.CourseId,qp.qpName,sqp.QpId,T_Qxm.ConvertTo ) , T_Sxm1 as(SELECT  semesterexam, BatchId, StudentId,RollNo,ShiftId,SubId,CourseId,ConvertTo,sum(ConvertMarks) as ConvertMarks, MCQ,   CQ,Practical FROM   T_Sxm PIVOT ( SUM(Marks) FOR [qpName] IN ( MCQ,CQ,Practical)) AS P group by semesterexam, BatchId, StudentId,RollNo,ShiftId,SubId,CourseId,ConvertTo,p.mcq,p.cq,p.Practical ),T_Sxm2 as (select BatchId,StudentId,RollNo,ShiftId,SubId, CourseId, sum(isnull(mcq,0)) MCQ,sum(isnull(cq,0)) CQ ,sum(isnull(Practical,0)) Practical, sum(ConvertMarks) [90 % of CQ]from T_Sxm1 group by semesterexam,BatchId,StudentId,RollNo,ShiftId,SubId, CourseId)," +
                        "T_tbl as(select T_Sxm2.batchid,T_Sxm2.studentid,T_Sxm2.subid,T_Sxm2.courseid,round((T_Qxm2.[10 % of CQ]+T_Sxm2.[90 % of CQ]) ,0) *.4 TotalCQ, T_Sxm2.mcq *.4 mcq,T_Sxm2.Practical*.4 Practical from T_Sxm2 inner join T_Qxm2 on T_Sxm2.StudentId=T_Qxm2.StudentId and  T_Sxm2.subid=T_Qxm2.subid and T_Sxm2.CourseId=T_Qxm2.CourseId inner join NewSubject ns on T_Sxm2.subid=ns.subid inner join AddCourseWithSubject acs on T_Sxm2.subid=acs.subid and T_Sxm2.courseid=acs.courseid inner join BatchInfo on T_Sxm2.BatchId=BatchInfo.BatchId inner join ClassSubject cs on BatchInfo.ClassId=cs.ClassId and T_Sxm2.subid=cs.subid and T_Sxm2.courseid=cs.courseid ), " +

                        "FST as(select T_tbl.BatchId,T_tbl.StudentId,T_tbl.SubId,T_tbl.Courseid,F_tbl.TotalCQ as CQ1,F_tbl.mcq as MCQ1,F_tbl.Practical as Prac1,S_tbl.TotalCQ as CQ2,S_tbl.mcq as MCQ2,S_tbl.Practical as Prac2,T_tbl.TotalCQ as CQ3,T_tbl.mcq as MCQ3,T_tbl.Practical as Prac3 , (F_tbl.TotalCQ+S_tbl.TotalCQ+T_tbl.TotalCQ) CQ, (F_tbl.mcq+S_tbl.mcq+T_tbl.mcq) MCQ,(F_tbl.Practical+S_tbl.Practical+T_tbl.Practical) Practical,round((F_tbl.TotalCQ+S_tbl.TotalCQ+T_tbl.TotalCQ)+ (F_tbl.mcq+S_tbl.mcq+T_tbl.mcq)+(F_tbl.Practical+S_tbl.Practical+T_tbl.Practical),0) TotalOM from T_tbl inner join S_tbl on T_tbl.studentId=S_tbl.studentid and T_tbl.subId=S_tbl.subId and T_tbl.courseid=S_tbl.courseid inner join F_tbl on T_tbl.studentId=F_tbl.studentid and T_tbl.subId=F_tbl.subId and T_tbl.courseid=F_tbl.courseid)," +

                        "ar as (Select sum(case  when AttStatus='p' or AttStatus= 'l'   then 1 else 0 end) as Present,sum(case  when AttStatus='a'  then 1 else 0 end) as Absent,sum(case  when AttStatus='p' or AttStatus= 'l'   then 1 else 0 end)+sum(case  when AttStatus='a'  then 1 else 0 end) as Total,StudentId  from DailyAttendanceRecord where StudentId in(select distinct StudentId from FST ) " +
                        "and AttDate<'" + date + "' and BatchId='" + batchId + "' group by StudentId)," +
                        "psn as(select FST.StudentId,csi.ShiftId,csi.ClsSecId,csi.ClsGrpId, sum(FST.TotalOM) TotalMark,ROW_NUMBER() OVER (order by  sum(FST.TotalOM) desc) as PositionInGrp,ROW_NUMBER() OVER(PARTITION BY csi.ClsSecid order by   sum(FST.TotalOM) desc) as PositionInSec   from  FST inner join v_CurrentStudentInfo csi on FST.batchid=csi.batchid and FST.StudentId=csi.StudentId group by FST.StudentId,csi.ShiftId ,csi.ClsSecId,csi.ClsGrpId) " +

                        "select '0' as IsIndependent, ClassName,FST.batchid,FST.studentid,FST.subid,FST.courseid,ns.SubName,acs.CourseName,cs.SubCode,cs.Marks,CQ1,MCQ1,Prac1,CQ2,MCQ2,Prac2,CQ3,MCQ3,Prac3,CQ,MCQ,Practical,TotalOM,csi.FullName,csi.FathersName,csi.MothersName,csi.Session,convert(varchar(10), csi.DateOfBirth,105) as DateOfBirth,csi.ShiftName ,csi.GroupName,csi.SectionName,csi.RollNo ,ar.Absent,ar.Present,ar.Total,psn.PositionInSec,psn.PositionInGrp, case when gs.MSStatus is null then 1 else gs.MSStatus end as MSStatus from FST  inner join NewSubject ns on FST.subid=ns.subid inner join AddCourseWithSubject acs on FST.subid=acs.subid and FST.courseid=acs.courseid inner join BatchInfo on FST.BatchId=BatchInfo.BatchId inner join ClassSubject cs on BatchInfo.ClassId=cs.ClassId and FST.subid=cs.subid and FST.courseid=cs.courseid inner join v_CurrentStudentInfo csi on FST.batchid=csi.batchid and FST.StudentId=csi.StudentId left join ar on FST.StudentId=ar.StudentId inner join psn on FST.StudentId=psn.StudentId left join v_StudentGroupSubSetupDetails gs on FST.Batchid=gs.Batchid and FST.StudentId=gs.StudentId and FST.Subid=gs.Subid " +
                        "where csi.ClsSecId=" + sectionId + "";
                }
                dt = CRUD.ReturnTableNull(sql);

                return dt;
            }
            catch { return dt; }
        }
        public string getDependencyExInId(string ExamInId,string IsFinal) 
        {
            dt = CRUD.ReturnTableNull("select  DependencyIExamId ,ParentExInId from  ExamDependencyInfo dei inner  join ExamInfo ei on dei.DependencyIExamId= ei.ExInId where dei.ParentExInId='" + ExamInId + "' and dei.IsFinal='"+IsFinal+"'order by ExStartDate desc");
            ExamInId = "'"+ExamInId+"'";
            if (dt.Rows.Count > 0)
            {
                for (byte b = 0; b < dt.Rows.Count; b++)
                    ExamInId +=",'" +dt.Rows[b]["DependencyIExamId"].ToString()+"'";
            }
                
                    return ExamInId;
        }
        public DataTable LoadMonthlyTest(string tableName, string shiftId,
         string batchId, string groupId, string sectionId, string examinId)
        {
            try
            {
                string[] examsplit = examinId.Split('_');
                string[] dateyear = examsplit[1].Split('-');
                string date = dateyear[2] + "-" + dateyear[0] + "-" + dateyear[1];

                sql = ";with a as(SELECT Max(Obtainmarks) as higestmarks,ExInId FROM v_Tbl_Exam_MonthlyTest where BatchId='"+batchId+"' and ShiftId='"+shiftId+"' and ClsGrpID='"+groupId+"' and ClsSecId='"+sectionId+"' and ExStartDate Between (Select isnull(Max(ExStartDate),convert(varchar(4),Year(GetDate()))+'-01'+'-01') ExStartDate  From ExamInfo inner join ExamType on ExamInfo.ExId=ExamType.ExId where BatchId='"+batchId+"' and SemesterExam=1 and ExStartDate<'"+date+"') and  '"+date+"' group by ExInId ) SELECT  RollNo,DATENAME(MONTH,ExStartDate) Month,Patternmarks,Passmarks,v_Tbl_Exam_MonthlyTest.Obtainmarks,a.higestmarks FROM v_Tbl_Exam_MonthlyTest inner join a on v_Tbl_Exam_MonthlyTest.ExInId=a.ExInId where BatchId='"+batchId+"' and ShiftId='"+shiftId+"' and ClsGrpID='"+groupId+"' and ClsSecId='"+sectionId+"' and ExStartDate Between (Select isnull(Max(ExStartDate),convert(varchar(4),Year(GetDate()))+'-01'+'-01') ExStartDate  From ExamInfo inner join ExamType on ExamInfo.ExId=ExamType.ExId  where BatchId='"+batchId+"' and SemesterExam=1 and ExStartDate<'"+date+"') and  '"+date+"' order by v_Tbl_Exam_MonthlyTest.RollNo,v_Tbl_Exam_MonthlyTest.ExStartDate";

                dt = CRUD.ReturnTableNull(sql);
                return dt;
            }
            catch { return dt; }
        }
        public DataTable LoadGrading()
        {
            try
            {
                sql = "Select * From Grading";

                dt = CRUD.ReturnTableNull(sql);
                return dt;
            }
            catch { return dt; }
        }
        public void InsertToResultSheet(string ExamID,string BatchID,string ShiftID,string ClsGrpID,string ClsSecID, string StudentID, string RollNo, string GPA, string Grade, string withoutOpGPA, string withoutOpGrade, string TotalMarks, string withoutOpTotalMarks, string IsPassed,string  FailSubjectCode,string AbsentSubjectCode,string FailAbsentSubjectCode, string NumberOfFailSubject,string NumberOfAbsentSubject,string NumberOfFailSubjectTotal, List<ExamResultFailSubject>  examResultFailSubject)
        {
            
            //---delete----
            sql = "delete Exam_ResultSheet where ExamID = '" + ExamID + "' and StudentID = '" + StudentID + "'";
            CRUD.ExecuteQuery(sql);
            //--------------
            sql = @"INSERT INTO [dbo].[Exam_ResultSheet]
           ([StudentID]
           ,[RollNo]
           ,[GPA]
           ,[Grade]
           ,[withoutOpGPA]
           ,[withoutOpGrade]
           ,[TotalMarks] 
           ,[withoutOpTotalMarks]
           ,[IsPassed],[FailSubjectCode],[AbsentSubjectCode],[FailAbsentSubjectCode],[NumberOfFailSubject],[NumberOfAbsentSubject],[NumberOfFailSubjectTotal],[BatchID],[ExamID],[ShiftID],[ClsGrpID],[ClsSecID])
           VALUES
           ('" + StudentID + "','" + RollNo + "','" + GPA + "','" + Grade + "','" + withoutOpGPA + "','" + withoutOpGrade + "','" + TotalMarks + "','" + withoutOpTotalMarks + "','" + IsPassed + "','"+ FailSubjectCode + "','"+ AbsentSubjectCode + "','"+ FailAbsentSubjectCode + "',"+ NumberOfFailSubject + ","+ NumberOfAbsentSubject +","+ NumberOfFailSubjectTotal + "," + BatchID + ",'" + ExamID + "','" + ShiftID + "',"+ ClsGrpID + ","+ ClsSecID + "); SELECT SCOPE_IDENTITY()";
         int ResultID=  CRUD.GetMaxID(sql);
            if (ResultID > 0)
            {
                // fail subject insert hear 
                foreach (var item in examResultFailSubject)
                {
                    InsertFailSubject(ResultID,ExamID,StudentID,item.SubID,item.CourseID,item.IsAbsent.ToString(),item.IsOptionalSub.ToString());
                }
               
            }
        }
        public void InsertFailSubject(int ResultID,string ExamID,string StudentID,string SubID,string CourseID,string IsAbsent,string IsOptionalSub)
        {
            sql= @"INSERT INTO [dbo].[Exam_ResultFailSubject]
           ([ResultID]
           ,[ExamID]
           ,[StudentID]
           ,[SubID]
           ,[CourseID]
           ,[IsAbsent],[IsOptionalSub])
     VALUES
           (" + ResultID + ","+ ExamID + ","+ StudentID + ","+ SubID + ","+ CourseID + ",'"+ IsAbsent + "','"+ IsOptionalSub + "')";
            CRUD.ExecuteQuery(sql);
        }
        public void InsertMeritlist(string ResultID,string BatchID,string ExamID,string BatchRank,string ShiftID,string ShiftRank,string ClsGrpID,string GrpRank,string ClsSecID,string SecRank)
        {
            sql = @"INSERT INTO [dbo].[Exam_ResultMeritList]
           ([ResultID]
           ,[BatchID]
           ,[ExamID]
           ,[BatchRank]
           ,[ShiftID]
           ,[ShiftRank]
           ,[ClsGrpID]
           ,[GrpRank]
           ,[ClsSecID]
           ,[SecRank])
           VALUES
           ("+ ResultID + ","+ BatchID + ","+ ExamID + ","+ BatchRank + ","+ ShiftID + ","+ ShiftRank + ","+ ClsGrpID + ","+ GrpRank + ","+ ClsSecID + ","+ SecRank + ")";
            CRUD.ExecuteQuery(sql);
        }

        public DataTable getMeritList(string BatchID,string ShiftID,string ClsGrpID,string ClsSecID,string RollNo)
        {
            try
            {
                ClsSecID = (ClsSecID == "0") ? "" : " and ml.ClsSecID = " + ClsSecID;
                if (RollNo != "")
                    RollNo = " and rs.RollNo = " + RollNo;
                sql = "select rs.StudentID,rs.RollNo,csi.FullName,ml.BatchID,csi.BatchName,ml.ShiftID,csi.ShiftName,ml.BatchRank,ml.ShiftRank,ml.ClsGrpID,cgs.GroupName,ml.ClsSecID,cgs.SectionName,rs.GPA,rs.TotalMarks,ml.GrpRank,ml.SecRank,csi.ClassName,ei.ExName+' - '+convert(varchar(4),year(ei.ExStartDate)) as ExamName,csi.BatchName from Exam_ResultMeritList ml inner join Exam_ResultSheet rs on ml.ResultID = rs.SL inner join v_CurrentStudentInfo csi on rs.StudentID = csi.StudentId and rs.BatchID = csi.BatchID inner join v_Class_Group_Section cgs on ml.ClsGrpID = cgs.ClsGrpID and ml.ClsSecID = cgs.ClsSecID inner join ExamInfo ei on rs.ExamID=ei.ExInSl  where ml.BatchID = " + BatchID + " and ml.ShiftID = "+ ShiftID + " and ml.ClsGrpID = "+ ClsGrpID + ClsSecID+ RollNo+ " order by GrpRank,SecRank,RollNo";
                dt = new DataTable();
                return dt = CRUD.ReturnTableNull(sql);
            }
            catch (Exception ex)
            {
                return null;

            }
        }
        public DataTable getResultSummary(string ExamID, string ClsSecID)
        {
            try
            {
                ClsSecID = (ClsSecID == "0") ? "" : " and rs.ClsSecID = " + ClsSecID;
               
                sql = "select rs.BatchID,b.ClassName,rs.ClsGrpID,s.GroupName,rs.ExamID,e.ExName+' - ' +convert(varchar(4),year(e.ExStartDate)) as ExamName, count(StudentID) as TotalStudents,max(rs.TotalMarks) as HighestMarks,sum(case when IsPassed='True' then 1 else 0 end) as Passed,sum(case when IsPassed='True' then 0 else 1 end) as Failed,sum(case when GPA=5 then 1 else 0 end) as GPA5,sum(case when GPA>=4 and GPA<5 then 1 else 0 end) as GPA4,sum(case when GPA>=3.5 and GPA<4 then 1 else 0 end) as [GPA3.5],sum(case when GPA>=3 and GPA<3.5 then 1 else 0 end) as [GPA3],sum(case when GPA>=2 and GPA<3 then 1 else 0 end) as [GPA2],sum(case when GPA>=1 and GPA<2 then 1 else 0 end) as [GPA1],sum(case when NumberOfFailSubjectTotal=1 then 1 else 0 end) as [FSub1],sum(case when NumberOfFailSubjectTotal=2 then 1 else 0 end) as [FSub2],sum(case when NumberOfFailSubjectTotal=3 then 1 else 0 end) as [FSub3],sum(case when NumberOfFailSubjectTotal>3 then 1 else 0 end) as [FSub3+] from Exam_ResultSheet rs inner join v_Class_Group_Section s on rs.ClsSecID=s.ClsSecID and rs.ClsGrpID=s.ClsGrpID inner join v_BatchInfo b on rs.BatchID=b.BatchId inner join ExamInfo e on rs.ExamID=e.ExInSl  where rs.ExamID=" + ExamID+ ClsSecID+ "  group by rs.BatchID,b.ClassName,rs.ClsGrpID,s.GroupName,rs.ExamID,e.ExName+' - ' +convert(varchar(4),year(e.ExStartDate))";
                dt = new DataTable();
                return dt = CRUD.ReturnTableNull(sql);
            }
            catch (Exception ex)
            {
                return null;

            }
        }
        public DataTable getSubjectWiseResultAnalysis(string ExamID, string ClsSecID)
        {
            try
            {
                ClsSecID = (ClsSecID == "0") ? "" : " and m.ClsSecID = " + ClsSecID;
               
                sql = @"with ms as (
select m.BatchID, m.SubId,m.CourseId,m.StudentId, sum(marks) as Marks,sum( case when IsPassed = 1 then 0 else 1 end) as IsFailed,sum(case when isnull(gs.MSStatus, 1) = 1 then 0 else 1 end) as IsOptional  from Class_ElevenMarksSheet m left join v_StudentGroupSubSetupDetails gs on m.StudentId = gs.StudentId and m.SubId = gs.SubId and m.CourseId = gs.CourseId  where ExamID = "+ ExamID + ClsSecID + @" group by m.BatchID,m.SubId,m.CourseId,m.StudentId
)
select ms.SubId, ms.CourseId,cs.SubName + ISNULL(cs.CourseName, '') as SubName,case when ms.IsOptional = 0 then 0 else 1 end as IsOptional, count(StudentId) as NumberOfStudent, sum(case when IsFailed = 0  then 1 else 0 end) as Passed,sum(case when IsFailed = 0  then 0 else 1 end) as Failed,max(ms.Marks) as HighestMarks,
sum( case when IsFailed=0 and Marks>=33 and Marks<40 then 1 else 0 end) as D,
sum( case when IsFailed=0 and Marks>=40 and Marks<50 then 1 else 0 end) as C,
sum( case when IsFailed=0 and Marks>=50 and Marks<60 then 1 else 0 end) as B,
sum( case when IsFailed=0 and Marks>=60 and Marks<70 then 1 else 0 end) as [A-],
sum( case when IsFailed=0 and Marks>=70 and Marks<80 then 1 else 0 end) as AA,
sum( case when IsFailed=0 and Marks>=80 then 1 else 0 end) as [A+]   from ms inner join BatchInfo b on b.BatchId = ms.BatchId inner join v_ClassSubjectList cs on b.ClassID = cs.ClassID and ms.SubId = cs.SubId and ms.CourseId = cs.CourseId group by ms.SubId, ms.CourseId,cs.SubName + ISNULL(cs.CourseName, ''),case when ms.IsOptional = 0 then 0 else 1 end";
                dt = new DataTable();
                return dt = CRUD.ReturnTableNull(sql);
            }
            catch (Exception ex)
            {
                return null;

            }
        }
        public DataTable getFailedStudentReport(string ExamID, string ClsSecID)
        {
            try
            {
                ClsSecID = (ClsSecID == "0") ? "" : " and rs.ClsSecID = " + ClsSecID;

                sql = @"select rs.ExamID,ex.ExName,b.ClassName,b.BatchName,rs.BatchID, rs.ClsGrpID,csi.GroupName,rs.ClsSecID,cgs.SectionName, rs.StudentID,rs.RollNo,csi.FullName, rs.NumberOfFailSubjectTotal,rs.NumberOfAbsentSubject, string_agg( case when fs.CourseId=0 then s.SubName else s.SubName+' '+c.CourseName end+ case when fs.IsAbsent=1 then '(A)' else '' end,',') as SubName from Exam_ResultSheet rs inner join ExamInfo ex on rs.ExamID=ex.ExInSl inner join v_CurrentStudentInfo csi on rs.StudentID=csi.StudentId and rs.BatchID=csi.BatchID inner join v_Class_Group_Section cgs on rs.ClsGrpID=cgs.ClsGrpID and rs.ClsSecID=cgs.ClsSecID inner join Exam_ResultFailSubject fs on rs.SL=fs.ResultID inner join NewSubject s on fs.SubID=s.SubId inner join AddCourseWithSubject c on fs.CourseID=c.CourseId inner join v_BatchInfo b on rs.BatchID=b.BatchId  where IsPassed=0 and rs.ExamID=" + ExamID + ClsSecID + @" group by rs.ExamID,ex.ExName,b.ClassName,b.BatchName,rs.BatchID, rs.ClsGrpID,csi.GroupName,rs.ClsSecID,cgs.SectionName, rs.StudentID,rs.RollNo,csi.FullName, rs.NumberOfFailSubjectTotal,rs.NumberOfAbsentSubject 
                  order by rs.ClsSecID, rs.RollNo";
                dt = new DataTable();
                return dt = CRUD.ReturnTableNull(sql);
            }
            catch (Exception ex)
            {
                return null;

            }
        }
        public DataTable getFailedAccordingToNumberOfSubjects(string ExamID, string ClsSecID)
        {
            try
            {
                ClsSecID = (ClsSecID == "0") ? "" : " and ClsSecID = " + ClsSecID;

                sql = @"with  nfs as(
select NumberOfFailSubjectTotal,count(NumberOfFailSubjectTotal) as NumberOfStudent,STRING_AGG( RollNo,',') as RollNo from Exam_ResultSheet where ExamID="+ ExamID + ClsSecID +@" and IsPassed=0 group by NumberOfFailSubjectTotal 
)
select NumberOfFailSubjectTotal, case when NumberOfFailSubjectTotal=1 then 'One Subject' else case when NumberOfFailSubjectTotal=2 then 'Two Subjects' else case when NumberOfFailSubjectTotal=3 then 'Three Subjects' else 'Four and More Subjects' end end end as NumberOfFailSubjectTotalText,NumberOfStudent,RollNo   from nfs order by NumberOfFailSubjectTotal";
                dt = new DataTable();
                return dt = CRUD.ReturnTableNull(sql);
            }
            catch (Exception ex)
            {
                return null;

            }
        }
        public DataTable getSubjectWiseFailedStudentSummary(string ExamID, string ClsSecID)
        {
            try
            {
                ClsSecID = (ClsSecID == "0") ? "" : " and ClsSecID = " + ClsSecID;
                sql = @"select cs.SubName+' '+isnull(cs.CourseName,'') as NumberOfFailSubjectTotalText ,cs.Ordering,IsOptionalSub as NumberOfFailSubjectTotal,count(ResultID) as NumberOfStudent,STRING_AGG( RollNo,',') as RollNo from Exam_ResultFailSubject fs  inner join Exam_ResultSheet rs on fs.ResultID=rs.SL inner join BatchInfo b on rs.BatchID=b.BatchId inner join v_ClassSubjectList cs on cs.ClassID=b.ClassID and fs.SubID=cs.SubId and fs.CourseID=cs.CourseId  where fs.ExamID="+ ExamID+ ClsSecID + @" group by rs.BatchID,fs.SubID,fs.CourseID,cs.SubName+' '+isnull(cs.CourseName,''),cs.Ordering ,IsOptionalSub order by IsOptionalSub desc, cs.Ordering";
                dt = new DataTable();
                return dt = CRUD.ReturnTableNull(sql);
            }
            catch (Exception ex)
            {
                return null;

            }
        }
        public DataTable getSubjectWiseFailedStudentReport(string getClass, string ExamID, string ClsSecID)
        {
            try
            {
                ClsSecID = (ClsSecID == "0") ? "" : " and rs.ClsSecID = " + ClsSecID;

                sql = @"


with swfs as(
select rs.BatchID,b.BatchName,b.ClassName,rs.ClsGrpID,rs.ClsSecID,cgs.GroupName,cgs.SectionName, rs.ExamID,ex.ExName+' - '+convert(varchar(4),ex.ExStartDate,120) as ExamName ,rs.StudentID,rs.RollNo,csi.FullName,cs.Ordering,cs.SubCode,convert(varchar,fs.SubID)+'_'+convert(varchar,fs.CourseID) as SubCourseID,fs.SubID,fs.CourseID,cs.SubName +' '+ isnull(cs.CourseName,'') as SubName,fs.IsAbsent,fs.IsOptionalSub,ms.SubQPId,sqp.QPName,ms.Marks,ms.IsPassed from  Exam_ResultFailSubject fs inner join Exam_ResultSheet rs on fs.ResultID=rs.SL inner join Class_"+ getClass + @"MarksSheet ms on rs.ExamID=ms.ExamID and rs.StudentID=ms.StudentId and fs.SubID=ms.SubId and fs.CourseID=ms.CourseId inner join v_SubjectQuestionPattern sqp on ms.SubQPId=sqp.SubQPId  inner join v_BatchInfo b on rs.BatchID=b.BatchId
  inner join ExamInfo ex on rs.ExamID=ex.ExInSl inner join CurrentStudentInfo csi on rs.StudentID=csi.StudentId and rs.BatchID=csi.BatchID inner join v_Class_Group_Section cgs on rs.ClsGrpID=cgs.ClsGrpID and rs.ClsSecID=cgs.ClsSecID inner join v_ClassSubjectList cs on fs.SubID=cs.SubId and fs.CourseID=cs.CourseId and b.ClassID= cs.ClassID   where fs.ExamID="+ExamID+ ClsSecID+@" 
)
select BatchID,BatchName,ClassName,ClsGrpID,ClsSecID,GroupName,SectionName, ExamID,ExamName,StudentID,RollNo,FullName,Ordering,SubCode,SubCourseID,SubID,CourseID,SubName,IsAbsent,IsOptionalSub,sum(marks) as TotalOptainMarks,sum(case when QPName='MCQ' then 
marks else 0 end) as MCQ,sum(case when QPName='CQ' then 
marks else 0 end) as CQ,sum(case when QPName='PRACTICAL' then 
marks else 0 end) as PRACTICAL
,sum(case when QPName='MCQ' then 
IsPassed else 0 end) as pMCQ,sum(case when QPName='CQ' then 
IsPassed else 0 end) as pCQ,sum(case when QPName='PRACTICAL' then 
IsPassed else 0 end) as pPRACTICAL
,sum(case when QPName='MCQ' then 
SubQPId else 0 end) as sqpMCQ,sum(case when QPName='CQ' then 
SubQPId else 0 end) as sqpCQ,sum(case when QPName='PRACTICAL' then 
SubQPId else 0 end) as sqpPRACTICAL
 from swfs group by BatchID,BatchName,ClassName,ClsGrpID,ClsSecID,GroupName,SectionName, ExamID,ExamName,StudentID,RollNo,FullName,Ordering,SubCode,SubCourseID,SubID,CourseID,SubName,IsAbsent,IsOptionalSub
 order by Ordering,RollNo
";
                dt = new DataTable();
                return dt = CRUD.ReturnTableNull(sql);
            }
            catch (Exception ex)
            {
                return null;

            }
        }

    }
}
