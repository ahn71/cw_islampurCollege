using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DS.UI.PaymentMethod.SSLCommerzInfos
{
    public partial class Default : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string baseUrl = Request.Url.Scheme + "://" + Request.Url.Authority + Request.ApplicationPath.TrimEnd('/') + "/";

            string amount = Request.QueryString["amount"].ToString();
            string invoice = Request.QueryString["invoice"].ToString();
            string payment_type = Request.QueryString["payment_type"].ToString();
            string store_name = Request.QueryString["store_name"].ToString();
            string student_name = Request.QueryString["student_name"].ToString();
            string student_mobile = Request.QueryString["student_mobile"].ToString();
            string student_email = Request.QueryString["student_email"].ToString();
            string student_id = Request.QueryString["student_id"].ToString();
            if (student_email.Trim() == "")
                student_email = "nayem.codeware@gmail.com";
            // CREATING LIST OF POST DATA
            NameValueCollection PostData = new NameValueCollection();
            PostData.Add("total_amount", amount);
            PostData.Add("tran_id", invoice);
            PostData.Add("success_url", baseUrl + "/UI/PaymentMethod/SSLCommerzInfos/Success.aspx");
            PostData.Add("fail_url", baseUrl + "/UI/DSWS/PaymentFailed.aspx"); // "Fail.aspx" page needs to be created
            PostData.Add("cancel_url", baseUrl + "/UI/DSWS/PaymentFailed.aspx");
           // PostData.Add("ipn_url", baseUrl+"IPN.aspx"); // If IPN is implemented, provide the url;

            PostData.Add("cus_name", student_name);
            PostData.Add("cus_email", student_email);
            PostData.Add("cus_add1", "Address Line One");
            PostData.Add("cus_add2", student_id);
            PostData.Add("cus_city", "Dhaka");
            PostData.Add("cus_postcode", "1000");
            PostData.Add("cus_country", "Bangladesh");
            PostData.Add("cus_phone", student_mobile);

            PostData.Add("value_a", store_name);
            PostData.Add("value_b", student_name);
            PostData.Add("value_c", student_id);
            PostData.Add("value_d", student_mobile);

            PostData.Add("shipping_method", "NO");
            PostData.Add("num_of_item", "1");
            PostData.Add("product_name", "Demo");
            PostData.Add("product_profile", "general");
            PostData.Add("product_category", "Demo");
              

            // Add more parameters as needed. Parameter reference page - https://developer.sslcommerz.com/doc/v4/#initiate-payment

            SSLCommerz sslcz = new SSLCommerz("<your store id>", "<your store password>", true); // Use true for sandbox, false for live.
            String response = sslcz.InitiateTransaction(PostData);
            Response.Redirect(response);
        }
    }
}