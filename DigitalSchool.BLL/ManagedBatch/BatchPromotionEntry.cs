using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DS.PropertyEntities.Model.ManagedBatch;
using DS.PropertyEntities.Model.Admission;
using System.Data;
using DS.DAL;
using DS.PropertyEntities.Model.ManagedClass;
using DS.BLL.Admission;

namespace DS.BLL.ManagedBatch
{
    public class BatchPromotionEntry : IDisposable
    {
        string sql = string.Empty;
        bool result = false;
        CurrentStdEntry cntStdEntry;
        public BatchPromotionEntry()
        { }
        public bool Update(List<BatchPromotionEntities> stdPrmtnList,string status)
        {
            foreach (var stdPrmtn in stdPrmtnList)
            {
                // change by force promotion
                //sql = string.Format("SELECT ClassID,ClsGrpID,ClsSecID,RollNo,ConfigId,"
                //+ "BatchID,SpendYear FROM CurrentStudentInfo WHERE StudentId='" + stdPrmtn.Student.StudentID +"'");
                sql = string.Format("SELECT csi.ClassID,ClsGrpID,ClsSecID,RollNo,ConfigId,csi.BatchID,bi.Year as SpendYear FROM CurrentStudentInfo csi inner join BatchInfo bi on csi.BatchID=bi.BatchId  WHERE StudentId='" + stdPrmtn.Student.StudentID + "'");
                DataTable dt = new DataTable();
                dt = CRUD.ReturnTableNull(sql);
                if(cntStdEntry==null)
                {
                 cntStdEntry= new CurrentStdEntry();
                }
                CurrentStdEntities cntStnEntities = new CurrentStdEntities();
                cntStnEntities.StudentID = stdPrmtn.Student.StudentID;
                cntStnEntities.ClassID = int.Parse(dt.Rows[0]["ClassID"].ToString());
                cntStnEntities.ClsGrpID = int.Parse(dt.Rows[0]["ClsGrpID"].ToString());
                cntStnEntities.ClsSecID = int.Parse(dt.Rows[0]["ClsSecID"].ToString());
                cntStnEntities.RollNo = int.Parse(dt.Rows[0]["RollNo"].ToString());
                cntStnEntities.ConfigId = int.Parse(dt.Rows[0]["ConfigId"].ToString());
                cntStnEntities.BatchID = int.Parse(dt.Rows[0]["BatchID"].ToString());
                cntStnEntities.SpendYear = int.Parse(dt.Rows[0]["SpendYear"].ToString());
                cntStdEntry.AddEntities = cntStnEntities;
                result = cntStdEntry.CurrentStdInfo_Log();
                if (result == true)
                {
                    int spendYear=0;
                    if (status == "00")
                    {
                        spendYear = int.Parse(dt.Rows[0]["SpendYear"].ToString()) + 1;
                    }
                    else
                    {
                        spendYear=int.Parse(dt.Rows[0]["SpendYear"].ToString());
                    }
                    sql = string.Format("UPDATE [dbo].[CurrentStudentInfo] SET " +
                                        "ClassID = '" + stdPrmtn.NewClassID + "'," +
                                        "ClassName = '" + stdPrmtn.NewClassName + "'," +
                                        "ClsGrpID='" + stdPrmtn.NewClsgrpID + "'," +
                                        "ClsSecID = '" + stdPrmtn.NewClsSecID + "'," +
                                        "SectionName = '" + stdPrmtn.NewSectionName + "'," +
                                        "RollNo = '" + stdPrmtn.NewRoll + "'," +
                                        "BatchID = '" + stdPrmtn.NewBatchID + "', " +
                                        "BatchName = '" + stdPrmtn.NewBatchName + "' ," +
                                        "SpendYear ='"+spendYear+"' "+
                                        " WHERE [StudentID] = '" + stdPrmtn.Student.StudentID + "'");
                    result = CRUD.ExecuteQuery(sql);
                }
            }
            return result;
        }

        public List<BatchPromotionEntities> GetEntitiesData(string batchID,string shiftID,string condition,string clsgrpId,string clssecID)
        {            
            List<BatchPromotionEntities> ListEntities = new List<BatchPromotionEntities>();
            //sql = string.Format(" SELECT fes.StudentId,FullName,CurrentStudentInfo.RollNo,Classes.ClassName,"
            //+"CurrentStudentInfo.ClassID,fes.ClsGrpID,Tbl_Group.GroupName, fes.ClsSecID,Sections.SectionName, "
            //+"CASE WHEN fes.FinalGPA_OfExam_WithOptionalSub = 0 THEN fes.FinalGPA_OfExam ELSE fes.FinalGPA"
            //+"_OfExam_WithOptionalSub END AS GPA,fes.ClsGrpID,Tbl_Group.GroupName,CurrentStudentInfo.FullName "
            //+"FROM dbo.Exam_Final_Result_Stock_Of_All_Batch fes INNER JOIN CurrentStudentInfo ON fes.StudentId"
            //+"=CurrentStudentInfo.StudentId Left OUTER JOIN Tbl_Class_Group ON fes.ClsGrpID=Tbl_Class_Group.ClsGrpID  "
            //+"Left OUTER JOIN Tbl_Group ON Tbl_Class_Group.GroupID=Tbl_Group.GroupID INNER JOIN Classes ON "
            //+"CurrentStudentInfo.ClassID=Classes.ClassID INNER JOIN Tbl_Class_Section ON fes.clsSecID=Tbl_"
            //+"Class_Section.clsSecID INNER JOIN Sections ON Tbl_Class_Section.SectionID=Sections.SectionID "
            //+ "WHERE CurrentStudentInfo.BatchId='" + batchID + "' AND fes.IsFinalExam='1' AND fes.FinalGrad_OfExam" + condition + "'F' AND CurrentStudentInfo.ClsGrpID"
            //+ "='" + clsgrpId + "' AND CurrentStudentInfo.ClsSecId='" + clssecID + "' AND CurrentStudentInfo.ConfigId='" + shiftID + "' AND ExInId="
            //+"(SELECT DISTINCT ExInId FROM Exam_Final_Result_Stock_Of_All_Batch WHERE IsFinalExam='1' AND BatchId='"+batchID+"') ORDER BY CASE "
            //+"WHEN fes.FinalGPA_OfExam_WithOptionalSub =0 THEN fes.FinalGPA_OfExam ELSE fes.FinalGPA_OfExam_WithOptionalSub "
            //+"END DESC,fes.FinalTotalMarks_OfExam Desc");
            sql = " SELECT fes.StudentId,FullName,fes.RollNo,Classes.ClassName,fes.ClassID,fes.ClsGrpID,Tbl_Group.GroupName, " +
"fes.ClsSecID,Sections.SectionName,'5.00' AS GPA,fes.ClsGrpID,Tbl_Group.GroupName,fes.FullName " +
"FROM  CurrentStudentInfo fes Left OUTER JOIN Tbl_Class_Group ON fes.ClsGrpID=Tbl_Class_Group.ClsGrpID " +
" Left OUTER JOIN Tbl_Group ON Tbl_Class_Group.GroupID=Tbl_Group.GroupID INNER JOIN Classes ON fes.ClassID=Classes.ClassID " +
" INNER JOIN Tbl_Class_Section ON fes.clsSecID=Tbl_Class_Section.clsSecID INNER JOIN Sections ON Tbl_Class_Section.SectionID=Sections.SectionID " +
" WHERE fes.BatchId='" + batchID + "'  AND fes.ClsGrpID='" + clsgrpId + "' AND fes.ClsSecId='" + clssecID + "' AND fes.ConfigId='" + shiftID + "' ";
            DataTable dt = new DataTable();
            dt = CRUD.ReturnTableNull(sql);
            if (dt != null)
            {
                if (dt.Rows.Count > 0)
                {
                    ListEntities = (from DataRow row in dt.Rows
                                    select new BatchPromotionEntities
                                    {
                                        Student = new CurrentStdEntities()
                                        {
                                            StudentID = int.Parse(row["StudentId"].ToString()),
                                            FullName = row["FullName"].ToString(),
                                            ClassID = int.Parse(row["ClassID"].ToString()),
                                            ClassName = row["ClassName"].ToString(),
                                            ClsSecID = int.Parse(row["ClsSecID"].ToString()),
                                            SectionName = row["SectionName"].ToString(),
                                            RollNo = int.Parse(row["RollNo"].ToString()),
                                            ClsGrpID = row["ClsGrpID"].ToString()==""?0:int.Parse(row["ClsGrpID"].ToString())
                                            
                                        },
                                        Group=new GroupEntities()
                                        {                                            
                                            GroupName = row["GroupName"].ToString()
                                        },
                                        GPA = decimal.Parse(row["GPA"].ToString()),
                                        NewBatchID = 0,
                                        NewBatchName = null,
                                        NewClassName = null,
                                        NewRoll = 0,
                                        NewClassID = 0,
                                        NewClsgrpID=0,
                                        NewClsSecID = 0,
                                        NewSectionName = null
                                    }).ToList();
                    return ListEntities;
                }
            }
            return ListEntities = null;
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
