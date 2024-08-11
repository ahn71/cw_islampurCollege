using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DS.PropertyEntities.Model.Timetable;
using DS.DAL;
using System.Data;
using DS.PropertyEntities.Model.HR;

namespace DS.BLL.Timetable
{
    public class ClassRoutine : IDisposable
    {
        private ClassRoutineEntities _Entities;
        string sql = string.Empty;
        bool result = true;

        public ClassRoutine()
        {}

        public ClassRoutineEntities AddEntities
        {
            set
            {
                _Entities = value;
            }
        }

        public bool Insert(out int id)
        {
            sql = string.Format("INSERT INTO [dbo].[Tbl_Class_Routine] " +
                "([EId],[SubjectId],[CourseId],[DayID],[BatchId],[ClsGrpId],[ClsSecId],[ShiftId],[BatchYear],[RoomID],[ClsTimeID]) VALUES (" +
                "'" + _Entities.EmpInfo.EmployeeId  + "'," +
                 "'" + _Entities.SubInfo.SubjectId + "'," +
                  "'" + _Entities.CourseInfo.CourseId + "'," +  
                "'" + _Entities.Day.Id + "'," +                
                "'" + _Entities.BatchId + "'," +
                "'" + _Entities.ClasGrpId + "'," +
                "'" + _Entities.ClsSecId + "'," +
                "'" + _Entities.ShiftId + "', " +
                  "'" + _Entities.BatchYear + "', " +
                   "'" + _Entities.Room.RoomId + "', " +
                "'" + _Entities.ClassTime.ClsTimeID + "'); SELECT SCOPE_IDENTITY()");
            id = CRUD.GetMaxID(sql);
            if (id > 0)
            {
                return true; 
            }
            else
            {
                return false; 
            }            
        }
        public bool Update(string ForRoom)
        {
            if (ForRoom=="br")
            sql = string.Format("UPDATE [dbo].[Tbl_Class_Routine] SET" +                    
                    " [DayID] = '" + _Entities.Day.Id + "'," +                   
                    " [BatchId] = '" + _Entities.BatchId + "'," +
                    " [ClsGrpId] = '" + _Entities.ClasGrpId + "'," +
                    " [ClsSecId] = '" + _Entities.ClsSecId + "'," +
                    " [ShiftId] = '" + _Entities.ShiftId+ "'," +
                    " [RoomID] = '" + _Entities.Room.RoomId + "'" +                    
                    " WHERE [ClassRoutineID] = '" + _Entities.ClassRoutineID + "'");
            else if (ForRoom == "s")
                sql = string.Format("UPDATE [dbo].[Tbl_Class_Routine] SET" +
                    " [SubjectId] = '" + _Entities.SubInfo.SubjectId + "'," +
                    " [CourseId] = '" + _Entities.CourseInfo.CourseId + "'," + 
                    " [DayID] = '" + _Entities.Day.Id + "'," +                   
                    " [BatchId] = '" + _Entities.BatchId + "'," +
                    " [ClsGrpId] = '" + _Entities.ClasGrpId + "'," +
                    " [ClsSecId] = '" + _Entities.ClsSecId + "'," +
                    " [ShiftId] = '" + _Entities.ShiftId + "'" +                  
                    " WHERE [ClassRoutineID] = '" + _Entities.ClassRoutineID + "'");   
            else
                sql = string.Format("UPDATE [dbo].[Tbl_Class_Routine] SET" +
                     " [EId] = '" + _Entities.EmpInfo.EmployeeId + "'," +                   
                    " [DayID] = '" + _Entities.Day.Id + "'," +
                    " [BatchId] = '" + _Entities.BatchId + "'," +
                    " [ClsGrpId] = '" + _Entities.ClasGrpId + "'," +
                    " [ClsSecId] = '" + _Entities.ClsSecId + "'," +
                    " [ShiftId] = '" + _Entities.ShiftId + "'" +
                    " WHERE [ClassRoutineID] = '" + _Entities.ClassRoutineID + "'");   

            return result = CRUD.ExecuteQuery(sql);
        }
        public bool Update(int clsRoutineId, int subTeacheId, int rmId)
        {
            sql = string.Format("UPDATE [dbo].[Tbl_Class_Routine] SET" +
                    " [SubjectTeacherID] = '" + subTeacheId + "'," +
                    " [RoomID] = '" + rmId + "'" +
                    " WHERE [ClassRoutineID] = '" + clsRoutineId + "'");
            return result = CRUD.ExecuteQuery(sql);
        }

        public List<ClassRoutineEntities> GetEntitiesData(int? ShiftId)
        {
            List<ClassRoutineEntities> ListEntities = new List<ClassRoutineEntities>();
            if (ShiftId == null)
            {
                sql = string.Format("SELECT cr.ClassRoutineID,cr.ClsTimeID, cr.EId, cr.SubjectId, cr.CourseId, "
                +"cr.DayID, cr.BatchId, cr.ClsGrpId, cr.ClsSecId, cr.RoomID, e.EName, s.SubName, "
                +"r.RoomName,  b.BuildingName FROM    dbo.Tbl_Class_Routine AS cr LEFT OUTER JOIN "
                +"dbo.ClassSubject AS st ON cr.CourseId = st.CourseId AND cr.SubjectId = st.SubId "
                +"LEFT OUTER JOIN dbo.EmployeeInfo AS e ON cr.EId = e.EID LEFT OUTER JOIN dbo.NewSubject "
                +"AS s ON st.SubId = s.SubId LEFT OUTER JOIN dbo.Tbl_BuildingWith_Room AS r ON cr.RoomID "
                +"= r.RoomId LEFT OUTER JOIN dbo.[Tbl_Bu‎ilding_Name] AS b ON r.BuildingId = b.BuildingId");
            }
            else
            {
                sql = "SELECT ClassRoutineID,ClsTimeID,EId,EName,SubjectId,CourseId,DayID,BatchName,SectionName,ShiftName,RoomID," +
                    "TCodeNo,EName,SubName,CourseName,RoomName,BuildingName,ClsGrpId FROM v_Tbl_Class_Routine WHERE ShiftId = '" + ShiftId + "'";
            }           
            DataTable dt = new DataTable();
            dt = CRUD.ReturnTableNull(sql);
            if (dt != null)
            {
                if (dt.Rows.Count > 0)
                {
                    ListEntities = (from DataRow row in dt.Rows

                                    select new ClassRoutineEntities
                                    {
                                        ClassRoutineID = int.Parse(row["ClassRoutineID"].ToString().Trim()),
                                        ClassTime=new ClassTimeSpecificationEntities{
                                            ClsTimeID = int.Parse(row["ClsTimeID"].ToString().Trim())
                                        },
                                        EmpInfo = new Employee{
                                            EmployeeId = int.Parse(row["EId"].ToString().Trim()),
                                            EmpName = row["EName"].ToString().Trim(),
                                            TCode = row["TCodeNo"].ToString().Trim()
                                        },
                                        SubInfo=new PropertyEntities.Model.ManagedSubject.SubjectEntities{
                                            SubjectId = int.Parse(row["SubjectId"].ToString().Trim()),
                                            SubjectName = row["SubName"].ToString().Trim()

                                        },
                                        CourseInfo=new PropertyEntities.Model.ManagedSubject.CourseEntity{
                                            CourseId = int.Parse(row["CourseId"].ToString().Trim()),
                                            CourseName = row["CourseName"].ToString().Trim()

                                        },
                                        Day = new WeeklyDaysEntities
                                        {
                                            Id = int.Parse(row["DayID"].ToString().Trim())
                                        },                                       
                                        Batch = row["BatchName"].ToString().Trim(),
                                        Section = row["SectionName"].ToString().Trim(),
                                        Shift = row["ShiftName"].ToString().Trim(),
                                        ClsGroup = row["ClsGrpId"].ToString().Trim(),
                                        Room = new RoomEntities
                                        {
                                            RoomId = int.Parse(row["RoomID"].ToString().Trim()),
                                            RoomName = row["RoomName"].ToString().Trim(),
                                            BuildingName = row["BuildingName"].ToString().Trim()
                                        }                                        
                                    }).ToList();
                    return ListEntities;
                }
            }
            return ListEntities = null;
        }
        public List<ClassRoutineEntities> GetRoutine(string year)
        {
            List<ClassRoutineEntities> ListEntities = new List<ClassRoutineEntities>();
            sql = "SELECT DayID,EId,EName,TCodeNo FROM v_Tbl_Class_Routine where BatchYear='"+year+"'";
            DataTable dt = new DataTable();
            dt = CRUD.ReturnTableNull(sql);
            if (dt != null)
            {
                if (dt.Rows.Count > 0)
                {
                    ListEntities = (from DataRow row in dt.Rows

                                    select new ClassRoutineEntities
                                    {
                                       
                                        EmpInfo = new Employee
                                        {
                                            EmployeeId = int.Parse(row["EId"].ToString().Trim()),
                                            EmpName = row["EName"].ToString().Trim(),
                                            TCode = row["TCodeNo"].ToString().Trim()
                                        },                                       
                                        Day = new WeeklyDaysEntities
                                        {
                                            Id = int.Parse(row["DayID"].ToString().Trim())
                                        }
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
