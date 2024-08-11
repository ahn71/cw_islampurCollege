using DS.BLL;
using DS.BLL.SMS;
using DS.DAL;
using DS.PropertyEntities.Model.SMS;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Data;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Web;
using System.Web.Http;
using System.Windows.Forms;

namespace DS.Controller
{
    public class IPNController : ApiController
    {
        // GET: api/IPN
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET: api/IPN/5
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/IPN
        
        public void Post()
        {
            try{

               
                var formData = HttpContext.Current.Request.Form.ToString();
                string _response = convertFromDataToJson(formData);
                
               
           
             
            if (_response == "")
            {
                int SL = CRUD.GetMaxID(@"INSERT INTO [PaymentInfo_IPN_log]
           ([Response],[CreatedAt])
            VALUES
           (N'received null or balnk','" + TimeZoneBD.getCurrentTimeBD().ToString("yyyy-MM-dd HH:ss:mm") + "'); SELECT SCOPE_IDENTITY() ");
            }
            else
            {
                    dynamic model = JsonConvert.DeserializeObject(_response);                   
                    int SL = CRUD.GetMaxID(@"INSERT INTO [PaymentInfo_IPN_log]
           ([Response],[CreatedAt])
            VALUES
           (N'" + _response + "','" + TimeZoneBD.getCurrentTimeBD().ToString("yyyy-MM-dd HH:ss:mm") + "'); SELECT SCOPE_IDENTITY() ");
                    string status = model.status;
                    string tran_id = model.tran_id;
                    if (tran_id != "")
                    {
                        CRUD.ExecuteQuery("Update [PaymentInfo_IPN_log] set [OrderNo]='" + tran_id + "' Where SL=" + SL.ToString());

                       
                        string missingFields = "";
                        string PaymentMedia = "";                       
                        string PaidAmount = "0";
                        string clientMobileNo = "";
                        string tran_date = "2001-01-01 00:00:00";
                        string updateStoreNameKey = "";
                        
                        if (status == "VALID" || status == "VALIDATED")
                        {
                            string IsPaid = "1";
                            status = "Success";
                            DataTable dt = new DataTable();
                            dt = CRUD.ReturnTableNull("select TotalAmount,StoreNameKey from PaymentInfo Where  OrderNo='" + tran_id + "' ");
                            if (dt != null && dt.Rows.Count > 0)
                            {
                                try
                                {
                                    PaidAmount = model.amount.ToString();
                                    if (double.Parse(PaidAmount) < double.Parse(dt.Rows[0]["TotalAmount"].ToString()))
                                    {

                                        missingFields += ",amount not match";
                                        IsPaid = "0";
                                    }
                                }
                                catch (Exception ex)
                                {
                                    missingFields += ",amount";
                                }
                                try
                                {
                                    updateStoreNameKey = model.value_a.ToString();
                                }
                                catch (Exception ex)
                                {
                                    missingFields += ",store_name";
                                }
                                try
                                {
                                    PaymentMedia = model.card_type;
                                }
                                catch (Exception ex)
                                {
                                    missingFields += ",card_type";
                                }
                                try
                                {
                                    tran_date = model.tran_date;
                                }
                                catch (Exception ex)
                                {
                                    missingFields += ",tran_date";
                                }
                                try
                                {
                                    clientMobileNo = model.value_d;// mobile no
                                }
                                catch (Exception ex) { missingFields += ",clientMobileNo"; }

                                if (IsPaid == "1")
                                {
                                    CRUD.ExecuteQuery(@"Update [dbo].[PaymentInfo] Set IsPaid=" + IsPaid + ",Response='" + _response + "',PaidAmount=" + PaidAmount + ",clientMobileNo='" + clientMobileNo + "',issuerPaymentDateTime='" + tran_date + "',status='" + status + "',UpdatedAt='" + TimeZoneBD.getCurrentTimeBD().ToString("yyyy-MM-dd HH:ss:mm") + "',PaymentMedia='" + PaymentMedia + "',missingFields='" + missingFields + "',updateStoreNameKey='" + updateStoreNameKey + "' Where OrderNo='" + tran_id + "'");

                                    // Send SMS
                                    try
                                    {
                                        dt = new DataTable();
                                        dt = CRUD.ReturnTableNull("select FeeCatName,Isnull( Isnull(cs.Mobile,adm.Mobile),op.MobileNo) as Mobile from PaymentInfo p left join CurrentStudentInfo cs on p.StudentId=cs.StudentId left join FeesCategoryInfo ct on p.FeeCatId=ct.FeeCatId left join Student_AdmissionFormInfo adm on p.AdmissionFormNo=adm.AdmissionFormNo left join PaymentOpenStudentInfo op on p.OpenStudentId=op.id where OrderNo='" + tran_id + "'");
                                        string SMSResponse = "";
                                        if (dt != null && dt.Rows.Count > 0)
                                        {
                                            string MobileNo = dt.Rows[0]["Mobile"].ToString();
                                            string CategoryName = dt.Rows[0]["FeeCatName"].ToString();
                                            MobileNo = MobileNo.Replace("+88", "");
                                            string Msg = string.Format("Govt. Islampur College received the payment for '" + CategoryName + "'. Your Invoice No : '" + tran_id + "'. Download Invoice to click : http://islampurcollege.edu.bd/payment/invoice/" + tran_id + ". Thank you");
                                            //string Msg = string.Format("Islampur College received the payment for '" + CategoryName + "'. Your Invoice No : '" + OrderNo + "'.Thank you.");
                                            if (MobileNo.Length == 11 && "017,019,018,016,015,013,014".Contains(MobileNo.Substring(0, 3)))
                                            {


                                                CRUD.ExecuteQuery("Update [PaymentInfo_IPN_log] set [SMSResponse]=N'befor send->" + MobileNo + "' Where SL=" + SL.ToString());
                                                string resopse = API.SMSSend(Msg, MobileNo);

                                                SMSResponse = resopse;
                                                //string[] r = resopse.Split('|');
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

                                        CRUD.ExecuteQuery("Update [PaymentInfo_IPN_log] set [SMSResponse]=N'" + SMSResponse + "' Where SL=" + SL.ToString());

                                    }
                                    catch (Exception ex)
                                    {
                                        try
                                        {
                                            CRUD.ExecuteQuery("Update [PaymentInfo_IPN_log] set [SMSResponse]=N'ex->" + ex.Message.ToString() + "' Where SL=" + SL.ToString());
                                        }
                                        catch { CRUD.ExecuteQuery("Update [PaymentInfo_IPN_log] set [SMSResponse]=N'ex2-> ex insert failed!' Where SL=" + SL.ToString()); }
                                    }
                                }
                                else
                                   CRUD.ExecuteQuery(@"Update [dbo].[PaymentInfo] Set IsPaid=" + IsPaid + ",Response='" + _response + "',PaidAmount=" + PaidAmount + ",clientMobileNo='" + clientMobileNo + "',issuerPaymentDateTime='" + tran_date + "',status='" + status + "',UpdatedAt='" + TimeZoneBD.getCurrentTimeBD().ToString("yyyy-MM-dd HH:ss:mm") + "',PaymentMedia='" + PaymentMedia + "',missingFields='" + missingFields + "',updateStoreNameKey='" + updateStoreNameKey + "' Where OrderNo='" + tran_id + "'");
                            }
                        }
                        

                        


                       

                        
                    }
                    



            }
            }
            catch (Exception ex) {
                CRUD.GetMaxID(@"INSERT INTO [PaymentInfo_IPN_log]
           ([Response],[CreatedAt])
            VALUES
           (N'catch->" + ex.Message.ToString() + "','" + TimeZoneBD.getCurrentTimeBD().ToString("yyyy-MM-dd HH:ss:mm") + "'); SELECT SCOPE_IDENTITY() ");
            }

        }

        private string convertFromDataToJson(string urlEncodedData)
        {
            try {               

            // Decode the URL-encoded data
                NameValueCollection urlParams = System.Web.HttpUtility.ParseQueryString(urlEncodedData);
                
            // Convert the NameValueCollection to a Dictionary
            var dataDictionary = urlParams.AllKeys.ToDictionary(key => key, key => urlParams[key]);

            // Convert the dictionary to a JSON string
            string jsonData = JsonConvert.SerializeObject(dataDictionary, Formatting.Indented);

            // Print the JSON data
            return jsonData;
            }
            catch (Exception ex) { return ""; }
        }
        // PUT: api/IPN/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/IPN/5
        public void Delete(int id)
        {
        }
    }
}
