using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using DS.BLL.Timetable;
using DS.PropertyEntities.Model;
using DS.PropertyEntities.Model.Timetable;
using DS.BLL.ControlPanel;
using DS.BLL.GeneralSettings;
using DS.PropertyEntities.Model.GeneralSettings;

namespace DS.UI.Academic.Timetable.SetTimings
{
    public partial class ClassTimeSpecification : System.Web.UI.Page
    {
        public ClsTimeSpecificationEntry clsTimeSpecification;
        ShiftEntry shtEntry;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (!PrivilegeOperation.SetPrivilegeControl(int.Parse(Session["__UserTypeId__"].ToString()), "ClassTimeSpecification.aspx", btnSave)) Response.Redirect(Request.UrlReferrer.ToString() + "?hasperm=no");
                //TimeNumberCreator(DrpSHour, DrpSMin);
                //TimeNumberCreator(DrpEHour, DrpEMin);
                ShiftEntry.GetDropDownList(dlShift);
                DataBindToGridView(null);
            }
            lblMessage.InnerText = "";
        }             

        private void TimeNumberCreator(DropDownList DrpHList, DropDownList DrpMList)
        {
            for (byte h = 1; h <= 12; h++)
            {
                if (h < 10) DrpHList.Items.Add("0" + h.ToString());
                else DrpHList.Items.Add(h.ToString());
            }
            DrpHList.Text = "07";
            for (byte m = 0; m < 60; m++)
            {
                if (m < 10) DrpMList.Items.Add("0" + m.ToString());
                else DrpMList.Items.Add(m.ToString());
            }
        }

        private ClassTimeSpecificationEntities GetFormData()
        {
            ClassTimeSpecificationEntities clsTimeSpcificationEntities = new ClassTimeSpecificationEntities();
            clsTimeSpcificationEntities.ClsTimeID = int.Parse(lblClsTimeId.Value);
            clsTimeSpcificationEntities.ShiftId = int.Parse(dlShift.SelectedValue);
            clsTimeSpcificationEntities.Name = ddlPeriod.Text.Trim();
            string[] stT= txtstartTime.Text.Split(' ');
            string stTime = stT[0] + ":00" + " " + stT[1];
            string[] ET = txtEndTime.Text.Split(' ');
            string EnTime = ET[0] + ":00" + " " + ET[1];
            clsTimeSpcificationEntities.StartTime = DateTime.Parse(stTime);
            clsTimeSpcificationEntities.EndTime = DateTime.Parse(EnTime);
            clsTimeSpcificationEntities.OrderBy = int.Parse(txtOrderBy.Text);
            clsTimeSpcificationEntities.IsbreakTime = ChkBrkTime.Checked;            
            return clsTimeSpcificationEntities;
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            if (lblClsTimeId.Value.ToString() == string.Empty)
            {
                if (Session["__Save__"].ToString().Equals("false")) { lblMessage.InnerText = "warning-> You don't have permission to save!"; DataBindToGridView(int.Parse(dlShift.SelectedValue)); return; }
                if (Validation() == false)
                {                    
                    DataBindToGridView(int.Parse(dlShift.SelectedValue));
                    ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "call me", "LoadJS();", true);
                    return;
                }
                lblClsTimeId.Value = "0";
                if (SaveName() == true)
                {
                    lblClsTimeId.Value = string.Empty;
                    ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "call me", "SavedSuccess();", true);
                }
            }
            else
            {
                if (UpdateName() == true)
                {
                    lblClsTimeId.Value = string.Empty;
                    ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "call me", "updateSuccess();", true);
                }
            }
        }
        private bool Validation()
        {
            try
            {
                List<ClassTimeSpecificationEntities> ClsTimeSpcftnList = null;
                if (clsTimeSpecification == null)
                {
                    clsTimeSpecification = new ClsTimeSpecificationEntry();
                }
               
                ClsTimeSpcftnList = clsTimeSpecification.GetOrderByData(int.Parse(dlShift.SelectedValue),int.Parse(txtOrderBy.Text));
                if (ClsTimeSpcftnList == null)
                {
                    
                }
                else
                {
                    lblMessage.InnerText = "warning->Already this Order Asssign for "+dlShift.SelectedItem.Text+" Shift";
                    txtOrderBy.Focus();
                    return false;
                }
                if(shtEntry==null)
                {
                    shtEntry = new ShiftEntry();
                }
                List<ShiftEntities> shtList = new List<ShiftEntities>();
                shtList = shtEntry.GetEntitiesData().FindAll(c=>c.ShiftConfigId==int.Parse(dlShift.SelectedValue));
                if(shtList!=null)
                {
                    string[] stT = txtstartTime.Text.Split(' ');
                    DateTime stTime =Convert.ToDateTime(stT[0] + ":00" + " " + stT[1]);
                    DateTime sftstTime = Convert.ToDateTime(shtList[0].StartTime.ToString());
                    DateTime sftednTime=Convert.ToDateTime(shtList[0].EndTime.ToString());

                    string[] ET = txtEndTime.Text.Split(' ');
                    DateTime EnTime =Convert.ToDateTime(ET[0] + ":00" + " " + ET[1]);
                    if((sftstTime>stTime||sftednTime<stTime))
                    {
                        lblMessage.InnerText = "warning->" + dlShift.SelectedItem.Text + " Shift Period Range " + sftstTime.ToString("hh:mm tt") + " to " + sftednTime.ToString("hh:mm tt") + "";
                        return false;
                    }
                    else if (sftstTime > EnTime || sftednTime < EnTime)
                    {
                        lblMessage.InnerText = "warning->" + dlShift.SelectedItem.Text + " Shift Period Range " + sftstTime.ToString("hh:mm tt") + " to " + sftednTime.ToString("hh:mm tt") + "";
                        return false;
                    }
                }

                return true;
            }
            catch { return false; }
        }

        private Boolean SaveName()
        {
            try
            {
                using (ClassTimeSpecificationEntities entities = GetFormData())
                {
                    if (clsTimeSpecification == null)
                    {
                        clsTimeSpecification = new ClsTimeSpecificationEntry();
                    }
                    clsTimeSpecification.AddEntities = entities;
                    bool result = clsTimeSpecification.Insert();
                    DataBindToGridView(entities.ShiftId);
                    lblClsTimeId.Value = string.Empty;
                    if(!result)
                    {
                        lblMessage.InnerText = "error-> Unable to save";
                        return false;
                    }
                    return true;      
                }                
            }
            catch (Exception ex)
            {
                lblMessage.InnerText = "error->" + ex.Message;
                return false;
            }
        }

        private Boolean UpdateName()
        {
            try
            {
                using (ClassTimeSpecificationEntities entities = GetFormData())
                {
                    if (clsTimeSpecification == null)
                    {
                        clsTimeSpecification = new ClsTimeSpecificationEntry();
                    }
                    clsTimeSpecification.AddEntities = entities;
                    bool result = clsTimeSpecification.Update();
                    lblClsTimeId.Value = string.Empty;
                    DataBindToGridView(entities.ShiftId);
                    if (!result)
                    {
                        lblMessage.InnerText = "error-> Unable to update";
                        return false;
                    }
                    return true;
                }
            }
            catch (Exception ex)
            {
                lblMessage.InnerText = "error->" + ex.Message;
                return false;
            }
        }
        private void DataBindToGridView(int? SID)
        {
            string divInfo = string.Empty;
            List<ClassTimeSpecificationEntities> ClsTimeSpcftnList = null;
            if (clsTimeSpecification == null)
            {
                clsTimeSpecification = new ClsTimeSpecificationEntry();
            }
            if (SID != null)
            {
                ClsTimeSpcftnList = clsTimeSpecification.GetEntitiesData(SID);
            }
            divInfo = " <table id='tblClassList' class='table table-striped table-bordered dt-responsive nowrap'cellspacing='0' Width='100%'> ";
            divInfo += "<thead>";
            divInfo += "<tr>";
            divInfo += "<th>Serial No</th>";
            divInfo += "<th>Shift Name</th>";
            divInfo += "<th>Name</th>";
            divInfo += "<th>Start Time</th>";
            divInfo += "<th>End Time</th>";
            if (Session["__Update__"].ToString().Equals("true"))
            divInfo += "<th>Edit</th>";            
            divInfo += "</tr>";
            divInfo += "</thead>";
            divInfo += "<tbody>";
            if (ClsTimeSpcftnList == null)
            {                
                divInfo += "</tbody>";               
                divInfo += "</table>";
                divList.Controls.Add(new LiteralControl(divInfo));
                return;
            }
            string shiftID = string.Empty;
            string ClsTID = string.Empty;
            string ClsTimeSetNameId = string.Empty;
            string brkTime = string.Empty;
            for (int x = 0; x < ClsTimeSpcftnList.Count; x++)
            {                
                shiftID = ClsTimeSpcftnList[x].ShiftId.ToString();
                ClsTID = ClsTimeSpcftnList[x].ClsTimeID.ToString();
                brkTime = ClsTimeSpcftnList[x].IsbreakTime.ToString().ToLower();
                divInfo += "<tr id='r_" + ClsTID + "'>";
                divInfo += "<td id=order" + ClsTID + ">" + ClsTimeSpcftnList[x].OrderBy.ToString() + "</td>";
                divInfo += "<td><span id=ShiftName" + ClsTID + ">" + ClsTimeSpcftnList[x].ShiftName.ToString() + "</span></td>";
                divInfo += "<td><span id=ClsTimeName" + ClsTID + ">" + ClsTimeSpcftnList[x].Name.ToString() + "</span></td>";
                divInfo += "<td><span id=ClsStartTime" + ClsTID + ">" + String.Format("{0:t}", ClsTimeSpcftnList[x].StartTime) + "</span></td>";
                divInfo += "<td><span id=ClsEndTime" + ClsTID + ">" + String.Format("{0:t}", ClsTimeSpcftnList[x].EndTime) + "</span></td>";
                if (Session["__Update__"].ToString().Equals("true"))
                    divInfo += "<td class='numeric_control' >" + "<img src='/Images/gridImages/edit.png' class='editImg' onclick='editRow(" + ClsTID + "," + shiftID + ", " + brkTime + ");'/>";
            }
            divInfo += "</tbody>";
            divInfo += "<tfoot>";
            divInfo += "</table>";
            divList.Controls.Add(new LiteralControl(divInfo));
        }        

        protected void btnClear_Click(object sender, EventArgs e)
        {
            ClearField();
        }

        private void ClearField()
        {
            dlShift.SelectedValue = "0";            
            //DrpSHour.Text = "07";
            //DrpSMin.Text = "00";
            //DrpSPeriod.SelectedValue = "AM";
            //DrpEHour.Text = "07";
            //DrpEMin.Text = "00";
            //DrpEPeriod.SelectedValue = "AM";
            txtOrderBy.Text = string.Empty;
            ChkBrkTime.Checked = false;
        }

        protected void dlShift_SelectedIndexChanged(object sender, EventArgs e)
        {
            DataBindToGridView(int.Parse(dlShift.SelectedValue));
            ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "call me", "loaddatatable();", true);
        }
    }
}