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
                sql = string.Format("SELECT ctrp.RollNo, dbo.BatchInfo.BatchName, FORMAT(dbo.ExamInfo.ExInDate,"
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
                +"dbo.ShiftConfiguration.ShiftName, dbo.Sections.SectionName, msheet.ExInId, Format(dbo.ExamInfo.ExInDate, "
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
                + "dbo.ShiftConfiguration.ShiftName, dbo.Sections.SectionName, msheet.ExInId, Format(dbo.ExamInfo.ExInDate, "
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
                + "dbo.ShiftConfiguration.ShiftName, dbo.Sections.SectionName, msheet.ExInId, Format(dbo.ExamInfo.ExInDate, "
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
                +"dbo.Tbl_Group.GroupName, dbo.Sections.SectionName, FORMAT(dbo.ExamInfo.ExInDate, 'yyyy') "
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
                +"dbo.Tbl_Group.GroupName, dbo.Sections.SectionName, FORMAT(dbo.ExamInfo.ExInDate, 'yyyy'), dbo.ExamType.ExName");
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
                +"dbo.Tbl_Group.GroupName, dbo.ExamType.ExName, FORMAT(dbo.ExamInfo.ExInDate, 'yyyy') "
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
                + "dbo.Tbl_Group.GroupName, dbo.ExamType.ExName, FORMAT(dbo.ExamInfo.ExInDate, 'yyyy') "
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
                + "dbo.Tbl_Group.GroupName, dbo.ExamType.ExName, FORMAT(dbo.ExamInfo.ExInDate, 'yyyy') "
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
    }
}
