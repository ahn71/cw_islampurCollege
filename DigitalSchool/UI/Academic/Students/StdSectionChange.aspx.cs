using DS.BLL;
using DS.BLL.Admission;
using DS.BLL.ControlPanel;
using DS.BLL.GeneralSettings;
using DS.BLL.ManagedBatch;
using DS.BLL.ManagedClass;
using DS.DAL.ComplexScripting;
using DS.PropertyEntities.Model.Admission;
using DS.PropertyEntities.Model.ManagedBatch;
using DS.PropertyEntities.Model.ManagedClass;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DS.UI.Academic.Students
{
    public partial class StdSectionChange : System.Web.UI.Page
    {
        List<SectionChangeEntities> SecChangeList;
        ClassGroupEntry clsgrpEntry;
        CurrentStdEntry cstd;
        SectionChangeEntry secCngEntry;
        protected void Page_Load(object sender, EventArgs e)
        {
            lblMessage.InnerText = string.Empty;
            if (!IsPostBack)
            {
                //---url bind---
                aDashboard.HRef = "~/" + Classes.Routing.DashboardRouteUrl;
                aAcademicHome.HRef = "~/" + Classes.Routing.AcademicRouteUrl;
                aStudentHome.HRef = "~/" + Classes.Routing.StudentHomeRouteUrl;
                //---url bind end---

                if (!PrivilegeOperation.SetPrivilegeControl(float.Parse(Session["__UserTypeId__"].ToString()), "StdPromotion.aspx")) Response.Redirect(Request.UrlReferrer.ToString() + "?&hasperm=no");
                BatchEntry.GetDropdownlist(dlPreviousBatch, "True");
                ShiftEntry.GetDropDownList(dlShift);
                GetBlankTable(string.Empty);
                txtChangeDate.Text = TimeZoneBD.getCurrentTimeBD("dd-MM-yyyy");
            }

        }
        private void GetBlankTable(string msg)
        {
            if (msg == string.Empty)
            {
                msg = "Please search for student promotion";
            }
            string divInfo = string.Empty;
            divInfo = " <table id='tblClassList' class='display'> ";
            divInfo += "<thead>";
            divInfo += "<tr>";
            divInfo += "<th>S.No</th>";
            divInfo += "<th>Roll No</th>";
            divInfo += "<th>Student Name</th>";
            divInfo += "<th>Class</th>";
            divInfo += "<th>Group</th>";
            divInfo += "<th>Current Section</th>";
            divInfo += "<th>New. Section</th>";
            divInfo += "</tr>";
            divInfo += "</thead>";
            divInfo += "<tbody>";
            divInfo += "<tr><td colspan='10'>" + msg + "</td></tr>";
            divInfo += "</tbody>";
            divInfo += "<tfoot>";
            divInfo += "</table>";
            admStdAssignPanel.Controls.Add(new LiteralControl(divInfo));
        }

        protected void dlPreviousBatch_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                string[] BatchClsID = dlPreviousBatch.SelectedValue.Split('_');

                if (clsgrpEntry == null)
                {
                    clsgrpEntry = new ClassGroupEntry();
                }
                clsgrpEntry.GetDropDownListClsGrpId(int.Parse(BatchClsID[1]), ddlMainGroup);
                ClassSectionEntry.GetEntitiesData(ddlSection, int.Parse(BatchClsID[1]), ddlMainGroup.SelectedValue);

                if (ddlMainGroup.Enabled == true)
                {
                    divGroup.Visible = true;
                }
                else
                {
                    divGroup.Visible = false;
                }
            }
            catch { }
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            SearchCurrentStudent();
        }
        private void SearchCurrentStudent()
        {
            try
            {
                if (dlPreviousBatch.SelectedValue != "0" && dlShift.SelectedValue != "0" && ddlSection.SelectedValue != "0")
                {


                    string[] PBatchClsID = dlPreviousBatch.SelectedValue.Split('_');

                    BatchPromotionEntry btcprm = new BatchPromotionEntry();
                    if (cstd == null)
                        cstd = new CurrentStdEntry();
                    DataTable dt = new DataTable();
                    string GroupID = (ddlMainGroup.Enabled) ? " and ClsGrpID='" + ddlMainGroup.SelectedValue + "'" : "";
                    dt = cstd.GetCurrentStudent(" where ShiftID='" + dlShift.SelectedValue + "' and BatchID='" + PBatchClsID[0] + "' " + GroupID + "  and ClsSecID='" + ddlSection.SelectedValue + "' ");

                    gvstdlist.DataSource = dt;
                    gvstdlist.DataBind();
                    return;
                }
                lblMessage.InnerText = "warning-> Please Select Shift, Batch and Section before searching";
            }
            catch { }
        }

        protected void gvstdlist_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            try
            {
                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    //Find the DropDownList in the Row
                    string[] batchClasId = dlPreviousBatch.SelectedValue.Split('_');
                    DropDownList ddlNewSection = (e.Row.FindControl("ddlNewSection") as DropDownList);
                    ClassSectionEntry.GetEntitiesData(ddlNewSection, int.Parse(batchClasId[1]), ddlMainGroup.SelectedValue);

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
            catch { }
        }

        protected void ddlMainGroup_SelectedIndexChanged(object sender, EventArgs e)
        {
            string[] BatchClsID = dlPreviousBatch.SelectedValue.Split('_');
            ClassSectionEntry.GetEntitiesData(ddlSection, int.Parse(BatchClsID[1]), ddlMainGroup.SelectedValue);
        }

        protected void hdChk_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                CheckBox chk = (CheckBox)gvstdlist.HeaderRow.FindControl("hdChk");
                if (chk.Checked)
                {
                    foreach (GridViewRow row in gvstdlist.Rows)
                    {
                        chk = (CheckBox)row.Cells[7].FindControl("chkStatus");
                        chk.Checked = true;

                    }
                }
                else
                {
                    foreach (GridViewRow row in gvstdlist.Rows)
                    {
                        chk = (CheckBox)row.Cells[7].FindControl("chkStatus");
                        chk.Checked = false;

                    }
                }


            }
            catch { }
        }

        protected void chkStatus_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                GridViewRow gvr = ((GridViewRow)((Control)sender).Parent.Parent);
                int index_row = gvr.RowIndex;

                CheckBox chk = (CheckBox)gvstdlist.Rows[index_row].Cells[7].FindControl("chkStatus");

                byte Action = (chk.Checked) ? (byte)1 : (byte)0;

                //--for checked and select header rows----------------------------------------
                byte checkedRowsAmount = 0;
                CheckedRowsAmount(7, "chkStatus", out  checkedRowsAmount);
                chk = (CheckBox)gvstdlist.HeaderRow.FindControl("hdChk");

                if (checkedRowsAmount == gvstdlist.Rows.Count)
                {

                    chk.Checked = true;
                }
                else { chk.Checked = false; }
                //----------------------------------------------------------------------------
            }
            catch { }
        }
        private void CheckedRowsAmount(byte cIndex, string ControlName, out byte checkedRowsAmount)
        {
            try
            {
                byte i = 0;
                foreach (GridViewRow gvr in gvstdlist.Rows)
                {
                    CheckBox chk = (CheckBox)gvr.Cells[cIndex].FindControl(ControlName);
                    if (chk.Checked) i++;
                }
                checkedRowsAmount = i;
            }
            catch { checkedRowsAmount = 0; }
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            try
            {
               
                if (gvstdlist.Rows.Count > 0)
                {
                    if (txtChangeDate.Text.Trim().Length < 8)
                    { lblMessage.InnerText = "warning-> Please,select change date."; txtChangeDate.Focus(); return; }
                    byte checkedRowsAmount = 0;
                    CheckedRowsAmount(7, "chkStatus", out  checkedRowsAmount);
                    if (checkedRowsAmount == 0)
                    {
                        lblMessage.InnerText = "warning-> Please,select student(s)."; return;
                    }
                  //  string[] ChangeDate = txtChangeDate.Text.Trim().Split('-');
                    var saveList = new List<SectionChangeEntities>();
                    int count = 0;
                    foreach (GridViewRow row in gvstdlist.Rows)
                    {
                        CheckBox chkStatus = row.FindControl("chkStatus") as CheckBox;
                        if (chkStatus.Checked == true)
                        {
                            HiddenField stdId = row.FindControl("stdID") as HiddenField;

                            DropDownList ddlNewSection = row.FindControl("ddlNewSection") as DropDownList;
                            if (ddlNewSection.SelectedIndex < 1)
                            { lblMessage.InnerText = "warning-> Please,select new section."; ddlNewSection.Focus(); return; }
                            saveList.Add(new SectionChangeEntities()
                            {
                                Student = new CurrentStdEntities()
                                {
                                    StudentID = int.Parse(stdId.Value.Trim())
                                },
                                PreClsSecID = int.Parse(gvstdlist.DataKeys[row.RowIndex].Values[0].ToString()),
                                NewClsSecID = int.Parse(ddlNewSection.SelectedValue),
                                ChangeDate = convertDateTime.DMYtoYMD(txtChangeDate.Text.Trim())// DateTime.Parse(ChangeDate[2] + "-" + ChangeDate[1] + "-" + ChangeDate[0])
                        });
                            count++;
                        }

                    }
                    if (count > 0)
                    {
                        if (secCngEntry == null)
                        {
                            secCngEntry = new SectionChangeEntry();
                        }
                        if (secCngEntry.SectionChange(saveList))
                        {
                            lblMessage.InnerText = "success->Successfully Section Changed.";

                        }
                    }
                }


            }
            catch
            { }
        }
    }
}