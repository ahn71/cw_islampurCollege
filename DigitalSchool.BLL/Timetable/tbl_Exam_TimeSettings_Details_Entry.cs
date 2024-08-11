using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DS.DAL;
using DS.PropertyEntities.Model.Timetable;
using System.Data;

namespace DS.BLL.Timetable
{
    public class tbl_Exam_TimeSettings_Details_Entry
    {
        private tbl_Exam_TimeSettings_Details _Entities;
        bool result=false;

        public tbl_Exam_TimeSettings_Details SetValues
        {
            set {
                _Entities = value;
            }
        }

        public bool Insert()
        {
            try
            {

                DataTable dt = CRUD.ReturnTableNull("select max(ExScId) as ExScId from tbl_Exam_TimeSettings_Info");
                string Details1 = (_Entities.Details1.Equals("&nbsp;")) ? " " : _Entities.Details1;
                string Details2 = (_Entities.Details2.Equals("&nbsp;")) ? " " : _Entities.Details2;
                string Details3 = (_Entities.Details3.Equals("&nbsp;")) ? " " : _Entities.Details3;
                string Details4 = (_Entities.Details4.Equals("&nbsp;")) ? " " : _Entities.Details4;
                string Details5 = (_Entities.Details5.Equals("&nbsp;")) ? " " : _Entities.Details5;
                result = CRUD.ExecuteQuery("Insert into tbl_Exam_TimeSettings_Details (ExScId,ExamDate,ExamTimeDuration1,Details1,ExamTimeDuration2,Details2,ExamTimeDuration3,Details3,ExamTimeDuration4,Details4,ExamTimeDuration5,Details5) values " +
                    "(" + dt.Rows[0]["ExScId"].ToString() + ",'" + _Entities.ExamDate + "','" + _Entities.ExamTimeDuration1 + "','" + Details1 + "','" + _Entities.ExamTimeDuration2 + "','" + Details2 + "','" + _Entities.ExamTimeDuration3 + "','" + Details3 + "',"+
                    "'" + _Entities.ExamTimeDuration4 + "','" + Details4 + "','" + _Entities.ExamTimeDuration5 + "','" + Details5+ "') ");
                return result;
            }
            catch { return false; }
        }

        public static void SetNoOfPeriod(string NoOfPeriod,string ExamTimeSetNameId)
        {
            try
            {
                CRUD.ExecuteQuery("Update tbl_Exam_TimeSettings_Info set NoOfPeriod=" + NoOfPeriod + " where ExamTimeSetNameId =" + ExamTimeSetNameId + "");
            }
            catch { }
        }

        
    }
}
