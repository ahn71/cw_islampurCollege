using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DS.PropertyEntities.Model.Attendance;
using DS.DAL;

namespace DS.BLL.Attendace
{
    public class StudentAbsentDetailsEntry
    {
        private StudentAbsentDetails _Entities;
        bool result=false;

        public StudentAbsentDetails SetValues
        {
            set {
                _Entities = value;
            }
        }

        public static bool Insert(string BatchId, string StudentId, string AbsentDate, string AbsentFine, string IsPaid)
        {
            try
            {
                bool result = CRUD.ExecuteQuery("insert into StudentAbsentDetails (BatchId,StudentId,AbsentDate,AbsentFine,IsPaid) " +
                    "values (" + BatchId + "," +StudentId + ",'" + AbsentDate + "'," +AbsentFine + "," + IsPaid + ")");
                return result;
            }
            catch { return false; }
        }

        public static bool Delete(string AbsentDate,string StudentId)
        {
            try
            {
                bool result = CRUD.ExecuteQuery("delete from StudentAbsentDetails where AbsentDate='"+AbsentDate+"' AND StudentId='"+StudentId+"'");
                return result;
                
            }
            catch { return false; }
        }
    }
}
