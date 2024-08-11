using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DS.PropertyEntities.Model.SMS;
using System.Data;
using DS.DAL;
using DS.PropertyEntities.Model.ManagedBatch;

namespace DS.BLL.SMS
{
    public class FailedStudent : IDisposable
    {       
        string sql = string.Empty;
        bool result = true;
        public FailedStudent()
        { }

        public List<ExamInfo> GetExamInfoId(string from, string to)
        {
             List<ExamInfo> ListEntities = new List<ExamInfo>();
             sql = string.Format("SELECT [ExInSl],[ExInId],[ExStartDate] FROM [dbo].[ExamInfo] "
            + " WHERE Convert(datetime,[ExStartDate],105) between Convert(datetime,'" + from + "',105) "
            +"AND Convert(datetime,'"+to+"',105) ");
            DataTable dt = new DataTable();

            dt = CRUD.ReturnTableNull(sql);
            if (dt != null)
            {
                if (dt.Rows.Count > 0)
                {
                    ListEntities = (from DataRow row in dt.Rows
                                    select new ExamInfo
                                    {
                                        ExInSI = int.Parse(row["ExInSl"].ToString()),
                                        ExInId = row["ExInId"].ToString(),
                                        ExInDate = Convert.ToDateTime(row["ExStartDate"].ToString())                                                                                                                
                                    }).ToList();
                    return ListEntities;
                }

            }
            return ListEntities = null;
        }
        public List<FailedStdEntities> GetAllFailedStd(string exmId)
        {
            List<FailedStdEntities> ListEntities = new List<FailedStdEntities>();
            sql = string.Format("SELECT [sf].[SL],[sf].[StudentId],[sf].[ExInId],[sf].[SubQpId],[sf].[getMarks], " +
                                "[cs].[FullName],[cs].[RollNo],[cs].[ClassName],[cs].[SectionName],[cs].[Shift], " +
                                "[cs].[GuardianMobileNo],[sub].[SubName] " +
                                "FROM [dbo].[StudentFailList] sf INNER JOIN [dbo].[CurrentStudentInfo] cs ON " +
                                "([sf].[StudentId]=[cs].[StudentId]) INNER JOIN [dbo].[SubjectQuestionPattern] sqp ON " +
                                "([sf].[SubQpId] = [sqp].[SubQPId]) INNER JOIN [dbo].[NewSubject] sub ON " +
                                "([sqp].[SubId]=[sub].[SubId]) INNER JOIN [dbo].[Classes] cls ON " +
                                "([cs].[ClassID] = [cls].[ClassID]) WHERE [sf].[ExInId] = '" + exmId + "' " +
                                "ORDER BY [cls].[ClassOrder] ASC");
            DataTable dt = new DataTable();

            dt = CRUD.ReturnTableNull(sql);
            if (dt != null)
            {
                if (dt.Rows.Count > 0)
                {
                    ListEntities = (from DataRow row in dt.Rows
                                    select new FailedStdEntities
                                    {
                                        ID = int.Parse(row["SL"].ToString()),
                                        Student = new StdEntities()
                                        {
                                            StudentID = int.Parse(row["StudentId"].ToString()),
                                            StudentName = row["FullName"].ToString(),
                                            Roll = int.Parse(row["RollNo"].ToString()),
                                            ClassName = row["ClassName"].ToString(),
                                            Section = row["SectionName"].ToString(),
                                            Shift = row["Shift"].ToString(),
                                            GuardiantMobile = row["GuardianMobileNo"].ToString()
                                        },
                                        ExmInfo = new ExamInfo(){
                                            ExInId = row["ExInId"].ToString()
                                        },
                                        SubjectName = row["SubName"].ToString(),
                                        GetMark = Convert.ToDecimal(row["getMarks"].ToString())                                                                                                                   
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
