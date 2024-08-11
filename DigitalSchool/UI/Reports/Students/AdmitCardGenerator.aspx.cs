using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using DS.DAL.AdviitDAL;
using System.Collections;
using System.Data.SqlClient;
using System.Data;
using DS.DAL.ComplexScripting;
using DS.BLL.ManagedBatch;
using DS.BLL.GeneralSettings;
using DS.BLL.ManagedClass;
using DS.BLL.Admission;
using DS.BLL.ControlPanel;

namespace DS.UI.Reports.Students
{
    public partial class AdmitCardGenerator : System.Web.UI.Page
    {
        ClassGroupEntry clsgrpEntry;
        CurrentStdEntry currentstdEntry;
        protected void Page_Load(object sender, EventArgs e)
        {

            if (!IsPostBack)
            {
                if (!PrivilegeOperation.SetPrivilegeControl(int.Parse(Session["__UserTypeId__"].ToString()), "AdmitCardGenerator.aspx", "")) Response.Redirect(Request.UrlReferrer.ToString() + "?hasperm=no"); 
                loadExaType(); //Show Exam Name
                //............Admit Card Generate For All Students...............
                BatchEntry.GetDropdownlist(dlBatch, "True");
                dlBatch.Items.Insert(1, new ListItem("All", "All"));
                dlBatch.SelectedValue = "All";
                dlSection.Items.Insert(0, new ListItem("All", "All"));
                dlSection.SelectedValue = "All";
                ShiftEntry.GetDropDownList(dlShiftAdmit);
                dlShiftAdmit.Items.Insert(1, new ListItem("All", "All"));
                dlShiftAdmit.SelectedValue = "All";

                //.........Admit Card Generate For Individual Student..................
                BatchEntry.GetDropdownlist(dlBatchForAdmintcardByRoll, "True");
                dlBatchForAdmintcardByRoll.Items.Insert(1, new ListItem("All", "All"));
                dlBatchForAdmintcardByRoll.SelectedValue = "All";
                dlSectionForAdmintcardByRoll.Items.Insert(0, new ListItem("All", "All"));
                dlSectionForAdmintcardByRoll.SelectedValue = "All";
                ShiftEntry.GetDropDownList(dlShiftForAdmitRoll);
                dlShiftForAdmitRoll.Items.Insert(1, new ListItem("All", "All"));
                dlShiftForAdmitRoll.SelectedValue = "All";

                //............Id Card Generate For All Students...............
                BatchEntry.GetDropdownlist(dlBatchForIdCard, "True");
                dlBatchForIdCard.Items.Insert(1, new ListItem("All", "All"));
                dlBatchForIdCard.SelectedValue = "All";
                dlSectionForIdCard.Items.Insert(0, new ListItem("All", "All"));
                dlSectionForIdCard.SelectedValue = "All";
                ShiftEntry.GetDropDownList(dlShiftForIdCard);
                dlShiftForIdCard.Items.Insert(1, new ListItem("All", "All"));
                dlShiftForIdCard.SelectedValue = "All";

                //.........Id Card Generate For Individual Student..................
                BatchEntry.GetDropdownlist(dlBatchForIdCardByROll, "True");
                dlBatchForIdCardByROll.Items.Insert(1, new ListItem("All", "All"));
                dlBatchForIdCardByROll.SelectedValue = "All";
                dlSectionForIdCardByRoll.Items.Insert(0, new ListItem("All", "All"));
                dlSectionForIdCardByRoll.SelectedValue = "All";
                ShiftEntry.GetDropDownList(dlShiftForIdCardRoll);
                dlShiftForIdCardRoll.Items.Insert(1, new ListItem("All", "All"));
                dlShiftForIdCardRoll.SelectedValue = "All";

            }
        }
        private void loadExaType() //Show Class Name
        {
            try
            {
                sqlDB.bindDropDownList("select ExName from ExamType", "ExName", dlExamType);
                sqlDB.bindDropDownList("select ExName from ExamType", "ExName", dlExamForAdmintcardByRoll);
            }
            catch { }
        }
        protected void dlBatch_SelectedIndexChanged(object sender, EventArgs e)
        {
            string[] BatchClsID = dlBatch.SelectedValue.Split('_');
            if (clsgrpEntry == null)
            {
                clsgrpEntry = new ClassGroupEntry();
            }
            clsgrpEntry.GetDropDownListClsGrpId(int.Parse(BatchClsID[1]), dlGroup);
            ClassSectionEntry.GetEntitiesData(dlSection, int.Parse(BatchClsID[1]), dlGroup.SelectedValue);
            dlGroup.Items.Insert(1, new ListItem("All", "All"));
            if (dlGroup.Enabled == true)
                dlGroup.SelectedValue = "All";
            dlSection.Items.Insert(1, new ListItem("All", "All"));
        }
        protected void dlGroup_SelectedIndexChanged(object sender, EventArgs e)
        {
            string[] BatchClsID = dlBatch.SelectedValue.Split('_');
            string GroupId = "0";
            if (dlGroup.SelectedValue != "All")
            {
                GroupId = dlGroup.SelectedValue;
            }
            ClassSectionEntry.GetEntitiesData(dlSection, int.Parse(BatchClsID[1]), GroupId);
            dlSection.Items.Insert(1, new ListItem("All", "All"));
        }

        protected void dlBatchForAdmintcardByRoll_SelectedIndexChanged(object sender, EventArgs e)
        {
            string[] BatchClsID = dlBatchForAdmintcardByRoll.SelectedValue.Split('_');
            if (clsgrpEntry == null)
            {
                clsgrpEntry = new ClassGroupEntry();
            }
            clsgrpEntry.GetDropDownListClsGrpId(int.Parse(BatchClsID[1]), dlGroupForAdmintcardByRoll);
            ClassSectionEntry.GetEntitiesData(dlSectionForAdmintcardByRoll, int.Parse(BatchClsID[1]), dlGroupForAdmintcardByRoll.SelectedValue);
            dlGroupForAdmintcardByRoll.Items.Insert(1, new ListItem("All", "All"));
            if (dlGroupForAdmintcardByRoll.Enabled == true)
                dlGroupForAdmintcardByRoll.SelectedValue = "All";
            dlSectionForAdmintcardByRoll.Items.Insert(1, new ListItem("All", "All"));
        }

        protected void dlGroupForAdmintcardByRoll_SelectedIndexChanged(object sender, EventArgs e)
        {
            string[] BatchClsID = dlBatchForAdmintcardByRoll.SelectedValue.Split('_');
            string GroupId = "0";
            if (dlGroupForAdmintcardByRoll.SelectedValue != "All")
            {
                GroupId = dlGroupForAdmintcardByRoll.SelectedValue;
            }
            ClassSectionEntry.GetEntitiesData(dlSectionForAdmintcardByRoll, int.Parse(BatchClsID[1]), GroupId);
            dlSectionForAdmintcardByRoll.Items.Insert(1, new ListItem("All", "All"));
        }

        protected void dlSectionForAdmintcardByRoll_SelectedIndexChanged(object sender, EventArgs e)
        {
            string[] BatchClsID = dlBatchForAdmintcardByRoll.SelectedValue.Split('_');
            if (currentstdEntry == null)
            {
                currentstdEntry = new CurrentStdEntry();
            }
            string grpId = "0";
            if (dlGroupForAdmintcardByRoll.SelectedValue != "All")
            {
                grpId = dlGroupForAdmintcardByRoll.SelectedValue;
            }
            currentstdEntry.GetRollNo(dlRollForAdmitCard, dlShiftForAdmitRoll.SelectedValue, BatchClsID[0], grpId, dlSectionForAdmintcardByRoll.SelectedValue);
            dlRollForAdmitCard.Items.Insert(1, new ListItem("All", "All"));
            dlRollForAdmitCard.SelectedValue = "All";
        }

        protected void dlBatchForIdCard_SelectedIndexChanged(object sender, EventArgs e)
        {
            string[] BatchClsID = dlBatchForIdCard.SelectedValue.Split('_');
            if (clsgrpEntry == null)
            {
                clsgrpEntry = new ClassGroupEntry();
            }
            clsgrpEntry.GetDropDownListClsGrpId(int.Parse(BatchClsID[1]), dlGroupForIdCard);
            ClassSectionEntry.GetEntitiesData(dlSectionForIdCard, int.Parse(BatchClsID[1]), dlGroupForIdCard.SelectedValue);
            dlGroupForIdCard.Items.Insert(1, new ListItem("All", "All"));
            if (dlGroupForIdCard.Enabled == true)
                dlGroupForIdCard.SelectedValue = "All";
            dlSectionForIdCard.Items.Insert(1, new ListItem("All", "All"));
        }

        protected void dlGroupForIdCard_SelectedIndexChanged(object sender, EventArgs e)
        {
            string[] BatchClsID = dlBatchForIdCard.SelectedValue.Split('_');
            string GroupId = "0";
            if (dlGroupForIdCard.SelectedValue != "All")
            {
                GroupId = dlGroupForIdCard.SelectedValue;
            }
            ClassSectionEntry.GetEntitiesData(dlSectionForIdCard, int.Parse(BatchClsID[1]), GroupId);
            dlSectionForIdCard.Items.Insert(1, new ListItem("All", "All"));
        }

        protected void dlBatchForIdCardByROll_SelectedIndexChanged(object sender, EventArgs e)
        {
            string[] BatchClsID = dlBatchForIdCardByROll.SelectedValue.Split('_');
            if (clsgrpEntry == null)
            {
                clsgrpEntry = new ClassGroupEntry();
            }
            clsgrpEntry.GetDropDownListClsGrpId(int.Parse(BatchClsID[1]), dlGroupForIDCardRoll);
            ClassSectionEntry.GetEntitiesData(dlSectionForIdCardByRoll, int.Parse(BatchClsID[1]), dlGroupForIDCardRoll.SelectedValue);
            dlGroupForIDCardRoll.Items.Insert(1, new ListItem("All", "All"));
            if (dlGroupForIDCardRoll.Enabled == true)
                dlGroupForIDCardRoll.SelectedValue = "All";
            dlSectionForIdCardByRoll.Items.Insert(1, new ListItem("All", "All"));
        }

        protected void dlGroupForIDCardRoll_SelectedIndexChanged(object sender, EventArgs e)
        {
            string[] BatchClsID = dlBatchForIdCardByROll.SelectedValue.Split('_');
            string GroupId = "0";
            if (dlGroupForIDCardRoll.SelectedValue != "All")
            {
                GroupId = dlGroupForIDCardRoll.SelectedValue;
            }
            ClassSectionEntry.GetEntitiesData(dlSectionForIdCardByRoll, int.Parse(BatchClsID[1]), GroupId);
            dlSectionForIdCardByRoll.Items.Insert(1, new ListItem("All", "All"));
        }

        protected void dlSectionForIdCardByRoll_SelectedIndexChanged(object sender, EventArgs e)
        {
            string[] BatchClsID = dlBatchForIdCardByROll.SelectedValue.Split('_');
            if (currentstdEntry == null)
            {
                currentstdEntry = new CurrentStdEntry();
            }
            string grpId = "0";
            if (dlGroupForIDCardRoll.SelectedValue != "All")
            {
                grpId = dlGroupForIDCardRoll.SelectedValue;
            }
            currentstdEntry.GetRollNo(dlRollForIDCard, dlShiftForIdCardRoll.SelectedValue, BatchClsID[0], grpId, dlSectionForIdCardByRoll.SelectedValue);
            dlRollForIDCard.Items.Insert(1, new ListItem("All", "All"));
            dlRollForIDCard.SelectedValue = "All";
        }

        protected void btnACGenerate_Click(object sender, EventArgs e)
        {
            try
            {
                ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "call me", "goToNewTab('/Images/others/Report/AdmitCardPrint.aspx?getcs=" + dlExamType.Text + "_" + dlShiftAdmit.SelectedValue + "_" + dlBatch.SelectedValue + "_" + dlGroup.SelectedValue + "_" + dlSection.SelectedValue + "_"+" "+"_AC');", true);  //Open New Tab for Sever side code
            }
            catch { }
        }

        protected void btnIdCardGenerate_Click(object sender, EventArgs e)
        {
            try
            {
                // Response.Redirect("/Report/AdmitCardPrint.aspx?getcs=" + dlClassForIdCard.Text + "_" + dlSectionForIdCard.Text + "_" + dlExamType.Text+"_IC_");
                ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "call me", "goToNewTab('/Report/AdmitCardPrint.aspx?getcs= _" + dlShiftForIdCard.SelectedValue + "_" + dlBatchForIdCard.SelectedValue + "_" + dlGroupForIdCard.SelectedValue + "_" + dlSectionForIdCard.SelectedValue + "_" + " " + "_IC');", true);  //Open New Tab for Sever side code
            }
            catch { }
        }

        protected void btnAdmitCardProcessByRoll_Click(object sender, EventArgs e)
        {
            try
            {
                if (dlRollForAdmitCard.Text != "")
                {
                    // Response.Redirect("/Report/AdmitCardPrint.aspx?getcs=" + dlClassForAdmintcardByRoll.Text + "_" + dlSectionForAdmintcardByRoll.Text + "_" + txtAdmitCardRoll.Text + "_AC_ACByRoll");
                    ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "call me", "goToNewTab('/Images/others/Report/AdmitCardPrint.aspx?getcs=" + dlExamForAdmintcardByRoll.Text + "_" + dlShiftForAdmitRoll.SelectedValue + "_" + dlBatchForAdmintcardByRoll.SelectedValue + "_" + dlGroupForAdmintcardByRoll.SelectedValue + "_" + dlSectionForAdmintcardByRoll.SelectedValue + "_" + dlRollForAdmitCard.SelectedValue + "_AC');", true);  //Open New Tab for Sever side code
                }
            }
            catch { }
        }

        protected void Button2_Click(object sender, EventArgs e)
        {
            try
            {
                if (dlRollForIDCard.Text != "")
                {
                    // Response.Redirect("/Report/AdmitCardPrint.aspx?getcs=" + dlClassForIdCardByROll.Text + "_" + dlSectionForIdCardByRoll.Text + "_" + txtIdCardRoll.Text + "_IC_ICByRoll");
                    ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "call me", "goToNewTab('/Report/AdmitCardPrint.aspx?getcs= _" + dlShiftForIdCardRoll.SelectedValue + "_" + dlBatchForIdCardByROll.SelectedValue + "_" + dlGroupForIDCardRoll.SelectedValue + "_" + dlSectionForIdCardByRoll.SelectedValue + "_" + dlRollForIDCard.SelectedValue + "_IC');", true);  //Open New Tab for Sever side code
                }
                else lblMessage.InnerText = "warning->Inpur roll";
            }
            catch { }
        }      
    }
}