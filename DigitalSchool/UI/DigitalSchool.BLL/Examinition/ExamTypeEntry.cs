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
        string sql = string.Empty;
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
                sql = "insert into ExamType (ExName) values ('" + _Entities.ExName + "')";
                return result = CRUD.ExecuteQuery(sql);  
            }
            catch { return false; }
        }

        public bool Update()
        {
            try
            {
                sql = "update ExamType set ExName='" + _Entities.ExName + "' where ExId='"+_Entities.ExId+"'";
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

                sql = "Select * from ExamType  Order by ExId desc";
                DataTable dt = new DataTable();
                dt = CRUD.ReturnTableNull(sql);

                if (dt != null)
                {
                    if (dt.Rows.Count > 0)
                    {
                        getList = (from DataRow dr in dt.Rows
                                   select new ExamTypeEntities {
                                       ExId =int.Parse(dr["ExId"].ToString()),
                                       ExName = dr["ExName"].ToString()                                                                             
                                   }).ToList();
                    
                    }
                    return getList;
                }
                return getList = null;
            }
                 
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
                List<ExamTypeEntities> ExamTypList =new List<ExamTypeEntities>();
                ExamTypeEntry ex = new ExamTypeEntry();
                ExamTypList = ex.GetAllExamTypeList;

                ddl.DataSource =ExamTypList;
                ddl.DataTextField = "ExName";
                ddl.DataValueField = "ExId";
                ddl.DataBind();
                ddl.Items.Insert(0, new ListItem("...Select...", "0"));
                //ddl.Items.Add("...Select...");
                //ddl.SelectedIndex = ;
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
                   
    }
}
