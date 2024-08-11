using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DS.PropertyEntities.Model.Examinition;
using System.Web.UI.WebControls;
using System.Data;
using DS.DAL;

namespace DS.BLL.Examinition
{
   
    public class ExamInfoEntry
    {
        private ExamInfoEntity _ExamInfoEntities;
        bool result;
        string sql;


        public ExamInfoEntity SetExamInfoEntity
        {
            set 
            {
                _ExamInfoEntities = value;
            
            }
        }


        public bool Insert(string SelectExamId)
        {
            try
            {
                sql = "insert into ExamInfo (ExInDate,ExInId,BatchId,ExId) values ('" + _ExamInfoEntities.ExInDate + "','" + _ExamInfoEntities.ExInId + "', " + 
                       "'"+_ExamInfoEntities.BatchId+"','"+SelectExamId+"')";

                result = CRUD.ExecuteQuery(sql);
                if (result)
                {                  
                    return true;
                }
                else return false;
            }
            catch { return false; }
        }

        public static void GetExamIdList(DropDownList ddl,string BatchaId)
        {

            DataTable dt = new DataTable();

            dt = CRUD.ReturnTableNull("select ExInId from ExamInfo where BatchId ="+BatchaId+" order by ExInSl desc");
            List<ExamInfoEntity> GetList = new List<ExamInfoEntity>();
            if (dt.Rows.Count > 0)
            {


                GetList = (from DataRow dr in dt.Rows
                                          select new ExamInfoEntity
                                          {
                                              ExInId=dr["ExInId"].ToString(),                                           

                                          }).ToList();
            }



            ddl.DataSource = GetList.ToList();
            ddl.DataTextField = "ExInId";
            ddl.DataValueField = "ExInId";
            ddl.DataBind();
            ddl.Items.Insert(0, new ListItem("Select", "0"));

        }
        public static void GetStudentWiseExamIdList(DropDownList ddl, string stdID)
        {

            DataTable dt = new DataTable();

            dt = CRUD.ReturnTableNull("SELECT Distinct ExInId,ExInSl From ExamInfo WHERE BatchID "
            +"in(SELECT DISTINCT BatchID FROM CurrentStudent_log WHERE StudentId='"+stdID+"') "
            + " or BatchId=(SELECT BatchID FROM CurrentStudentInfo where StudentId='" + stdID + "') ORDER BY ExInSl desc");
            List<ExamInfoEntity> GetList = new List<ExamInfoEntity>();
            if (dt.Rows.Count > 0)
            {


                GetList = (from DataRow dr in dt.Rows
                           select new ExamInfoEntity
                           {
                               ExInId = dr["ExInId"].ToString(),

                           }).ToList();
            }



            ddl.DataSource = GetList.ToList();
            ddl.DataTextField = "ExInId";
            ddl.DataValueField = "ExInId";
            ddl.DataBind();
            ddl.Items.Insert(0, new ListItem("...Select...", "0"));

        }
        public List<ExamInfoEntity> GetEntitiesData(string batchID, string ExamType)
        {
            List<ExamInfoEntity> ListEntities = new List<ExamInfoEntity>();
            try
            {
                sql = string.Format("SELECT ExInId FROM ExamInfo "
                + "WHERE BatchId='" + batchID + "' and ExInId LIKE '" + ExamType + "%'");
                DataTable dt = new DataTable();
                dt = CRUD.ReturnTableNull(sql);
                if (dt != null)
                {
                    if (dt.Rows.Count > 0)
                    {
                        ListEntities = (from DataRow row in dt.Rows
                                        select new ExamInfoEntity
                                        {
                                            ExInId = row["ExInId"].ToString()
                                        }).ToList();
                        return ListEntities;
                    }
                }

            }
            catch { return ListEntities = null; }

            return ListEntities = null;
        } 

        public static void GetExamIdListForSetDependency(CheckBoxList cbl, string BatchaId,string ExamId)
        {
            List<ExamInfoEntity> GetList = new List<ExamInfoEntity>();
            DataTable dt = new DataTable();

            dt = CRUD.ReturnTableNull("select ExInId from ExamInfo where BatchId ="+BatchaId+" order by ExInSl desc");

            if (dt.Rows.Count > 0)
            {

                DataRow[]  rows = dt.Select("ExInId='" + ExamId + "'", null);
                foreach (DataRow r in rows) r.Delete();
                    

                
            }
            cbl.DataSource = dt;
            cbl.DataTextField = "ExInId";
            cbl.DataValueField = "ExInId";            
            cbl.DataBind();    

        }

    }
}
