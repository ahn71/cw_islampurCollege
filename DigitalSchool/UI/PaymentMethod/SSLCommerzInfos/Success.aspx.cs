﻿using DS.BLL;
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

                SSLCommerz sslcz = new SSLCommerz("codew652cbfc82018f", "codew652cbfc82018f@ssl", true);
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
                string PaidAmount = "0";
                string clientMobileNo = "";
                string issuerPaymentDateTime = "2001-01-01 00:00:00";
                string updateStoreNameKey = "";
                try
                {
                    OrderNo = Request.Form["tran_id"];
                    try
                    {
                        DataTable dt = new DataTable();
                        dt = CRUD.ReturnTableNull("select OrderID from PaymentInfo Where  OrderNo='" + OrderNo + "'and IsPaid=1");
                        if (dt.Rows.Count == 0)
                        {

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
                                PaymentMedia = request.Form["card_type"];
                            }
                            catch (Exception ex) { missingFields += ",PaymentMedia"; }
                            try
                            {
                                clientMobileNo = request.Form["value_d"];// mobile no
                            }
                            catch (Exception ex) { missingFields += ",clientMobileNo"; }
                            try
                            {
                                issuerPaymentDateTime = request.Form["tran_date"];
                            }
                            catch (Exception ex) { missingFields += ",tran_date"; }

                            if (CRUD.ExecuteQuery(@"Update [dbo].[PaymentInfo] Set IsPaid=1,Response='" + response + "',PaidAmount=" + PaidAmount + ",clientMobileNo='" + clientMobileNo + "',issuerPaymentDateTime='" + issuerPaymentDateTime + "',status='Success',UpdatedAt='" + TimeZoneBD.getCurrentTimeBD().ToString("yyyy-MM-dd HH:ss:mm") + "',PaymentMedia='" + PaymentMedia + "',missingFields='" + missingFields + "',updateStoreNameKey='" + updateStoreNameKey + "' Where OrderNo='" + OrderNo + "'"))
                            {
                                try { Response.Redirect("http://islampurcollege.edu.bd//payment/success/" + OrderNo, false); } catch (Exception ex) { }
                            }
                            else
                            {
                                try { Response.Redirect("http://islampurcollege.edu.bd//payment/failed/", false); } catch (Exception ex) { }
                            }
                        }
                        else
                        {
                            try { Response.Redirect("http://islampurcollege.edu.bd//payment/success/" + OrderNo, false); } catch (Exception ex) { }
                        }

                    }
                    catch (Exception ex)
                    {
                        missingFields += ",status";
                    }
                }
                catch (Exception ex)
                {
                    missingFields += ",tran_id";
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