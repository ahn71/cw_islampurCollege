using DS.DAL;
using DS.PropertyEntities.Model.Admission;
using DS.PropertyEntities.Model.Examinition;
using DS.PropertyEntities.Model.GeneralSettings;
using DS.PropertyEntities.Model.ManagedBatch;
using DS.PropertyEntities.Model.ManagedClass;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace DS.BLL.Examinition
{
    public class Tbl_Exam_MonthlyTestDetailsEntry : IDisposable
    {
        private Tbl_Exam_MonthlyTestDetailsEntities _Entities;
        string sql = string.Empty;
        bool result = true;
        DataTable dt;

        public Tbl_Exam_MonthlyTestDetailsEntities AddEntities
        {
            set
            {
                _Entities = value;
            }
        }

        public bool Insert()
        {
            sql = string.Format("INSERT INTO [dbo].[Tbl_Exam_MonthlyTestDetails] " +
                "([MonthlyTest_Id], [StudentId],[RollNo],[Obtainmarks]" +
                ") VALUES (" +
                "'" + _Entities.MonthlyTest_Id + "'," +
                "'" + _Entities.StudentId + "'," +
                "'" + _Entities.RollNo + "'," +
                "'" + _Entities.Obtainmarks + "')");
            return result= CRUD.ExecuteQuery(sql);
        }
        public DataTable GetMonthlyTest(string shiftId, string batchId, string clsgrpId, string clssecId,string exinid)
        {
            try
            {
                dt = new DataTable();
                sql = string.Format("Select * from v_Tbl_Exam_MonthlyTest WHERE BatchID='" + batchId + "' "
                + "AND ShiftId='" + shiftId + "' AND ClsGrpID='" + clsgrpId + "' AND ClsSecID='" + clssecId + "' and ExInId='"+exinid+"' Order By RollNo");
                dt = CRUD.ReturnTableNull(sql);
                return dt;
            }
            catch { return dt; }
        }
        public DataTable GetMonthlyTestReportData(string shiftId, string batchId, string clsgrpId, string clssecId, string exinid)
        {
            try
            {
                dt = new DataTable();
                sql = string.Format("Select ExName,right(CONVERT(varchar(11),ExStartDate,106),8) ExStartDate,BatchName,Patternmarks,"
                +"FullName,RollNo,Obtainmarks,(Select Max(Obtainmarks) from  v_Tbl_Exam_MonthlyTest where ShiftId='"+shiftId+"' and"
                +" BatchId='"+batchId+"' and ClsGrpID='"+clsgrpId+"' and ClsSecId='"+clssecId+"') as Higestmarks,DENSE_RANK() "
                +"OVER (ORDER BY Obtainmarks DESC) AS 'position'  from v_Tbl_Exam_MonthlyTest  where ShiftId='"+shiftId+"' "
                + "and BatchId='" + batchId + "' and ClsGrpID='" + clsgrpId + "' and ClsSecId='" + clssecId + "' and exinid='"+exinid+"' order by RollNo");
                dt = CRUD.ReturnTableNull(sql);
                return dt;
            }
            catch { return dt; }
        }

        public List<Tbl_Exam_MonthlyTestDetailsEntities> GetEntitiesData()
        {
            List<Tbl_Exam_MonthlyTestDetailsEntities> ListEntities = new List<Tbl_Exam_MonthlyTestDetailsEntities>();
            try
            {
                sql = string.Format("Select * from v_Tbl_Exam_MonthlyTest");
                DataTable dt = new DataTable();
                dt = CRUD.ReturnTableNull(sql);
                if (dt != null)
                {
                    if (dt.Rows.Count > 0)
                    {
                        ListEntities = (from DataRow row in dt.Rows
                                        select new Tbl_Exam_MonthlyTestDetailsEntities
                                        {
                                            MonthlyTest_Id = int.Parse(row["SubQPId"].ToString()),
                                            tbl_exam_monthlytest = new Tbl_Exam_MontlyTestEntities()
                                            {
                                                ShiftId = int.Parse(row["ShiftId"].ToString()),
                                                BatchId = int.Parse(row["BatchId"].ToString()),
                                                ClsGrpID =row["ClsGrpID"].ToString() == "" ? 0 : int.Parse(row["ClsGrpID"].ToString()),
                                                ClsSecId = int.Parse(row["ClsSecId"].ToString()),
                                                ExInId = row["ExInId"].ToString(),
                                                Patternmarks = float.Parse(row["Patternmarks"].ToString())
                                            },
                                            Shift=new ShiftEntities()
                                            {
                                                ShiftName=row["ShiftName"].ToString()
                                            },
                                            Batch = new BatchEntities()
                                            {                                               
                                                BatchName = row["BatchName"].ToString()
                                            },
                                            Group = new ClassGroupEntities()
                                            {                                                
                                                GroupName = row["GroupName"].ToString()
                                            },
                                            Section=new SectionEntities()
                                            {
                                                SectionName=row["SectionName"].ToString()
                                            },                                           
                                           Student=new CurrentStdEntities()
                                           {
                                               FullName = row["FullName"].ToString()
                                           },
                                            StudentId=int.Parse(row["StudentId"].ToString()),                                           
                                            RollNo = int.Parse(row["RollNo"].ToString()),
                                            Obtainmarks = float.Parse(row["Obtainmarks"].ToString())
                                        }).ToList();
                        return ListEntities;
                    }
                }

            }
            catch { return ListEntities = null; }

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
