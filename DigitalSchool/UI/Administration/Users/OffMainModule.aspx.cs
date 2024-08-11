using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DS.BLL.ControlPanel;

namespace DS.UI.Administration.Users
{
    public partial class OffMainModule : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (!PrivilegeOperation.SetPrivilegeControl(Session["__UserTypeId__"].ToString(), "OffMainModule.aspx")) Response.Redirect(Request.UrlReferrer.ToString() + "?hasperm=no");
                UserTypeInfoEntry.GetUserTypeModulePrivilege(gvUserTypeList);
            }

           
        }

        protected void chkAcademicModule_CheckedChanged(object sender,EventArgs e)
        {
            try
            {
                GridViewRow gvr = ((GridViewRow)((Control)sender).Parent.Parent);
                int index_row = gvr.RowIndex;

                int UserTypeDId = Convert.ToInt32(gvUserTypeList.DataKeys[index_row].Value.ToString());

                CheckBox chk = (CheckBox)gvUserTypeList.Rows[index_row].Cells[1].FindControl("chkAcademicModule");

                byte Action = (chk.Checked) ? (byte)1 : (byte)0;

                PrivilegeOperation.setAcademicModule(UserTypeDId.ToString(), Action);
            }
            catch { }
        }
        protected void chkAdministrationModule_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                GridViewRow gvr = ((GridViewRow)((Control)sender).Parent.Parent);
                int index_row = gvr.RowIndex;

                int UserTypeDId = Convert.ToInt32(gvUserTypeList.DataKeys[index_row].Value.ToString());

                CheckBox chk = (CheckBox)gvUserTypeList.Rows[index_row].Cells[1].FindControl("chkAdministrationModule");

                byte Action = (chk.Checked) ? (byte)1 : (byte)0;

                PrivilegeOperation.setAdministrationModule(UserTypeDId.ToString(), Action);
            }
            catch { }
        }
        protected void chkNotificationModule_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                GridViewRow gvr = ((GridViewRow)((Control)sender).Parent.Parent);
                int index_row = gvr.RowIndex;

                int UserTypeDId = Convert.ToInt32(gvUserTypeList.DataKeys[index_row].Value.ToString());

                CheckBox chk = (CheckBox)gvUserTypeList.Rows[index_row].Cells[1].FindControl("chkNotificationModule");

                byte Action = (chk.Checked) ? (byte)1 : (byte)0;

                PrivilegeOperation.setNotificationModule(UserTypeDId.ToString(), Action);
            }
            catch { }
        }
        protected void chkReportsModule_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                GridViewRow gvr = ((GridViewRow)((Control)sender).Parent.Parent);
                int index_row = gvr.RowIndex;

                int UserTypeDId = Convert.ToInt32(gvUserTypeList.DataKeys[index_row].Value.ToString());

                CheckBox chk = (CheckBox)gvUserTypeList.Rows[index_row].Cells[1].FindControl("chkReportsModule");

                byte Action = (chk.Checked) ? (byte)1 : (byte)0;

                PrivilegeOperation.setReportsModule(UserTypeDId.ToString(), Action);
            }
            catch { }
        }
    }
}