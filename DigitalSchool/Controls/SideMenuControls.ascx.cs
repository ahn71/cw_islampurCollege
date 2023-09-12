using DS.DAL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DS.Controls
{
    public partial class SideMenuControls : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            
                if (Session["__UserTypeId__"].ToString() != null)
                {
                    DataTable dt = CRUD.ReturnTableNull("select AcademicModule,AdministrationModule,NotificationModule,ReportsModule from UserTypeInfo_ModulePrivilege where UserTypeId=" + Session["__UserTypeId__"].ToString().ToString() + "");
                    if (dt.Rows.Count > 0)
                    {
                        if (bool.Parse(dt.Rows[0]["AcademicModule"].ToString()).Equals(false)) liAcademicModuleDB.Visible = false;
                        if (bool.Parse(dt.Rows[0]["AdministrationModule"].ToString()).Equals(false)) liAdministrationModuleDB.Visible = false;
                        if (bool.Parse(dt.Rows[0]["NotificationModule"].ToString()).Equals(false)) liNotificationModuleDB.Visible = false;
                        if (bool.Parse(dt.Rows[0]["ReportsModule"].ToString()).Equals(false)) liReportsModuleDB.Visible = false;
                    }
                }
            
        }
    }
}