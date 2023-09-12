using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;

namespace DS.BLL.SMS
{
    public static class API
    {
        public static string url= "http://66.45.237.70/api.php";
        public static string userID= "01722289239";
        public static string password= "3M5TYWDH";

        public static string MsgStatus(int value)
        {
            string returnMsg = string.Empty;
            switch (value)
            {
                case 1101:
                    returnMsg = "Success";
                    break;
                case 1000:
                    returnMsg = "Invalid user or Password";
                    break;
                case 1002:
                    returnMsg = "Empty Number";
                    break;
                case 1003:
                    returnMsg = "Invalid message or empty message";
                    break;
                case 1004:
                    returnMsg = "Invalid number";
                    break;
                case 1005:
                    returnMsg = "All Number is Invalid";
                    break;
                case 1006:
                    returnMsg = "insufficient Balance";
                    break;
                case 1009:
                    returnMsg = "Inactive Account";
                    break;
                case 1010:
                    returnMsg = "Max number limit exceeded";
                    break;
                default:
                    returnMsg = "";
                    break;
            }
            return returnMsg;
        }
        public  static string SMSSend(string messsage, string number)
        {

            String userid = API.userID; //Your Login ID
            String password = API.password; //Your Password
                                      //Recipient Phone Number multiple number must be separated by comma
            String message = System.Uri.EscapeUriString(messsage);

            // Create a request using a URL that can receive a post.   
            WebRequest request = WebRequest.Create(API.url);
            // Set the Method property of the request to POST.  
            request.Method = "POST";
            // Create POST data and convert it to a byte array.  
            string postData = "username=" + userid + "&password=" + password + "&number=" + number + "&message=" + message;
            byte[] byteArray = Encoding.UTF8.GetBytes(postData);
            // Set the ContentType property of the WebRequest.  
            request.ContentType = "application/x-www-form-urlencoded";
            // Set the ContentLength property of the WebRequest.  
            request.ContentLength = byteArray.Length;
            // Get the request stream.  
            Stream dataStream = request.GetRequestStream();
            // Write the data to the request stream.  
            dataStream.Write(byteArray, 0, byteArray.Length);
            // Close the Stream object.  
            dataStream.Close();
            // Get the response.  
            WebResponse response = request.GetResponse();
            // Display the status.  
            Console.WriteLine(((HttpWebResponse)response).StatusDescription);
            // Get the stream containing content returned by the server.  
            dataStream = response.GetResponseStream();
            // Open the stream using a StreamReader for easy access.  
            StreamReader reader = new StreamReader(dataStream);
            // Read the content.  
            string responseFromServer = reader.ReadToEnd();
            // Display the content.  
            //Console.WriteLine(responseFromServer);
            // Clean up the streams.  
            reader.Close();
            dataStream.Close();
            response.Close();
            return responseFromServer;

        }
    }

}
