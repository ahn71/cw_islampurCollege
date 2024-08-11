using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DS.PropertyEntities.Model.DSWS;
using DS.BLL.DSWS;
using System.Text.RegularExpressions;
using System.Globalization;
using System.Data;

namespace DS
{
    public partial class index : System.Web.UI.Page
    {
        List<AddPresidentEntities> entitiesPr;
        List<AddPresidentEntities> entitiesPnc;
        List<AddSecialDescriptionEntities> entitiesSD;
        List<AddNoticeEntities> entitiesNotice;
        AddPresidentSpeechEntry EntrySpeech;
        AddSpecialDescriptionEntry EntrySD;
        AddNoticeEntry EntryNotice;
        string divInfo = "";
        DataTable dt;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                
                LoadInitial();
                LoadPresidentAndPrincipalSpeech();
                LoadSecialDescription();
                LoadSlider();
                LoadLatestNoticeandCirculars();
                loadAlbumDetails();
                loadEvent();
            }
            
        }
        private void LoadInitial()
        {
            try
            {

                this.Title = Session["__InstituteTitle__"].ToString() ;
                lblPresidentMessageHeader.InnerText = Session["__PresidentMessageHeader__"].ToString();
                lblPresidentDsg.InnerText = Session["__PresidentDsg__"].ToString();
                lblNoticeBoardHeader.InnerText = Session["__NoticeBoardHeader__"].ToString();
                lblPrincipalMessageHeader.InnerText = Session["__PrincipalMessageHeader__"].ToString();
                lblPrincipalDsg.InnerText = Session["__PrincipalDsg__"].ToString();
            }
            catch (Exception ex)
            {

            }
        }
        private void LoadPresidentAndPrincipalSpeech()
        {
            try
            {
                entitiesPr = new List<AddPresidentEntities>();
                entitiesPnc = new List<AddPresidentEntities>();
                if (EntrySpeech == null)
                {
                    EntrySpeech = new AddPresidentSpeechEntry();
                }
                entitiesPr = EntrySpeech.getPresidentSpeechData();
                hPresidentName.InnerText = entitiesPr[0].PresidentName;
                //   divInfo = entitiesPr[0].Speech + "<a href='UI/DSWS/NoticeView.aspx?PSL=" + entitiesPr[0].SPId + "'>...বিস্তারিত</a>";
                divInfo = entitiesPr[0].Speech + "<a href='about/chairman-message' target='_blank'>...বিস্তারিত</a>";
                pPresidentSpeech.Controls.Add(new LiteralControl(divInfo));
                string url = @"/Images/dsimages/" + entitiesPr[0].ImgPath;
                imgPresident.ImageUrl = url;
                entitiesPnc = EntrySpeech.getPrincipalSpeechData();
                hPrincipalName.InnerText = entitiesPnc[0].PresidentName;
             //    divInfo = entitiesPnc[0].Speech + "<a href='UI/DSWS/NoticeView.aspx?PrinSL=" + entitiesPnc[0].SPId + "'>...বিস্তারিত</a>";
                divInfo = entitiesPnc[0].Speech + "<a href='about/principal-message' target='_blank'>...বিস্তারিত</a>";
                pPrincipalSpeech.Controls.Add(new LiteralControl(divInfo));
                url = @"/Images/dsimages/" + entitiesPnc[0].ImgPath;
                imgPrincipal.ImageUrl = url;
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
                entitiesSD = EntrySD.getSecialDescriptionByType("হোম");
                Session["__BackgroundTitle__"]= h1HomeSupject.InnerText = entitiesSD[0].Subject;
                divInfo = entitiesSD[0].Details + "<a href='about/background'>...বিস্তারিত</a>";
                pHomeDetails.Controls.Add(new LiteralControl(divInfo));
                Session["__BackgroundImgUrl__"] = @"/Images/dsimages/" + entitiesSD[0].Image;
                imgHome.ImageUrl = @"/Images/dsimages/" + entitiesSD[0].Image;
            }
            catch { }
        }
        private void LoadLatestNoticeandCirculars()
        {
            try
            {
                //..............Latest News......................
                //entitiesNotice = new List<AddNoticeEntities>();
                if (EntryNotice == null)
                {
                    EntryNotice = new AddNoticeEntry();
                }
                //entitiesNotice = EntryNotice.getNoticeData();
                dt = new DataTable();
                dt = EntryNotice.getNoticeAttach();
                string ImportantNews = "";
                divInfo = "";
                divInfo += "<table class='table table-striped table - bordered'>" +
                                  "<thead>" +
                    "<tr>" +
                    "<th class='text-center'>Date</th>" +
                                    "<th>Notice Title</th>" +
                                  "</tr>" +
                                "</thead>" +
                                "<tbody>";

                if (dt.Rows.Count > 0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        string href = "";
                        if (dt.Rows[i]["NDetails"].ToString().Trim() != "")
                            href = "UI/DSWS/NoticeView.aspx?NSL=" + dt.Rows[i]["NSL"].ToString() + "";
                        else
                            href = "Images/dsimages/Notice/" + dt.Rows[i]["FileName"].ToString() + "";
                        divInfo += " <tr>" +
                                                      "<td class='text-center'>" + dt.Rows[i]["PublishdDate"].ToString() + "</td>" +
                                                      "<td><a href = '" + href + "' target='_blank' >" + dt.Rows[i]["Title"].ToString() + "</a></td>" +

                                                    "</tr>";
                        if (dt.Rows[i]["IsImportantNews"].ToString().Equals("True"))
                            ImportantNews += " <a href = '" + href + "' target='_blank'>" + dt.Rows[i]["Title"].ToString() + "</a>";
                    }
                  
                  //  ImportantNews = ImportantNews.Remove(0,1);
                }
              //  aNewsHeadling.InnerText = ImportantNews;
                //divInfo += " <tr>" +
                //               "<td class='text-center'>27-May-2018</td>" +
                //               "<td><a href = 'Images/dsimages/Notice/XI Admission Circular.pdf' target='_blank' >ফলাফল</a></td>" +

                //             "</tr>";
                //divInfo += " <tr>" +
                //                  "<td class='text-center'>13-May-2018</td>" +
                //                  "<td><a href = 'Images/dsimages/Notice/XI Admission Circular.pdf' target='_blank' >একাদশ শ্রেণীতে ভর্তি বিজ্ঞপ্তি</a></td>" +

                //                "</tr>";
                //divInfo += " <tr>" +
                //                    "<td class='text-center'>03-May-2018</td>" +
                //                    "<td><a href = 'Images/dsimages/notice.jpg' target='_blank' >নিয়োগ বিজ্ঞপ্তি</a></td>" +

                //                  "</tr>";

                //if (entitiesNotice != null)
                //{
                //    for (int i = 0; i < entitiesNotice.Count; i++)
                //    {
                //        divInfo += " <tr>" +
                //                        "<td class='text-center'>" + entitiesNotice[i].NPublishedDate.ToString("dd-MMMM-yyyy") + "</td>" +
                //                        "<td><a href = 'UI/DSWS/NoticeView.aspx?NSL=" + entitiesNotice[i].NSL + "' target='_blank' > " + entitiesNotice[i].NSubject + "</a></td>" +

                //                      "</tr>";
                //    }
                //}

                divInfo += "</tbody></table>";
                LatestNoticeView.Controls.Add(new LiteralControl(divInfo));
                divImportantNews.Controls.Add(new LiteralControl(ImportantNews));
                //................... END Notice and Circulars.................
            }
            catch { }
        }
        private void LoadLatestNoticeandCirculars_OldCode()
        {
            try
            {
                //..............Latest News......................
                entitiesNotice = new List<AddNoticeEntities>();
                if (EntryNotice == null)
                {
                    EntryNotice = new AddNoticeEntry();
                }
                entitiesNotice = EntryNotice.getNoticeData().FindAll(c => c.Type == "Latest News");
                divInfo = "";
                divInfo += "<ul>";
                for (int i = 0; i < entitiesNotice.Count; i++)
                {
                    //string Details = ;   
                    //Details= Regex.Match(Details, @"^(\w+\b.*?){20}").ToString();                
                    divInfo += "<li id=" + i + "><a href='UI/DSWS/NoticeView.aspx?NSL=" + entitiesNotice[i].NSL + "'><i class='fa fa-caret-square-o-right icon'></i> " + entitiesNotice[i].NSubject + "</a></li>";
                }
                divInfo += "</ul>";
                //        LatestNoticeView.Controls.Add(new LiteralControl(divInfo));

                //................End Latest News

                //..............Notice and Circulars......................
                divInfo = "";
                entitiesNotice = new List<AddNoticeEntities>();
                if (EntryNotice == null)
                {
                    EntryNotice = new AddNoticeEntry();
                }
                entitiesNotice = EntryNotice.getNoticeData().FindAll(c => c.Type == "Notices & Circulars");
                divInfo = "";
                divInfo += "<ul>";
                for (int i = 0; i < entitiesNotice.Count; i++)
                {
                    //string Details = ;   
                    //Details= Regex.Match(Details, @"^(\w+\b.*?){20}").ToString();
                    if (i > 3)
                    {
                        // Readmore.Visible = true;
                        break;
                    }
                    divInfo += "<li id=" + i + "><a href='UI/DSWS/NoticeView.aspx?NSL=" + entitiesNotice[i].NSL + "'><i class='fa fa-caret-square-o-right icon'></i> " + entitiesNotice[i].NSubject + "</a></li>";
                }
                divInfo += "</ul>";
                // divNoticesCirculars.Controls.Add(new LiteralControl(divInfo));
                //................... END Notice and Circulars.................
            }
            catch { }
        }
        private void LoadSlider()  // For Slider Information
        {
            try
            {
                SliderEntry entry = new SliderEntry();
                List<SliderEntities> GetImgLocation = new List<SliderEntities>();
                GetImgLocation = entry.getSliderData();
                string divIndicator = "";
                string divContent = "";
                string active = "active";
                string cls = "item active";

                for (int j = 0; j < GetImgLocation.Count; j++)
                {
                    divIndicator += "\n<li data-target='#myCarousel' data-slide-to='" + j + "' class='" + active + "'></li>";
                    divContent += "\n<div class='" + cls + "'>" +
                        "\n<img src ='" + GetImgLocation[j].Location.Remove(0, 2) + "'  style = 'width:100%;'/>" +
                        "\n</div >";
                    //    divContent += " <div class='item " + active + "'> <img src = '" + GetImgLocation[j].Location.Remove(0, 2) + "' alt = 'Los Angeles' style = 'width:100%;' ></ div > ";
                    cls = "item";
                    active = "";

                }

                //              divInfo = "<ol class='carousel-indicators'> "+ divIndicator + " </ol>" +
                //                 "<div class='carousel-inner'>"+ divContent + "</div>" +

                // "<a class='left carousel-control' href='#myCarousel' data-slide='prev'>" +
                //   "<span class='glyphicon glyphicon-chevron-left'></span>" +
                //  " <span class='sr-only'>Previous</span>" +
                // "</a>" +
                // "<a class='right carousel-control' href='#myCarousel' data-slide='next'>" +
                //  " <span class='glyphicon glyphicon-chevron-right'></span>" +
                //   "<span class='sr-only'>Next</span>" +
                //"</a>";
                //             divInfo += "<div id='htmlcaption' class='nivo-html-caption'> <strong>This</strong> is an example of a <em>HTML</em> caption with <a href='#'>a link</a>. </div>";

                divInfo = "<div id='myCarousel' class='carousel slide' data-ride='carousel'>";
                divInfo += @"<ol class='carousel-indicators'>
          " + divIndicator + "   " +
           "\n</ol>" +
           "\n<div class='carousel-inner'>" +
           divContent +
           "\n</div>" +
                    "\n<a class='left carousel-control' href='#myCarousel' data-slide='prev'>" +
                    "\n<span class='glyphicon glyphicon-chevron-left'></span>" +
                    "\n<span class='sr-only'>Previous</span>" +
                    "\n</a>" +
                    "\n<a class='right carousel-control' href='#myCarousel' data-slide='next'>" +
                    "\n<span class='glyphicon glyphicon-chevron-right'></span>" +
                    "\n<span class='sr-only'>Next</span>" +
                    "\n</a>";
                divInfo += "\n</div>";
                divSlider.Controls.Add(new LiteralControl(divInfo));
            }
            catch { }
        }
        private void loadAlbumDetails()
        {
            try
            {
                divInfo = "";
                List<AlbumDetailsEntities> albumDetails = new List<AlbumDetailsEntities>();
                AlbumInfoDetails album = new AlbumInfoDetails();
                albumDetails = album.getAlbumDetails();
                if (albumDetails != null)
                {
                    for (int r = 0; r < albumDetails.Count; r++)
                        //  divInfo += "<div class='gallery-images'>" +
                        //    " <a class='fancybox' rel='gallery1' href='../../" + albumDetails[r].imgLocation.Remove(0, 2) + "' ><img id='photo_" + r + "' alt='...' src='../../" + albumDetails[r].imgLocation.Remove(0, 2) + "' width='88' height='65' border='0' ></a>" +
                        //"</div> ";
                        divInfo += "<div class='col-md-4 '>" +
                                    "<a class='lightbox' href='" + albumDetails[r].imgLocation.Remove(0, 2) + "'>" +
                        "<img src='" + albumDetails[r].imgLocation.Remove(0, 2) + "' alt='' height='98'> </a>" +
                                "</div>";
                }
                divPhotoAlbum.Controls.Add(new LiteralControl(divInfo));
            }
            catch { }
        }
        private void loadEvent()  // For Event Information
        {
            try
            {
                EventDetailsEntry ev = new EventDetailsEntry();
                List<EventDetailsEntities> GetEventData = new List<EventDetailsEntities>();
                GetEventData = ev.getEventData();
                string divInfo = "";
                divInfo = "<ul class='event-list'>";

                for (int i = 0; i < GetEventData.Count; i++)
                {
                    string date = "";
                    string day = "";
                    string month = "";
                    string year = "";
                    if (GetEventData[i].EventDate.HasValue)
                    {
                        date = GetEventData[i].EventDate.Value.ToString("yyyy-MM-dd");
                        day = GetEventData[i].EventDate.Value.ToString("dd");
                        month = GetEventData[i].EventDate.Value.ToString("MMM");
                        year = GetEventData[i].EventDate.Value.ToString("yyyy");
                    }

                    divInfo += " <li>" +
                                    "<time datetime=" + date + ">" +
                                        "<span class='day'>" + day + "</span>" +
                                        "<span class='month'>" + month + "</span>" +
                                        "<span class='year'>" + year + "</span>" +
                                    "</time>" +
                                    "<div class='info'>" +
                                        "<h3 class='title'>" + GetEventData[i].Title + "</h3>" +
                                        "<p class='desc'>" + GetEventData[i].Description + "</p>" +
                                    "</div>" +
                                "</li>";
                }
                //divInfo += "<li> <a href='#'>" +
                //         " <img src='../../" + GetEventData[i].imgLocation.Remove(0, 2) + "'title='" + GetEventData[i].Title + "'/>" +
                //         "</a>" +
                //         "</li>";
                divInfo += "</ul>";
                divEventContainer.Controls.Add(new LiteralControl(divInfo));
            }
            catch { }
        }
    }
}