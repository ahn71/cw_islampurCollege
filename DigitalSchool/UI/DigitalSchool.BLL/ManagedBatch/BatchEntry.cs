using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DS.DAL;
using DS.PropertyEntities.Model.ManagedBatch;
using System.Data;
using System.Web.UI.WebControls;

namespace DS.BLL.ManagedBatch
{
    public class BatchEntry : IDisposable
    {
        private BatchEntities _Entities;
        private static List<BatchEntities> batchList;
        string sql = string.Empty;
        bool result = true;
        public BatchEntry() { }
        public BatchEntities AddEntities
        {
            set
            {
                _Entities = value;
            }
        }

        public bool Insert()
        {
            sql = string.Format("INSERT INTO [dbo].[BatchInfo] " +
                "([BatchName], [IsUsed],[Year],[ClassID]) VALUES ( '" + _Entities.BatchName + "', " +
                " '" + _Entities.IsUsed + "','"+_Entities.Year+"','"+_Entities.ClassId+"')");
            return result = CRUD.ExecuteQuery(sql);
        }
        public bool Update()
        {
            sql = string.Format("UPDATE [dbo].[BatchInfo] SET " +
                "[BatchName] = '" + _Entities.BatchName + "', " +
                "[IsUsed] = '" + _Entities.IsUsed + "' " +
                "[Year] = '" + _Entities.Year + "' " +
                "[ClassID] = '" + _Entities.ClassId + "' " +
                "WHERE [BatchId] = '" + _Entities.BatchId + "'");
            return result = CRUD.ExecuteQuery(sql);
        }
        public List<BatchEntities> GetEntitiesData()
        {
            List<BatchEntities> ListEntities = new List<BatchEntities>();
            sql = string.Format("SELECT [BatchId],[BatchName],[IsUsed],[Year],[ClassID] "
            + "FROM [dbo].[BatchInfo] ORDER BY [ClassID]");
            DataTable dt = new DataTable();

            dt = CRUD.ReturnTableNull(sql);
            if (dt != null)
            {
                if (dt.Rows.Count > 0)
                {
                    ListEntities = (from DataRow row in dt.Rows
                                    select new BatchEntities
                                    {
                                        BatchId = int.Parse(row["BatchId"].ToString()),
                                        BatchName = row["BatchName"].ToString(),
                                        IsUsed = bool.Parse(row["IsUsed"].ToString()),
                                        Year=int.Parse(row["Year"].ToString()),
                                        ClassId = int.Parse(row["ClassID"].ToString())
                                    }).ToList();
                    return ListEntities;
                }

            }
            return ListEntities = null;
        }       
        public DataTable GetSubQPatternEntitiesData()
        {

            sql = string.Format("SELECT DISTINCT CONVERT(varchar(20),bi."
            + "BatchId)+'_'+CONVERT(varchar(20),bi.ClassID) as BatchId,bi."
            + "BatchName FROM SubjectQuestionPattern sqp INNER JOIN BatchInfo"
            + " bi ON sqp.BatchId=bi.BatchId");
            DataTable dt = new DataTable();
            dt = CRUD.ReturnTableNull(sql);
            return dt;
        }
        public DataTable GetEntitiesData(string batchclassID)
        {
           
            sql = string.Format("SELECT CONVERT(varchar(20), BatchID)+'_'"
                +"+CONVERT(varchar(20), ClassID) as BatchId,[BatchName],[IsUsed],[Year],[ClassID] "
                + "FROM [dbo].[BatchInfo] WHERE [IsUsed]='" + batchclassID + "'  ORDER BY [ClassID]");
            DataTable dt = new DataTable();

            dt = CRUD.ReturnTableNull(sql);
            return dt;
        }
        public static void GetDropdownlist(DropDownList dl)
        {
            BatchEntry batch = new BatchEntry();
            if (batchList == null)
            {
                batchList = batch.GetEntitiesData().FindAll(c => c.IsUsed == true).ToList();
            }
            dl.DataValueField = "BatchId";
            dl.DataTextField = "BatchName";
            dl.DataSource = batchList;
            dl.DataBind();
            dl.Items.Insert(0, new ListItem("...Select...", "0"));
            dl.Items.Insert(1, new ListItem("All", "All"));
        }
        public static void GetDropdownlist(DropDownList dl,bool batchUsed)
        {
            BatchEntry batch = new BatchEntry();
            if (batch.GetEntitiesData() != null) { 
            batchList = batch.GetEntitiesData().FindAll(c => c.IsUsed == batchUsed).ToList();          
            dl.DataValueField = "BatchId";
            dl.DataTextField = "BatchName";
            dl.DataSource = batchList;
            dl.DataBind();
            dl.Items.Insert(0, new ListItem("...Select...", "0"));
            }
        }
        public static void GetDropdownlist(DropDownList dl,string BatchClassId)
        {
            BatchEntry batch = new BatchEntry();
            DataTable dt = batch.GetEntitiesData(BatchClassId);           
            dl.DataValueField = "BatchId";
            dl.DataTextField = "BatchName";
            dl.DataSource = dt;
            dl.DataBind();
            dl.Items.Insert(0, new ListItem("...Select...", "0"));
        }
        public static void GetSubQPatternDropdownlist(DropDownList dl)
        {
            BatchEntry batch = new BatchEntry();
            DataTable dt = batch.GetSubQPatternEntitiesData();
            dl.DataValueField = "BatchId";
            dl.DataTextField = "BatchName";
            dl.DataSource = dt;
            dl.DataBind();
            dl.Items.Insert(0, new ListItem("...Select...", "0"));
        }
        public static void loadGroupByBatchId(DropDownList ddl, string BatchId)
        {
            DataTable dt = new DataTable();
            dt = CRUD.ReturnTableNull("SELECT ClsGrpID,GroupName from v_Tbl_Class_Group where  BatchId =" + BatchId + " order by BatchId");
            ddl.DataValueField = "ClsGrpID";
            ddl.DataTextField = "GroupName";
            ddl.DataSource = dt;
            ddl.DataBind();
            ddl.Items.Insert(0, new ListItem("Select", "0"));
        }
        public static void GetBatchInfoForExamDependency(DropDownList ddl)
        {
            DataTable dt = new DataTable();
            dt = CRUD.ReturnTableNull("SELECT BatchId,BatchName from BatchInfo where IsUsed='1' "
            +"and BatchId not in (select distinct batchId from v_ExamDependencyInfo) order by BatchId");
            ddl.DataValueField = "BatchId";
            ddl.DataTextField = "BatchName";
            ddl.DataSource = dt;
            ddl.DataBind();
            ddl.Items.Insert(0, new ListItem("Select", "0"));
        }
        public static void GetBatchForNewStdAssign(DropDownList dl,int clsID)
        {
            DataTable dt = new DataTable();
            dt = CRUD.ReturnTableNull("SELECT BatchId,BatchName from BatchInfo where IsUsed='1' and ClassID='" + clsID + "'");
            dl.DataValueField = "BatchId";
            dl.DataTextField = "BatchName";
            dl.DataSource = dt;
            dl.DataBind();
            dl.Items.Insert(0, new ListItem("...Select...", "0"));
        }
        public static void GetUpperClassBatch(DropDownList dl,string classID)
        {
            DataTable dt = new DataTable();
            dt = CRUD.ReturnTableNull("SELECT CONVERT(varchar(20), BatchID)+'_'"
                + "+CONVERT(varchar(20), ClassID) as BatchId,BatchName from v_BatchInfo where IsUsed='1'"
                + "AND ClassOrder>(SELECT ClassOrder FROM Classes WHERE ClassID='" + classID + "') ");
            dl.DataValueField = "BatchId";
            dl.DataTextField = "BatchName";
            dl.DataSource = dt;
            dl.DataBind();
            dl.Items.Insert(0, new ListItem("...Select...", "0"));
        }
        public static void GetSameClassBatch(DropDownList dl, string classID,string batchID)
        {
            DataTable dt = new DataTable();
            dt = CRUD.ReturnTableNull("SELECT CONVERT(varchar(20), BatchID)+'_'"
                + "+CONVERT(varchar(20), ClassID) as BatchId,BatchName from v_BatchInfo where IsUsed='1'"
                + "AND ClassID='" + classID + "' AND BatchID!='"+batchID+"' ");
            dl.DataValueField = "BatchId";
            dl.DataTextField = "BatchName";
            dl.DataSource = dt;
            dl.DataBind();
            if (dt.Rows.Count == 0)
            {
                dl.Items.Insert(0,new ListItem("...Select...","0"));
            }
        }
        public static void GetStdBatch(DropDownList dl,string stdId)
        {
            try
            {
                DataTable dt = new DataTable();
                dt = CRUD.ReturnTableNull("SELECT BatchId,BatchName FROM v_CurrentStudentInfo "
                +"WHERE StudentId='"+stdId+"' Union SELECT CurrentStudent_Log.BatchID,BatchInfo.BatchName "
                +"FROM CurrentStudent_Log INNER JOIN BatchInfo ON CurrentStudent_Log.BatchID=BatchInfo."
                +"BatchId WHERE CurrentStudent_Log.StudentId='"+stdId+"'  ");
                dl.DataValueField = "BatchId";
                dl.DataTextField = "BatchName";
                dl.DataSource = dt;
                dl.DataBind();
                if (dt.Rows.Count > 1)
                {
                    dl.Items.Insert(0, new ListItem("...All...", "All"));
                }
                else
                {
                    dl.Items.Insert(0, new ListItem("...Select...", "0"));
                }
            }
            catch { }
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
