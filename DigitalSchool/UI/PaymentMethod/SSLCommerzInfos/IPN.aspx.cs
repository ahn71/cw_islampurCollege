﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DS.UI.PaymentMethod.SSLCommerzInfos
{
    public partial class IPN : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try {
                string response = Request.Form.ToString();
            } catch(Exception ex) { }
        }
    }
}