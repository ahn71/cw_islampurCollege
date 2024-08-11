using DS.DAL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace DS.BLL.Examinition
{
   public class ExamineeEntry
    {
        private bool result = false;
        private string sql = string.Empty;
        public bool insert(string ExamID, string StudentID, string ClsSecID, string ClsGrpID, string BatchID)
        {
            try {
                sql = @"INSERT INTO [dbo].[ExamExaminee]
           ([ExamID]
           ,[BatchID]
           ,[ClsGrpID]
           ,[ClsSecID]
           ,[StudentID])
     VALUES
           ("+ ExamID + ","+ BatchID + ","+ ClsGrpID + ","+ ClsSecID + ","+ StudentID + ")";
              return  CRUD.ExecuteQuery(sql);
            } catch(Exception) { return false; }
        }
        public bool delete(string ExamineeID)
        {
            try
            {
                sql = @"Delete [dbo].[ExamExaminee] Where ExamineeID="+ ExamineeID;
                return CRUD.ExecuteQuery(sql);
            }
            catch (Exception) { return false; }
        }
        public DataTable loadExaminee(string ConfigId,string BatchID,string ClsGrpID,string ExamID, string ClsSecID)
        {
            try
            {
                if (ConfigId == "0")
                    ConfigId = "";
                else
                    ConfigId = " and ConfigId="+ConfigId;
                if (ClsSecID == "0")
                    ClsSecID = "";
                else
                    ClsSecID = " and  csi.ClsSecID="+ClsSecID;
                sql = "select isnull(ee.ExamineeID,0) as ExamineeID,csi.StudentId,csi.RollNo,csi.FullName,csi.GroupName,csi.ClsGrpID,csi.SectionName,csi.ClsSecID,csi.BatchID,case when ee.ExamineeID is null then 0 else 1 end as Status from v_CurrentStudentInfo csi left join ExamExaminee ee on csi.StudentId=ee.StudentID and ee.ExamID=" + ExamID + "  where  csi.BatchID="+ BatchID + " and csi.ClsGrpID="+ ClsGrpID + ConfigId+ ClsSecID+" order by RollNo";                
                return CRUD.ReturnTableNull(sql);
            }
            catch (Exception ex)
            {
                return null;
            }
        }

    }
}
