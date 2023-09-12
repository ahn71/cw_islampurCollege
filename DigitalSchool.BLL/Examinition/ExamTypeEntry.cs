using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DS.PropertyEntities.Model.Examinition;
using DS.DAL;
using System.Data;
using System.Web.UI.WebControls;
using DS.DAL.AdviitDAL;

namespace DS.BLL.Examinition
{
    public class ExamTypeEntry
    {

        private ExamTypeEntities _Entities;
        static string sql = string.Empty;
        bool result = true;

        public ExamTypeEntities SetEntities
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
                if (_Entities.semesterexam != null)
                sql = "insert into ExamType (ExName,Ordering,SemesterExam,IsActive) values ('" + _Entities.ExName + "','" + _Entities.Ordering + "','" + _Entities.semesterexam + "','"+_Entities.IsActive+"')";
                else
                    sql = "insert into ExamType (ExName,Ordering,SemesterExam,IsActive) values ('" + _Entities.ExName + "','" + _Entities.Ordering + "',null,'" + _Entities.IsActive + "')";
                return result = CRUD.ExecuteQuery(sql);  
            }
            catch { return false; }
        }

        public bool Update()
        {
            try
            {
                if (_Entities.semesterexam!=null)
                sql = "update ExamType set ExName='" + _Entities.ExName + "',Ordering='" + _Entities.Ordering + "',SemesterExam='" + _Entities.semesterexam + "',IsActive='" + _Entities.IsActive + "' where ExId='" + _Entities.ExId + "'";
                else
                sql = "update ExamType set ExName='" + _Entities.ExName + "',Ordering='" + _Entities.Ordering + "',SemesterExam=null,IsActive='" + _Entities.IsActive + "' where ExId='" + _Entities.ExId + "'";
                return result = CRUD.ExecuteQuery(sql);
            }
            catch { return false; }
        }

        public bool Delete()
        {
            try
            {
                sql = "Delete from ExamType where ExId='"+_Entities.ExId+"'";
                return result = CRUD.ExecuteQuery(sql);

            }
            catch { return false; }
        }

        public List<ExamTypeEntities> GetAllExamTypeList
        {
            
        
            get
            {
                List<ExamTypeEntities> getList = new List<ExamTypeEntities>();

                sql = "Select ExId,ExName,SemesterExam,Ordering,ISNULL(IsActive,1) as IsActive from ExamType Order by Ordering";
                DataTable dt = new DataTable();
                dt = CRUD.ReturnTableNull(sql);
                
                if (dt != null)
                {
                    if (dt.Rows.Count > 0)
                    {
                        getList = (from DataRow dr in dt.Rows
                                   select new ExamTypeEntities {
                                       ExId =int.Parse(dr["ExId"].ToString()),
                                       ExName = dr["ExName"].ToString(),
                                       Ordering = int.Parse(dr["Ordering"].ToString()),
                                       semesterexam = ParseTryReturnNull(dr["SemesterExam"].ToString()),
                                       IsActive = bool.Parse(dr["IsActive"].ToString())
                                   
                                           
                                   }).ToList();
                    
                    }
                    return getList;
                }
                return getList = null;
            }
                 
        }
        public static string GetIsSemesterExam(string ExId)
        {
            try
            {
               
                DataTable dt = new DataTable();
                dt = CRUD.ReturnTableNull("Select SemesterExam from ExamType  where ExId=" + ExId + "");
                return dt.Rows[0]["SemesterExam"].ToString();
            }
            catch { return null; }
           

        }
        public bool? ParseTryReturnNull(string value) 
        {
            try
            {
                return bool.Parse(value);
            }
            catch { return null; }
        }
        public List<ExamTypeEntities> GetDropDownEntitiesData()
        {
            List<ExamTypeEntities> ListEntities = new List<ExamTypeEntities>();
            try
            {
                sql = string.Format("SELECT DISTINCT et.ExId,et.ExName FROM SubjectQuestionPattern "
                    + "sqp INNER JOIN ExamType et ON sqp.ExId=et.ExId");
                DataTable dt = new DataTable();
                dt = CRUD.ReturnTableNull(sql);
                if (dt != null)
                {
                    if (dt.Rows.Count > 0)
                    {
                        ListEntities = (from DataRow row in dt.Rows
                                        select new ExamTypeEntities
                                        {

                                            ExId = int.Parse(row["ExId"].ToString()),
                                            ExName = row["ExName"].ToString()

                                        }).ToList();
                        return ListEntities;
                    }
                }

            }
            catch { return ListEntities = null; }

            return ListEntities = null;
        }

        public static void GetExamType(DropDownList ddl)
        {
            try
            {
                sql = "Select ExId,ExName,SemesterExam,Ordering,ISNULL(IsActive,1) as IsActive from ExamType where IsActive is null or IsActive=1 Order by Ordering";
                DataTable dt = new DataTable();
                dt = CRUD.ReturnTableNull(sql);
                ddl.DataSource = dt;
                ddl.DataTextField = "ExName";
                ddl.DataValueField = "ExId";
                ddl.DataBind();
                ddl.Items.Insert(0, new ListItem("...Select...", "0"));
               
            }
            catch { }
        }

        public static void GetDropDownList(DropDownList dl)
        {
            try
            {
                List<ExamTypeEntities> ExamTypList = new List<ExamTypeEntities>();
                ExamTypeEntry examtypeEntry = new ExamTypeEntry();
                ExamTypList = examtypeEntry.GetDropDownEntitiesData();
                dl.DataSource = ExamTypList;
                dl.DataTextField = "ExName";
                dl.DataValueField = "ExId";
                dl.DataBind();
                dl.Items.Insert(0, new ListItem("...Select...", "0"));
            }
            catch { }
        }
        //public static void GetExamType(DropDownList dl)
        //{
        //    try
        //    {
        //        List<ExamTypeEntities> ExamTypList = new List<ExamTypeEntities>();
        //        ExamTypeEntry examtypeEntry = new ExamTypeEntry();
        //        ExamTypList = examtypeEntry.GetDropDownEntitiesData();
        //        dl.DataSource = ExamTypList;
        //        dl.DataTextField = "ExName";
        //        dl.DataValueField = "ExId";
        //        dl.DataBind();
        //        dl.Items.Insert(0, new ListItem("...Select...", "0"));
        //    }
        //    catch { }
        //} 
                   
    }
}
