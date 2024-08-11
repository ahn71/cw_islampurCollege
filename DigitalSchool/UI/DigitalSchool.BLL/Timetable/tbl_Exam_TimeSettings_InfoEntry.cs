using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DS.DAL;
using DS.PropertyEntities.Model.Timetable;

namespace DS.BLL.Timetable
{
    public class tbl_Exam_TimeSettings_InfoEntry
    {
        private tbl_Exam_TimeSettings_Info _Entities;
        public bool result;

        public tbl_Exam_TimeSettings_Info setValues
        {
            set
            {
                _Entities = value;
            }
        }

        public bool Insert()
        {
            try
            {
               result=CRUD.ExecuteQuery("insert into tbl_Exam_TimeSettings_Info (ExamTimeSetNameId,Year) values ("+_Entities.ExamTimeSetNameId+","+_Entities.Year+")");
               return result;
            }
            catch { return false; }
        }


    }
}
