using DS.BLL;
using DS.BLL.SMS;
using DS.DAL;
using DS.PropertyEntities.Model.SMS;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DS.UI.PaymentMethod.SSLCommerzInfos
{
    public partial class Success : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!String.IsNullOrEmpty(Request.Form["status"]) && Request.Form["status"] == "VALID")
            {
                string response = Request.Form.ToString();      
               
               

                string TrxID = Request.Form["tran_id"];

                int SL = CRUD.GetMaxID("INSERT INTO [dbo].[PaymentInfo_log] ([OrderNo],[CreatedAt],[SMSResponse]) VALUES ('" + TrxID + "','" + TimeZoneBD.getCurrentTimeBD().ToString("yyyy-MM-dd HH:ss:mm") + "','initial'); SELECT SCOPE_IDENTITY() ");

                CRUD.ExecuteQuery("Update [dbo].[PaymentInfo_log] set [Response]='" + response + "' Where SL=" + SL.ToString());
                // AMOUNT and Currency FROM DB FOR THIS TRANSACTION          

                string amount = "15";
                string currency = "BDT";
                DataTable dt = new DataTable();
                dt = CRUD.ReturnTableNull("select TotalAmount,StoreNameKey from PaymentInfo Where  OrderNo='" + TrxID + "' ");
                if (dt != null && dt.Rows.Count > 0)
                    amount = dt.Rows[0]["TotalAmount"].ToString();

                SSLCommerz sslcz = new SSLCommerz("testbox", "qwerty", true);
                //Response.Write("Validation Response: " + sslcz.OrderValidate(TrxID, amount, currency, Request));
                if (sslcz.OrderValidate(SL, TrxID, amount, currency, Request))
                {
                    Post(SL, Request);
                }
                else
                {
                    try { Response.Redirect("http://islampurcollege.edu.bd//payment/failed/", false); } catch (Exception ex) { }
                }
            }
            else
            {
                Response.Write("not found");
                
            }
        }
        public void Post(int logID, HttpRequest request)
        {
            try
            {

                string response = Request.Form.ToString();
                string OrderNo = "";
                string missingFields = "";
                string PaymentMedia = "";
                try
                {
                    OrderNo = Request.Form["tran_id"];
                }
                catch (Exception ex)
                {
                    missingFields += ",orderId";
                }
                string paymentRefId = "";
                string PaidAmount = "0";
                string clientMobileNo = "";
                string orderDateTime = "2001-01-01 00:00:00";
                string issuerPaymentDateTime = "2001-01-01 00:00:00";
                string issuerPaymentRefNo = "";
                string status = "";
                string statusCode = "";
                string serviceType = "";
                string IsPaid = "0";
                string updateStoreNameKey = "";
                try
                {
                    updateStoreNameKey = request.Form["value_a"];// store_name;
                }
                catch (Exception ex)
                {
                    missingFields += ",store_name";
                }                
                try
                {
                    PaidAmount = Request.Form["amount"];
                }
                catch (Exception ex)
                {
                    missingFields += ",amount";
                }        
                try
                {
                    status = "success";
                    if (status == "success")
                    {
                        IsPaid = "1";
                        DataTable dt = new DataTable();
                        dt = CRUD.ReturnTableNull("select OrderID from PaymentInfo Where  OrderNo='" + OrderNo + "'and IsPaid=1");
                        if (dt.Rows.Count == 0)
                        {
                            // Send SMS
                            try
                            {
                                dt = new DataTable();
                                dt = CRUD.ReturnTableNull("select FeeCatName,Isnull( Isnull(cs.Mobile,adm.Mobile),op.MobileNo) as Mobile from PaymentInfo p left join CurrentStudentInfo cs on p.StudentId=cs.StudentId left join FeesCategoryInfo ct on p.FeeCatId=ct.FeeCatId left join Student_AdmissionFormInfo adm on p.AdmissionFormNo=adm.AdmissionFormNo left join PaymentOpenStudentInfo op on p.OpenStudentId=op.id where OrderNo='" + OrderNo + "'");
                                string SMSResponse = "";
                                if (dt != null && dt.Rows.Count > 0)
                                {
                                    string MobileNo = dt.Rows[0]["Mobile"].ToString();
                                    string CategoryName = dt.Rows[0]["FeeCatName"].ToString();
                                    MobileNo = MobileNo.Replace("+88", "");
                                    string Msg = string.Format("Govt. Islampur College received the payment for '" + CategoryName + "'. Your Invoice No : '" + OrderNo + "'. Download Invoice to click : http://islampurcollege.edu.bd/payment/invoice/" + OrderNo + ". Thank you");
                                    //string Msg = string.Format("Islampur College received the payment for '" + CategoryName + "'. Your Invoice No : '" + OrderNo + "'.Thank you.");
                                    if (MobileNo.Length == 11 && "017,019,018,016,015,013,014".Contains(MobileNo.Substring(0, 3)))
                                    {


                                        CRUD.ExecuteQuery("Update [dbo].[PaymentInfo_log] set [SMSResponse]='befor send->" + MobileNo + "' Where SL=" +logID);
                                        string _response = API.SMSSend(Msg, MobileNo);

                                        SMSResponse = _response;
                                        string[] r = _response.Split('|');
                                        SMSEntites smsEntities = new SMSEntites();
                                        smsEntities.ID = 1;
                                        smsEntities.MobileNo = dt.Rows[0]["Mobile"].ToString();
                                        smsEntities.Status = API.MsgStatus(int.Parse(r[0]));
                                        smsEntities.MessageBody = Msg;
                                        smsEntities.Purpose = "PaymentReceived";
                                        smsEntities.SentTime = DateTime.Now;
                                        List<SMSEntites> smsList = new List<SMSEntites>();
                                        smsList.Add(smsEntities);
                                        SMSReportEntry smsReport = new SMSReportEntry();
                                        smsReport.BulkInsert(smsList);
                                    }
                                    else
                                    {
                                        SMSResponse = "Invalid Mobile No!";
                                    }
                                }
                                else
                                {
                                    SMSResponse = "Student Not Found!";
                                }


                                CRUD.ExecuteQuery("Update [dbo].[PaymentInfo_log] set [SMSResponse]='" + SMSResponse + "' Where SL=" + logID);

                            }
                            catch (Exception ex)
                            {
                                try
                                {
                                    CRUD.ExecuteQuery("Update [dbo].[PaymentInfo_log] set [SMSResponse]='ex->" + ex.Message.ToString() + "' Where SL=" + logID);
                                }
                                catch { CRUD.ExecuteQuery("Update [dbo].[PaymentInfo_log] set [SMSResponse]='ex2-> ex insert failed!' Where SL=" + logID); }
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    missingFields += ",status";
                }              
               
                try
                {
                    PaymentMedia = request.Form["card_type"];
                }
                catch (Exception ex) { missingFields += ",PaymentMedia"; }


                if (CRUD.ExecuteQuery(@"Update [dbo].[PaymentInfo] Set IsPaid=" + IsPaid + ",Response='" + response + "',paymentRefId='" + paymentRefId + "',PaidAmount=" + PaidAmount + ",clientMobileNo='" + clientMobileNo + "',orderDateTime='" + orderDateTime + "',issuerPaymentDateTime='" + issuerPaymentDateTime + "',issuerPaymentRefNo='" + issuerPaymentRefNo + "',status='" + status + "',statusCode='" + statusCode + "',serviceType='" + serviceType + "',UpdatedAt='" + TimeZoneBD.getCurrentTimeBD().ToString("yyyy-MM-dd HH:ss:mm") + "',PaymentMedia='" + PaymentMedia + "',missingFields='" + missingFields + "',updateStoreNameKey='" + updateStoreNameKey + "' Where OrderNo='" + OrderNo + "'") && IsPaid == "1")
                {
                    try { Response.Redirect("http://islampurcollege.edu.bd//payment/success/" + OrderNo, false); } catch (Exception ex) { }
                }

                else
                {
                    try { Response.Redirect("http://islampurcollege.edu.bd//payment/failed/", false); } catch (Exception ex) { }
                }
                    
            }
            catch (Exception ex)
            {
                CRUD.ExecuteQuery("INSERT INTO [dbo].[PaymentError_log] ([ErrorMsg],[CreatedAt]) VALUES ('" + ex.Message.ToString() + "','" + TimeZoneBD.getCurrentTimeBD().ToString("yyyy-MM-dd HH:ss:mm") + "')");
                try { Response.Redirect("http://islampurcollege.edu.bd//payment/failed/", false); } catch (Exception ex1) { }
            }

        }

    }
}