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
            sql = string.Format("SELECT FeeCatId,FeeCatName FROM FeesCategoryInfo WHERE BatchId='" + batchID + "'");
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
            +"FORMAT(DateOfPayment,'dd-MM-yyyy') AS DateOfPayment,StudentId,AmountPaid,"
            +"FineAmount,DiscountTK,FeeAmount,ShiftName,BatchName,GroupName FROM v_StudentPaymentInfo  " + condition + "");
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
