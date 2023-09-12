using DS.DAL;
using DS.DAL.ComplexScripting;
using DS.PropertyEntities.Model.DSWS;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace DS.BLL.DSWS
{
    public class StdAttEntry:IDisposable
    {
         private StdAttEntities _Entities;
        static List<StdAttEntities> AdmStdInfoList;
        string sql = string.Empty;
        bool result = false;
        DataTable dt;
        public StdAttEntry()
        {}
        public StdAttEntities AddEntities
        {
            set
            {
                _Entities = value;
            }
        }

        public bool Insert()
        {
            sql = string.Format("INSERT INTO [dbo].[WSStudentAttendance] " +
                "([BatchId],[ClsGrpID],[ClsSecID],[TotalPresent],[TotalAbsent],[TotalStudent],[AttDate]) VALUES (" +
                "'" + _Entities.BatchId + "', " +
                "'" + _Entities.ClsGrpID + "' ," +
                "'" + _Entities.ClsSecID + "', " +
                "'" + _Entities.TotalPresent + "' ," +
                "'" + _Entities.TotalAbsent + "', " +
                "'" + _Entities.TotalStudent + "' ," +                
                "'" + _Entities.AttDate + "')");
            bool result = CRUD.ExecuteQuery(sql);
            return result;
        }
        public bool Delete(string date)
        {
            sql = string.Format("Delete FROM  [dbo].[WSStudentAttendance] WHERE [AttDate]='" + date + "'");
            bool result = CRUD.ExecuteQuery(sql);
            return result;
        }
        public List<StdAttEntities> getStdAttInfo(string date)
        {
            sql = " SELECT BatchId,BatchName,ClsGrpID,GroupName,ClsSecID, SectionName, TotalStudent,TotalPresent,TotalAbsent FROM v_WSStudentAttendance where IsUsed='True' and AttDate='"+date+"'";
            dt = new DataTable();
            List<StdAttEntities> ListEntities = new List<StdAttEntities>();
            dt = CRUD.ReturnTableNull(sql);
            if (dt != null)
            {
                if (dt.Rows.Count > 0)
                {
                    ListEntities = (from DataRow row in dt.Rows
                                    select new StdAttEntities
                                    {
                                        BatchId = int.Parse(row["BatchId"].ToString()),
                                        BatchName = row["BatchName"].ToString(),
                                        ClsGrpID = int.Parse(row["ClsGrpID"].ToString()),
                                        GroupName = row["GroupName"].ToString(),
                                        ClsSecID = int.Parse(row["ClsSecID"].ToString()),
                                        SectionName = row["SectionName"].ToString(),
                                        TotalAbsent = int.Parse(row["TotalAbsent"].ToString()),
                                        TotalPresent = int.Parse(row["TotalPresent"].ToString()),
                                        TotalStudent = int.Parse(row["TotalStudent"].ToString()),
                                        AttDate = (DateTime?)null

                                    }
                                    ).ToList();

                    return ListEntities;
                }

            }
            return ListEntities = null;

        }
        public List<StdAttEntities> getStdInfo()
        {
            sql = " SELECT BatchId,BatchName,ClsGrpID,GroupName,ClsSecID, SectionName,(SELECT COUNT(StudentId) FROM CurrentStudentInfo where BatchId=v_Tbl_Class_Section.BatchId and ClsGrpID=v_Tbl_Class_Section.ClsGrpID and ClsSecID=v_Tbl_Class_Section.ClsSecID) as TotalStudent FROM v_Tbl_Class_Section where IsUsed='True'";
            dt = new DataTable();
            List<StdAttEntities> ListEntities = new List<StdAttEntities>();
            dt = CRUD.ReturnTableNull(sql);
            if (dt != null)
            {
                if (dt.Rows.Count > 0)
                {
                    ListEntities = (from DataRow row in dt.Rows
                                    select new StdAttEntities
                                    {
                                        BatchId = int.Parse(row["BatchId"].ToString()),
                                        BatchName = row["BatchName"].ToString(),
                                        ClsGrpID = int.Parse(row["ClsGrpID"].ToString()),
                                        GroupName = row["GroupName"].ToString(),
                                        ClsSecID = int.Parse(row["ClsSecID"].ToString()),
                                        SectionName = row["SectionName"].ToString(),
                                        TotalAbsent=0,
                                        TotalPresent=0,
                                        TotalStudent = int.Parse(row["TotalStudent"].ToString()),
                                        AttDate =  (DateTime?)null                           

                                    }
                                    ).ToList();

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

