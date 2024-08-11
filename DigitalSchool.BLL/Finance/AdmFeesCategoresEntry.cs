using DS.DAL;
using DS.PropertyEntities.Model.Finance;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Web.UI.WebControls;

namespace DS.BLL.Finance
{
    public class AdmFeesCategoresEntry : IDisposable
    {
        private AdmFeesCategoriesEntities _Entities;
        string sql = string.Empty;
        bool result = true;
        static List<AdmFeesCategoriesEntities> grpList;
        public AdmFeesCategoresEntry() { }
        public AdmFeesCategoriesEntities AddEntities
        {
            set
            {
                _Entities = value;
            }
        }

        public bool Insert()
        {
            sql = string.Format("INSERT INTO [dbo].[Tbl_Adm_FeesCategory] " +
                "([FeeCatName],[ClassID],[DateOfCreation],[FeeAmount]) VALUES (" +
                "'" + _Entities.FeeCatName + "', " +
                "'" + _Entities.ClassID + "' ," +
                "'" + _Entities.DateOfCreation + "', " +
                "'" + _Entities.FeeAmount + "'); " +
                " SELECT SCOPE_IDENTITY()");
            int MaxId = CRUD.GetMaxID(sql);
            if (MaxId > 0)
            {
                _Entities.AdmFeeCatId = MaxId;
                result = InsertDateofPayment();
            }
            return result;          
        }
        public bool InsertDateofPayment()
        {
            sql = string.Format("INSERT INTO [dbo].[Tbl_Adm_DateOfPayment] " +
                "([AdmFeeCatId],[DateOfStart],[DateOfEnd],[IsActive]) VALUES (" +
                "'" + _Entities.AdmFeeCatId + "', " +
                "'" + _Entities.DateOfStart + "', " +
                "'" + _Entities.DateOfEnd + "', " +
                "'" + _Entities.IsActive + "')");
            return result = CRUD.ExecuteQuery(sql);
        }
        public bool Update()
        {
            sql = string.Format("UPDATE [dbo].[Tbl_Adm_FeesCategory] SET " +
                "[FeeCatName] = '" + _Entities.FeeCatName + "', " +
                "[ClassID] = '" + _Entities.ClassID + "', " +
                "[DateOfCreation] = '" + _Entities.DateOfCreation + "', " +
                "[FeeAmount] = '" + _Entities.FeeAmount + "' " +
                "WHERE [AdmFeeCatId] = '" + _Entities.AdmFeeCatId + "'");
            result = CRUD.ExecuteQuery(sql);
            if (result == true)
            {
              result=UpdateDateofPayment();
            }
            return result;
        }
        public bool UpdateDateofPayment()
        {
            sql = string.Format("UPDATE [dbo].[Tbl_Adm_DateOfPayment] SET " +                
                "[DateOfStart] = '" + _Entities.DateOfStart + "' ," +
                "[DateOfEnd] = '" + _Entities.DateOfEnd + "', " +
                "[IsActive] = '" + _Entities.IsActive + "' " +
                "WHERE [AdmFeeCatId] = '" + _Entities.AdmFeeCatId + "'");
            return result = CRUD.ExecuteQuery(sql);
        }
        public List<AdmFeesCategoriesEntities> GetEntitiesData(string classId)
        {
            List<AdmFeesCategoriesEntities> ListEntities = new List<AdmFeesCategoriesEntities>();
            if (classId == "0")
            {
                sql = string.Format("SELECT [Tbl_Adm_FeesCategory].[AdmFeeCatId],[Tbl_Adm_FeesCategory]."
                    + "[FeeCatName],[Tbl_Adm_FeesCategory].[ClassID],[Classes].ClassName,[Tbl_Adm_DateOfPayment].[DateOfStart],"
                    + "[Tbl_Adm_DateOfPayment].[DateOfEnd],[Tbl_Adm_FeesCategory].[DateOfCreation],"
                    + "[Tbl_Adm_FeesCategory].[FeeAmount] FROM [dbo].[Tbl_Adm_FeesCategory] INNER JOIN [Classes]"
                + " ON [dbo].[Tbl_Adm_FeesCategory].[ClassID]=[Classes].[ClassID] INNER JOIN Tbl_Adm_DateOfPayment ON "
                + " Tbl_Adm_FeesCategory.AdmFeeCatId=Tbl_Adm_DateOfPayment.AdmFeeCatId");
            }
            else
            {
                sql = string.Format("SELECT [Tbl_Adm_FeesCategory].[AdmFeeCatId],[Tbl_Adm_FeesCategory]."
                        + "[FeeCatName],[Tbl_Adm_FeesCategory].[ClassID],[Classes].ClassName,[Tbl_Adm_DateOfPayment].[DateOfStart],"
                        + "[Tbl_Adm_DateOfPayment].[DateOfEnd],[Tbl_Adm_FeesCategory].[DateOfCreation],"
                        + "[Tbl_Adm_FeesCategory].[FeeAmount] FROM [dbo].[Tbl_Adm_FeesCategory] INNER JOIN [Classes]"
                        + " ON [dbo].[Tbl_Adm_FeesCategory].[ClassID]=[Classes].[ClassID] INNER JOIN Tbl_Adm_DateOfPayment ON "
                        + " Tbl_Adm_FeesCategory.AdmFeeCatId=Tbl_Adm_DateOfPayment.AdmFeeCatId where Tbl_Adm_FeesCategory.ClassID='" + classId + "'");
            }
            DataTable dt = new DataTable();

            dt = CRUD.ReturnTableNull(sql);
            if (dt != null)
            {
                if (dt.Rows.Count > 0)
                {
                    ListEntities = (from DataRow row in dt.Rows
                                    select new AdmFeesCategoriesEntities
                                    {
                                        AdmFeeCatId = int.Parse(row["AdmFeeCatId"].ToString()),
                                        FeeCatName = row["FeeCatName"].ToString(),
                                        ClassID = int.Parse(row["ClassID"].ToString()),
                                        ClassName = row["ClassName"].ToString(),
                                        DateOfStart = Convert.ToDateTime(row["DateOfStart"].ToString()),
                                        DateOfEnd = Convert.ToDateTime(row["DateOfEnd"].ToString()),
                                        DateOfCreation =DateTime.Parse( row["DateOfCreation"].ToString()),
                                        FeeAmount =double.Parse(row["FeeAmount"].ToString())                                       
                                    }).ToList();
                    return ListEntities;
                }
            }
            return ListEntities = null;
        }
        public static void GetDropDownList(DropDownList dl,string classId)
        {
            AdmFeesCategoresEntry clsGrp = new AdmFeesCategoresEntry();           
            grpList = clsGrp.GetEntitiesData(classId).OrderByDescending(c=>c.AdmFeeCatId).ToList();            
            dl.DataTextField = "FeeCatName";
            dl.DataValueField = "AdmFeeCatId";
            if(grpList!=null)
            { 
            dl.DataSource = grpList.ToList();
            dl.DataBind();
            }
            else
            {
                dl.Items.Clear();
            }
            dl.Enabled = true;

        }
        public string SearchCondition(string shiftID,string clsID,string AdmfeeCatID)
        {
            string condition = "";
            if (shiftID == "All" && clsID == "All" && AdmfeeCatID == "All")
            {
                condition = "";
            }
            else if (shiftID == "All" && clsID == "All" && AdmfeeCatID != "All")
            {
                condition = " WHERE FeeCatId='" + AdmfeeCatID + "' AND PayStatus='True' AND BatchID='0'";
            }
            else if (shiftID == "All" && clsID != "All" && AdmfeeCatID == "All")
            {
                condition = " WHERE ClassID='" + clsID + "' AND PayStatus='True' AND BatchID='0'";
            }
            else if (shiftID == "All" && clsID != "All" && AdmfeeCatID != "All")
            {
                condition = " WHERE ClassID='" + clsID + "' AND FeeCatId='" + AdmfeeCatID 
                            + "' AND PayStatus='True' AND BatchID='0'";
            }
            else if (shiftID != "All" && clsID == "All" && AdmfeeCatID == "All")
            {
                condition = " WHERE ShiftID='" + shiftID + "' AND PayStatus='True' AND BatchID='0'";
            }
            else if (shiftID != "All" && clsID == "All" && AdmfeeCatID != "All")
            {
                condition = " WHERE ShiftID='" + shiftID + "' AND FeeCatId='" + AdmfeeCatID
                            + "' AND PayStatus='True' AND BatchID='0'";
            }
            else if (shiftID != "All" && clsID != "All" && AdmfeeCatID == "All")
            {
                condition = " WHERE ShiftID='" + shiftID + "' AND ClassID='" + clsID 
                            + "' AND PayStatus='True' AND BatchID='0'";
            }
            else if (shiftID != "All" && clsID != "All" && AdmfeeCatID != "All")
            {
                condition = " WHERE ShiftID='" + shiftID + "' AND ClassID='" + clsID + "' AND FeeCatId='" 
                            + AdmfeeCatID + "' AND PayStatus='True' AND BatchID='0'";
            }
            return condition;
        }
        public   DataTable  AdmMoneyReceiptReportData(string StudentId) 
        {

            sql = "Select StudentId,DateOfPayment,TransactionNo,DiscountTK,DueAmount,PId,PName,AdmFeeCatId,Amount,FeeCatName,RollNo,FeeAmount,DateOfPayment,FullName,ClassName,SectionName,GroupName,ShiftName FROM v_AdmissionStudent where StudentId='" + StudentId + "'" +
                " union "+
                "SELECT (select distinct StudentId FROM v_AdmissionStudent where StudentId='" + StudentId + "'),DateOfPayment,TransactionNo,DiscountTK,DueAmount ,PId,PName,AdmFeeCatId,Amount,FeeCatName,RollNo,FeeAmount,DateOfPayment,FullName,ClassName,SectionName,GroupName,ShiftName   FROM v_AdmissionStudent where  AdmFeeCatId is null order by AdmFeeCatId desc ";
            DataTable dt = new DataTable();
            dt = CRUD.ReturnTableNull(sql);
            return dt;
        }
        public DataTable MoneyReceiptReportData(string StudentId,string feecatagoryid,string stdtypeid)
        {

            sql = "Select StudentId,DateOfPayment,TransactionNo,DiscountTK,DueAmount,PId,PName,FeeCatId,Amount,FeeCatName,RollNo,FeeAmount,DateOfPayment,FullName,ClassName,SectionName,GroupName,ShiftName,AmountPaid FROM v_StudentMoneyReceipt where StudentId='" + StudentId + "' and FeeCatId='" + feecatagoryid + "' and StdTypeId='" + stdtypeid + "'" +
                " union " +
                "SELECT (select distinct StudentId FROM v_StudentMoneyReceipt where StudentId='" + StudentId + "' and FeeCatId='" + feecatagoryid + "' and StdTypeId='" + stdtypeid + "'),DateOfPayment,TransactionNo,(select distinct DiscountTK FROM v_StudentMoneyReceipt where StudentId='" + StudentId + "' and FeeCatId='" + feecatagoryid +
                "' and StdTypeId='" + stdtypeid + "'),(select distinct DueAmount FROM v_StudentMoneyReceipt where StudentId='" + StudentId + "' and FeeCatId='" + feecatagoryid + "' and StdTypeId='" + stdtypeid + "') ,PId,PName,FeeCatId,Amount,FeeCatName,RollNo,FeeAmount,DateOfPayment,FullName,ClassName,SectionName,GroupName,ShiftName,(select distinct AmountPaid FROM v_StudentMoneyReceipt where StudentId='" + StudentId +
                "' and FeeCatId='" + feecatagoryid + "' and StdTypeId='" + stdtypeid + "')   FROM v_StudentMoneyReceipt where  FeeCatId is null union select StudentId,DateOfPayment, null TransactionNo ,DiscountTK,DueAmount,null PId,OthersParticular PName,FeeCatId,OthersAmount Amount,null FeeCatName,RollNo,null FeeAmount,DateOfPayment,null FullName,null ClassName,null SectionName,null GroupName,null ShiftName,AmountPaid " +
                " from StudentPayment where  StudentId='" + StudentId + "' and FeeCatId='" + feecatagoryid + "' and StdTypeId='" + stdtypeid + "' union select StudentId,DateOfPayment, null TransactionNo ,DiscountTK,DueAmount,null PId,'Previous due' PName,FeeCatId,PreDueAmount Amount,null FeeCatName,RollNo,null FeeAmount,DateOfPayment,null FullName,null ClassName,null SectionName,null GroupName,null ShiftName,AmountPaid " +
                " from StudentPayment where  StudentId='" + StudentId + "' and FeeCatId='" + feecatagoryid + "' and StdTypeId='" + stdtypeid + "' order by FeeCatId desc ,PId desc";
            DataTable dt = new DataTable();
            dt = CRUD.ReturnTableNull(sql);
            return dt;
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
