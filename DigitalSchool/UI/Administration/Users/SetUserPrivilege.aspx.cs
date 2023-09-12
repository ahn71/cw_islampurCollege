using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DS.BLL.ControlPanel;
using DS.DAL;
using System.Data;
using System.Data.SqlClient;
using DS.BLL.ControlPanel;

namespace DS.UI.Administration.Users
{
    public partial class SetUserPrivilege : System.Web.UI.Page
    {
        SqlCommand cmd;
        protected void Page_Load(object sender, EventArgs e)
        {
            lblMessage.InnerText = "";
            if (!IsPostBack)
            {
                UserAccountEntry.GetCurrentUserList(ddlCurrentUserList);
                
            }
        }
        private void CheckedRowsAmount(byte cIndex,string ControlName,out byte checkedRowsAmount)
        {
            try
            {
                byte i=0;
                foreach (GridViewRow gvr in gvPageInfoList.Rows)
                {
                    CheckBox chk = (CheckBox)gvr.Cells[cIndex].FindControl(ControlName);
                    if (chk.Checked) i++;
                }
                checkedRowsAmount = i;
            }
            catch { checkedRowsAmount = 0; }
        }
        
        protected void chkViewAction_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                GridViewRow gvr = ((GridViewRow)((Control)sender).Parent.Parent);
                int index_row = gvr.RowIndex;

                int PageNameId = Convert.ToInt32(gvPageInfoList.DataKeys[index_row].Value.ToString());

                CheckBox chk = (CheckBox)gvPageInfoList.Rows[index_row].Cells[3].FindControl("chkViewAction");

                byte Action = (chk.Checked) ? (byte)1 : (byte)0;

                PrivilegeOperation.setViewPrivilege(PageNameId.ToString(), Action, ddlCurrentUserList.SelectedItem.Value);  // set user view privilege
                
                //--for checked and select header rows----------------------------------------
                byte checkedRowsAmount = 0;
                CheckedRowsAmount(3, "chkViewAction",out  checkedRowsAmount);
                chk = (CheckBox)gvPageInfoList.HeaderRow.FindControl("hdChkViewAction");

                if (checkedRowsAmount == gvPageInfoList.Rows.Count)
                {
                   
                    chk.Checked = true;
                }
                else { chk.Checked = false; }
                //----------------------------------------------------------------------------
            }
            catch { }
        }
        protected void chkSaveAction_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                GridViewRow gvr = ((GridViewRow)((Control)sender).Parent.Parent);
                int index_row = gvr.RowIndex;

                int PageNameId = Convert.ToInt32(gvPageInfoList.DataKeys[index_row].Value.ToString());

                CheckBox chk = (CheckBox)gvPageInfoList.Rows[index_row].Cells[4].FindControl("chkSaveAction");

                byte Action = (chk.Checked) ? (byte)1 : (byte)0;

                PrivilegeOperation.setSavePrivilege(PageNameId.ToString(), Action, ddlCurrentUserList.SelectedItem.Value);  // set user save privilege

                //--for checked and select header rows----------------------------------------
                byte checkedRowsAmount = 0;
                CheckedRowsAmount(4, "chkSaveAction", out  checkedRowsAmount);
                chk = (CheckBox)gvPageInfoList.HeaderRow.FindControl("hdChkSaveAction");

                if (checkedRowsAmount == gvPageInfoList.Rows.Count)  chk.Checked = true;
                else { chk.Checked = false; }
                //----------------------------------------------------------------------------
            }
            catch { }
        }
        
        protected void chkUpdateAction_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                GridViewRow gvr = ((GridViewRow)((Control)sender).Parent.Parent);
                int index_row = gvr.RowIndex;

                int PageNameId = Convert.ToInt32(gvPageInfoList.DataKeys[index_row].Value.ToString());

                CheckBox chk = (CheckBox)gvPageInfoList.Rows[index_row].Cells[5].FindControl("chkUpdateAction");

                byte Action = (chk.Checked) ? (byte)1 : (byte)0;

                PrivilegeOperation.setUpdatePrivilege(PageNameId.ToString(), Action, ddlCurrentUserList.SelectedItem.Value);  // set user update privilege

                //--for checked and select header rows----------------------------------------
                byte checkedRowsAmount = 0;
                CheckedRowsAmount(5, "chkUpdateAction", out  checkedRowsAmount);
                chk = (CheckBox)gvPageInfoList.HeaderRow.FindControl("hdChkUpdateAction");

                if (checkedRowsAmount == gvPageInfoList.Rows.Count)chk.Checked = true;
                else { chk.Checked = false; }
                //----------------------------------------------------------------------------
            }
            catch { }
        }

        protected void chkDeleteAction_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                GridViewRow gvr = ((GridViewRow)((Control)sender).Parent.Parent);
                int index_row = gvr.RowIndex;

                int PageNameId = Convert.ToInt32(gvPageInfoList.DataKeys[index_row].Value.ToString());

                CheckBox chk = (CheckBox)gvPageInfoList.Rows[index_row].Cells[6].FindControl("chkDeleteAction");

                byte Action = (chk.Checked) ? (byte)1 : (byte)0;

                PrivilegeOperation.setDeletePrivilege(PageNameId.ToString(), Action, ddlCurrentUserList.SelectedItem.Value);  // set user delete privilege
                
                //--for checked and select header rows----------------------------------------
                byte checkedRowsAmount = 0;
                CheckedRowsAmount(6, "chkDeleteAction", out  checkedRowsAmount);
                chk = (CheckBox)gvPageInfoList.HeaderRow.FindControl("hdChkDeleteAction");

                if (checkedRowsAmount == gvPageInfoList.Rows.Count) chk.Checked = true;
                else { chk.Checked = false; }
                //----------------------------------------------------------------------------
            }
            catch { }
        }
        protected void chkGenerateAction_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                GridViewRow gvr = ((GridViewRow)((Control)sender).Parent.Parent);
                int index_row = gvr.RowIndex;

                int PageNameId = Convert.ToInt32(gvPageInfoList.DataKeys[index_row].Value.ToString());

                CheckBox chk = (CheckBox)gvPageInfoList.Rows[index_row].Cells[7].FindControl("chkGenerateAction");

                byte Action = (chk.Checked) ? (byte)1 : (byte)0;

                PrivilegeOperation.setGeneratePrivilege(PageNameId.ToString(), Action, ddlCurrentUserList.SelectedItem.Value);  // set user Generate privilege

                //--for checked and select header rows----------------------------------------
                byte checkedRowsAmount = 0;
                CheckedRowsAmount(7, "chkGenerateAction", out  checkedRowsAmount);
                chk = (CheckBox)gvPageInfoList.HeaderRow.FindControl("hdChkGenerateAction");

                if (checkedRowsAmount == gvPageInfoList.Rows.Count) chk.Checked = true;
                else { chk.Checked = false; }
                //----------------------------------------------------------------------------
            }
            catch { }
        }

        protected void ddlCurrentUserList_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (ddlCurrentUserList.SelectedItem.Value == "0")
                {
                    gvPageInfoList.DataSource = null;
                    gvPageInfoList.DataBind();
                    lblMessage.InnerText = "warning->Please select a valid user !";
                    return;
                }

                string[] Title = UserAccountEntry.GetTitel(ddlCurrentUserList.SelectedItem.Value, ddlCurrentUserList.SelectedItem.Text).Split('_');
                divTitle.InnerText = Title[0];
                ViewState["__UserTypeId__"] = Title[1];
                UserAccountEntry.GetPageListByUser(Title[1], false, gvPageInfoList, ddlCurrentUserList.SelectedItem.Value);

                // for test header checkbox checked at first time----------
                {
                    for (byte b = 3; b <= 7; b++)
                    {
                        CheckBox chk;
                        byte checkedRowsAmount = 0;
                        if (b == 3)
                        {
                            CheckedRowsAmount(3, "chkViewAction", out  checkedRowsAmount);
                            chk = (CheckBox)gvPageInfoList.HeaderRow.FindControl("hdChkViewAction");
                        }
                        else if (b == 4)
                        {
                            CheckedRowsAmount(4, "chkSaveAction", out  checkedRowsAmount);
                            chk = (CheckBox)gvPageInfoList.HeaderRow.FindControl("hdChkSaveAction");
                        }
                        else if (b == 5)
                        {
                            CheckedRowsAmount(5, "chkUpdateAction", out  checkedRowsAmount);
                            chk = (CheckBox)gvPageInfoList.HeaderRow.FindControl("hdChkUpdateAction");
                        }
                        else if (b == 6)
                        {
                            CheckedRowsAmount(6, "chkDeleteAction", out  checkedRowsAmount);
                            chk = (CheckBox)gvPageInfoList.HeaderRow.FindControl("hdChkDeleteAction");
                        }
                        else
                        {
                            CheckedRowsAmount(7, "chkGenerateAction", out  checkedRowsAmount);
                            chk = (CheckBox)gvPageInfoList.HeaderRow.FindControl("hdChkGenerateAction");
                        }

                        if (checkedRowsAmount == gvPageInfoList.Rows.Count)chk.Checked = true;                    
                        else chk.Checked = false; 
                    }
                }
                //--------------------------------------------------------
            }
            catch { }
        }

        protected void hdChkViewAction_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                CheckBox chk = (CheckBox)gvPageInfoList.HeaderRow.FindControl("hdChkViewAction");
                if (chk.Checked)
                {
                    foreach(GridViewRow row in gvPageInfoList.Rows)
                    {
                        chk =(CheckBox)row.Cells[3].FindControl("chkViewAction");
                        chk.Checked = true;
                        int PageNameId = Convert.ToInt32(gvPageInfoList.DataKeys[row.RowIndex].Value.ToString());
                        PrivilegeOperation.setViewPrivilege(PageNameId.ToString(), (byte)1, ddlCurrentUserList.SelectedItem.Value);
                    }
                }
                else
                {
                    foreach (GridViewRow row in gvPageInfoList.Rows)
                    {
                        chk = (CheckBox)row.Cells[3].FindControl("chkViewAction");
                        chk.Checked = false;
                        int PageNameId = Convert.ToInt32(gvPageInfoList.DataKeys[row.RowIndex].Value.ToString());
                        PrivilegeOperation.setViewPrivilege(PageNameId.ToString(), (byte)0, ddlCurrentUserList.SelectedItem.Value);
                    }
                }
             

            }
            catch { }
        }

        protected void hdChkSaveAction_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                CheckBox chk = (CheckBox)gvPageInfoList.HeaderRow.FindControl("hdChkSaveAction");
                if (chk.Checked)
                {
                    foreach (GridViewRow row in gvPageInfoList.Rows)
                    {
                        chk = (CheckBox)row.Cells[4].FindControl("chkSaveAction");
                        chk.Checked = true;
                        int PageNameId = Convert.ToInt32(gvPageInfoList.DataKeys[row.RowIndex].Value.ToString());
                        PrivilegeOperation.setSavePrivilege(PageNameId.ToString(), (byte)1, ddlCurrentUserList.SelectedItem.Value);
                    }
                }
                else
                {
                    foreach (GridViewRow row in gvPageInfoList.Rows)
                    {
                        chk = (CheckBox)row.Cells[4].FindControl("chkSaveAction");
                        chk.Checked = false;
                        int PageNameId = Convert.ToInt32(gvPageInfoList.DataKeys[row.RowIndex].Value.ToString());
                        PrivilegeOperation.setSavePrivilege(PageNameId.ToString(), (byte)0, ddlCurrentUserList.SelectedItem.Value);
                    }
                }


            }
            catch { }
        }

        protected void hdChkUpdateAction_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                CheckBox chk = (CheckBox)gvPageInfoList.HeaderRow.FindControl("hdChkUpdateAction");
                if (chk.Checked)
                {
                    foreach (GridViewRow row in gvPageInfoList.Rows)
                    {
                        chk = (CheckBox)row.Cells[5].FindControl("chkUpdateAction");
                        chk.Checked = true;
                        int PageNameId = Convert.ToInt32(gvPageInfoList.DataKeys[row.RowIndex].Value.ToString());
                        PrivilegeOperation.setUpdatePrivilege(PageNameId.ToString(), (byte)1, ddlCurrentUserList.SelectedItem.Value);
                    }
                }
                else
                {
                    foreach (GridViewRow row in gvPageInfoList.Rows)
                    {
                        chk = (CheckBox)row.Cells[5].FindControl("chkUpdateAction");
                        chk.Checked = false;
                        int PageNameId = Convert.ToInt32(gvPageInfoList.DataKeys[row.RowIndex].Value.ToString());
                        PrivilegeOperation.setUpdatePrivilege(PageNameId.ToString(), (byte)0, ddlCurrentUserList.SelectedItem.Value);
                    }
                }


            }
            catch { }
        }

        protected void hdChkDeleteAction_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                CheckBox chk = (CheckBox)gvPageInfoList.HeaderRow.FindControl("hdChkDeleteAction");
                if (chk.Checked)
                {
                    foreach (GridViewRow row in gvPageInfoList.Rows)
                    {
                        chk = (CheckBox)row.Cells[6].FindControl("chkDeleteAction");
                        chk.Checked = true;
                        int PageNameId = Convert.ToInt32(gvPageInfoList.DataKeys[row.RowIndex].Value.ToString());
                        PrivilegeOperation.setDeletePrivilege(PageNameId.ToString(), (byte)1, ddlCurrentUserList.SelectedItem.Value);
                    }
                }
                else
                {
                    foreach (GridViewRow row in gvPageInfoList.Rows)
                    {
                        chk = (CheckBox)row.Cells[6].FindControl("chkDeleteAction");
                        chk.Checked = false;
                        int PageNameId = Convert.ToInt32(gvPageInfoList.DataKeys[row.RowIndex].Value.ToString());
                        PrivilegeOperation.setDeletePrivilege(PageNameId.ToString(), (byte)0, ddlCurrentUserList.SelectedItem.Value);
                    }
                }


            }
            catch { }
        }

        protected void hdChkGenerateAction_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                CheckBox chk = (CheckBox)gvPageInfoList.HeaderRow.FindControl("hdChkGenerateAction");
                if (chk.Checked)
                {
                    foreach (GridViewRow row in gvPageInfoList.Rows)
                    {
                        chk = (CheckBox)row.Cells[6].FindControl("chkGenerateAction");
                        chk.Checked = true;
                        int PageNameId = Convert.ToInt32(gvPageInfoList.DataKeys[row.RowIndex].Value.ToString());
                        PrivilegeOperation.setGeneratePrivilege(PageNameId.ToString(), (byte)1, ddlCurrentUserList.SelectedItem.Value);
                    }
                }
                else
                {
                    foreach (GridViewRow row in gvPageInfoList.Rows)
                    {
                        chk = (CheckBox)row.Cells[6].FindControl("chkGenerateAction");
                        chk.Checked = false;
                        int PageNameId = Convert.ToInt32(gvPageInfoList.DataKeys[row.RowIndex].Value.ToString());
                        PrivilegeOperation.setGeneratePrivilege(PageNameId.ToString(), (byte)0, ddlCurrentUserList.SelectedItem.Value);
                    }
                }


            }
            catch { }
        }  

    }
}