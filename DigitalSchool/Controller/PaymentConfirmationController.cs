using DS.BLL;
using DS.BLL.SMS;
using DS.DAL;
using DS.PropertyEntities.Model.SMS;
using Newtonsoft.Json;

//using Newtonsoft.Json;
//using SigmaERP.util;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;

namespace DS.api
{
    public class PaymentConfirmationController : ApiController
    {
        // GET: api/PaymentConfirmation
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET: api/PaymentConfirmation/5
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/PaymentConfirmation

        public string Post(object model1)
        {
            try
            {
                if (model1 == null)
                {
                    CRUD.GetMaxID("INSERT INTO [dbo].[PaymentInfo_log] ([Response],[CreatedAt],[SMSResponse]) VALUES ('received null','" + TimeZoneBD.getCurrentTimeBD().ToString("yyyy-MM-dd HH:ss:mm") + "','initial'); SELECT SCOPE_IDENTITY() ");
                    return "http://islampurcollege.edu.bd//payment/failed/";
                }                    
                //System.Diagnostics.Debug.WriteLine("json-->"+getJson(model1));
                //string _response= getJson(model1); 
                dynamic model = model1;

               // return model.ToString();
               // Console.WriteLine(model.ToString());

                
                
                string OrderNo = "";
                string missingFields = "";
                string PaymentMedia = "";
                try
                {
                    OrderNo = model.orderId.ToString();
                    
                }
                catch (Exception ex)
                {                   
                    missingFields += ",orderId";
                }
                int SL=   CRUD.GetMaxID("INSERT INTO [dbo].[PaymentInfo_log] ([OrderNo],[CreatedAt],[SMSResponse]) VALUES ('" + OrderNo + "','" + TimeZoneBD.getCurrentTimeBD().ToString("yyyy-MM-dd HH:ss:mm") + "','initial'); SELECT SCOPE_IDENTITY() ");
                string Response = model.ToString();
                //string Response = _response;
                
                //var _response = new Dictionary<string, string>
                //{

                //};
                //    var req = HttpContext.Current.Request.Form;
                //    foreach (var item in req.Keys)
                //{

                //    var v = HttpContext.Current.Request.Form[item.ToString()];
                //        _response.Add(item.ToString(), v);

                //}


                //    Response = JsonConvert.SerializeObject(_response);
                
                CRUD.ExecuteQuery("Update [dbo].[PaymentInfo_log] set [Response]='" + Response + "' Where SL="+SL.ToString());

                //return _response["orderId"].ToString();
                //string OrderNo = _response["orderId"];
                //string paymentRefId = _response["paymentRefId"];
                //string PaidAmount = _response["amount"];
                //string clientMobileNo = _response["clientMobileNo"];
                //string orderDateTime = DateTime.Parse(_response["orderDateTime"].ToString()).ToString("yyyy-MM-dd HH:mm:ss");
                //string issuerPaymentDateTime = DateTime.Parse(_response["issuerPaymentDateTime"].ToString()).ToString("yyyy-MM-dd HH:mm:ss");
                //string issuerPaymentRefNo = _response["issuerPaymentRefNo"];
                //string status = _response["status"];
                //string statusCode = _response["statusCode"];
                //string serviceType = _response["serviceType"];           



                //return Response;
                //dynamic model;
               
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
                    updateStoreNameKey = model.store_name.ToString();
                }
                catch (Exception ex)
                {
                    missingFields += ",store_name";
                }         

                try
                {
                    paymentRefId = model.paymentRefId.ToString();
                }
                catch (Exception ex)
                {
                    missingFields += ",paymentRefId";
                }
                try
                {
                    PaidAmount = model.amount.ToString();
                }
                catch (Exception ex)
                {
                    missingFields += ",amount";
                }
                try
                {
                    clientMobileNo = model.clientMobileNo.ToString();
                }
                catch (Exception ex)
                {
                    missingFields += ",clientMobileNo";
                }
                try
                {
                    orderDateTime = DateTime.Parse(model.orderDateTime.ToString()).ToString("yyyy-MM-dd HH:mm:ss");
                }
                catch (Exception ex)
                {
                    missingFields += ",orderDateTime";
                }
                try
                {
                    issuerPaymentDateTime = DateTime.Parse(model.issuerPaymentDateTime.ToString()).ToString("yyyy-MM-dd HH:mm:ss");
                }
                catch (Exception ex)
                {
                    missingFields += ",issuerPaymentDateTime";
                }
                try
                {
                    issuerPaymentRefNo = model.issuerPaymentRefNo.ToString();
                }
                catch (Exception ex)
                {
                    missingFields += ",issuerPaymentRefNo";
                }
                try
                {
                    status = model.status.ToString();
                    if (status.ToLower() == "success")
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


                                        CRUD.ExecuteQuery("Update [dbo].[PaymentInfo_log] set [SMSResponse]='befor send->" + MobileNo + "' Where SL=" + SL.ToString());
                                        string resopse = API.SMSSend(Msg, MobileNo);

                                        SMSResponse = resopse;
                                       // string[] r = resopse.Split('|');
                                        SMSEntites smsEntities = new SMSEntites();
                                        smsEntities.ID = 1;
                                        smsEntities.MobileNo = dt.Rows[0]["Mobile"].ToString();
                                        // smsEntities.Status = API.MsgStatus(int.Parse(r[0]));

                                        Dictionary<string, object> responseDictionary = JsonConvert.DeserializeObject<Dictionary<string, object>>(resopse);
                                        int responseCode = Convert.ToInt32(responseDictionary["response_code"]);
                                        Console.WriteLine("responseCode: " + responseCode);
                                        smsEntities.Status = API.MsgStatus(responseCode);

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


                                CRUD.ExecuteQuery("Update [dbo].[PaymentInfo_log] set [SMSResponse]='" + SMSResponse + "' Where SL=" + SL.ToString());

                            }
                            catch (Exception ex)
                            {
                                try {
                                    CRUD.ExecuteQuery("Update [dbo].[PaymentInfo_log] set [SMSResponse]='ex->" + ex.Message.ToString() + "' Where SL=" + SL.ToString());
                                }
                                catch { CRUD.ExecuteQuery("Update [dbo].[PaymentInfo_log] set [SMSResponse]='ex2-> ex insert failed!' Where SL=" + SL.ToString()); }
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
                    statusCode = model.statusCode.ToString();
                }
                catch (Exception ex)
                {
                    missingFields += ",statusCode";
                }
                try
                {
                    serviceType = model.serviceType.ToString();
                }
                catch (Exception ex)
                {
                    missingFields += ",serviceType";
                }
                try
                {
                    PaymentMedia = model.payment_media.ToString();
                }
                catch (Exception ex) { missingFields += ",PaymentMedia"; }


                if (CRUD.ExecuteQuery(@"Update [dbo].[PaymentInfo] Set IsPaid=" + IsPaid + ",Response='" + Response + "',paymentRefId='" + paymentRefId + "',PaidAmount=" + PaidAmount + ",clientMobileNo='" + clientMobileNo + "',orderDateTime='" + orderDateTime + "',issuerPaymentDateTime='" + issuerPaymentDateTime + "',issuerPaymentRefNo='" + issuerPaymentRefNo + "',status='" + status + "',statusCode='" + statusCode + "',serviceType='" + serviceType + "',UpdatedAt='" + TimeZoneBD.getCurrentTimeBD().ToString("yyyy-MM-dd HH:ss:mm") + "',PaymentMedia='" + PaymentMedia + "',missingFields='" + missingFields + "',updateStoreNameKey='"+ updateStoreNameKey + "' Where OrderNo='" + OrderNo + "'") && IsPaid == "1")
                    return "http://islampurcollege.edu.bd//payment/success/" + OrderNo;
                else
                    return "http://islampurcollege.edu.bd//payment/failed/";
            }
            catch (Exception ex) {
                CRUD.ExecuteQuery("INSERT INTO [dbo].[PaymentError_log] ([ErrorMsg],[CreatedAt]) VALUES ('"+ex.Message.ToString()+ "','" + TimeZoneBD.getCurrentTimeBD().ToString("yyyy-MM-dd HH:ss:mm") + "')");
                return "http://islampurcollege.edu.bd//payment/failed/";
            }

        }

        // PUT: api/PaymentConfirmation/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/PaymentConfirmation/5
        public void Delete(int id)
        {
        }

        //string getJson(Object obj)
        //{
        //    var settings = new Newtonsoft.Json.JsonSerializerSettings
        //    {
        //        NullValueHandling = NullValueHandling.Ignore,
        //        Converters = { new DataRowConverter() },
        //    };
        //    var json = JsonConvert.SerializeObject(obj, Formatting.Indented, settings);
        //    return json;
        //}
    }
}
