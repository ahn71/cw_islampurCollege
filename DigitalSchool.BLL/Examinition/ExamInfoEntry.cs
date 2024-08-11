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
                sql = "insert into ExamInfo (ExStartDate,ExInId,BatchId,ExId,ExEndDate,ExName,ClsGrpID) values ('" + _ExamInfoEntities.ExStartDate.ToString("yyyy-MM-dd") + "','" + _ExamInfoEntities.ExInId + "', " +
                       "'" + _ExamInfoEntities.BatchId + "','" + SelectExamId + "','" + _ExamInfoEntities.ExEndDate.ToString("yyyy-MM-dd") + "','" + _ExamInfoEntities.ExName + "'," + _ExamInfoEntities.ClsGrpID + ")";
                result = CRUD.ExecuteQuery(sql);
                if (result)
                {
                    return true;
                }
                else return false;
            }
            catch { return false; }
        }

        public static void GetExamIdList(DropDownList ddl,string BatchaId,Boolean examtype)
        {

            DataTable dt = new DataTable();

            dt = CRUD.ReturnTableNull("select ExInId from ExamInfo inner join ExamType on ExamInfo.ExId=ExamType.ExId where BatchId ='" + BatchaId + "' and SemesterExam='" + examtype + "' order by Ordering Asc");
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
            ddl.Items.Insert(0, new ListItem("...Select...", "0"));

        }
        public static void GetExamIdList(DropDownList ddl, string BatchaId) // created by nayem. date: 16-12-2017. purpose: all exam info loading in dropdownlist
        {

            DataTable dt = new DataTable();

            dt = CRUD.ReturnTableNull("select ExInId from ExamInfo inner join ExamType on ExamInfo.ExId=ExamType.ExId where BatchId ='" + BatchaId + "' order by Ordering Asc");
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
        public static void GetExamIdListWithExInSl(DropDownList ddl, string BatchaId) // created by nayem. date: 16-12-2017. purpose: all exam info loading in dropdownlist
        {

            DataTable dt = new DataTable();
            if(BatchaId=="All")
                dt = CRUD.ReturnTableNull("select ExInSl,ExInId from ExamInfo inner join ExamType on ExamInfo.ExId=ExamType.ExId order by ExInSl desc");
            else
                dt = CRUD.ReturnTableNull("select ExInSl,ExInId from ExamInfo inner join ExamType on ExamInfo.ExId=ExamType.ExId where BatchId ='" + BatchaId + "' order by Ordering Asc");
            List<ExamInfoEntity> GetList = new List<ExamInfoEntity>();
            if (dt.Rows.Count > 0)
            {


                GetList = (from DataRow dr in dt.Rows
                           select new ExamInfoEntity
                           {ExInSl= int.Parse(dr["ExInSl"].ToString()),
                               ExInId = dr["ExInId"].ToString(),

                           }).ToList();
            }



            ddl.DataSource = GetList.ToList();
            ddl.DataTextField = "ExInId";
            ddl.DataValueField = "ExInSl";
            ddl.DataBind();
            ddl.Items.Insert(0, new ListItem("...Select...", "0"));

        }
        public static void GetExamIdListWithExInSl(DropDownList ddl, string BatchaId, string ClsGrpID) // created by nayem. date: 12-01-2020. purpose: all exam info loading in dropdownlist
        {

            DataTable dt = new DataTable();

            dt = CRUD.ReturnTableNull("select ExInSl,ExInId from ExamInfo inner join ExamType on ExamInfo.ExId=ExamType.ExId where BatchId ='" + BatchaId + "' and ClsGrpID='" + ClsGrpID + "' order by Ordering Asc");
            List<ExamInfoEntity> GetList = new List<ExamInfoEntity>();
            if (dt.Rows.Count > 0)
            {


                GetList = (from DataRow dr in dt.Rows
                           select new ExamInfoEntity
                           {
                               ExInSl = int.Parse(dr["ExInSl"].ToString()),
                               ExInId = dr["ExInId"].ToString(),

                           }).ToList();
            }
            ddl.DataSource = GetList.ToList();
            ddl.DataTextField = "ExInId";
            ddl.DataValueField = "ExInSl";
            ddl.DataBind();
            ddl.Items.Insert(0, new ListItem("...Select...", "0"));

        }
        public static void GetExamIdList(DropDownList ddl, string BatchaId, string ClsGrpID) // created by nayem. date: 12-01-2020. purpose: all exam info loading in dropdownlist
        {

            DataTable dt = new DataTable();

            dt = CRUD.ReturnTableNull("select ExInId from ExamInfo inner join ExamType on ExamInfo.ExId=ExamType.ExId where BatchId ='" + BatchaId + "' and ClsGrpID='" + ClsGrpID + "' order by Ordering Asc");
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
        public static void GetExamIdListWithoutQuiz(DropDownList ddl, string BatchaId) // created by nayem. date: 04-02-2018. purpose: all exam info without Quiz loading in dropdownlist
        {

            DataTable dt = new DataTable();

            dt = CRUD.ReturnTableNull("select ExInId,ExInId+' | '+case when ExamInfo.ExName is null then '' else ExamInfo.ExName end as ExName from ExamInfo inner join ExamType on ExamInfo.ExId=ExamType.ExId and ExamType.SemesterExam is not null where BatchId ='" + BatchaId + "' order by ExStartDate desc");
            List<ExamInfoEntity> GetList = new List<ExamInfoEntity>();
            if (dt.Rows.Count > 0)
            {


                GetList = (from DataRow dr in dt.Rows
                           select new ExamInfoEntity
                           {
                               ExInId = dr["ExInId"].ToString(),
                               ExName = dr["ExName"].ToString()
                           }).ToList();
            }



            ddl.DataSource = GetList.ToList();
            ddl.DataTextField = "ExName";
            ddl.DataValueField = "ExInId";
            ddl.DataBind();
            ddl.Items.Insert(0, new ListItem("Select", "0"));

        }
        public static void GetExamIdListWithoutQuiz(DropDownList ddl, string BatchaId,string ClsGrpID) // created by nayem. date: 04-02-2018. purpose: all exam info without Quiz loading in dropdownlist
        {

            DataTable dt = new DataTable();

            dt = CRUD.ReturnTableNull("select ExInId,ExInId+' | '+case when ExamInfo.ExName is null then '' else ExamInfo.ExName end as ExName from ExamInfo inner join ExamType on ExamInfo.ExId=ExamType.ExId and ExamType.SemesterExam is not null where BatchId ='" + BatchaId + "' and ClsGrpID ="+ ClsGrpID + " order by ExStartDate desc");
            List<ExamInfoEntity> GetList = new List<ExamInfoEntity>();
            if (dt.Rows.Count > 0)
            {


                GetList = (from DataRow dr in dt.Rows
                           select new ExamInfoEntity
                           {
                               ExInId = dr["ExInId"].ToString(),
                               ExName = dr["ExName"].ToString()
                           }).ToList();
            }



            ddl.DataSource = GetList.ToList();
            ddl.DataTextField = "ExName";
            ddl.DataValueField = "ExInId";
            ddl.DataBind();
            ddl.Items.Insert(0, new ListItem("Select", "0"));

        }
        public static void GetExName(DropDownList ddl,string BatchaId)
        {
            try
            {
                DataTable dt = new DataTable();

                dt = CRUD.ReturnTableNull("SELECT ExamType.ExName +'_'+ BatchInfo.BatchName ExName,ExamInfo.ExInId  FROM ExamInfo "
                + "INNER JOIN BatchInfo ON ExamInfo.BatchId=BatchInfo.BatchId INNER JOIN ExamType ON ExamInfo.ExId=ExamType.ExId WHERE ExamInfo.BatchId='"+BatchaId+"'");




                ddl.DataSource = dt;
                ddl.DataTextField = "ExName";
                ddl.DataValueField = "ExInId";
                ddl.DataBind();
                ddl.Items.Insert(0, new ListItem("Select", "0"));
            }
            catch { }
        }
        public static void getExamByBatchName(DropDownList ddl, string BatchaName)
        {
            try
            {
                DataTable dt = new DataTable();
                dt = CRUD.ReturnTableNull("select ExInId,ExName+( case when GroupName is null then '' else ' ('+GroupName+')' end) as ExName from v_ExamInfo where BatchId in (select BatchId from BatchInfo where BatchName='" + BatchaName+"') and IsPublished=1");
                ddl.DataSource = dt;
                ddl.DataTextField = "ExName";
                ddl.DataValueField = "ExInId";
                ddl.DataBind();
                ddl.Items.Insert(0, new ListItem("Select", "0"));
            }
            catch { }
        }
        public static void getExamByBatchNameWithExInSl(DropDownList ddl, string BatchaName)
        {
            try
            {
                DataTable dt = new DataTable();
                dt = CRUD.ReturnTableNull("select ExInSl,ExInId,ExName+( case when GroupName is null then '' else ' ('+GroupName+')' end) as ExName from v_ExamInfo where BatchId in (select BatchId from BatchInfo where BatchName='" + BatchaName + "') and IsPublished=1");
                ddl.DataSource = dt;
                ddl.DataTextField = "ExName";
                ddl.DataValueField = "ExInSl";
                ddl.DataBind();
                ddl.Items.Insert(0, new ListItem("Select", "0"));
            }
            catch { }
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
