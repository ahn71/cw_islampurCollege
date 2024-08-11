using DS.BLL.Admission;
using DS.BLL.ControlPanel;
using DS.BLL.GeneralSettings;
using DS.BLL.ManagedBatch;
using DS.BLL.ManagedClass;
using DS.BLL.ManagedSubject;
using DS.PropertyEntities.Model.Admission;
using DS.PropertyEntities.Model.ManagedBatch;
using DS.PropertyEntities.Model.ManagedClass;
using DS.PropertyEntities.Model.ManagedSubject;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DS.UI.Academic.Examination.ManagedSubject
{
    public partial class StudentGroupSubSetup : System.Web.UI.Page
    {
        DataTable dt;
        ClassGroupEntry clsgrpEntry;
        ClassSubjectEntry clssubEntry;
        CurrentStdEntry crntstdEntry;
        StdGroupSubSetupEntry stdGrpSubEntry;
        StdGroupSubSetupDetailsEntry stdGrpSubDetailsEntry;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (!PrivilegeOperation.SetPrivilegeControl(int.Parse(Session["__UserTypeId__"].ToString()), "StudentGroupSubSetup.aspx", btnProcess)) Response.Redirect(Request.UrlReferrer.ToString() + "?hasperm=no");
                ShiftEntry.GetDropDownList(dlShift);
                BatchEntry.GetDropdownlist(dlBatch, "True");
            }
            lblMessage.InnerText = "";
        }
        protected void dlBatch_SelectedIndexChanged(object sender, EventArgs e)
        {
            string[] batchClassId = dlBatch.SelectedValue.Split('_');
            if (clsgrpEntry == null)
            {
                clsgrpEntry = new ClassGroupEntry();
            }
            clsgrpEntry.GetDropDownList(int.Parse(batchClassId[1]), dlGroup);
        }
        protected void btnProcess_Click(object sender, EventArgs e)
        {
            LoadStudentGroupSubSetup();
        }
        private void LoadStudentGroupSubSetup()
        {
            try
            {
                try
                {
                    if (crntstdEntry == null)
                    {
                        crntstdEntry = new CurrentStdEntry();
                    }
                    string[] batchClassId = dlBatch.SelectedValue.Split('_');
                    if (clsgrpEntry == null)
                    {
                        clsgrpEntry = new ClassGroupEntry();
                    }                   
                    string clsgrpId = clsgrpEntry.LoadclsGroupId(batchClassId[1], dlGroup.SelectedValue);
                    dt = new DataTable();
                    dt = crntstdEntry.LoadGroupStudentList(batchClassId[0], clsgrpId);
                    if (dt.Rows.Count == 0)
                    {
                        lblMessage.InnerText = "warning->No Group Student Subject";
                        return;
                    }
                    gvstdgrpsubsetuplist.DataSource = dt;
                    //gvstdgrpsubsetuplist.Columns[4].Visible = false;
                    gvstdgrpsubsetuplist.DataBind();                   
                }
                catch { }
            }
            catch { }
        }
     

        protected void gvstdgrpsubsetuplist_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            try
            {
                if (e.Row.RowType == DataControlRowType.DataRow)
                {

                    string[] batchClasId = dlBatch.SelectedValue.Split('_');
                    //Find the Hiddenfield,CheckBoxList,RadioButtonList in the Row
                    HiddenField stdID = (e.Row.FindControl("stdID") as HiddenField);
                    CheckBoxList checkMandatorySub = (e.Row.FindControl("checkMendatory") as CheckBoxList);
                    RadioButtonList checkOptionalySub = (e.Row.FindControl("checkOptinal") as RadioButtonList);
                    //.........END Find the Hiddenfield,CheckBoxList,RadioButtonList in the Row.....................
                    if (clssubEntry == null)
                    {
                        clssubEntry = new ClassSubjectEntry();
                    }
                    if (dlGroup.SelectedValue != "0")
                    {
                        gvstdgrpsubsetuplist.Columns[4].Visible = true;
                        clssubEntry.LoadMandatorySubjectListWithCommon(batchClasId[1], dlGroup.SelectedValue, checkMandatorySub);//...........Load MandatorySubject From Class Subject Setup
                        dt = new DataTable();
                        if (stdGrpSubDetailsEntry == null)
                        {
                            stdGrpSubDetailsEntry = new StdGroupSubSetupDetailsEntry();
                        }
                        dt = stdGrpSubDetailsEntry.LoadStdGroupSub(stdID.Value, batchClasId[0]);//.........Load Student Group Subject FROM Class Subject Setup............
                        for (int i = 0; i < checkMandatorySub.Items.Count; i++)
                        {
                            DataRow[] dr = dt.Select("SubId='" + checkMandatorySub.Items[i].Value + "' AND MSStatus='True'");
                            if (dr.Count() > 0)
                            {
                                checkMandatorySub.Items[i].Selected = true;
                            }
                        }
                    }
                    else
                    {
                        gvstdgrpsubsetuplist.Columns[4].Visible = false;
                    }
                    if (clssubEntry == null)
                    {
                        clssubEntry = new ClassSubjectEntry();
                    }
                    clssubEntry.LoadOptionalSubjectList(batchClasId[1], dlGroup.SelectedValue, checkOptionalySub);//.........Load Optional Subject From Class Subject Setup...........
                    if (checkOptionalySub.Items.Count == 0 && dlGroup.SelectedValue == "0")
                    {
                        gvstdgrpsubsetuplist.DataSource = null;
                        gvstdgrpsubsetuplist.DataBind();
                        lblMessage.InnerText = "warning->"+dlBatch.SelectedItem.Text+" have not Optional Subject";
                        return;
                    }
                    gvstdgrpsubsetuplist.Columns[6].Visible = true;
                    if (checkOptionalySub.Items.Count == 1 && dlGroup.SelectedValue == "0")
                    {                        
                        int result = 0;
                        using (StdGroupSubSetupEntities entities = GetFormData("0", stdID.Value, batchClasId[0]))
                        {
                            if (stdGrpSubEntry == null)
                            {
                                stdGrpSubEntry = new StdGroupSubSetupEntry();
                            }
                            stdGrpSubEntry.AddEntities = entities;
                            bool Status = stdGrpSubEntry.Delete();
                            result = stdGrpSubEntry.Insert();
                        }
                        using (StdGroupSubSetupDetailsEntities entities = GetFormData(result.ToString(), checkOptionalySub.Items[0].Value, false))
                        {
                            if (stdGrpSubDetailsEntry == null)
                            {
                                stdGrpSubDetailsEntry = new StdGroupSubSetupDetailsEntry();
                            }
                            stdGrpSubDetailsEntry.AddEntities = entities;
                            stdGrpSubDetailsEntry.Insert();
                        }
                        gvstdgrpsubsetuplist.Columns[6].Visible = false;
                        dt = new DataTable();
                        dt = stdGrpSubDetailsEntry.LoadStdGroupSub(stdID.Value, batchClasId[0]);//.........Load Student Group Subject FROM Class Subject Setup............
                    }
                    for (int i = 0; i < checkOptionalySub.Items.Count; i++)
                    {
                        DataRow[] dr = dt.Select("SubId='" + checkOptionalySub.Items[i].Value + "' AND MSStatus='False'");
                        if (dr.Count() > 0)
                        {
                            checkOptionalySub.Items[i].Selected = true;
                        }
                    }
                    //try
                    //{
                    //    if (e.Row.RowType == DataControlRowType.DataRow)
                    //    {
                    //        e.Row.Attributes["onmouseover"] = "javascript:SetMouseOver(this)";
                    //        e.Row.Attributes["onmouseout"] = "javascript:SetMouseOut(this)";
                    //    }
                    //}
                    //catch { }
                }
            }
            catch { }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            int result = 0;
            bool Finalresult = false;
            string[] batchClasId = dlBatch.SelectedValue.Split('_');
            GridViewRow gvr = ((GridViewRow)((Control)sender).Parent.Parent);
            int index_row = gvr.RowIndex;
            CheckBoxList chkMandatory = (CheckBoxList)gvstdgrpsubsetuplist.Rows[index_row].Cells[5].FindControl("checkMendatory");
            RadioButtonList chkOptional = (RadioButtonList)gvstdgrpsubsetuplist.Rows[index_row].Cells[6].FindControl("checkOptinal");
            int count = 0;
            for (int x = 0; x < chkMandatory.Items.Count; x++)
            {
                string MansubId = "", OpsubId = "";
                if(chkMandatory.Items[x].Selected==true)
                {
                    count++;
                    MansubId = chkMandatory.Items[x].Value;
                }
                for (int y = 0; y < chkOptional.Items.Count; y++)
                {
                    if (chkOptional.Items[y].Selected == true)
                    {
                        OpsubId = chkOptional.Items[y].Value;
                    }
                    if (MansubId.Equals(OpsubId) && MansubId!="" && OpsubId!="")
                    {
                        lblMessage.InnerText = "warning->Mandatory Subject and Optional Subject Does Not Same";
                        return;
                    }
                }
            }
            if (clsgrpEntry == null)
            {
                clsgrpEntry = new ClassGroupEntry();
            }
            int numofsub = clsgrpEntry.LoadNumberofSub(batchClasId[1],dlGroup.SelectedValue);
            if (count != numofsub)
            {
                lblMessage.InnerText = "warning->Must be Select "+numofsub+" Subject for Mandatory";
                return;
            }
           HiddenField stdID = (HiddenField)gvstdgrpsubsetuplist.Rows[index_row].Cells[1].FindControl("stdID");          
           using (StdGroupSubSetupEntities entities = GetFormData("0", stdID.Value, batchClasId[0]))
           {
               if (stdGrpSubEntry == null)
               {
                   stdGrpSubEntry = new StdGroupSubSetupEntry();
               }
               stdGrpSubEntry.AddEntities = entities;
               bool Status = stdGrpSubEntry.Delete();
           }   
           
           using (StdGroupSubSetupEntities entities = GetFormData("0", stdID.Value, batchClasId[0]))
            {
                if (stdGrpSubEntry == null)
                {
                    stdGrpSubEntry = new StdGroupSubSetupEntry();
                }
                stdGrpSubEntry.AddEntities = entities;
                result = stdGrpSubEntry.Insert();
            }   
            
            for (int i = 0; i < chkMandatory.Items.Count; i++)
            {
                if (chkMandatory.Items[i].Selected == true)
                {
                    using (StdGroupSubSetupDetailsEntities entities = GetFormData(result.ToString(),chkMandatory.Items[i].Value, true))
                    {
                        if (stdGrpSubDetailsEntry == null)
                        {
                            stdGrpSubDetailsEntry = new StdGroupSubSetupDetailsEntry();
                        }
                        stdGrpSubDetailsEntry.AddEntities = entities;
                        Finalresult = stdGrpSubDetailsEntry.Insert();
                    }   
                }
            }          
            
            for (int i = 0; i < chkOptional.Items.Count; i++)
            {                
                if (chkOptional.Items[i].Selected == true)
                {
                    using (StdGroupSubSetupDetailsEntities entities = GetFormData(result.ToString(), chkOptional.Items[i].Value, false))
                    {
                        if (stdGrpSubDetailsEntry == null)
                        {
                            stdGrpSubDetailsEntry = new StdGroupSubSetupDetailsEntry();
                        }
                        stdGrpSubDetailsEntry.AddEntities = entities;
                         Finalresult = stdGrpSubDetailsEntry.Insert();
                    }      
                }
            }
            //LoadStudentGroupSubSetup();
            if (Finalresult == true)
            {
                lblMessage.InnerText = "success->Successfully Saved";
            }

        }
        private StdGroupSubSetupEntities GetFormData(string SGSID,string stdID,string batchID)
        {
            StdGroupSubSetupEntities StdGrpSubEntities = new StdGroupSubSetupEntities();
            StdGrpSubEntities.SGSubId = int.Parse(SGSID);
            StdGrpSubEntities.StudentId = int.Parse(stdID);
            StdGrpSubEntities.BatchId = int.Parse(batchID);           
            return StdGrpSubEntities;
        }
        private StdGroupSubSetupDetailsEntities GetFormData(string SGSID, string subID, bool MSStatus)
        {
            StdGroupSubSetupDetailsEntities StdGrpSubDetailsEntities = new StdGroupSubSetupDetailsEntities();
            StdGrpSubDetailsEntities.SGSubId = int.Parse(SGSID);
            StdGrpSubDetailsEntities.SubId = int.Parse(subID);
            StdGrpSubDetailsEntities.MSStatus = MSStatus;
            return StdGrpSubDetailsEntities;
        }

        protected void gvstdgrpsubsetuplist_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            LoadStudentGroupSubSetup();
            gvstdgrpsubsetuplist.PageIndex = e.NewPageIndex;
            gvstdgrpsubsetuplist.DataBind();
        }
    }
}