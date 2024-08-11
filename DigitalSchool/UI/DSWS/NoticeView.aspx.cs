using DS.BLL.DSWS;
using DS.PropertyEntities.Model.DSWS;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DS.UI.DSWS
{
    public partial class NoticeView : System.Web.UI.Page
    {
        List<AddNoticeEntities> entitiesNotice;
        List<AddSecialDescriptionEntities> entitiesSD;
        AddNoticeEntry EntryNotice;
        AddSpecialDescriptionEntry EntrySD;
        List<AddPresidentEntities> entitiesPr;
        AddPresidentSpeechEntry EntrySpeech;
        string divInfo = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(Request.QueryString["NSL"]))
            {
                if (Request.QueryString["NSL"].ToString() == "NoticeCircular")
                {
                    LoadNoticeandCircular();
                }
                else
                {
                    LoadNotice();
                }
            }
            else if (!string.IsNullOrEmpty(Request.QueryString["DSL"]))
            {
                LoadOtherPage();
            }
            else if(!string.IsNullOrEmpty(Request.QueryString["PSL"]))
            {
                LoadPSLPage();
            }
            else if (!string.IsNullOrEmpty(Request.QueryString["PrinSL"]))
            {
                LoadPrinSLPage();
            }
            else if (!string.IsNullOrEmpty(Request.QueryString["নোটিশ"]))
            {
                LoadAllNotice();
            }
        }
        private void LoadNotice()
        {
            divInfo = "";
            DataTable dt = new DataTable();
            if (EntryNotice == null)
            {
                EntryNotice = new AddNoticeEntry();
            }
            dt = EntryNotice.getIndividualNoticeWithAttachment(Request.QueryString["NSL"].ToString());
            if (dt.Rows.Count > 0)
            {
                divInfo = "<h2>" + dt.Rows[0]["Title"].ToString() + "</h2>" +
                      "<p>" + dt.Rows[0]["NDetails"].ToString() + "<p>";
                if (dt.Rows[0]["FileName"].ToString().Trim()!= "")
                {
                 
                    divInfo += "<p><a role='button' class='btn btn-default' href='../../Images/dsimages/Notice/" + dt.Rows[0]["FileName"].ToString() + "'>ডাউনলোড</a></p>";
                }
            }
            
           


            divNoticeViewer.Controls.Add(new LiteralControl(divInfo));
        }
        private void LoadNoticeOld()
        {
            divInfo = "";
            entitiesNotice = new List<AddNoticeEntities>();
            if (EntryNotice == null)
            {
                EntryNotice = new AddNoticeEntry();
            }
            entitiesNotice = EntryNotice.getIndividualNoticeData(Request.QueryString["NSL"].ToString());

            divInfo = "<h2>" + entitiesNotice[0].NSubject + "</h2>" +
                      "<p>" + entitiesNotice[0].NDetails + "<p>";
            if (entitiesNotice[0].FileName != "")
            {
                divInfo += "<p><a role='button' class='btn btn-default' href='/PDFFiles/" + entitiesNotice[0].FileName + "'>ডাউনলোড</a></p>";
            }


            divNoticeViewer.Controls.Add(new LiteralControl(divInfo));
        }
        private void LoadNoticeandCircular()
        {
            try
            {
                //..............Notice and Circulars......................
                divInfo = "";
                entitiesNotice = new List<AddNoticeEntities>();
                if (EntryNotice == null)
                {
                    EntryNotice = new AddNoticeEntry();
                }
                entitiesNotice = EntryNotice.getNoticeData().FindAll(c => c.Type == "Notices & Circulars");
                divInfo = "";
                divInfo += "<div class='border'>";
                divInfo += "<div class='notices'>";
                divInfo += "<h3>Notices & Circulars</h3>";
                divInfo += "<ul>";
                for (int i = 4; i < entitiesNotice.Count; i++)
                {
                    divInfo += "<li id=" + i + "><a href='/UI/DSWS/NoticeView.aspx?NSL=" + entitiesNotice[i].NSL + "'><i class='fa fa-caret-square-o-right icon'></i> " + entitiesNotice[i].NSubject + "</a></li>";
                }
                divInfo += "</ul>";
                divInfo += "</div>";
                divInfo += "</div>";
                divNoticeViewer.Controls.Add(new LiteralControl(divInfo));
            }
            catch { }
        }
        private void LoadOtherPage()
        {
            divInfo = "";
            entitiesSD = new List<AddSecialDescriptionEntities>();
            if (EntrySD == null)
            {
                EntrySD = new AddSpecialDescriptionEntry();
            }
            entitiesSD = EntrySD.getSecialDescription().FindAll(c => c.DSL == int.Parse(Request.QueryString["DSL"].ToString())); ;
            if (entitiesSD == null || entitiesSD.Count==0) return;
            divInfo = "<h2>" + entitiesSD[0].Subject + "</h2>" +
                      "<p>" + entitiesSD[0].Details + "<p>";
            divNoticeViewer.Controls.Add(new LiteralControl(divInfo));
        }
        private void LoadPSLPage()
        {
            divInfo = "";
            entitiesPr = new List<AddPresidentEntities>();
            if (EntrySpeech == null)
            {
                EntrySpeech = new AddPresidentSpeechEntry();
            }
            entitiesPr = EntrySpeech.getPresidentSpeechData().FindAll(c => c.SPId == int.Parse(Request.QueryString["PSL"].ToString())); ;
            if (entitiesPr == null || entitiesPr.Count == 0) return;
            divInfo = "<h2>সভাপতির বাণী </h2>" +
                      "<p>" + entitiesPr[0].Speech + "<p>";
            divNoticeViewer.Controls.Add(new LiteralControl(divInfo));
        }
        private void LoadPrinSLPage()
        {
            divInfo = "";
            entitiesPr = new List<AddPresidentEntities>();
            if (EntrySpeech == null)
            {
                EntrySpeech = new AddPresidentSpeechEntry();
            }
            entitiesPr = EntrySpeech.getPrincipalSpeechData().FindAll(c => c.SPId == int.Parse(Request.QueryString["PrinSL"].ToString())); ;
            if (entitiesPr == null || entitiesPr.Count == 0) return;
            divInfo = "<h2>প্রধান শিক্ষকের বাণী </h2>" +
                      "<p>" + entitiesPr[0].Speech + "<p>";
            divNoticeViewer.Controls.Add(new LiteralControl(divInfo));
        }
        private void LoadAllNotice()
        {
            try
            {
                divInfo = "";
                entitiesNotice = new List<AddNoticeEntities>();
                if (EntryNotice == null)
                {
                    EntryNotice = new AddNoticeEntry();
                }
                entitiesNotice = EntryNotice.getNoticeData().FindAll(c => c.Type == "Notices & Circulars");
                divInfo = "<h2 style='border-bottom: 5px solid #efefef;padding: 10px;'>নোটিশ</h2>";
                for (int i = 0; i < entitiesNotice.Count; i++)
                {                    
                    divInfo += "<h3>" + entitiesNotice[i].NSubject + "</h3><h4>" + entitiesNotice[i].NPublishedDate + "</h4>" +
                      "<p>" + entitiesNotice[i].NDetails + "<p>";  
                    if( entitiesNotice[i].FileName!="")
                    {
                        divInfo += "<p><a role='button' class='btn btn-default' href='/PDFFiles/" + entitiesNotice[i].FileName + "'>ডাউনলোড</a></p>";
                    }
                }
                
                divNoticeViewer.Controls.Add(new LiteralControl(divInfo));
            }
            catch { }
        }
    }
}