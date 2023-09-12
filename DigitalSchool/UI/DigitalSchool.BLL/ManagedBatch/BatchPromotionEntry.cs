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
                sql = string.Format("SELECT ClassID,ClsGrpID,ClsSecID,RollNo,ConfigId,"
                + "BatchID,SpendYear FROM CurrentStudentInfo WHERE StudentId='" + stdPrmtn.Student.StudentID +"'");
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
            sql = string.Format("SELECT  fes.StudentId, cs.FullName, cs.ClassName, cs.ClassID, cs.RollNo, "
            +"dbo.Tbl_Class_Section.ClsSecID, dbo.Sections.SectionName, dbo.Tbl_Class_Group.ClsGrpID, "
            +"dbo.Tbl_Group.GroupName, CASE WHEN fes.FinalGPA_OfExam_WithOptionalSub = 0 THEN fes."
            +"FinalGPA_OfExam ELSE fes.FinalGPA_OfExam_WithOptionalSub END AS GPA FROM dbo.Tbl_Group "
            +"INNER JOIN dbo.Tbl_Class_Group ON dbo.Tbl_Group.GroupID = dbo.Tbl_Class_Group.GroupID "
            +"RIGHT OUTER JOIN dbo.Exam_Final_Result_Stock_Of_All_Batch AS fes INNER JOIN dbo."
            +"CurrentStudentInfo AS cs ON fes.StudentId = cs.StudentId INNER JOIN dbo.Tbl_Class_Section "
            +"ON cs.ClsSecID = dbo.Tbl_Class_Section.ClsSecID INNER JOIN dbo.Sections ON dbo.Tbl_Class_"
            +"Section.SectionID = dbo.Sections.SectionID ON dbo.Tbl_Class_Group.ClsGrpID = fes.ClsGrpID "
            + "WHERE (fes.FinalGrad_OfExam "+condition+" 'F') AND (cs.BatchID = '" + batchID + "') AND (cs.ConfigId "
            + "='" + shiftID + "') AND (fes.IsFinalExam = 'false') AND cs.ClsGrpID='"+clsgrpId+"' "
            + " AND cs.ClsSecID='" + clssecID + "' ORDER BY CASE WHEN fes.FinalGPA_OfExam_"
            +"WithOptionalSub = 0 THEN fes.FinalGPA_OfExam ELSE fes.FinalGPA_OfExam_WithOptionalSub END DESC"
            + ",fes.FinalTotalMarks_OfExam Desc");
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
