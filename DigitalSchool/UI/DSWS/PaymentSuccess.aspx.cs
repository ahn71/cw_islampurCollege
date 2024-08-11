using DS.BLL.Examinition;
using DS.DAL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DS.UI.DSWS
{
    public partial class PaymentSuccess : System.Web.UI.Page
    {
        ExamineeEntry examineeEntry;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                string[] OrderNo = HttpContext.Current.Request.Url.AbsolutePath.ToString().Split('/');
                ViewState["__InvoiceNo__"] = OrderNo[OrderNo.Length - 1].ToString();
                DataTable dt = new DataTable();
                dt = CRUD.ReturnTableNull("select ct.ExInSl,p.StudentId,cs.ClsSecID,cs.ClsGrpID,p.BatchID from PaymentInfo p left join FeesCategoryInfo ct on p.FeeCatId=ct.FeeCatId inner join ExamInfo xm on ct.ExInSl=xm.ExInSl inner join CurrentStudentInfo cs on p.StudentId=cs.StudentId and p.BatchID=cs.BatchID where p.IsPaid=1 and p.status='success' and p.OrderNo='" + ViewState["__InvoiceNo__"] .ToString()+ "'");
                if (dt != null && dt.Rows.Count > 0)
                {
                    saveExaminee(dt.Rows[0]["ExInSl"].ToString(), dt.Rows[0]["StudentId"].ToString(), dt.Rows[0]["ClsSecID"].ToString(), dt.Rows[0]["ClsGrpID"].ToString(), dt.Rows[0]["BatchID"].ToString());
                }

            }
        }

        protected void btnPreview_Click(object sender, EventArgs e)
        {
            Response.Redirect("http://islampurcollege.edu.bd/payment/invoice/" + ViewState["__InvoiceNo__"].ToString());
        }
        private void saveExaminee(string ExamID, string StudentID, string ClsSecID, string ClsGrpID, string BatchID)
        {
            try {
                if (examineeEntry == null)
                    examineeEntry = new ExamineeEntry();
                examineeEntry.insert(ExamID, StudentID, ClsSecID, ClsGrpID, BatchID);
            }
            catch (Exception ex)
            {
            }
        }
      
    }
}