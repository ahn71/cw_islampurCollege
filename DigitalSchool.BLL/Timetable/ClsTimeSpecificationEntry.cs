using DS.DAL;
using DS.PropertyEntities.Model.Timetable;
using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Text;

namespace DS.BLL.Timetable
{
    public class ClsTimeSpecificationEntry : IDisposable
    {
        private ClassTimeSpecificationEntities _Entities;        
        private string sql = string.Empty;
        bool result = true;
        public ClsTimeSpecificationEntry()
        {

        }

        public ClassTimeSpecificationEntities AddEntities
        {
            set
            {
                _Entities = value;
            }
        }       

        public bool Insert()
        {
            sql = string.Format("INSERT INTO [dbo].[Tbl_ClassTime_Specification] " +
                " ([ConfigId],[Name],[StartTime],[EndTime],[OrderBy],[IsBreakTime]) " +
                " VALUES ( '" + _Entities.ShiftId + "','" + _Entities.Name + "', '" + _Entities.StartTime + "', " +
                " '" + _Entities.EndTime + "', '" + _Entities.OrderBy + "', '" + _Entities.IsbreakTime + "')");
            result = CRUD.ExecuteQuery(sql);
            
            return result;
        }        

        public bool Update()
        {
            sql = string.Format("UPDATE [dbo].[Tbl_ClassTime_Specification] SET" +
                                " [Name] = '" + _Entities.Name + "'," +
                                " [StartTIme] = '" + _Entities.StartTime + "', " +
                                " [EndTime] = '" + _Entities.EndTime + "', " +
                                " [OrderBy] = '" + _Entities.OrderBy + "', " +
                                " [IsBreakTime] = '" + _Entities.IsbreakTime + "'" +
                                " WHERE [ClsTimeID] = '" + _Entities.ClsTimeID + "'");
            return result = CRUD.ExecuteQuery(sql); 
        }

        public List<ClassTimeSpecificationEntities> GetEntitiesData(int? shiftID)
        {
            List<ClassTimeSpecificationEntities> ListEntities = new List<ClassTimeSpecificationEntities>();
            sql = string.Format("SELECT a.[ClsTimeID], a.[ConfigId],a.[Name],a.[StartTime],a.[EndTime], " +
                    " a.[Period],a.[OrderBy],a.[IsBreakTime], b.[ShiftName] FROM [dbo].[Tbl_ClassTime_Specification] a INNER JOIN " +
                    " [dbo].[ShiftConfiguration] b ON (a.[ConfigId] = b.[ConfigId])" +
                    " WHERE b.[ConfigId] = '" + shiftID + "' ORDER BY a.[OrderBy]");
            DataTable dt = new DataTable();
            dt = CRUD.ReturnTableNull(sql);
            if (dt != null)
            {
                if (dt.Rows.Count > 0)
                {
                    ListEntities = (from DataRow row in dt.Rows

                                    select new ClassTimeSpecificationEntities
                                    {
                                        ClsTimeID = int.Parse(row["ClsTimeID"].ToString()),
                                        ShiftId = int.Parse(row["ConfigId"].ToString()),
                                        ShiftName = row["ShiftName"].ToString(),
                                        Name = row["Name"].ToString(),
                                        StartTime = DateTime.Parse(row["StartTime"].ToString()),
                                        EndTime = DateTime.Parse(row["EndTime"].ToString()),
                                        period = row["Period"].ToString(),
                                        OrderBy = int.Parse(row["OrderBy"].ToString()),
                                        IsbreakTime = bool.Parse(row["IsBreakTime"].ToString())
                                    }).ToList();
                    return ListEntities;
                }
            }
            return ListEntities = null;
        }
        public List<ClassTimeSpecificationEntities> GetOrderByData(int? shiftID,int? OrderBy)
        {
            List<ClassTimeSpecificationEntities> ListEntities = new List<ClassTimeSpecificationEntities>();
            sql = string.Format("SELECT a.[OrderBy] FROM [dbo].[Tbl_ClassTime_Specification] a "+
                    " WHERE a.[ConfigId] = '" + shiftID + "' AND a.[OrderBy]='"+OrderBy+"' ");
            DataTable dt = new DataTable();
            dt = CRUD.ReturnTableNull(sql);
            if (dt != null)
            {
                if (dt.Rows.Count > 0)
                {
                    ListEntities = (from DataRow row in dt.Rows

                                    select new ClassTimeSpecificationEntities
                                    {                                        
                                        OrderBy = int.Parse(row["OrderBy"].ToString())                                       
                                    }).ToList();
                    return ListEntities;
                }
            }
            return ListEntities = null;
        }

        public bool ValidationChk()
        {
            return result = false;
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
