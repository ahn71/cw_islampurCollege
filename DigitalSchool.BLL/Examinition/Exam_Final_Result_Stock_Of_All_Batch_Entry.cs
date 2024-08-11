using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DS.PropertyEntities.Model.Examinition;
using DS.DAL;
using System.Data;
using System.Web.UI.WebControls;

namespace DS.BLL.Examinition
{
    public class Exam_Final_Result_Stock_Of_All_Batch_Entry
    {
        private Exam_Final_Result_Stock_Of_All_Batch Entities;
        string sql;
        DataTable dt;
        
        public Exam_Final_Result_Stock_Of_All_Batch SetValues
        {
            set 
            {
                Entities = value;           
            }
        }

        public bool Insert()
        {
            try
            {
                sql = "insert into Exam_Final_Result_Stock_Of_All_Batch (ExInId,StudentId,BatchId,ShiftId,ClsSecId,ClsGrpId,"
                + "FinalGPA_OfExam_WithOptionalSub,FinalGrade_OfExam_WithOptionalSub,FinalTotalMarks_OfExam_WithOptionalSub,"
                + "FinalGPA_OfExam,FinalGrad_OfExam,FinalTotalMarks_OfExam,PublishDate,IsFinalExam) values "
                +"('"+Entities.ExInId+"',"+Entities.StudentId+","+Entities.BatchId+","
                +Entities.ShiftId+","+Entities.ClsSecId+","+Entities.ClsGrpID+","+Entities.FinalGPA_OfExam_WithOptionalSub+","
                +"'"+Entities.FinalGrade_OfExam_WithOptionalSub+"',"
                +Entities.FinalTotalMarks_OfExam_WithOptionalSub+","
                +""+Entities.FinalGPA_OfExam+",'"+Entities.FinalGrad_OfExam+"',"+
                Entities.FinalTotalMarks_OfExam+",'"+Entities.PublishDate+"','"+Entities.IsFinalExam+"')";

                return CRUD.ExecuteQuery(sql);
            }
            catch { return false; }
        }
        public DataTable getExamFinalResult(string tableName, string shiftId,
            string batchId,string groupId,string sectionId,string examinId)
        {
            DataTable dt = new DataTable();
            try
            {
                sql = string.Format("SELECT DISTINCT efr.StudentId, dbo.BatchInfo.BatchName, dbo.Sections.SectionName,"
                +"efr.BatchId, efr.ShiftId, dbo.ShiftConfiguration.ShiftName, dbo.ExamType.ExName, dbo.ExamInfo.ExInId,"
                +"cmstr.RollNo, efr.FinalGPA_OfExam_WithOptionalSub, efr.FinalGrade_OfExam_WithOptionalSub, "
                +"efr.FinalTotalMarks_OfExam_WithOptionalSub, efr.FinalGPA_OfExam, efr.FinalTotalMarks_OfExam, "
                + "efr.PublishDate, efr.FinalGrad_OfExam, dbo.CurrentStudentInfo.FullName, FORMAT(dbo.ExamInfo.ExStartDate,"
                +"'yyyy') AS ExInDate, dbo.Tbl_Group.GroupName, CASE WHEN dbo.Tbl_Group.GroupID IS NULL THEN 0 ELSE dbo."
                +"Tbl_Group.GroupID END AS GroupID FROM dbo.ShiftConfiguration INNER JOIN dbo.BatchInfo INNER JOIN "
                +"dbo.CurrentStudentInfo INNER JOIN dbo.Exam_Final_Result_Stock_Of_All_Batch AS efr INNER "
                +"JOIN dbo.ExamInfo INNER JOIN dbo.ExamType ON dbo.ExamInfo.ExId = dbo.ExamType.ExId ON efr.ExInId "
                +"= dbo.ExamInfo.ExInId ON dbo.CurrentStudentInfo.StudentId = efr.StudentId ON dbo.BatchInfo.BatchId "
                +"= efr.BatchId ON dbo.ShiftConfiguration.ConfigId = efr.ShiftId INNER JOIN dbo.Sections INNER JOIN "
                +"dbo.Tbl_Class_Section ON dbo.Sections.SectionID = dbo.Tbl_Class_Section.SectionID ON efr.ClsSecId = "
                +"dbo.Tbl_Class_Section.ClsSecID INNER JOIN dbo.Class_"+tableName+"MarksSheet_TotalResultProcess AS cmstr ON "
                +"efr.ExInId = cmstr.ExInId AND efr.StudentId = cmstr.StudentId LEFT OUTER JOIN dbo.Tbl_Class_Group ON "
                +"efr.ClsGrpID = dbo.Tbl_Class_Group.ClsGrpID LEFT OUTER JOIN dbo.Tbl_Group ON dbo.Tbl_Class_Group."
                +"GroupID = dbo.Tbl_Group.GroupID WHERE (efr.ExInId = '"+examinId+"') AND (efr.BatchId = '"+batchId+"') "
                +"AND (efr.ShiftId = '"+shiftId+"') AND (efr.ClsGrpID='"+groupId+"') AND (efr.ClsSecId = '"+sectionId+"') "
                +"order by cmstr.RollNo");               
                dt = CRUD.ReturnTableNull(sql);
                return dt;
            }
            catch(Exception ex)
            {
                ex.Message.ToString();
                return dt = null;
            }
        }
        public DataTable getExamFinalResult(string tableName, 
           string batchId, string examinId,string stdID)
        {
            DataTable dt = new DataTable();
            try
            {
                sql = string.Format("SELECT DISTINCT efr.StudentId, dbo.BatchInfo.BatchName, dbo.Sections.SectionName,"
                + "efr.BatchId, efr.ShiftId, dbo.ShiftConfiguration.ShiftName, dbo.ExamType.ExName, dbo.ExamInfo.ExInId,"
                + "cmstr.RollNo, efr.FinalGPA_OfExam_WithOptionalSub, efr.FinalGrade_OfExam_WithOptionalSub, "
                + "efr.FinalTotalMarks_OfExam_WithOptionalSub, efr.FinalGPA_OfExam, efr.FinalTotalMarks_OfExam, "
                + "efr.PublishDate, efr.FinalGrad_OfExam, dbo.CurrentStudentInfo.FullName, FORMAT(dbo.ExamInfo.ExStartDate,"
                + "'yyyy') AS ExInDate, dbo.Tbl_Group.GroupName, CASE WHEN dbo.Tbl_Group.GroupID IS NULL THEN 0 ELSE dbo."
                + "Tbl_Group.GroupID END AS GroupID FROM dbo.ShiftConfiguration INNER JOIN dbo.BatchInfo INNER JOIN "
                + "dbo.CurrentStudentInfo INNER JOIN dbo.Exam_Final_Result_Stock_Of_All_Batch AS efr INNER "
                + "JOIN dbo.ExamInfo INNER JOIN dbo.ExamType ON dbo.ExamInfo.ExId = dbo.ExamType.ExId ON efr.ExInId "
                + "= dbo.ExamInfo.ExInId ON dbo.CurrentStudentInfo.StudentId = efr.StudentId ON dbo.BatchInfo.BatchId "
                + "= efr.BatchId ON dbo.ShiftConfiguration.ConfigId = efr.ShiftId INNER JOIN dbo.Sections INNER JOIN "
                + "dbo.Tbl_Class_Section ON dbo.Sections.SectionID = dbo.Tbl_Class_Section.SectionID ON efr.ClsSecId = "
                + "dbo.Tbl_Class_Section.ClsSecID INNER JOIN dbo.Class_" + tableName + "MarksSheet_TotalResultProcess AS cmstr ON "
                + "efr.ExInId = cmstr.ExInId AND efr.StudentId = cmstr.StudentId LEFT OUTER JOIN dbo.Tbl_Class_Group ON "
                + "efr.ClsGrpID = dbo.Tbl_Class_Group.ClsGrpID LEFT OUTER JOIN dbo.Tbl_Group ON dbo.Tbl_Class_Group."
                + "GroupID = dbo.Tbl_Group.GroupID WHERE efr.StudentID='"+stdID+"' AND (efr.ExInId = '" + examinId + "') AND (efr.BatchId = '" + batchId + "') "
                + " ");
                dt = CRUD.ReturnTableNull(sql);
                return dt;
            }
            catch (Exception ex)
            {
                ex.Message.ToString();
                return dt = null;
            }
        }
        public DataTable getExamFinalResult(string tableName, string examinId,string stdId)
        {
            DataTable dt = new DataTable();
            try
            {
                sql = string.Format("SELECT DISTINCT efr.StudentId, dbo.BatchInfo.BatchName, dbo.Sections.SectionName,"
                + "efr.BatchId, efr.ShiftId, dbo.ShiftConfiguration.ShiftName, dbo.ExamType.ExName, dbo.ExamInfo.ExInId,"
                + "cmstr.RollNo, efr.FinalGPA_OfExam_WithOptionalSub, efr.FinalGrade_OfExam_WithOptionalSub, "
                + "efr.FinalTotalMarks_OfExam_WithOptionalSub, efr.FinalGPA_OfExam, efr.FinalTotalMarks_OfExam, "
                + "efr.PublishDate, efr.FinalGrad_OfExam, dbo.CurrentStudentInfo.FullName, FORMAT(dbo.ExamInfo.ExStartDate,"
                + "'yyyy') AS ExInDate, dbo.Tbl_Group.GroupName, CASE WHEN dbo.Tbl_Group.GroupID IS NULL THEN 0 ELSE dbo."
                + "Tbl_Group.GroupID END AS GroupID FROM dbo.ShiftConfiguration INNER JOIN dbo.BatchInfo INNER JOIN "
                + "dbo.CurrentStudentInfo INNER JOIN dbo.Exam_Final_Result_Stock_Of_All_Batch AS efr INNER "
                + "JOIN dbo.ExamInfo INNER JOIN dbo.ExamType ON dbo.ExamInfo.ExId = dbo.ExamType.ExId ON efr.ExInId "
                + "= dbo.ExamInfo.ExInId ON dbo.CurrentStudentInfo.StudentId = efr.StudentId ON dbo.BatchInfo.BatchId "
                + "= efr.BatchId ON dbo.ShiftConfiguration.ConfigId = efr.ShiftId INNER JOIN dbo.Sections INNER JOIN "
                + "dbo.Tbl_Class_Section ON dbo.Sections.SectionID = dbo.Tbl_Class_Section.SectionID ON efr.ClsSecId = "
                + "dbo.Tbl_Class_Section.ClsSecID INNER JOIN dbo.Class_" + tableName + "MarksSheet_TotalResultProcess AS cmstr ON "
                + "efr.ExInId = cmstr.ExInId AND efr.StudentId = cmstr.StudentId LEFT OUTER JOIN dbo.Tbl_Class_Group ON "
                + "efr.ClsGrpID = dbo.Tbl_Class_Group.ClsGrpID LEFT OUTER JOIN dbo.Tbl_Group ON dbo.Tbl_Class_Group."
                + "GroupID = dbo.Tbl_Group.GroupID WHERE (efr.ExInId = '" + examinId + "') AND efr.StudentId='"+stdId+"' ");
                dt = CRUD.ReturnTableNull(sql);
                return dt;
            }
            catch (Exception ex)
            {
                ex.Message.ToString();
                return dt = null;
            }
        }
        public DataTable getGuideStudentFinalResult(string tableName, string examinId, string EID)
        {
            DataTable dt = new DataTable();
            try
            {
                sql = string.Format("SELECT DISTINCT efr.StudentId, dbo.BatchInfo.BatchName, dbo.Sections.SectionName,"
                + "efr.BatchId, efr.ShiftId, dbo.ShiftConfiguration.ShiftName, dbo.ExamType.ExName, dbo.ExamInfo.ExInId,"
                + "cmstr.RollNo, efr.FinalGPA_OfExam_WithOptionalSub, efr.FinalGrade_OfExam_WithOptionalSub, "
                + "efr.FinalTotalMarks_OfExam_WithOptionalSub, efr.FinalGPA_OfExam, efr.FinalTotalMarks_OfExam, "
                + "efr.PublishDate, efr.FinalGrad_OfExam, dbo.CurrentStudentInfo.FullName, FORMAT(dbo.ExamInfo.ExStartDate,"
                + "'yyyy') AS ExInDate, dbo.Tbl_Group.GroupName, CASE WHEN dbo.Tbl_Group.GroupID IS NULL THEN 0 ELSE dbo."
                + "Tbl_Group.GroupID END AS GroupID FROM dbo.ShiftConfiguration INNER JOIN dbo.BatchInfo INNER JOIN "
                + "dbo.CurrentStudentInfo INNER JOIN dbo.Exam_Final_Result_Stock_Of_All_Batch AS efr INNER "
                + "JOIN dbo.ExamInfo INNER JOIN dbo.ExamType ON dbo.ExamInfo.ExId = dbo.ExamType.ExId ON efr.ExInId "
                + "= dbo.ExamInfo.ExInId ON dbo.CurrentStudentInfo.StudentId = efr.StudentId ON dbo.BatchInfo.BatchId "
                + "= efr.BatchId ON dbo.ShiftConfiguration.ConfigId = efr.ShiftId INNER JOIN dbo.Sections INNER JOIN "
                + "dbo.Tbl_Class_Section ON dbo.Sections.SectionID = dbo.Tbl_Class_Section.SectionID ON efr.ClsSecId = "
                + "dbo.Tbl_Class_Section.ClsSecID INNER JOIN dbo.Class_" + tableName + "MarksSheet_TotalResultProcess AS cmstr ON "
                + "efr.ExInId = cmstr.ExInId AND efr.StudentId = cmstr.StudentId LEFT OUTER JOIN dbo.Tbl_Class_Group ON "
                + "efr.ClsGrpID = dbo.Tbl_Class_Group.ClsGrpID LEFT OUTER JOIN dbo.Tbl_Group ON dbo.Tbl_Class_Group."
                + "GroupID = dbo.Tbl_Group.GroupID WHERE (efr.ExInId = '" + examinId + "') AND efr.StudentId "
                +"in (SELECT StudentId FROM tbl_Guide_Teacher WHERE EID='"+EID+"') ");
                dt = CRUD.ReturnTableNull(sql);
                return dt;
            }
            catch (Exception ex)
            {
                ex.Message.ToString();
                return dt = null;
            }
        } 
        public DataTable getOptionalSub(string tableName,string batchID,string exinId,string shiftId,string groupID,string sectionId)
        {
            DataTable dt = new DataTable();
            try
            {               
                sql = string.Format("SELECT Distinct SubId FROM Class_"+tableName+"MarksSheet_TotalResultProcess"
                + " WHERE IsOptional='true' AND BatchId='" + batchID + "' AND ExInId='" + exinId + "' AND ShiftId='" + shiftId 
                + "' AND ClsGrpID='" + groupID + "' AND ClsSecId='"+sectionId+"' ");
                dt = CRUD.ReturnTableNull(sql);
                return dt;
            }
            catch
            {
                return dt = null;
            }
        }
        public DataTable getOptionalSub(string tableName, string batchID, string exinId,string stdID)
        {
            DataTable dt = new DataTable();
            try
            {
                sql = string.Format("SELECT Distinct SubId FROM Class_" + tableName + "MarksSheet_TotalResultProcess"
                + " WHERE IsOptional='true' AND BatchId='" + batchID + "' AND ExInId='" + exinId + "' and StudentID='"+stdID+"' ");
                dt = CRUD.ReturnTableNull(sql);
                return dt;
            }
            catch
            {
                return dt = null;
            }
        }
        public DataTable getOptionalSub(string tableName, string exinId,string stdID)
        {
            DataTable dt = new DataTable();
            try
            {
                sql = string.Format("SELECT Distinct SubId FROM Class_" + tableName + "MarksSheet_TotalResultProcess"
                + " WHERE IsOptional='true' AND ExInId='" + exinId + "' AND StudentId='"+stdID+"' ");
                dt = CRUD.ReturnTableNull(sql);
                return dt;
            }
            catch
            {
                return dt = null;
            }
        }
        public DataTable getGuideStudentOptionalSub(string tableName, string exinId, string EID)
        {
            DataTable dt = new DataTable();
            try
            {
                sql = string.Format("SELECT Distinct SubId FROM Class_" + tableName + "MarksSheet_TotalResultProcess"
                + " WHERE IsOptional='true' AND ExInId='" + exinId + "' AND StudentId in (SELECT StudentId FROM tbl_Guide_Teacher WHERE EID='"+EID+"')");
                dt = CRUD.ReturnTableNull(sql);
                return dt;
            }
            catch
            {
                return dt = null;
            }
        }
        public DataTable GetPassList(string tableName,string batchId,
            string shiftId,string sectionId,string groupId,string ExinId)
        {
            try
            {
                dt = new DataTable();
                sql = string.Format("SELECT Distinct dbo.Tbl_Class_Section.SectionID,"
                +"dbo.BatchInfo.BatchName, dbo.Tbl_Group.GroupName, dbo.Sections.SectionName,"
                + "FORMAT(dbo.ExamInfo.ExStartDate, 'yyyy') AS ExInDate, dbo.ExamType.ExName, "
                +"cs.FullName, rp.RollNo FROM dbo.Tbl_Group RIGHT OUTER JOIN dbo.Tbl_Class_Group"
                +" ON dbo.Tbl_Group.GroupID = dbo.Tbl_Class_Group.GroupID RIGHT OUTER JOIN dbo."
                +"ExamInfo INNER JOIN dbo.Exam_Final_Result_Stock_Of_All_Batch AS fr INNER JOIN "
                +"dbo.BatchInfo ON fr.BatchId = dbo.BatchInfo.BatchId ON dbo.ExamInfo.ExInId = "
                +"fr.ExInId INNER JOIN dbo.ExamType ON dbo.ExamInfo.ExId = dbo.ExamType.ExId INNER "
                +"JOIN dbo.Sections INNER JOIN dbo.Tbl_Class_Section ON dbo.Sections.SectionID = "
                +"dbo.Tbl_Class_Section.SectionID ON fr.ClsSecId = dbo.Tbl_Class_Section.ClsSecID "
                +"INNER JOIN dbo.CurrentStudentInfo AS cs ON fr.StudentId = cs.StudentId INNER JOIN "
                +"dbo.Class_"+tableName+"MarksSheet_TotalResultProcess AS rp ON fr.StudentId = rp.StudentId "
                +"AND fr.ExInId = rp.ExInId ON dbo.Tbl_Class_Group.ClsGrpID = fr.ClsGrpID WHERE (fr.ExInId "
                +"= '"+ExinId+"') AND (fr.BatchId = '"+batchId+"') AND (fr.ShiftId = '"+shiftId+"') AND (fr.FinalGrad"
                +"_OfExam <> 'F') AND (fr.ClsSecId = '"+sectionId+"') AND (ISNULL(fr.ClsGrpID,'') = "
                +"'"+groupId+"') ORDER BY rp.RollNo");
                dt = CRUD.ReturnTableNull(sql);
                return dt;
            }
            catch
            {
                return dt = null;
            }
        }
        public DataTable GetFailList(string tableName, string batchId,
           string shiftId, string sectionId, string groupId, string ExinId)
        {
            try
            {
                dt = new DataTable();
                sql = string.Format("SELECT Distinct dbo.Tbl_Class_Section.SectionID,"
                 + "dbo.BatchInfo.BatchName, dbo.Tbl_Group.GroupName, dbo.Sections.SectionName,"
                 + "FORMAT(dbo.ExamInfo.ExStartDate, 'yyyy') AS ExInDate, dbo.ExamType.ExName, "
                 + "cs.FullName, rp.RollNo FROM dbo.Tbl_Group RIGHT OUTER JOIN dbo.Tbl_Class_Group"
                 + " ON dbo.Tbl_Group.GroupID = dbo.Tbl_Class_Group.GroupID RIGHT OUTER JOIN dbo."
                 + "ExamInfo INNER JOIN dbo.Exam_Final_Result_Stock_Of_All_Batch AS fr INNER JOIN "
                 + "dbo.BatchInfo ON fr.BatchId = dbo.BatchInfo.BatchId ON dbo.ExamInfo.ExInId = "
                 + "fr.ExInId INNER JOIN dbo.ExamType ON dbo.ExamInfo.ExId = dbo.ExamType.ExId INNER "
                 + "JOIN dbo.Sections INNER JOIN dbo.Tbl_Class_Section ON dbo.Sections.SectionID = "
                 + "dbo.Tbl_Class_Section.SectionID ON fr.ClsSecId = dbo.Tbl_Class_Section.ClsSecID "
                 + "INNER JOIN dbo.CurrentStudentInfo AS cs ON fr.StudentId = cs.StudentId INNER JOIN "
                 + "dbo.Class_" + tableName + "MarksSheet_TotalResultProcess AS rp ON fr.StudentId = rp.StudentId "
                 + "AND fr.ExInId = rp.ExInId ON dbo.Tbl_Class_Group.ClsGrpID = fr.ClsGrpID WHERE (fr.ExInId "
                 + "= '" + ExinId + "') AND (fr.BatchId = '" + batchId + "') AND (fr.ShiftId = '"+shiftId+"') AND (fr.FinalGrad"
                 + "_OfExam='F') AND (fr.ClsSecId = '" + sectionId + "') AND (ISNULL(fr.ClsGrpID,'') = "
                 + "'" + groupId + "') ORDER BY rp.RollNo");
                dt = CRUD.ReturnTableNull(sql);
                return dt;
            }
            catch
            {
                return dt = null;
            }
        }
        public DataTable GetExamOverView(string tableName, string batchId,
          string shiftId, string sectionId, string groupId, string ExinId)
        {
            try
            {
                dt = new DataTable();
                sql = string.Format("SELECT DISTINCT fr.StudentId, dbo.ShiftConfiguration.ShiftName, "
                +"fr.FinalGrade_OfExam_WithOptionalSub, fr.FinalGrad_OfExam, dbo.Tbl_Group.GroupName, "
                +"FORMAT(fr.PublishDate, 'yyyy') AS PublishDate,dbo.Sections.SectionName, dbo.BatchInfo."
                +"BatchName, dbo.ExamType.ExName, cs.Gender FROM dbo.Tbl_Class_Section INNER JOIN dbo.Sections "
                +"ON dbo.Tbl_Class_Section.SectionID = dbo.Sections.SectionID INNER JOIN dbo.Exam_Final_Result_"
                +"Stock_Of_All_Batch AS fr INNER JOIN dbo.CurrentStudentInfo AS cs ON cs.StudentId = fr.StudentId "
                +"INNER JOIN dbo.BatchInfo ON fr.BatchId = dbo.BatchInfo.BatchId INNER JOIN dbo.ShiftConfiguration "
                +"ON fr.ShiftId = dbo.ShiftConfiguration.ConfigId INNER JOIN dbo.ExamInfo ON fr.ExInId = dbo.ExamInfo"
                +".ExInId INNER JOIN dbo.ExamType ON dbo.ExamInfo.ExId = dbo.ExamType.ExId ON dbo.Tbl_Class_Section."
                +"ClsSecID = fr.ClsSecId LEFT OUTER JOIN dbo.Tbl_Group RIGHT OUTER JOIN dbo.Tbl_Class_Group ON dbo."
                +"Tbl_Group.GroupID = dbo.Tbl_Class_Group.GroupID ON fr.ClsGrpID = dbo.Tbl_Class_Group.ClsGrpID "
                +"WHERE (fr.ExInId = '"+ExinId+"') AND (fr.BatchId = '"+batchId+"') AND (fr.ShiftId = '"+shiftId+"') "
                +"AND (ISNULL(fr.ClsGrpID, '') = '"+groupId+"') AND (fr.ClsSecId='"+sectionId+"')");
                dt = CRUD.ReturnTableNull(sql);
                return dt;
            }
            catch
            {
                return dt = null;
            }
        }
        public DataTable GetAcademicTranscript(string tableName, string batchId,
          string shiftId, string sectionId, string groupId, string ExinId, string rollNo)  //Academic Transcript Show
        {
            try
            {
                dt = new DataTable();
                sql = string.Format("SELECT DISTINCT cs.FullName, dbo.NewSubject.Ordering, rp.RollNo,"
                +"cs.FathersName, cs.MothersName, sc.ShiftName, dbo.Sections.SectionName, dbo.NewSubject"
                + ".SubName, dbo.BatchInfo.BatchName, dbo.ExamType.ExName, FORMAT(dbo.ExamInfo.ExStartDate, "
                +"'yyyy') AS ExInDate, rp.IsOptional, rp.GradeOfSubject_WithAllDependencySub, "
                +"rp.PointOfSubject_WithAllDependencySub, fr.FinalGPA_OfExam, fr.PublishDate, "
                +"fr.FinalGPA_OfExam_WithOptionalSub, dbo.Tbl_Group.GroupName FROM dbo.Tbl_Group RIGHT "
                +"OUTER JOIN dbo.Tbl_Class_Group ON dbo.Tbl_Group.GroupID = dbo.Tbl_Class_Group.GroupID "
                +"RIGHT OUTER JOIN dbo.Class_"+tableName+"MarksSheet_TotalResultProcess AS rp INNER JOIN dbo."
                +"CurrentStudentInfo AS cs ON rp.StudentId = cs.StudentId INNER JOIN dbo.ExamInfo ON "
                +"rp.ExInId = dbo.ExamInfo.ExInId INNER JOIN dbo.ExamType ON dbo.ExamInfo.ExId = "
                +"dbo.ExamType.ExId INNER JOIN dbo.BatchInfo ON rp.BatchId = dbo.BatchInfo.BatchId "
                +"INNER JOIN dbo.NewSubject ON rp.SubId = dbo.NewSubject.SubId INNER JOIN dbo.ShiftConfiguration "
                +"AS sc ON rp.ShiftId = sc.ConfigId INNER JOIN dbo.Exam_Final_Result_Stock_Of_All_Batch AS "
                +"fr ON rp.ExInId = fr.ExInId AND rp.StudentId = fr.StudentId AND rp.BatchId = fr.BatchId "
                +"AND rp.ShiftId = fr.ShiftId INNER JOIN dbo.Sections INNER JOIN dbo.Tbl_Class_Section ON "
                +"dbo.Sections.SectionID = dbo.Tbl_Class_Section.SectionID ON rp.ClsSecId = dbo.Tbl_Class_"
                +"Section.ClsSecID ON dbo.Tbl_Class_Group.ClsGrpID = fr.ClsGrpID WHERE (rp.BatchId = '"+batchId+"') "
                +"AND (rp.RollNo = '"+rollNo+"') AND (rp.ExInId = '"+ExinId+"') AND (rp.ShiftId = '"+shiftId+"') "
                +"AND (rp.ClsSecId = '"+sectionId+"') AND (ISNULL(rp.ClsGrpID, '') = '"+groupId+"') ORDER BY dbo.NewSubject.Ordering");
                dt = CRUD.ReturnTableNull(sql);
                return dt;
            }
            catch
            {
                return dt = null;
            }
        }
        public DataTable GetAcademicTranscript(string tableName, string ExinId, string stdID)  //Academic Transcript Show
        {
            try
            {
                dt = new DataTable();
                sql = string.Format("SELECT DISTINCT cs.FullName, dbo.NewSubject.Ordering, rp.RollNo,"
                + "cs.FathersName, cs.MothersName, sc.ShiftName, dbo.Sections.SectionName, dbo.NewSubject"
                + ".SubName, dbo.BatchInfo.BatchName, dbo.ExamType.ExName, FORMAT(dbo.ExamInfo.ExStartDate, "
                + "'yyyy') AS ExInDate, rp.IsOptional, rp.GradeOfSubject_WithAllDependencySub, "
                + "rp.PointOfSubject_WithAllDependencySub, fr.FinalGPA_OfExam, fr.PublishDate, "
                + "fr.FinalGPA_OfExam_WithOptionalSub, dbo.Tbl_Group.GroupName FROM dbo.Tbl_Group RIGHT "
                + "OUTER JOIN dbo.Tbl_Class_Group ON dbo.Tbl_Group.GroupID = dbo.Tbl_Class_Group.GroupID "
                + "RIGHT OUTER JOIN dbo.Class_" + tableName + "MarksSheet_TotalResultProcess AS rp INNER JOIN dbo."
                + "CurrentStudentInfo AS cs ON rp.StudentId = cs.StudentId INNER JOIN dbo.ExamInfo ON "
                + "rp.ExInId = dbo.ExamInfo.ExInId INNER JOIN dbo.ExamType ON dbo.ExamInfo.ExId = "
                + "dbo.ExamType.ExId INNER JOIN dbo.BatchInfo ON rp.BatchId = dbo.BatchInfo.BatchId "
                + "INNER JOIN dbo.NewSubject ON rp.SubId = dbo.NewSubject.SubId INNER JOIN dbo.ShiftConfiguration "
                + "AS sc ON rp.ShiftId = sc.ConfigId INNER JOIN dbo.Exam_Final_Result_Stock_Of_All_Batch AS "
                + "fr ON rp.ExInId = fr.ExInId AND rp.StudentId = fr.StudentId AND rp.BatchId = fr.BatchId "
                + "AND rp.ShiftId = fr.ShiftId INNER JOIN dbo.Sections INNER JOIN dbo.Tbl_Class_Section ON "
                + "dbo.Sections.SectionID = dbo.Tbl_Class_Section.SectionID ON rp.ClsSecId = dbo.Tbl_Class_"
                + "Section.ClsSecID ON dbo.Tbl_Class_Group.ClsGrpID = fr.ClsGrpID WHERE  (rp.ExInId = '" 
                + ExinId + "') AND rp.StudentId='"+stdID+"' ORDER BY dbo.NewSubject.Ordering");
                dt = CRUD.ReturnTableNull(sql);
                return dt;
            }
            catch
            {
                return dt = null;
            }
        }
        public void GetExamID(DropDownList dl)
        {
            try
            {
                dt = CRUD.ReturnTableNull("SELECT DISTINCT ExInId FROM Exam_Final_Result_Stock_Of_All_Batch");
                dl.DataSource = dt;
                dl.DataTextField = "ExInId";
                dl.DataValueField = "ExInId";
                dl.DataBind();
                
            }
            catch {  }
        }
    }
}
