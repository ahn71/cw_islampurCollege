using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DS.DAL.AdviitDAL;
using System.Data.SqlClient;
using System.Data;
using DS.PropertyEntities.Model.ManagedSubject;
using DS.BLL.ManagedSubject;
using DS.BLL.Examinition;
using DS.PropertyEntities.Model.Examinition;
using DS.BLL.ControlPanel;

namespace DS.UI.Academic.Examination.ManagedSubject
{
    public partial class NewSubject : System.Web.UI.Page
    {
        SubjectEntry subject_entry;
        bool result;

        protected void Page_Load(object sender, EventArgs e)
        {
                if (!IsPostBack)
                {
                    if (!PrivilegeOperation.SetPrivilegeControl(int.Parse(Session["__UserTypeId__"].ToString()), "NewSubject.aspx", btnSave)) Response.Redirect(Request.UrlReferrer.ToString() + "?hasperm=no");
                    LoadCompulsorySubject("");                   
                  
                }
        }       

        protected void btnSave_Click(object sender, EventArgs e)
        {
            if (lblSubId.Value.ToString().Length == 0)
            {
                if (Session["__Save__"].ToString().Equals("false")) { lblMessage.InnerText = "warning-> You don't have permission to save!"; LoadCompulsorySubject(""); return; }
                if (saveNewSubject() == true)
                    ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "call me", "SaveSuccess();", true);
            }
            else
            {
                if (updateNewSubject() == true)
                    ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "call me", "updateSuccess();", true);
            }
        }

        private SubjectEntities GetData
        {
            get 
            {
                try
                {
                    SubjectEntities se = new SubjectEntities();
                    se.SubjectId = (lblSubId.Value.ToString() == "") ? 0 : int.Parse(lblSubId.Value.ToString());
                    se.SubjectName = txtSubName.Text.Trim();                               
                    se.OrderBy = int.Parse(txtOrder.Text);
                    se.IsActive = chkIsActive.Checked;
                    return se;
                }
                catch { return null; }
            
            }

        }

        private Boolean saveNewSubject()
        {
            try
            {
                using (SubjectEntities se = GetData)
                {
                    if (subject_entry == null) subject_entry = new SubjectEntry();
                    subject_entry.AddEntities = GetData;
                    result = subject_entry.Insert();

                }                         
                if (result)
                {
                    lblMessage.InnerText = "success->Successfully saved";
                    LoadCompulsorySubject("");
                   
                    return true;
                }
                else
                {
                    lblMessage.InnerText = "error->Unable to save";
                    return false;
                }
            }
            catch (Exception ex)
            {
                lblMessage.InnerText = "error->" + ex.Message;
                return false;
            }
        }
        private Boolean updateNewSubject()
        {
            try
            {
                using(SubjectEntities se=GetData)
                {
                    if (subject_entry==null)subject_entry=new SubjectEntry ();
                    subject_entry.AddEntities=se;
                    result=subject_entry.Update();
                    if (result)
                    {
                        lblMessage.InnerText = "success->Successfully Updated";
                        LoadCompulsorySubject("");
                      
                        return true;
                    }
                    
                    else
                    {
                        lblMessage.InnerText = "error->Unable to Update";
                        return false;
                    }
                }
            }
            catch (Exception ex)
            {
                lblMessage.InnerText = "error->" + ex.Message;
                return false;
            }
        }
        private void LoadCompulsorySubject(string sqlcmd)
        {

            List<SubjectEntities> GetCompolsorySubjectList = SubjectEntry.GetEntitiesData;
            string divInfo = "";
            if (GetCompolsorySubjectList==null ||GetCompolsorySubjectList.Count == 0)
            {
                divInfo = "<div class='noData'>Compulsory Subject not available</div>";
                divInfo += "<div class='dataTables_wrapper'><div class='head'></div></div>";
                divSubjectList.Controls.Add(new LiteralControl(divInfo));
                return;
            }
            divInfo = " <table id='tblClassList' class='table table-striped table-bordered dt-responsive nowrap' cellspacing='0' width='100%' > ";
            divInfo += "<thead>";
            divInfo += "<tr>";
            divInfo += "<th>Subject Name</th>";
            divInfo += "<th style='width:70px;text-align:center;visible:False'>Active</th>";
            divInfo += "<th style='width:70px;text-align:center;visible:False'>Order</th>";
            if (Session["__Update__"].ToString().Equals("true")) divInfo += "<th>Edit</th>";           
            divInfo += "</tr>";
            divInfo += "</thead>";
            divInfo += "<tbody>";
            string id = "";
            for (int x = 0; x < GetCompolsorySubjectList.Count; x++)
            {
                id = GetCompolsorySubjectList[x].SubjectId.ToString();
                divInfo += "<tr id='r_" + id + "'>";
                divInfo += "<td >" + GetCompolsorySubjectList[x].SubjectName + "</td>";
               
                divInfo += (GetCompolsorySubjectList[x].IsActive.Equals(true)) ? "<td style='width:70px;text-align:center'>Yes</td>" : "<td style='width:70px;text-align:center'>No</td>";
               
                divInfo += "<td style='width:70px;text-align:center'>" + GetCompolsorySubjectList[x].OrderBy.ToString() + "</td>";
                if (Session["__Update__"].ToString().Equals("true"))
                divInfo += "<td class='numeric_control' >" + "<img src='/Images/gridImages/edit.png' class='editImg'   onclick='editSubject(" + id + ");'  />";
            }
            divInfo += "</tbody>";
            divInfo += "<tfoot>";
            divInfo += "</table>";
            divSubjectList.Controls.Add(new LiteralControl(divInfo));
        }
       
    }
}