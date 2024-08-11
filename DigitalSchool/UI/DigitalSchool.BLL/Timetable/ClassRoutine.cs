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
                "([SubjectTeacherID],[DayID],[ClsTimeSetNameID],[ClassTimeID],[BatchId],[ClsGrpId],[ClsSecId],[ShiftId],[TSAId],[BatchYear],[RoomID]) VALUES (" +
                "'" + _Entities.SubjectTeacherID.SubTecherId  + "'," +
                "'" + _Entities.Day.Id + "'," +
                "'" + _Entities.ClassTime.ClsTimeSetNameId + "'," +
                "'" + _Entities.ClassTime.ClassTimeId + "'," +
                "'" + _Entities.BatchId + "'," +
                "'" + _Entities.ClasGrpId + "'," +
                "'" + _Entities.ClsSecId + "'," +
                "'" + _Entities.ShiftId + "', " +
                "'" + _Entities.TSAId + "', " +
                  "'" + _Entities.BatchYear + "', " +
                "'" + _Entities.Room.RoomId + "'); SELECT SCOPE_IDENTITY()");
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
        public bool Update(bool ForRoom)
        {
            if (ForRoom)
            sql = string.Format("UPDATE [dbo].[Tbl_Class_Routine] SET" +
                    " [SubjectTeacherID] = '" + _Entities.SubjectTeacherID.SubTecherId + "'," +                    
                    " [DayID] = '" + _Entities.Day.Id + "'," +
                    " [ClsTimeSetNameID] = '" + _Entities.ClassTime.ClsTimeSetNameId + "'," +
                    " [ClassTimeID] = '" + _Entities.ClassTime.ClassTimeId + "'," +
                    " [BatchId] = '" + _Entities.BatchId + "'," +
                    " [ClsGrpId] = '" + _Entities.ClasGrpId + "'," +
                    " [ClsSecId] = '" + _Entities.ClsSecId + "'," +
                    " [ShiftId] = '" + _Entities.ShiftId+ "'," +
                    " [RoomID] = '" + _Entities.Room.RoomId + "'" +
                    
                    " WHERE [ClassRoutineID] = '" + _Entities.ClassRoutineID + "'");  
            else
                sql = string.Format("UPDATE [dbo].[Tbl_Class_Routine] SET" +
                    " [SubjectTeacherID] = '" + _Entities.SubjectTeacherID.SubTecherId + "'," +
                    " [DayID] = '" + _Entities.Day.Id + "'," +
                    " [ClsTimeSetNameID] = '" + _Entities.ClassTime.ClsTimeSetNameId + "'," +
                    " [ClassTimeID] = '" + _Entities.ClassTime.ClassTimeId + "'," +
                    " [BatchId] = '" + _Entities.BatchId + "'," +
                    " [ClsGrpId] = '" + _Entities.ClasGrpId + "'," +
                    " [ClsSecId] = '" + _Entities.ClsSecId + "'," +
                    " [ShiftId] = '" + _Entities.ShiftId + "'," +                  
                    " [TSAId] = '" + _Entities.TSAId + "'" +
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

        public List<ClassRoutineEntities> GetEntitiesData(int? ClsTimeSetNameID)
        {
            List<ClassRoutineEntities> ListEntities = new List<ClassRoutineEntities>();
            if (ClsTimeSetNameID == null)
            {
                sql = string.Format("SELECT cr.[ClassRoutineID],cr.[SubjectTeacherID],cr.[DayID],cr.[ClsTimeSetNameID]," +
                      "cr.[ClassTimeID],cr.[Batch],cr.[Section],cr.[Shift],cr.[RoomID],e.[EName],s.[SubName]," +
                      "r.[RoomName],b.[BuildingName] FROM [dbo].[Tbl_Class_Routine] cr LEFT OUTER JOIN " +
                      "[dbo].[Tbl_Subject_Teacher] st ON (cr.[SubjectTeacherID] = st.[SubTecherId]) LEFT OUTER JOIN " +
                      "[dbo].[EmployeeInfo] e ON (st.[EId] = e.[EID]) LEFT OUTER JOIN [dbo].[NewSubject] s " +
                      "ON (st.[SubId] = s.[SubId]) LEFT OUTER JOIN [dbo].[Tbl_BuildingWith_Room] r ON " +
                      "(cr.[RoomID] = r.[RoomId]) LEFT OUTER JOIN [dbo].[Tbl_Bu‎ilding_Name] b ON (r.[BuildingId] = b.[BuildingId])");
            }
            else
            {
                sql = "SELECT ClassRoutineID,SubjectTeacherID,DayID,ClsTimeSetNameID,ClassTimeID,BatchName,SectionName,ShiftName,RoomID,"+
                    "TCodeNo,EName,SubName,CourseName,RoomName,BuildingName FROM v_Tbl_Class_Routine WHERE ClsTimeSetNameID = " + ClsTimeSetNameID + "";
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
                                        SubjectTeacherID = new SubTeacherNameList{
                                            SubTecherId = int.Parse(row["SubjectTeacherID"].ToString().Trim()),
                                            Subject = row["SubName"].ToString().Trim() + " " + row["CourseName"].ToString().Trim(),
                                            Teacher = row["TCodeNo"].ToString().Trim()
                                        },
                                        Day = new WeeklyDaysEntities
                                        {
                                            Id = int.Parse(row["DayID"].ToString().Trim())
                                        },
                                        ClassTime = new ClassTimeSpecificationEntities
                                        {
                                            ClassTimeId = int.Parse(row["ClassTimeID"].ToString().Trim()),
                                            ClsTimeSetNameId = int.Parse(row["ClsTimeSetNameID"].ToString().Trim())
                                        },
                                        Batch = row["BatchName"].ToString().Trim(),
                                        Section = row["SectionName"].ToString().Trim(),
                                        Shift = row["ShiftName"].ToString().Trim(),
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
