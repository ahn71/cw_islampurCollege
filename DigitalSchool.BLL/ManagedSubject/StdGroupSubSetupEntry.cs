using DS.DAL;
using DS.PropertyEntities.Model.ManagedSubject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DS.BLL.ManagedSubject
{
    public class StdGroupSubSetupEntry
    {
        private StdGroupSubSetupEntities _Entities;
        string sql = string.Empty;
        bool result = true;
        int MaxId = 0;
        public StdGroupSubSetupEntry()
        {}
        public StdGroupSubSetupEntities AddEntities
        {
            set
            {
                _Entities = value;
            }
        }        
        public int Insert()
        {
            sql = string.Format("INSERT INTO [dbo].[StudentGroupSubSetup] " +
                "([StudentId],[BatchId]) VALUES (" +               
                "'" + _Entities.StudentId + "'," +
                "'" + _Entities.BatchId + "'); " +
                " SELECT SCOPE_IDENTITY()");
            return MaxId = CRUD.GetMaxID(sql);
        }
        public bool Delete()
        {
            sql = string.Format("DELETE FROM StudentGroupSubSetup WHERE StudentId='" + _Entities.StudentId + "' AND BatchId='"+_Entities.BatchId+"'");
            return result = CRUD.ExecuteQuery(sql);
        }
        /*
        public List<StdGroupSubSetupEntities> GetEntitiesData()
       {
           List<StdGroupSubSetupEntities> ListEntities = new List<StdGroupSubSetupEntities>();
           try
           {
               sql = string.Format("SELECT cs.StudentId,cs.FullName,cs.RollNo,cs.ConfigId,"
               +"sc.ShiftName, cs.BatchID,b.BatchName,grp.GroupID,Tbl_Group.GroupName,"
               +"s.SectionID,sec.SectionName,ns.SubId,ns.SubName,os.BatchId as OpBatchId "
               +"FROM CurrentStudentInfo cs INNER JOIN ShiftConfiguration sc ON cs.ConfigId="
               +"sc.ConfigId INNER JOIN BatchInfo b ON cs.BatchID=b.BatchId LEFT OUTER JOIN "
               +"Tbl_Class_Group grp ON cs.ClsGrpID=grp.ClsGrpID LEFT OUTER JOIN Tbl_Group "
               +"ON Tbl_Group.GroupID=grp.GroupID INNER JOIN Tbl_Class_Section s ON "
               +"cs.ClsSecID=s.ClsSecID INNER JOIN Sections sec ON sec.SectionID="
               +"s.SectionID LEFT OUTER JOIN OptionalSubjectInfo os ON cs.StudentId="
               +"os.StudentId LEFT OUTER JOIN NewSubject ns ON os.SubId=ns.SubId ORDER BY cs.RollNo, ns.Ordering");
               DataTable dt = new DataTable();
               dt = CRUD.ReturnTableNull(sql);
               if (dt != null)
               {
                   if (dt.Rows.Count > 0)
                   {
                       ListEntities = (from DataRow row in dt.Rows
                                       select new OptionalSubjectEntities
                                       {                                          
                                           Student = new CurrentStdEntities()
                                           {
                                               StudentID = int.Parse(row["StudentId"].ToString()),
                                               FullName = row["FullName"].ToString(),
                                               RollNo = int.Parse(row["RollNo"].ToString()),
                                           },
                                           Shift=new ShiftEntities()
                                           {
                                               ShiftConfigId = int.Parse(row["ConfigId"].ToString()),
                                               ShiftName = row["ShiftName"].ToString()
                                           },
                                           Batch=new BatchEntities()
                                           {
                                               BatchId = int.Parse(row["BatchID"].ToString()),
                                               BatchName = row["BatchName"].ToString()
                                           },
                                           Group=new GroupEntities()
                                           {
                                               GroupID =row["GroupID"].ToString()==""?0:int.Parse(row["GroupID"].ToString()),
                                               GroupName = row["GroupName"].ToString()
                                           },
                                           Section = new SectionEntities()
                                           {
                                               SectionID = int.Parse(row["SectionID"].ToString()),
                                               SectionName = row["SectionName"].ToString()
                                           },                                         
                                           Subject = new SubjectEntities()
                                           {
                                               SubjectId =row["SubId"].ToString()==""?0:int.Parse(row["SubId"].ToString()),
                                               SubjectName = row["SubName"].ToString()
                                           },
                                           OpBatchId = row["OpBatchId"].ToString() == "" ? 0 : int.Parse(row["OpBatchId"].ToString())
                                       }).ToList();
                       return ListEntities;
                   }
               }

           }
           catch { return ListEntities = null; }

           return ListEntities = null;
       }
         */
       
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
