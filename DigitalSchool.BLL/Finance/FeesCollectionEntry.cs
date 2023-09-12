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
    public class FeesCollectionEntry:IDisposable
    {
        private FeesCollectionEntities _Entities;
        string sql = string.Empty;
        private DataTable dt;
        public FeesCollectionEntry() { }
        public FeesCollectionEntities AddEntities
        {
            set
            {
                _Entities = value;
            }
        }
        public void LoadFeesCategory(DropDownList dl,string batchID)
        {
            if (batchID == "All")
            {
                sql = string.Format("SELECT FeeCatId,FeeCatName FROM FeesCategoryInfo ");
            }
            else
            {
                sql = string.Format("SELECT FeeCatId,FeeCatName FROM FeesCategoryInfo WHERE BatchId='" + batchID + "'");
            }
            dt = CRUD.ReturnTableNull(sql);
            dl.DataSource = dt;
            dl.DataTextField = "FeeCatName";
            dl.DataValueField = "FeeCatId";
            dl.DataBind();
            dl.Items.Insert(0,new ListItem("...Select...","0"));
        }
        public DataTable LoadFeeCollection(string condition)
        {
            dt = new DataTable();
            dt = CRUD.ReturnTableNull("SELECT ClassName,FeeCatId,FeeCatName,SectionName,"
            + "convert(varchar(11),DateOfPayment,105) AS DateOfPayment,StudentId,AmountPaid,"
            + "FineAmount,DiscountTK,FeeAmount,ShiftName,BatchName,GroupName,StdTypeName,IdCard,DueAmount,FullName,RollNo,StdTypeId,ClassName,GrandTotal FROM v_StudentPaymentInfo  " + condition + "");
            return dt;
        }
        public DataTable LoadFeeCollectionDetails(string condition)
        {
            dt = new DataTable();
            dt = CRUD.ReturnTableNull("select StudentId, RollNo, AmountPaid, FullName, ClassName, DiscountTK, GrandTotal, FeeCatId, StudentId, ShiftID, ShiftName,format(DateOfPayment,'dd-MM-yyyy') as DateOfPayment  FROM   v_StudentPaymentInfo " + condition + "");
            return dt;
        }
        public DataTable LoadFeeCollectionDetailsParticular(string condition)
        {
            dt = new DataTable();
            dt = CRUD.ReturnTableNull("Select StudentId,PName,FeeCatId,Amount, PId FROM v_StudentPaymentDetails  " + condition + " union select StudentId,OthersParticular PName,FeeCatId,OthersAmount Amount,Null PId from StudentPayment " + condition +
                " union select StudentId,'Previous due' PName,FeeCatId,PreDueAmount Amount,Null PId from StudentPayment " + condition + " order by PId desc");
            return dt;
        }
        public DataTable LoadDueList(string condition)
        {
            dt = new DataTable();
            dt = CRUD.ReturnTableNull("SELECT FullName,RollNo,ShiftName,BatchName,GroupName,"
            +"SectionName,FeeCatName,FeeAmount FROM v_StudentPaymentInfo "+condition+"");
            return dt;
        }
        public DataTable LoadAdmCollection(string condition)
        {
            dt = new DataTable();
            dt = CRUD.ReturnTableNull("SELECT StudentId, ShiftName,ClassName,AmountPaid, FeeCatName,"
            +"FeeAmount FROM v_Adm_StudentPaymentInfo  " + condition + "");
            return dt;
        }
        public string GetSearchCondition(string shiftID, string batchID, string groupID, string sectionID)
        {
            string condition = "";
            string[] BatchClsID = batchID.Split('_');
            if (shiftID == "All" && batchID == "All" && groupID == "All" && sectionID == "All")
            {
                condition = "";
            }
            else if (shiftID == "All" && batchID == "All" && groupID == "All" && sectionID != "All")
            {
                condition = " WHERE ClsSecID='" + sectionID + "'";
            }
            else if (shiftID == "All" && batchID == "All" && groupID != "All" && sectionID == "All")
            {
                condition = " WHERE ClsGrpID='" + groupID + "'";
            }
            else if (shiftID == "All" && batchID == "All" && groupID != "All" && sectionID != "All")
            {
                condition = " WHERE ClsGrpID='" + groupID + "' AND ClsSecID='" + sectionID + "'";
            }
            else if (shiftID == "All" && batchID != "All" && groupID == "All" && sectionID == "All")
            {
                condition = " WHERE BatchID='" + BatchClsID[0] + "'";
            }
            else if (shiftID == "All" && batchID != "All" && groupID == "All" && sectionID != "All")
            {
                condition = " WHERE BatchID='" + BatchClsID[0] + "' AND ClsSecID='" + sectionID + "'";
            }
            else if (shiftID == "All" && batchID != "All" && groupID != "All" && sectionID == "All")
            {
                condition = " WHERE BatchID='" + BatchClsID[0] + "' AND ClsGrpID='" + groupID + "'";
            }
            else if (shiftID == "All" && batchID != "All" && groupID != "All" && sectionID != "All")
            {
                condition = " WHERE BatchID='" + BatchClsID[0] + "' AND ClsGrpID='"
                + groupID + "' AND ClsSecID='" + sectionID + "'";
            }
            else if (shiftID != "All" && batchID == "All" && groupID == "All" && sectionID == "All")
            {
                condition = " WHERE ShiftID='" + shiftID + "'";
            }
            else if (shiftID != "All" && batchID == "All" && groupID == "All" && sectionID != "All")
            {
                condition = " WHERE ShiftID='" + shiftID + "' AND ClsSecID='" + sectionID + "'";
            }
            else if (shiftID != "All" && batchID == "All" && groupID != "All" && sectionID == "All")
            {
                condition = " WHERE ShiftID='" + shiftID + "' AND ClsGrpID='" + groupID + "'";
            }
            else if (shiftID != "All" && batchID == "All" && groupID != "All" && sectionID != "All")
            {
                condition = " WHERE ShiftID='" + shiftID + "' AND ClsGrpID='"
                + groupID + "' AND ClsSecID='" + sectionID + "'";
            }
            else if (shiftID != "All" && batchID != "All" && groupID == "All" && sectionID == "All")
            {
                condition = " WHERE  ShiftID='" + shiftID + "' AND BatchID='" + BatchClsID[0] + "' ";
            }
            else if (shiftID != "All" && batchID != "All" && groupID == "All" && sectionID != "All")
            {
                condition = " WHERE ShiftID='" + shiftID + "' AND BatchID='"
                + BatchClsID[0] + "' AND ClsSecID='" + sectionID + "'";
            }
            else if (shiftID != "All" && batchID != "All" && groupID != "All" && sectionID == "All")
            {
                condition = " WHERE ShiftID='" + shiftID + "' AND BatchID='"
                + BatchClsID[0] + "' AND ClsGrpID='" + groupID + "' ";
            }
            else if (shiftID != "All" && batchID != "All" && groupID != "All" && sectionID != "All")
            {
                condition = " WHERE ShiftID='" + shiftID + "' AND BatchID='" + BatchClsID[0]
                + "' AND ClsGrpID='" + groupID + "' AND ClsSecID='" + sectionID + "' ";
            }
            return condition;
        }
        public  void LoadStudentFessCategory(DropDownList dl,string stdID)
        {
            try
            {
                dt = CRUD.ReturnTableNull("SELECT DISTINCT FeeCatId,FeeCatName FROM v_StudentPaymentInfo WHERE StudentId='" + stdID + "'");
                dl.DataSource = dt;
                dl.DataTextField = "FeeCatName";
                dl.DataValueField = "FeeCatId";
                dl.DataBind();
                dl.Items.Insert(0,new ListItem("...Select...","0"));
            }
            catch { }
        }
        public void LoadStudentFessCategory(DropDownList dl, string stdID, string batchId)
        {
            try
            {
                dt = CRUD.ReturnTableNull("SELECT DISTINCT FeeCatId,FeeCatName FROM v_StudentPaymentInfo WHERE StudentId='" + stdID + "' AND BatchId='" + batchId + "'");
                dl.DataSource = dt;
                dl.DataTextField = "FeeCatName";
                dl.DataValueField = "FeeCatId";
                dl.DataBind();
                dl.Items.Insert(0, new ListItem("...Select...", "0"));
            }
            catch { }
        }
        public DataTable LoadStudentPaymentDetails(string condition)
        {
            dt = new DataTable();
            dt = CRUD.ReturnTableNull("SELECT StudentId,FeeCatId,RollNo,Format(DateOfPayment,'dd-MM-yyyy') "
                +"as DateOfPayment,AmountPaid,FineAmount,DiscountStatus,FeeCatName,FullName,ShiftID,ShiftName,"
            +"BatchID,BatchName,ClassID,ClassName,ClsGrpID,GroupName,ClsSecID,SectionName,DiscountTK,FeeAmount,"
            +"PId,PName,Amount FROM v_StudentPaymentDetails "+condition+"");
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
