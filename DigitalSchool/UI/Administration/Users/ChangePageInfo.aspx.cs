using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DS.BLL.ControlPanel;
using System.Data.SqlClient;
using DS.DAL;

namespace DS.UI.Administration.Users
{
    public partial class ChangePageInfo : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            lblMessage.InnerText = "";
            if (!IsPostBack)
            {
                UserTypeInfoEntry.GetUserTypeList(ddlUserTypeList);
                try
                {
                    if (!PrivilegeOperation.SetPrivilegeControl(Session["__UserTypeId__"].ToString(), "ChangePageInfo.aspx")) Response.Redirect(Request.UrlReferrer.ToString() + "?hasperm=no");
                    string id = Request.QueryString["id"].ToString();
                    ddlUserTypeList.SelectedValue = id.ToString();
                    UserTypeInfoEntry.GetPageInfoListByUserType(ddlUserTypeList.SelectedValue.ToString(), gvPageInfoList);
                    setHeaderCheckBox();
                }
                catch { }
            }
        }
        protected void ddlUserTypeList_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlUserTypeList.SelectedItem.Value == "0")
            {
                gvPageInfoList.DataSource = null;
                gvPageInfoList.DataBind();
                lblMessage.InnerText = "warning->Please select a valid user type";
            }
            else UserTypeInfoEntry.GetPageInfoListByUserType(ddlUserTypeList.SelectedValue.ToString(),gvPageInfoList);
            setHeaderCheckBox();
            
        }

        private void setHeaderCheckBox()
        {
            try
            {
                // for test header checkbox checked at first time----------
                {
                    for (byte b = 3; b <= 8; b++)
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
                        else if (b == 7)
                        {
                            CheckedRowsAmount(7, "chkGenerateAction", out  checkedRowsAmount);
                            chk = (CheckBox)gvPageInfoList.HeaderRow.FindControl("hdChkGenerateAction");
                        }
                        else
                        {
                            CheckedRowsAmount(7, "chkAllAction", out  checkedRowsAmount);
                            chk = (CheckBox)gvPageInfoList.HeaderRow.FindControl("hdChkAllAction");
                        }
                        if (checkedRowsAmount == gvPageInfoList.Rows.Count) chk.Checked = true;
                        else chk.Checked = false;
                    }
                }
                //--------------------------------------------------------
            }
            catch { }
        }

        private void CheckedRowsAmount(byte cIndex, string ControlName, out byte checkedRowsAmount)
        {
            try
            {
                byte i = 0;
                foreach (GridViewRow gvr in gvPageInfoList.Rows)
                {
                    CheckBox chk = (CheckBox)gvr.Cells[cIndex].FindControl(ControlName);
                    if (chk.Checked) i++;
                }
                checkedRowsAmount = i;
            }
            catch { checkedRowsAmount = 0; }
        }

        protected void chkChosen_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                GridViewRow gvr = ((GridViewRow)((Control)sender).Parent.Parent);
                int index_row = gvr.RowIndex;

                int PageNameId = Convert.ToInt32(gvPageInfoList.DataKeys[index_row].Value.ToString());

                CheckBox chk = (CheckBox)gvPageInfoList.Rows[index_row].Cells[1].FindControl("chkChosen");

                byte Action = (chk.Checked) ? (byte)1 : (byte)0;

                if (!chk.Checked)
                {
                    SqlCommand cmd = new SqlCommand("delete from UserTypePageInfo where PageNameId=" + PageNameId + " AND UserTypeId="+ddlUserTypeList.SelectedValue.ToString()+"", DbConnection.Connection);
                    cmd.ExecuteNonQuery();
                }
                else
                {
                    SqlCommand cmd = new SqlCommand("insert into UserTypePageInfo (UserTypeId,PageNameId,Chosen) values(" + ddlUserTypeList.SelectedItem.Value + "," + PageNameId + "," + Action + ")", DbConnection.Connection);
                    cmd.ExecuteNonQuery();
                }
               // cmd = new SqlCommand("Update PageInfo set Chosen=" + Action + ",UserTypeId=" + ddlUserTypeList.SelectedItem.Value + " where PageNameId=" + PageNameId + "", DbConnection.Connection);
               // cmd.ExecuteNonQuery();
                
               
            }
            catch { }
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

                PrivilegeOperation.setViewPrivilege(PageNameId.ToString(), Action, ddlUserTypeList.SelectedItem.Value);  // set user view privilege

                //--for checked and select header rows----------------------------------------
                byte checkedRowsAmount = 0;
                CheckedRowsAmount(3, "chkViewAction", out  checkedRowsAmount);
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

                PrivilegeOperation.setSavePrivilege(PageNameId.ToString(), Action, ddlUserTypeList.SelectedItem.Value);  // set user save privilege

                //--for checked and select header rows----------------------------------------
                byte checkedRowsAmount = 0;
                CheckedRowsAmount(4, "chkSaveAction", out  checkedRowsAmount);
                chk = (CheckBox)gvPageInfoList.HeaderRow.FindControl("hdChkSaveAction");

                if (checkedRowsAmount == gvPageInfoList.Rows.Count) chk.Checked = true;
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

                PrivilegeOperation.setUpdatePrivilege(PageNameId.ToString(), Action, ddlUserTypeList.SelectedItem.Value);  // set user update privilege

                //--for checked and select header rows----------------------------------------
                byte checkedRowsAmount = 0;
                CheckedRowsAmount(5, "chkUpdateAction", out  checkedRowsAmount);
                chk = (CheckBox)gvPageInfoList.HeaderRow.FindControl("hdChkUpdateAction");

                if (checkedRowsAmount == gvPageInfoList.Rows.Count) chk.Checked = true;
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

                PrivilegeOperation.setDeletePrivilege(PageNameId.ToString(), Action, ddlUserTypeList.SelectedItem.Value);  // set user delete privilege

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

                PrivilegeOperation.setGeneratePrivilege(PageNameId.ToString(), Action, ddlUserTypeList.SelectedItem.Value);  // set user Generate privilege

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
        protected void chkAllAction_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                GridViewRow gvr = ((GridViewRow)((Control)sender).Parent.Parent);
                int index_row = gvr.RowIndex;
                int PageNameId = Convert.ToInt32(gvPageInfoList.DataKeys[index_row].Value.ToString());

                CheckBox chk = (CheckBox)gvPageInfoList.Rows[index_row].Cells[8].FindControl("chkAllAction");

                CheckBox chkView = (CheckBox)gvPageInfoList.Rows[index_row].Cells[3].FindControl("chkViewAction");
                CheckBox chkSave = (CheckBox)gvPageInfoList.Rows[index_row].Cells[4].FindControl("chkSaveAction");
                CheckBox chkUpdate = (CheckBox)gvPageInfoList.Rows[index_row].Cells[5].FindControl("chkUpdateAction");
                CheckBox chkDelete = (CheckBox)gvPageInfoList.Rows[index_row].Cells[6].FindControl("chkDeleteAction");
                CheckBox chkGenerate = (CheckBox)gvPageInfoList.Rows[index_row].Cells[7].FindControl("chkGenerateAction");
                if (chk.Checked)
                {
                    chkView.Checked = true; chkSave.Checked = true; chkUpdate.Checked = true; chkDelete.Checked = true; chkGenerate.Checked = true;

                    PrivilegeOperation.setViewPrivilege(PageNameId.ToString(), 1, ddlUserTypeList.SelectedItem.Value);  // set user view privilege
                    PrivilegeOperation.setSavePrivilege(PageNameId.ToString(), 1, ddlUserTypeList.SelectedItem.Value);  // set user save privilege
                    PrivilegeOperation.setUpdatePrivilege(PageNameId.ToString(), 1, ddlUserTypeList.SelectedItem.Value);  // set user update privilege
                    PrivilegeOperation.setDeletePrivilege(PageNameId.ToString(), 1, ddlUserTypeList.SelectedItem.Value);  // set user delete privilege
                    PrivilegeOperation.setGeneratePrivilege(PageNameId.ToString(),1, ddlUserTypeList.SelectedItem.Value);  // set user Generate privilege
                }
                else
                {
                    chkView.Checked = false; chkSave.Checked = false; chkUpdate.Checked = false; chkDelete.Checked = false; chkGenerate.Checked = false;
                    PrivilegeOperation.setViewPrivilege(PageNameId.ToString(), 0, ddlUserTypeList.SelectedItem.Value);  // set user view privilege
                    PrivilegeOperation.setSavePrivilege(PageNameId.ToString(), 0, ddlUserTypeList.SelectedItem.Value);  // set user save privilege
                    PrivilegeOperation.setUpdatePrivilege(PageNameId.ToString(), 0, ddlUserTypeList.SelectedItem.Value);  // set user update privilege
                    PrivilegeOperation.setDeletePrivilege(PageNameId.ToString(), 0, ddlUserTypeList.SelectedItem.Value);  // set user delete privilege
                    PrivilegeOperation.setGeneratePrivilege(PageNameId.ToString(), 0, ddlUserTypeList.SelectedItem.Value);  // set user Generate privilege
                }

                //--for checked and select header rows----------------------------------------
                byte checkedRowsAmount = 0;
                CheckedRowsAmount(8, "chkAllAction", out  checkedRowsAmount);
                chk = (CheckBox)gvPageInfoList.HeaderRow.FindControl("hdChkAllAction");

                if (checkedRowsAmount == gvPageInfoList.Rows.Count) chk.Checked = true;
                else { chk.Checked = false; }
                //----------------------------------------------------------------------------
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
                    foreach (GridViewRow row in gvPageInfoList.Rows)
                    {
                        chk = (CheckBox)row.Cells[3].FindControl("chkViewAction");
                        chk.Checked = true;
                        int PageNameId = Convert.ToInt32(gvPageInfoList.DataKeys[row.RowIndex].Value.ToString());
                        PrivilegeOperation.setViewPrivilege(PageNameId.ToString(), (byte)1, ddlUserTypeList.SelectedItem.Value);
                    }
                }
                else
                {
                    foreach (GridViewRow row in gvPageInfoList.Rows)
                    {
                        chk = (CheckBox)row.Cells[3].FindControl("chkViewAction");
                        chk.Checked = false;
                        int PageNameId = Convert.ToInt32(gvPageInfoList.DataKeys[row.RowIndex].Value.ToString());
                        PrivilegeOperation.setViewPrivilege(PageNameId.ToString(), (byte)0, ddlUserTypeList.SelectedItem.Value);
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
                        PrivilegeOperation.setSavePrivilege(PageNameId.ToString(), (byte)1, ddlUserTypeList.SelectedItem.Value);
                    }
                }
                else
                {
                    foreach (GridViewRow row in gvPageInfoList.Rows)
                    {
                        chk = (CheckBox)row.Cells[4].FindControl("chkSaveAction");
                        chk.Checked = false;
                        int PageNameId = Convert.ToInt32(gvPageInfoList.DataKeys[row.RowIndex].Value.ToString());
                        PrivilegeOperation.setSavePrivilege(PageNameId.ToString(), (byte)0, ddlUserTypeList.SelectedItem.Value);
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
                        PrivilegeOperation.setUpdatePrivilege(PageNameId.ToString(), (byte)1, ddlUserTypeList.SelectedItem.Value);
                    }
                }
                else
                {
                    foreach (GridViewRow row in gvPageInfoList.Rows)
                    {
                        chk = (CheckBox)row.Cells[5].FindControl("chkUpdateAction");
                        chk.Checked = false;
                        int PageNameId = Convert.ToInt32(gvPageInfoList.DataKeys[row.RowIndex].Value.ToString());
                        PrivilegeOperation.setUpdatePrivilege(PageNameId.ToString(), (byte)0, ddlUserTypeList.SelectedItem.Value);
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
                        PrivilegeOperation.setDeletePrivilege(PageNameId.ToString(), (byte)1, ddlUserTypeList.SelectedItem.Value);
                    }
                }
                else
                {
                    foreach (GridViewRow row in gvPageInfoList.Rows)
                    {
                        chk = (CheckBox)row.Cells[6].FindControl("chkDeleteAction");
                        chk.Checked = false;
                        int PageNameId = Convert.ToInt32(gvPageInfoList.DataKeys[row.RowIndex].Value.ToString());
                        PrivilegeOperation.setDeletePrivilege(PageNameId.ToString(), (byte)0, ddlUserTypeList.SelectedItem.Value);
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
                        PrivilegeOperation.setGeneratePrivilege(PageNameId.ToString(), (byte)1, ddlUserTypeList.SelectedItem.Value);
                    }
                }
                else
                {
                    foreach (GridViewRow row in gvPageInfoList.Rows)
                    {
                        chk = (CheckBox)row.Cells[6].FindControl("chkGenerateAction");
                        chk.Checked = false;
                        int PageNameId = Convert.ToInt32(gvPageInfoList.DataKeys[row.RowIndex].Value.ToString());
                        PrivilegeOperation.setGeneratePrivilege(PageNameId.ToString(), (byte)0, ddlUserTypeList.SelectedItem.Value);
                    }
                }


            }
            catch { }
        }
        protected void hdChkAllAction_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                CheckBox chk = (CheckBox)gvPageInfoList.HeaderRow.FindControl("hdChkAllAction");
                CheckBox chkView;
                CheckBox chkSave;
                CheckBox chkUpdate;
                CheckBox chkDelete;
                CheckBox chkGenerate;
                if (chk.Checked)
                {
                    foreach (GridViewRow row in gvPageInfoList.Rows)
                    {
                        int PageNameId = Convert.ToInt32(gvPageInfoList.DataKeys[row.RowIndex].Value.ToString());

                        chk = (CheckBox)row.Cells[6].FindControl("chkAllAction");
                        chk.Checked = true;


                        chkView = (CheckBox)row.Cells[3].FindControl("chkViewAction");
                        chkView.Checked = true;
                        PrivilegeOperation.setViewPrivilege(PageNameId.ToString(), (byte)1, ddlUserTypeList.SelectedItem.Value);

                        chkSave = (CheckBox)row.Cells[4].FindControl("chkSaveAction");
                        chkSave.Checked = true;
                        PrivilegeOperation.setSavePrivilege(PageNameId.ToString(), (byte)1, ddlUserTypeList.SelectedItem.Value);

                        chkUpdate = (CheckBox)row.Cells[5].FindControl("chkUpdateAction");
                        chkUpdate.Checked = true;
                        PrivilegeOperation.setUpdatePrivilege(PageNameId.ToString(), (byte)1, ddlUserTypeList.SelectedItem.Value);

                        chkDelete = (CheckBox)row.Cells[6].FindControl("chkDeleteAction");
                        chkDelete.Checked = true;
                        PrivilegeOperation.setDeletePrivilege(PageNameId.ToString(), (byte)1, ddlUserTypeList.SelectedItem.Value);

                        chkGenerate = (CheckBox)row.Cells[7].FindControl("chkGenerateAction");
                        chkGenerate.Checked = true;
                        PrivilegeOperation.setGeneratePrivilege(PageNameId.ToString(), (byte)1, ddlUserTypeList.SelectedItem.Value);
                       
                       
                    }
                    chk = (CheckBox)gvPageInfoList.HeaderRow.FindControl("hdChkViewAction");
                    chk.Checked = true;
                    chk = (CheckBox)gvPageInfoList.HeaderRow.FindControl("hdChkSaveAction");
                    chk.Checked = true;
                    chk = (CheckBox)gvPageInfoList.HeaderRow.FindControl("hdChkUpdateAction");
                    chk.Checked = true;
                    chk = (CheckBox)gvPageInfoList.HeaderRow.FindControl("hdChkDeleteAction");
                    chk.Checked = true;
                    chk = (CheckBox)gvPageInfoList.HeaderRow.FindControl("hdChkGenerateAction");
                    chk.Checked = true;
                }
                else
                {
                    foreach (GridViewRow row in gvPageInfoList.Rows)
                    {
                        int PageNameId = Convert.ToInt32(gvPageInfoList.DataKeys[row.RowIndex].Value.ToString());

                        chk = (CheckBox)row.Cells[6].FindControl("chkAllAction");
                        chk.Checked = false;


                        chkView = (CheckBox)row.Cells[3].FindControl("chkViewAction");
                        chkView.Checked = false;
                        PrivilegeOperation.setViewPrivilege(PageNameId.ToString(), (byte)0, ddlUserTypeList.SelectedItem.Value);

                        chkSave = (CheckBox)row.Cells[4].FindControl("chkSaveAction");
                        chkSave.Checked = false;
                        PrivilegeOperation.setSavePrivilege(PageNameId.ToString(), (byte)0, ddlUserTypeList.SelectedItem.Value);

                        chkUpdate = (CheckBox)row.Cells[5].FindControl("chkUpdateAction");
                        chkUpdate.Checked = false;
                        PrivilegeOperation.setUpdatePrivilege(PageNameId.ToString(), (byte)0, ddlUserTypeList.SelectedItem.Value);

                        chkDelete = (CheckBox)row.Cells[6].FindControl("chkDeleteAction");
                        chkDelete.Checked = false;
                        PrivilegeOperation.setDeletePrivilege(PageNameId.ToString(), (byte)0, ddlUserTypeList.SelectedItem.Value);

                        chkGenerate = (CheckBox)row.Cells[7].FindControl("chkGenerateAction");
                        chkGenerate.Checked = false;
                        PrivilegeOperation.setGeneratePrivilege(PageNameId.ToString(), (byte)0, ddlUserTypeList.SelectedItem.Value);
                    }
                    chk = (CheckBox)gvPageInfoList.HeaderRow.FindControl("hdChkViewAction");
                    chk.Checked = false;
                    chk = (CheckBox)gvPageInfoList.HeaderRow.FindControl("hdChkSaveAction");
                    chk.Checked = false;
                    chk = (CheckBox)gvPageInfoList.HeaderRow.FindControl("hdChkUpdateAction");
                    chk.Checked = false;
                    chk = (CheckBox)gvPageInfoList.HeaderRow.FindControl("hdChkDeleteAction");
                    chk.Checked = false;
                    chk = (CheckBox)gvPageInfoList.HeaderRow.FindControl("hdChkGenerateAction");
                    chk.Checked = false;
                }


            }
            catch { }
        }

        protected void gvPageInfoList_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            try
            {
                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    e.Row.Attributes["onmouseover"] = "javascript:SetMouseOver(this)";
                    e.Row.Attributes["onmouseout"] = "javascript:SetMouseOut(this)";
                }
            }
            catch { }
        }

        
    }
}