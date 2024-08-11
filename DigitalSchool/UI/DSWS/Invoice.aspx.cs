using DS.BLL.SMS;
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
    public partial class Invoice : System.Web.UI.Page
    {
        DataTable dt;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                string[] OrderNo = HttpContext.Current.Request.Url.AbsolutePath.ToString().Split('/');
                loadInvoice(OrderNo[OrderNo.Length-1]);
                

            }
        }
        private void loadInvoice(string OrderNo)
        {
            try {
                dt = new DataTable();
                dt = CRUD.ReturnTableNull("select StudentId,AdmissionFormNo,OpenStudentId from PaymentInfo where OrderNo='" + OrderNo + "' and IsPaid=1");
                if (dt != null && dt.Rows.Count > 0)
                {
                    string AdmissionNoHead = "Admission No";
                    if (dt.Rows[0]["OpenStudentId"].ToString() != "")// Open Payment
                    {
                        AdmissionNoHead = "Reg. No";
                        pOpenPayment.Visible = true;
                        dt = new DataTable();
                        dt = CRUD.ReturnTableNull(" select op.RegNo as AdmissionNo,op.StudentName as FullName,ClassName,GroupName,op.Year,op.RollNo, p.OrderNo,fee.FeeCatName,pd.Particular,pd.ParticularAmount,convert(varchar(10),p.CreatedAt,105) as PaymentDate,P.Amount,P.Discount,P.OnlineCharge,P.TotalAmount,p.PaymentType from PaymentInfo p left join PaymentOpenStudentInfo op  on p.OpenStudentId=op.id  left join Classes c on op.ClassID=c.ClassID left join Tbl_Class_Group cg on op.ClsGrpID=cg.ClsGrpID left join Tbl_Group g on cg.GroupID=g.GroupID left join FeesCategoryInfo fee on p.FeeCatId=fee.FeeCatId left join PaymentInfoDetails pd on p.OrderID=pd.OrderID where p.OrderNo='" + OrderNo + "' order by pd.SL");
                    }
                   else if (dt.Rows[0]["StudentId"].ToString() == "") // Admission Payment
                    {
                        dt = new DataTable();
                        dt = CRUD.ReturnTableNull(" select p.AdmissionFormNo as AdmissionNo,FullName,ClassName,GroupName,adm.AdmissionYear as Year,'' as RollNo, p.OrderNo,fee.FeeCatName,pd.Particular,pd.ParticularAmount,convert(varchar(10),p.CreatedAt,105) as PaymentDate,P.Amount,P.Discount,P.OnlineCharge,P.TotalAmount,p.PaymentType from PaymentInfo p left join Student_AdmissionFormInfo adm on p.AdmissionFormNo=adm.AdmissionFormNo  left join Classes c on adm.ClassID=c.ClassID left join Tbl_Class_Group cg on adm.ClsGrpID=cg.ClsGrpID left join Tbl_Group g on cg.GroupID=g.GroupID left join FeesCategoryInfo fee on p.FeeCatId=fee.FeeCatId left join PaymentInfoDetails pd on p.OrderID=pd.OrderID where p.OrderNo='" + OrderNo + "' order by pd.SL");
                    }
                    else // Regular Payment
                    {
                        dt = new DataTable();
                        dt = CRUD.ReturnTableNull("select AdmissionNo,FullName,ClassName,GroupName,Year,RollNo, p.OrderNo,fee.FeeCatName,pd.Particular,pd.ParticularAmount,convert(varchar(10),p.CreatedAt,105) as PaymentDate,P.Amount,P.Discount,P.OnlineCharge,P.TotalAmount,p.PaymentType from PaymentInfo p left join v_CurrentStudentInfo ci on p.StudentId=ci.StudentId and p.BatchID=ci.BatchID left join FeesCategoryInfo fee on p.FeeCatId=fee.FeeCatId left join PaymentInfoDetails pd on p.OrderID=pd.OrderID where  p.OrderNo='" + OrderNo + "' order by pd.SL");
                    }

                    if (dt != null && dt.Rows.Count > 0)
                    {
                        lblInvoiceNo2.Text=lblInvoiceNo.Text = dt.Rows[0]["OrderNo"].ToString();
                        lblDateOfPayment2.Text=lblDateOfPayment.Text = dt.Rows[0]["PaymentDate"].ToString();
                        lblAdmissionNoHead2.Text=lblAdmissionNoHead.Text = AdmissionNoHead;
                        lblAdmissionNo2.Text=lblAdmissionNo.Text = dt.Rows[0]["AdmissionNo"].ToString();
                        lblStudentName2.Text= lblStudentName.Text = dt.Rows[0]["FullName"].ToString();
                        lblClass2.Text=lblClass.Text = dt.Rows[0]["ClassName"].ToString();
                        lblGroup2.Text= lblGroup.Text = dt.Rows[0]["GroupName"].ToString();
                        lblClassRoll2.Text= lblClassRoll.Text = dt.Rows[0]["RollNo"].ToString();
                        lblYear2.Text=lblYear.Text = dt.Rows[0]["Year"].ToString();
                        string divInfo = @"<div style='margin-bottom: 10px;'><strong>Fee Category:</strong> " + dt.Rows[0]["FeeCatName"].ToString() + "</div>";
                        divInfo += @"<table class='table table-bordered'>
                        <thead>
                            <tr>
                                <th class='per70 text-center'>Description</th>
                                <th class='per25 text-right' style='width: 30%;'>Total</th>
                            </tr>
                        </thead>
                        <tbody>";
                        for (int i = 0; i < dt.Rows.Count; i++)
                        {
                            divInfo += @"<tr><td> " + dt.Rows[i]["Particular"].ToString() + " </td><td class='text-right'>" + dt.Rows[i]["ParticularAmount"].ToString() + "</td></tr>";
                        }
                        divInfo += @"</tbody>
                        <tfoot>
                            <tr>
                                <th class='text-right'>Sub Total</th>
                                <th class='text-right'>" + dt.Rows[0]["Amount"].ToString() + @" Tk</th>
                            </tr>
                            <tr>
                                <th class='text-right'>Discount</th>
                                <th class='text-right'>" + dt.Rows[0]["Discount"].ToString() + @" TK</th>
                            </tr>";
                        if (dt.Rows[0]["PaymentType"].ToString() != "ssl")
                                divInfo += @"<tr>
                                <th class='text-right'>Online Charge ("+ Math.Round(100 * Classes.commonTask.OnlineChargPer_Nagad, 2) + @" %)</th>
                                <th class='text-right'>" + dt.Rows[0]["OnlineCharge"].ToString() + @" TK</th>
                            </tr>";
                        divInfo += @"<tr>
                                <th class='text-right'>Total</th>
                                <th class='text-right'>" + dt.Rows[0]["TotalAmount"].ToString() + @" TK</th>
                            </tr>
                        </tfoot>
                    </table>";
                        divParticularCategoryList.Controls.Add(new LiteralControl(divInfo));
                        divParticularCategoryList2.Controls.Add(new LiteralControl(divInfo));
                    }
                }
                
            }
            catch(Exception ex) {  }
        }



    }
}