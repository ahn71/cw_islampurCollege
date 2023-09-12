using DS.DAL;
using DS.PropertyEntities.Model.Finance;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace DS.BLL.Finance
{    
    public class StudentFine:IDisposable
    {
        DataTable dt;
       
        private StudentFineEntities _Entities;
        string sql = string.Empty;
        bool result = false;
        public StudentFine() { }
        public StudentFineEntities AddEntities
        {
            set
            {
                _Entities = value;
            }
        }
        public bool Update()
        {
            sql = string.Format("UPDATE StudentFine SET FineamountPaid='" + _Entities.FineamountPaid + "',"
            + "PayDate='" + _Entities.PayDate + "' WHERE FineId='"+_Entities.FineId+"'");
            return result = CRUD.ExecuteQuery(sql);
        }
        public bool AbsentUpdate(string stdID)
        {
            sql = string.Format("UPDATE StudentAbsentDetails SET PayDate='" + _Entities.PayDate + "',"
           + "IsPaid='True' WHERE StudentId='" + stdID + "' AND IsPaid='False'");
            return result = CRUD.ExecuteQuery(sql);
        }
        public DataTable GetStudentFine(string stdID)
        {
            sql = string.Format("SELECT FineId,FinePurpose, Fineamount FROM "
            + "StudentFine WHERE Studentid='" + stdID + "' AND (FineamountPaid is null or FineamountPaid=0)");
            dt = CRUD.ReturnTableNull(sql);
            return dt;
        }
        public DataTable GetAbsentFine(string stdID)
        {
            sql = string.Format("SELECT Sum(AbsentFine) as Fineamount FROM StudentAbsentDetails "
            + "WHERE StudentID='" + stdID + "' AND IsPaid='0'");
            dt = CRUD.ReturnTableNull(sql);
            return dt;
        }
        public DataTable GetStudentFineList(string Categorycondition,string AbsentCondition)
        {
            try
            {
                sql = string.Format("SELECT StudentId,FullName,RollNo,ShiftID,ShiftName,BatchID,BatchName,"
                + "clsgrpId,GroupName,clsSecId,SectionName,FinePurpose,FineAmount,Format(PayDate,'dd-MM-yyyy') "
                +"as PayDate FROM v_getFineStudentInfo " + Categorycondition + ""
                + "UNION All "
                + "SELECT StudentId,FullName,RollNo,ShiftID,ShiftName,BatchID,BatchName,clsgrpId,GroupName,"
                + "clsSecId,SectionName,FinePurpose,AbsentFine as FineAmount,Format(PayDate,'dd-MM-yyyy') "
                +" as PayDate FROM v_StudentAbsentDetails " + AbsentCondition + "");
                dt = CRUD.ReturnTableNull(sql);
                return dt;
            }
            catch { return dt; }
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
