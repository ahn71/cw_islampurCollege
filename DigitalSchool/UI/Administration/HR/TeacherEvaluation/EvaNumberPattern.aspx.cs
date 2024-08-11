using DS.BLL.ControlPanel;
using DS.BLL.TeacherEvaluation;
using DS.PropertyEntities.Model.TeacherEvaluation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DS.UI.Administration.HR.TeacherEvaluation
{
    public partial class EvaNumberPattern : System.Web.UI.Page
    {
        private NumberPatternDetailsEntities EntitiesD;
        private NumberPatternEntities Entities;
        private NumberPatternEntry Entry;

        private List<NumberPatternEntities> EntitiesList;
        private List<NumberPatternDetailsEntities> EntitiesListD;
        protected void Page_Load(object sender, EventArgs e)
        {
            lblMessage.InnerText = "";
            if (!IsPostBack)
            {
                if (!PrivilegeOperation.SetPrivilegeControl(int.Parse(Session["__UserTypeId__"].ToString()), "AddDepartment.aspx", btnSave)) Response.Redirect(Request.UrlReferrer.ToString() + "?hasperm=no");
                LoadNumberPatternList();
                if (Entry == null)
                    Entry = new NumberPatternEntry();
                Entry.loadNumberPatternInGridview(gvNumberPattern,"0");
                txtNumberPattern.Focus();
            }
        }

        private void LoadNumberPatternList()
        {
            EntitiesList = new List<NumberPatternEntities>(); 
            if (Entry == null)
                Entry = new NumberPatternEntry();
            EntitiesList = Entry.GetEntitiesData();
            gvNumberPatternList.DataSource = EntitiesList;
            gvNumberPatternList.DataBind();
            //int totalRows = (EntitiesList == null) ? 0 : EntitiesList.Count;
            //string divInfo = "";

            //divInfo = " <table id='tblClassList' class='table table-striped table-bordered dt-responsive nowrap'cellspacing='0' Width='100%' > ";
            //divInfo += "<thead>";
            //divInfo += "<tr>";
            //divInfo += "<th>Number Pattern</th>";         
            //if (Session["__Update__"].ToString().Equals("true"))
            //    divInfo += "<th>Edit</th>";
            //divInfo += "</tr>";
            //divInfo += "</thead>";
            //divInfo += "<tbody>";
            //if (totalRows == 0)
            //{
            //    divInfo += "</tbody></table>";
            //    divInfo += "<div class='dataTables_wrapper'><div class='head'></div></div>";
            //    divCategoryList.Controls.Add(new LiteralControl(divInfo));
            //    return;
            //}
            //string id = "";            
            //for (int x = 0; x < EntitiesList.Count; x++)
            //{
            //    id = EntitiesList[x].NumPatternID.ToString();              
            //    divInfo += "<tr id='r_" + id + "'>";
            //    divInfo += "<td >" + EntitiesList[x].NumPattern.ToString() + "</td>";               
            //    if (Session["__Update__"].ToString().Equals("true"))
            //        divInfo += "<td class='numeric_control' >" + "<img src='/Images/gridImages/edit.png' class='editImg'   onclick='editCategory(" + id + ");'  />";
            //}

            //divInfo += "</tbody>";
            //divInfo += "<tfoot>";

            //divInfo += "</table>";
            //divInfo += "<div class='dataTables_wrapper'><div class='head'></div></div>";

            //divCategoryList.Controls.Add(new LiteralControl(divInfo));
        }
        private void save()
        {

            if (!validationAndGetControlValue()) return;
            if (Entry == null)
            {
                Entry = new NumberPatternEntry();
            }
            Entry.AddEntities = Entities;
            bool result = (btnSave.Text == "Save") ? Entry.Insert() : Entry.Update();
            if (result)
            {                
                foreach(NumberPatternDetailsEntities list in EntitiesListD)
                {
                    Entry.AddEntitiesD =list ;
                    Entry.InsertD();
                }
                LoadNumberPatternList();
                AllClear();
                lblMessage.InnerText = "success-> Successfully Submitted ";
            }
        }
        private bool validationAndGetControlValue() 
        {
            EntitiesListD = new List<NumberPatternDetailsEntities>();  

            foreach (GridViewRow row in gvNumberPattern.Rows)
            {
                CheckBox check = (CheckBox)row.FindControl("ckbChosen");
                if (check.Checked)
                {
                    string SubCategoryId = gvNumberPattern.DataKeys[row.RowIndex].Value.ToString();
                    TextBox txtFullNumber = (TextBox)row.FindControl("txtFullNumber");
                    if (txtFullNumber.Text.Trim().Length < 1)
                    {
                        lblMessage.InnerText = "warning->Please enter valid value !";
                        txtFullNumber.Focus();
                        txtFullNumber.BackColor = System.Drawing.Color.Yellow;
                        return false;
                    }
                    TextBox txtExcellent = (TextBox)row.FindControl("txtExcellent");
                    if (txtExcellent.Text.Trim().Length < 1)
                    {
                        lblMessage.InnerText = "warning->Please enter valid value !";
                        txtExcellent.Focus();
                        return false;
                    }
                    TextBox txtGood = (TextBox)row.FindControl("txtGood");
                    if (txtGood.Text.Trim().Length < 1)
                    {
                        lblMessage.InnerText = "warning->Please enter valid value !";
                        txtGood.Focus();
                        return false;
                    }
                    TextBox txtMedium = (TextBox)row.FindControl("txtMedium");
                    if (txtMedium.Text.Trim().Length < 1)
                    {
                        lblMessage.InnerText = "warning->Please enter valid value !";
                        txtMedium.Focus();
                        return false;
                    }
                    TextBox txtWeak = (TextBox)row.FindControl("txtWeak");
                    if (txtWeak.Text.Trim().Length < 1)
                    {
                        lblMessage.InnerText = "warning->Please enter valid value !";
                        txtWeak.Focus();
                        return false;
                    }
                    TextBox txtSoWeak = (TextBox)row.FindControl("txtSoWeak");
                    if (txtSoWeak.Text.Trim().Length < 1)
                    {
                        lblMessage.InnerText = "warning->Please enter valid value !";
                        txtSoWeak.Focus();
                        return false;
                    }
                    EntitiesListD.Add(
                        new NumberPatternDetailsEntities
                        {
                            SubCategoryID = int.Parse(SubCategoryId),
                            FullNumber = float.Parse(txtFullNumber.Text.Trim()),
                            Excellent = float.Parse(txtExcellent.Text.Trim()),
                            Good = float.Parse(txtGood.Text.Trim()),
                            Medium = float.Parse(txtMedium.Text.Trim()),
                            Weak = float.Parse(txtWeak.Text.Trim()),
                            SoWeak = float.Parse(txtSoWeak.Text.Trim())
                        });                

                }



            }
            if (txtNumberPattern.Text.Trim().Length < 1)
            {
                lblMessage.InnerText = "warning-> Please enter number pattern name !";
                txtNumberPattern.Focus();
                return false;
            }
            Entities = new NumberPatternEntities();
            if (btnSave.Text== "Update")
                Entities.NumPatternID = int.Parse(ViewState["__NumPatternId__"].ToString());
            Entities.NumPattern = txtNumberPattern.Text;
            

            return true;
        }
        private void getCotrolValueNP()
        {
            EntitiesD = new NumberPatternDetailsEntities();
           
            

        }
        private void AllClear()
        {
            btnSave.Text = "Save";
            txtNumberPattern.Text = "";
            if (Entry == null)
                Entry = new NumberPatternEntry();
            Entry.loadNumberPatternInGridview(gvNumberPattern, "0");
            txtNumberPattern.Focus();

        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            save();
        }

        protected void gvNumberPatternList_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName.Equals("Alter"))
            {
                

                int rIndex=int.Parse(e.CommandArgument.ToString());
              string NumPatternId=gvNumberPatternList.DataKeys[rIndex].Value.ToString();
              ViewState["__NumPatternId__"] = NumPatternId;
              if (Entry == null)
                  Entry = new NumberPatternEntry();
              Entry.loadNumberPatternInGridview(gvNumberPattern, NumPatternId);
              lblSubCategoryId.Value = NumPatternId;
              txtNumberPattern.Text = gvNumberPatternList.Rows[rIndex].Cells[1].Text;
              btnSave.Text = "Update";
                CheckUncheckCkbAll();
            }
        }
        private void CheckUncheckCkbAll()
        {
            if (gvNumberPattern.HeaderRow != null)
            {
                CheckBox ckbAll = (CheckBox)gvNumberPattern.HeaderRow.FindControl("ckbAll");
                foreach (GridViewRow _row in gvNumberPattern.Rows)
                {
                    CheckBox ckb = (CheckBox)_row.FindControl("ckbChosen");
                    if (!ckb.Checked)
                    {
                        ckbAll.Checked = false;
                        break;
                    }
                       
                }
            }
        }

        protected void btnReset_Click(object sender, EventArgs e)
        {
            AllClear();
        }

        protected void ckbAll_CheckedChanged(object sender, EventArgs e)
        {
            dynamic status = sender;
           
            foreach (GridViewRow _row in gvNumberPattern.Rows)
            {
                CheckBox ckb = (CheckBox)_row.FindControl("ckbChosen");
                ckb.Checked = status.Checked;
            }
        }
    }
}