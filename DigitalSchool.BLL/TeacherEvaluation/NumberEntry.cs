using DS.DAL;
using DS.PropertyEntities.Model.TeacherEvaluation;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace DS.BLL.TeacherEvaluation
{
    public class NumberEntry : IDisposable
    {
        private  NumberSheetEntities _Entities;
        string sql = string.Empty;
        bool result = false;
        DataTable dt;
        public NumberSheetEntities AddEntities 
        {
            set 
            {
                _Entities = value;
            }
        }
        public bool Insert() 
        {
            sql = string.Format("delete TE_NumberSheet where SessionID="+_Entities.SessionID+" and MemberID="+_Entities.MemberID+" and SubCategoryID="+_Entities.SubCategoryID+
                " and EID="+_Entities.EID+";"+
                " insert into TE_NumberSheet (EID,SessionID,MemberID,SubCategoryID,ObtainNumber) Values(" + _Entities.EID + "," + _Entities.SessionID + "," + _Entities.MemberID + 
                "," + _Entities.SubCategoryID + "," + _Entities.ObtainNumber + ")");
            return result = CRUD.ExecuteQuery(sql);  
        }

        public static DataTable GetNumberSheet(string SessionID,string MemberID) 
        {
            DataTable dt = new DataTable();
            string sql = "with sd as(select s.SessionID,cm.MemberID,n.NumPatternID,SubCategoryID,FullNumber from TE_Session s inner join TE_NumberPattern n on "+
                "s.NumPatternID=n.NumPatternID inner join TE_NumberPatternDetails nd on n.NumPatternID=nd.NumPatternID inner join TE_Committee cm on s.SessionID=cm.SessionID"+
                " where s.SessionID=" + SessionID + " and cm.MemberID=" + MemberID + "),ed as(select ei.EID,ECardNo,EName,DName,sd.SessionID,sd.MemberID,sd.SubCategoryID,sd.FullNumber,ei.IsFaculty from v_EmployeeInfo ei cross join sd ) " +
                "select ed.EID,ed.ECardNo,ed.EName,ed.DName,ed.SessionID,ed.MemberID,ed.SubCategoryID,ed.FullNumber,ns.ObtainNumber from ed left join TE_NumberSheet ns " +
                " on ed.EID=ns.EID and ed.SessionID=ns.SessionID and ed.SubCategoryID=ns.SubCategoryID and ed.MemberID=ns.MemberID "+
                " where ed.IsFaculty=1 order by EId,SubCategoryID";
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
