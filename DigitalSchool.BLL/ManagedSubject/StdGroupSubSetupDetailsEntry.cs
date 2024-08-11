using DS.DAL;
using DS.PropertyEntities.Model.ManagedSubject;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace DS.BLL.ManagedSubject
{
    public class StdGroupSubSetupDetailsEntry:IDisposable
    {
        private StdGroupSubSetupDetailsEntities _Entities;
        string sql = string.Empty;
        bool result = true;
        int MaxId = 0;
        DataTable dt;
        public StdGroupSubSetupDetailsEntry()
        {}
        public StdGroupSubSetupDetailsEntities AddEntities
        {
            set
            {
                _Entities = value;
            }
        }        
        public bool Insert()
        {
            sql = string.Format("INSERT INTO [dbo].[StudentGroupSubSetupDetails] " +
                "([SGSubId],[SubId],[MSStatus]) VALUES (" +
                "'" + _Entities.SGSubId + "'," +
                "'" + _Entities.SubId + "','"+_Entities.MSStatus+"')");
            return result = CRUD.ExecuteQuery(sql);
        }
        public DataTable LoadStdGroupSub(string stdID,string batchID)
        {
            dt = new DataTable();
            dt = CRUD.ReturnTableNull("SELECT SubId,MSStatus FROM v_StudentGroupSubSetup WHERE "
            +"StudentId='" + stdID + "' AND BatchId='" + batchID + "'");
            return dt;
        }
       public DataTable LoadStudentSubjectList(string batchId,string clsId,string condition) //Student Subject List
        {
            dt = new DataTable();
            sql = string.Format("SELECT StudentId, RollNo, FullName,ShiftID,ShiftName,BatchId,"
           +"BatchName,ClsGrpId,GroupName,ClsSecID,SectionName,SubCode,SubName,CourseName,"
           +"ordering,MSStatus as Compulsory FROM v_StudentGroupSubSetupDetails where "
           +"BatchId='"+batchId+"' "+condition+" Union SELECT StudentId,RollNo, FullName,ShiftID,ShiftName,"
           +"BatchId,BatchName,ClsGrpId,GroupName,ClsSecID,SectionName,SubCode,SubName,"
           +"CourseName,ordering,IsCommon as Compulsory FROM v_StudentWiseSubjectList "
           +"where ClassID='"+clsId+"' and IsOptional=0 and BothType=0 and IsCommon=1  "+condition+"  order "
           +"by RollNo,Ordering");
            dt = CRUD.ReturnTableNull(sql);
            return dt;
        }
        public DataTable GetStudentSubjects(string batchID, string classID, string clsGrpID, string studentID) //Student Subject List
        {
            dt = new DataTable();
            sql = string.Format(@"
SELECT StudentId, RollNo, FullName, ShiftID, ShiftName, BatchId, BatchName, ClsGrpId, GroupName, ClsSecID, SectionName, SubCode, SubName, CourseName, ordering, MSStatus as Compulsory FROM v_StudentGroupSubSetupDetails where BatchId = '"+ batchID + "'   AND StudentId = '"+ studentID + "' AND clsgrpID = '"+ clsGrpID + "' Union SELECT StudentId, RollNo, FullName, ShiftID, ShiftName, BatchId, BatchName, ClsGrpId, GroupName, ClsSecID, SectionName, SubCode, SubName, CourseName, ordering, IsCommon as Compulsory FROM v_StudentWiseSubjectList where ClassID = '"+ classID + "' and IsOptional = 0 and BothType = 0 and IsCommon = 1    AND StudentId = '"+ studentID + "' AND clsgrpID = '"+ clsGrpID + "'  order by RollNo, Ordering");
            dt = CRUD.ReturnTableNull(sql);
            return dt;
        }
        public DataTable LoadStudentGrpSubjectList(string batchId, string clsId, string condition) //Student Group Subject List
       {
           dt = new DataTable();
           sql = string.Format("SELECT StudentId, RollNo, FullName,ShiftID,ShiftName,BatchId,"
          + "BatchName,ClsGrpId,GroupName,ClsSecID,SectionName,SubCode,SubName,CourseName,"
          + "ordering,MSStatus as Compulsory FROM v_StudentGroupSubSetupDetails where "
          + "BatchId='" + batchId + "' " + condition + "  order "
          + "by RollNo,Ordering");
           dt = CRUD.ReturnTableNull(sql);
           return dt;
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
