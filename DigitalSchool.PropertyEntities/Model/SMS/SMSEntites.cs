using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DS.PropertyEntities.Model.SMS
{
    public class SMSEntites : IDisposable
    {
        public int ID { get; set; }
        public string SMSID { get; set; }
        public string MobileNo { get; set; }
        public string MessageBody { get; set; }
        public string Purpose { get; set; }
        public DateTime SentTime { get; set; }
        public string Status { get; set; }

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
    public class SMSGetEntities1
    {
        public string status { get; set; }
        public string m { get; set; }
        public string[] valid { get; set; }
        public string[] invalidNumber { get; set; }
        public string serverResponse { get; set; }


    }
    public class SMSGetEntities
    {
        public string status { get; set; }
        public string m { get; set; }
        public string[] valid { get; set; }
        public string[] invalidNumber { get; set; }
        public serverResponse serverResponse { get; set; }


    }
    public class serverResponse 
    {
        public string insertedSmsIds { get; set; }
        public string message { get; set; }
        public string isError { get; set; }
    }
    public class SMSTransactionLog 
    {
        public int SL { get; set; }
        public string insertedSmsIds { get; set; }
        public string SMStype { get; set; }
        public string Template { get; set; }
        public string SendingTime { get; set; }
    }
}