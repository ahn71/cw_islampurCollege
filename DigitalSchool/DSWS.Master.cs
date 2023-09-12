using DS.BLL;
using DS.BLL.DSWS;
using DS.DAL;
using DS.PropertyEntities.Model.DSWS;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DS
{
    public partial class DSWS : System.Web.UI.MasterPage
    {
        List<AddNoticeEntities> entitiesNotice;
        AddNoticeEntry EntryNotice;
        List<AddSecialDescriptionEntities> entitiesSD;
        AddSpecialDescriptionEntry EntrySD;
        VisitorCounter visitorcounter;
        List<WSQuickLink> wSQuickLinks;
        QuickLinkEntry quickLinkEntry;
        protected void Page_Load(object sender, EventArgs e)
        {
            //this.onlinevisitor.InnerText = "Users online: " + Application["TotalOnlineUsers"].ToString();
            if (!IsPostBack)
            {
                //---url bind---               
                aLogIn.HRef = "~/" + Classes.Routing.LoginRouteUrl;
                aHome.HRef = "~/" + Classes.Routing.IndexRouteUrl;
                aBackground.HRef = "~/" + Classes.Routing.BackgroundRouteUrl;
                aChairmanMessage.HRef = "~/" + Classes.Routing.ChairmanMessageRouteUrl;
                aPrincipalMessage.HRef = "~/" + Classes.Routing.PrincipalMessageRouteUrl;
                aGoverningBody.HRef = "~/" + Classes.Routing.GoverningBodyRouteUrl;
                aLab.HRef = "~/" + Classes.Routing.LabRouteUrl;
                aLibrary.HRef = "~/" + Classes.Routing.LibraryRouteUrl;
                aStudents.HRef = "~/" + Classes.Routing.StudentsRouteUrl;
                aTeachers.HRef = "~/" + Classes.Routing.TeachersRouteUrl;
                //aAcademicCalendar.HRef = "~/" + Classes.Routing.AcademicCalendarRouteUrl;
                aAcademicCalendar.HRef = "~/" + Classes.Routing.AdmitCardRouteUrl;
                aExamSchedule.HRef = "~/" + Classes.Routing.ExamScheduleRouteUrl;
                aResult.HRef = "~/" + Classes.Routing.ResultRouteUrl;
                aOnlineAdmission.HRef = "~/" + Classes.Routing.AdmissionFormRouteUrl;
                aContact.HRef = "~/" + Classes.Routing.ContactRouteUrl;
                aOnlinePayment .HRef = "~/" + Classes.Routing.PaymentRouteUrl;
                //---url bind end---

                Common.load_website_initialdata();
                load_initial();
                loadQuickLink();
              //  LoadLatestNotice();
              // LoadSecialDescription();
              // loadEvent();
                int visitornumber = int.Parse(Application["NoOfVisitors"].ToString());    
                if (visitorcounter == null)
                    visitorcounter = new VisitorCounter();
                int _visitornumber = visitorcounter.visitorNumber(TimeZoneBD.getCurrentTimeBD("yyyy-MM-dd"));
                lbluser.Text = (_visitornumber < visitornumber) ? visitornumber.ToString() : _visitornumber.ToString();
                visitorcounter.AddEntities = setVisitorCountValues();
                visitorcounter.Insert();
                DataTable dt = new DataTable();
                dt=CRUD.ReturnTableNull("select sum(hitNumber) Total FROM VisitorCounter");
                if (dt != null && dt.Rows.Count > 0)
                    totalv.InnerText = dt.Rows[0]["Total"].ToString();
                else
                    totalv.InnerText = "";
                

               // Application["NoOfVisitors"] = 0;
            }

        }
        private void loadQuickLink()
        {
            try
            {
                if (quickLinkEntry == null)
                    quickLinkEntry = new QuickLinkEntry();
                wSQuickLinks = new List<WSQuickLink>();
                wSQuickLinks = quickLinkEntry.listActive();
                string divElement = "";
                if (wSQuickLinks != null && wSQuickLinks.Count > 0)
                {
                    divElement += "<ul>";
                    foreach(WSQuickLink i in wSQuickLinks)
                    {
                        divElement += "<li><a href='"+i.Url+"' title='"+i.Title+ "'><i class='fa fa-angle-right'></i>" + i.Title + "</a></li>";
                    }                    
                    divElement += "</ul>";
                }
              //divQuickLink.Controls.Add(new LiteralControl(divElement));



            }
            catch(Exception ex)
            {

            }
        }
        private void load_initial()
        {
            try {
                //headerInstitueName.InnerText=Session["__InstituteTitle__"].ToString();
                headerInstitueSlogan.InnerText=Session["__InstituteSlogan__"].ToString();
                lblAddressPhone.InnerText= lblInstituePhone.InnerText=Session["__InstituePhone__"].ToString();
                lblAddressEmail.InnerText=lblInstitueEmail.InnerText=Session["__InstitueEmail__"].ToString();
                lblInstitueCodeHeader.InnerText=Session["__InstitueCode__"].ToString();
                lblInstitueEIINHeader.InnerText= Session["__InstitueEIIN__"].ToString();
                lblInstituteAddress.InnerHtml = Session["__InstitueAddress__"].ToString();
            } catch(Exception ex) { }
        }
        private VisitorCounterEntities setVisitorCountValues()
        {
            VisitorCounterEntities _entities = new VisitorCounterEntities();
            _entities.hitNumber = int.Parse(lbluser.Text.Trim());
            _entities.lastUpdate = TimeZoneBD.getCurrentTimeBD("yyyy-MM-dd");
            return _entities;
        }
        private void LoadLatestNotice()
        {
            try
            {
                entitiesNotice = new List<AddNoticeEntities>();
                if (EntryNotice == null)
                {
                    EntryNotice = new AddNoticeEntry();
                }
                entitiesNotice = EntryNotice.getNewsUpdateData();
                string divInfo = "";
                for (int i = 0; i < entitiesNotice.Count; i++)
                {
                    divInfo += "<p>" + entitiesNotice[i].NDetails + "</p>";
                }
              //  divNewsUpdates.Controls.Add(new LiteralControl(divInfo));
            }
            catch { }
        }
        private void LoadSecialDescription()
        {
            try
            {
                entitiesSD = new List<AddSecialDescriptionEntities>();
                if (EntrySD == null)
                {
                    EntrySD = new AddSpecialDescriptionEntry();
                }
                entitiesSD = EntrySD.getSecialDescription();
                //al2.HRef = "/UI/DSWS/NoticeView.aspx?DSL=" + (from s in entitiesSD where s.Type == "পঠিত বিষয়াবলী" select s.DSL).SingleOrDefault() + "";
                //al3.HRef = "/UI/DSWS/NoticeView.aspx?DSL=" + (from s in entitiesSD where s.Type == "শরীর চর্চা ও স্যানিটেশন" select s.DSL).SingleOrDefault() + "";
                //al4.HRef = "/UI/DSWS/NoticeView.aspx?DSL=" + (from s in entitiesSD where s.Type == "কম্পিউটার ব্যবহার" select s.DSL).SingleOrDefault() + "";
                //al5.HRef = "/UI/DSWS/NoticeView.aspx?DSL=" + (from s in entitiesSD where s.Type == "প্রশংসাপত্র / টি সি" select s.DSL).SingleOrDefault() + "";
                //al7.HRef = "/UI/DSWS/NoticeView.aspx?DSL=" + (from s in entitiesSD where s.Type == "নীতিমালা ও সার্কুলার" select s.DSL).SingleOrDefault() + "";
                //al8.HRef = "/UI/DSWS/NoticeView.aspx?DSL=" + (from s in entitiesSD where s.Type == "খেলার মাঠ" select s.DSL).SingleOrDefault() + "";
                //al9.HRef = "/UI/DSWS/NoticeView.aspx?DSL=" + (from s in entitiesSD where s.Type == "আমাদের সফলতা" select s.DSL).SingleOrDefault() + "";
            }
            catch { }



        }
   
    }
}