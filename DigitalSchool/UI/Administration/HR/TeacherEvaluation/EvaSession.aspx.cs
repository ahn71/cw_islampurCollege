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
    public partial class EvaSession : System.Web.UI.Page
    {
        private SessionEntities Entities;
        private List<CommitteeMemberEntities> memberlist;
        private SessionEntry Entry;      
        private List<SessionEntities> EntitiesList;
       
        protected void Page_Load(object sender, EventArgs e)
        {
            lblMessage.InnerText = "";
            if (!IsPostBack)
            {
                if (!PrivilegeOperation.SetPrivilegeControl(int.Parse(Session["__UserTypeId__"].ToString()), "AddDepartment.aspx", btnSave)) Response.Redirect(Request.UrlReferrer.ToString() + "?hasperm=no");
             //   LoadCategory();   
                SessionEntry.GetdataInGridview(gvSessionList);
                NumberPatternEntry.GetDropdownlist(ddlNumberPattern);
                UserAccountEntry.GetCurrentEvaluatorList(ckblCommittee);
            
            }
        }
        //private void LoadCategory()
        //{
        //    EntitiesList = new List<SessionEntities>();
        //    if (Entry == null)
        //        Entry = new SessionEntry();
        //    EntitiesList = Entry.GetEntitiesData();
        //    int totalRows = (EntitiesList == null) ? 0 : EntitiesList.Count;
        //    string divInfo = "";

        //    divInfo = " <table id='tblClassList' class='table table-striped table-bordered dt-responsive nowrap'cellspacing='0' Width='100%' > ";
        //    divInfo += "<thead>";
        //    divInfo += "<tr>";
        //    divInfo += "<th>Session Name</th>";
        //    divInfo += "<th>Start Date</th>";
        //    divInfo += "<th>End Date</th>";
        //    divInfo += "<th>Number Pattern</th>";
        //    if (Session["__Update__"].ToString().Equals("true"))
        //        divInfo += "<th>Edit</th>";
        //    divInfo += "</tr>";

        //    divInfo += "</thead>";

        //    divInfo += "<tbody>";
        //    if (totalRows == 0)
        //    {
        //        divInfo += "</tbody></table>";
        //        divInfo += "<div class='dataTables_wrapper'><div class='head'></div></div>";
        //        divCategoryList.Controls.Add(new LiteralControl(divInfo));
        //        return;
        //    }
        //    string id = "";
        //    string nid = "";
        //    for (int x = 0; x < EntitiesList.Count; x++)
        //    {
        //        id = EntitiesList[x].SessionID.ToString();
        //        nid = EntitiesList[x].NumPattern.NumPatternID.ToString();
        //        divInfo += "<tr id='r_" + id + "'>";
        //        divInfo += "<td >" + EntitiesList[x].Session.ToString() + "</td>";
        //        divInfo += "<td>" + EntitiesList[x].StartDate.ToString() + "</td>";
        //        divInfo += "<td>" + EntitiesList[x].EndDate.ToString() + "</td>";
        //        divInfo += "<td>" + EntitiesList[x].NumPattern.NumPattern.ToString() + "</td>";
        //        if (Session["__Update__"].ToString().Equals("true"))
        //            divInfo += "<td class='numeric_control' >" + "<img src='/Images/gridImages/edit.png' class='editImg'   onclick='editCategory(" + id + "," + nid + ");'  />";
        //    }

        //    divInfo += "</tbody>";
        //    divInfo += "<tfoot>";

        //    divInfo += "</table>";
        //    divInfo += "<div class='dataTables_wrapper'><div class='head'></div></div>";

        //    divCategoryList.Controls.Add(new LiteralControl(divInfo));
        //    gvSessionList.DataSource = EntitiesList;
        //    gvSessionList.DataBind();
        //}
        private void save()
        {
            //Entities=new CategoryEntities();
            if (!getValidationAndCotrolValue()) return;
            if (Entry == null)
            {
                Entry = new SessionEntry();
            }
            Entry.AddEntities = Entities;
              bool result = (btnSave.Text == "Save") ? Entry.Insert() : Entry.Update();
            if (result)
            {
                foreach (CommitteeMemberEntities list in memberlist)
                {
                    Entry.AddEntitiesCM =list ;
                    Entry.InsertCM();
                }
                SessionEntry.GetdataInGridview(gvSessionList);
                AllClear();
                lblMessage.InnerText = "success-> Successfully Saved ";
            }
        }
        private void AllClear()
        {
            lblCategoryId.Value = "";
            txtSessionName.Text = "";
            txtStartDate.Text = "";
            txtEndDate.Text = "";
            ckblCommittee.ClearSelection();
            ddlNumberPattern.SelectedValue = "0";
            btnSave.Text = "Save";
        }
     
        private bool  getValidationAndCotrolValue()
        {
            if (ddlNumberPattern.SelectedIndex < 1) 
            {
                lblMessage.InnerText = "warning-> Please select number pattern"; return false;
            }
            if (ckblCommittee==null || ckblCommittee.Items.Count < 1 || ckblCommittee.SelectedValue == "")
            {
                lblMessage.InnerText = "warning-> Please select evaluator member(s)"; return false;
            }
            string[] sd = txtStartDate.Text.Trim().Split('-');
            string[] ed = txtEndDate.Text.Trim().Split('-');
                Entities = new SessionEntities();
            if (lblCategoryId.Value != "")
                Entities.SessionID = int.Parse(lblCategoryId.Value);
            Entities.Session = txtSessionName.Text.Trim();
            Entities.StartDate = sd[2] + "-" + sd[1] + "-" + sd[0];
            Entities.EndDate = ed[2] + "-" + ed[1] + "-" + ed[0];
            Entities.NumPattern = new NumberPatternEntities();
            Entities.NumPattern.NumPatternID = int.Parse(ddlNumberPattern.SelectedValue);
            memberlist = new List<CommitteeMemberEntities>();
            foreach (ListItem listItem in  ckblCommittee.Items)
            {
                if (listItem.Selected)
                    memberlist.Add(new CommitteeMemberEntities 
                    { 
                        MemberID=int.Parse(listItem.Value)
                    });
            }
            return true;
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
             save();
             
        }

        protected void gvSessionList_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName.Equals("Alter"))
            {

                int rIndex = int.Parse(e.CommandArgument.ToString());
                string SessionID = gvSessionList.DataKeys[rIndex].Value.ToString();
                lblCategoryId.Value = SessionID;
                if (Entry == null)
                    Entry = new SessionEntry();
                EntitiesList = new List<SessionEntities>();
               EntitiesList= Entry.GetEntitiesData(SessionID);
               txtSessionName.Text = EntitiesList[0].Session;
               txtStartDate.Text = EntitiesList[0].StartDate;
               txtEndDate.Text = EntitiesList[0].EndDate;
               ddlNumberPattern.SelectedValue = EntitiesList[0].NumPattern.NumPatternID.ToString();
               ckblCommittee.ClearSelection();
               foreach (SessionEntities list in EntitiesList)
               {
                  
                   foreach (ListItem listItem in ckblCommittee.Items)
                   {
                       if (listItem.Value == list.Member.MemberID.ToString())
                       {
                           listItem.Selected = true;
                           break;
                       }
                   }
               }
                btnSave.Text = "Update";
            }
        }
    }
}