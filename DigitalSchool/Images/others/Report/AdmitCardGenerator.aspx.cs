using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using adviitRuntimeScripting;
using System.Data;

namespace DS.Forms
{
    public partial class AdmitCard : System.Web.UI.Page
    {


        protected void Page_Load(object sender, EventArgs e)
        {          

            if (!IsPostBack)
            {
                loadExaType(); //Show Exam Name
                loadClass(); //Show Class Name
                loadSection(); //Show Section Name
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

        private void loadClass() //Show Class Name
        {
            try
            {
                sqlDB.bindDropDownList("select ClassName from Classes", "ClassName", dlClass);
                sqlDB.bindDropDownList("select ClassName from Classes", "ClassName",dlClassForAdmintcardByRoll);
                sqlDB.bindDropDownList("select ClassName from Classes", "ClassName", dlClassForIdCard);
                sqlDB.bindDropDownList("select ClassName from Classes", "ClassName", dlClassForIdCardByROll);
               
            }
            catch { }
        }

        private void loadSection() //Show Section Name
        {
            try
            {
                sqlDB.bindDropDownList("select SectionName from Sections", "SectionName", dlSection);
                sqlDB.bindDropDownList("select SectionName from Sections", "SectionName",dlSectionForAdmintcardByRoll);
                sqlDB.bindDropDownList("select SectionName from Sections", "SectionName", dlSectionForIdCard);
                sqlDB.bindDropDownList("select SectionName from Sections", "SectionName", dlSectionForIdCardByRoll);
            }
            catch { }
        }

        protected void btnACGenerate_Click(object sender, EventArgs e)
        {
            try
            {
                //Response.Redirect("/Report/AdmitCardPrint.aspx?getcs=" + dlClass.Text + "_" + dlSection.Text+"_"+dlExamType.Text+"_AC_");
                ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "call me", "goToNewTab('/Report/AdmitCardPrint.aspx?getcs=" + dlClass.Text + "_" + dlSection.Text + "_" + dlExamType.Text + "_" + dlShiftAdmit.Text + "_AC_');", true);  //Open New Tab for Sever side code
            }
            catch { }
        }

        protected void btnIdCardGenerate_Click(object sender, EventArgs e)
        {
            try
            {
               // Response.Redirect("/Report/AdmitCardPrint.aspx?getcs=" + dlClassForIdCard.Text + "_" + dlSectionForIdCard.Text + "_" + dlExamType.Text+"_IC_");
                ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "call me", "goToNewTab('/Report/AdmitCardPrint.aspx?getcs=" + dlClassForIdCard.Text + "_" + dlSectionForIdCard.Text + "_" + dlExamType.Text + "_" + dlShiftForIdCard.Text.Trim() + "_IC_');", true);  //Open New Tab for Sever side code
            }
            catch { }
        }

        protected void btnAdmitCardProcessByRoll_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtAdmitCardRoll.Text != "")
                {
                   // Response.Redirect("/Report/AdmitCardPrint.aspx?getcs=" + dlClassForAdmintcardByRoll.Text + "_" + dlSectionForAdmintcardByRoll.Text + "_" + txtAdmitCardRoll.Text + "_AC_ACByRoll");
                    ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "call me", "goToNewTab('/Report/AdmitCardPrint.aspx?getcs=" + dlClassForAdmintcardByRoll.Text + "_" + dlSectionForAdmintcardByRoll.Text + "_" + txtAdmitCardRoll.Text + "_" + dlShiftForAdmitRoll.Text + "_AC_ACByRoll');", true);  //Open New Tab for Sever side code
                }
           }
            catch { }
        }

        protected void Button2_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtIdCardRoll.Text != "")
                {
                    // Response.Redirect("/Report/AdmitCardPrint.aspx?getcs=" + dlClassForIdCardByROll.Text + "_" + dlSectionForIdCardByRoll.Text + "_" + txtIdCardRoll.Text + "_IC_ICByRoll");
                    ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "call me", "goToNewTab('/Report/AdmitCardPrint.aspx?getcs=" + dlClassForIdCardByROll.Text + "_" + dlSectionForIdCardByRoll.Text + "_" + txtIdCardRoll.Text +"_"+ dlShiftForIdCardRoll.Text + "_IC_ICByRoll');", true);  //Open New Tab for Sever side code
                }
                else lblMessage.InnerText = "warning->Inpur roll";
            }
            catch { }
        }
       
    }
}