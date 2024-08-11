using DS.BLL.Finance;
using DS.PropertyEntities.Model.Finance;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DS.UI.Administration.Finance.Accounts
{
    public partial class AccountSetting : System.Web.UI.Page
    {
        AcccountsettingEntry AcS;
        protected void Page_Load(object sender, EventArgs e)
        {
            if(!IsPostBack)
            {
                if (AcS == null)
                    AcS = new AcccountsettingEntry();
                if(AcS.CheckFeesCount()==true)
                    rblcount.SelectedValue="1";
                else
                    rblcount.SelectedValue = "0";
            }
        }

        protected void rblcount_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                using (AccountsettingEntities entities = GetFormData())
                {
                    if (AcS == null)
                    {
                        AcS = new AcccountsettingEntry();
                    }
                    AcS.AddEntities = entities;
                    bool result = AcS.Update();
                }
            }
            catch { }
        }
        private AccountsettingEntities GetFormData()
        {
            AccountsettingEntities tleEntities = new AccountsettingEntities();
            tleEntities.feescount = rblcount.SelectedValue=="1"?true:false;
            return tleEntities;
        }
    }
}