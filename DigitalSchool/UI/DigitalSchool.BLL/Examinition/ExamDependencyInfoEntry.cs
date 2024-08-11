using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DS.PropertyEntities.Model.Examinition;
using System.Data;
using DS.DAL;

namespace DS.BLL.Examinition
{
    public class ExamDependencyInfoEntry
    {
        private ExamDependencyInfoEntity _Entities;
        bool result;
        

        public ExamDependencyInfoEntity setValue
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
                result = CRUD.ExecuteQuery("insert into ExamDependencyInfo (ParentExInId,DependencyIExamId) values ('" + _Entities.ParentExInId + "','0')");
                for (byte b = 0; b < _Entities.DependencyIExamId.Items.Count; b++)
                {                         
                    if (_Entities.DependencyIExamId.Items[b].Selected)
                    result = CRUD.ExecuteQuery("insert into ExamDependencyInfo (ParentExInId,DependencyIExamId) values ('" + _Entities.ParentExInId + "','"+_Entities.DependencyIExamId.Items[b].ToString()+"')");                                                    
                }
                return result;
            }
            catch { return false; }
        }

        public bool Delete(string getParentExInId)
        {
            try
            {
                result = CRUD.ExecuteQuery("Delete From ExamDependencyInfo where ParentExInId ='"+getParentExInId+"'");
                return result;
            }
            catch { return false; }
        }

        public static List<ExamDependencyInfoEntity> GetDependencyExamList
        {
            get
            {
                try
                {
                    DataTable dt = new DataTable();
                    dt = CRUD.ReturnTableNull("select DeId,ParentExInId from v_ExamDependencyInfo where DependencyIExamId='0'  order by BatchId ");

                    List<ExamDependencyInfoEntity> GetList = new List<ExamDependencyInfoEntity>();

                    GetList = (from DataRow dr in dt.Rows
                               select new ExamDependencyInfoEntity
                               {
                                   DeId=int.Parse(dr["DeId"].ToString()),
                                   ParentExInId = dr["ParentExInId"].ToString()
                               }).ToList();
                    return GetList;
                }
                catch { return null; }
            }
        
        }

        public static bool checkHasDependencyExam(string ExInId)
        {
            try
            {
                DataTable dt = new DataTable();
                dt = CRUD.ReturnTableNull("select DeId from v_ExamDependencyInfo where ExInId='" + ExInId + "' AND DependencyIExamId='0'");
                if (dt.Rows.Count > 0) return true;                   
                else return false;
            }
            catch { return false; }
        }
    }
}
