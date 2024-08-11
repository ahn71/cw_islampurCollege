<%@ Page Title="বাৎসরিক ছুটির তলিকা" Language="C#" MasterPageFile="~/DSWS.Master" AutoEventWireup="true" CodeBehind="YearlyLeaveList.aspx.cs" Inherits="DS.UI.DSWS.YearlyLeaveList" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style>

@font-face {
    font-family: "SolaimanLipi";
    src: url("fonts/SolaimanLipi.woff") format("woff"), url("fonts/SolaimanLipi.otf") format("otf"), url("fonts/SolaimanLipi.ttf") format("ttf"), url("fonts/SolaimanLipi.eot") format("eot");
}

.wrapper {
    margin: 0 auto;
    overflow: hidden;
    width: 1100px;
}

.info-tel {
    float: right;
    margin-top: 20px;
    width: 225px;
}
.arabic-title {
    font-size: 25px;
    margin: 0;
    padding: 0;
}

.info-tel h3 {
    color: #49709d;
    font-weight: normal;
    margin: 10px 12px 10px 0;
}
.info-tel p {
    color: #49709d;
    float: left;
    font-size: 22px;
    font-weight: normal;
    margin: 0;
    padding: 0;
    width: 100%;
}
.info-tel p span {
    color: #49709d;
    font-size: 22px;
}
.logpanel {
    margin: 10px 0 0 !important;
}
.logpanel h2 {
    margin-bottom: 20px !important;
    text-align: center !important;
    text-decoration: underline !important;
}
.service_page_main_area {
    margin-bottom: 10px;
}
section {
    overflow: hidden;
}

.box-pad {
    padding: 0 !important;
    width: 100% !important;
}
.carousel-indicators .active {
    background-color: #fe4853 !important;
    height: 12px;
    margin: 0;
    width: 12px;
}
.margin {
    margin-top: 20px;
}


.main-content {
    float: left;
    min-height: 468px;
    padding: 0 15px;
    width: 590px;
}
.common-content {    
    min-height: 468px;
    padding: 0 15px;    
}
.main-content h2, .common-content h2 {
    margin: 0;
}
.speach {
    float: left;
    width: 270px;
}

.event {
    float: right;
    width: 272px;
}
.event h3 {
    background: #0074ba none repeat scroll 0 0;
    color: white;
    line-height: 33px;
    margin: 0;
    text-align: center;
}

.box-margin {
    margin-top: 8px;
}

.scrollToTop {
    background: rgba(0, 0, 0, 0) url("images/back-to-top-icon.png") no-repeat scroll 0 20px;
    bottom: 35px;
    color: #444;
    display: none;
    font-weight: bold;
    height: 130px;
    padding: 10px;
    position: fixed;
    right: 10px;
    text-align: center;
    text-decoration: none;
    width: 100px;
}
.scrollToTop:hover {
    text-decoration: none;
}
.bellow-footer-bg span {
    color: #8f8e8e;
}
.bellow-footer-bg span a {
    color: #8f8e8e;
    transition: all 0.5s ease 0s;
}
.calender {
    background: #0096c3 none repeat scroll 0 0;
    float: right;
    height: 407px;
    margin-top: 3px;
    padding: 10px;
    width: 370px;
}
.calender h3 {
    border-bottom: 2px dashed #22a666;
    color: #ffffff;
    font-size: 30px;
    margin: 0;
    padding: 0 0 10px;
    text-align: center;
    text-transform: uppercase;
}
.cell {
    font-weight: bold;
}
.auto_text_slide {
    height: 300px;
    margin: 20px 0;
    overflow: hidden;
}
.auto_text_slide ul {
    display: block;
}
.auto_text_slide li {
    border-bottom: 1px dashed #22a666;
    padding: 8px 0;
}
.auto_text_slide a {
    color: #ffffff;
    text-decoration: none;
}
.auto_text_slide a:hover {
    color: #ffffff;
    text-decoration: none;
}
.main-content-inner {
    float: left;
    min-height: 468px;
    padding: 0 15px;
    width: 859px;
}
.main-content-inner h2 {
    margin: 0 0 15px;
}


.month {
    height: 3369px;
    overflow: hidden;
      margin-top: 15px;
}
.month_div {
    float: left;
    font-size: 14px;
    height: 1105px;
    margin-right: 2px;
    width: 200px;
}
.data_view .month_table {
    background: #f1f1f1 none repeat scroll 0 0;
}
caption {

    padding-bottom: 8px;
    padding-top: 8px;
   
}
.month_table caption {
    background: #a6a6a6 none repeat scroll 0 0;
    color: #f25510;
    font-weight: bold;
    text-align: center;
}
.data_view tbody tr {
    border-color: inherit;
    display: table-row-group;
    vertical-align: middle;
}
.data_view .month_table {
    border-collapse: separate;
    border-color: grey;
    border-spacing: 4px;
    display: table;
    width:100%;
}
.data_view tr th:first-child {
    width: 10px;
}
.data_view td, .data_view th {
    padding: 3px 0 3px 2px;
    text-align: center;
}
.month_table tr th {
    height: 30px !important;
}
caption {
    display: table-caption;
}
.home_event1 {
    color: #f0f;
}
.homeclasscontinue {
    color: #87c71d;
}
.home_holiday {
    color: #f00;
}
.home_event2 {
    color: #03c;
}
.home_event1 {
    color: #f0f;
}
.month_table tr th {
    height: 30px !important;
}
.data_view table tr:nth-child(2n+1) td {
    background: #f5f5f5 none repeat scroll 0 0;
    color: #000;
}
.data_view table tr:nth-child(2n) td {
    background: #fff none repeat scroll 0 0;
}
.album-cover {
    height: 250px;
}



.scrollToTop {
    background: rgba(0, 0, 0, 0) url("images/back-to-top-icon.png") no-repeat scroll 0 20px;
    bottom: 35px;
    color: #444;
    display: none;
    font-weight: bold;
    height: 130px;
    padding: 10px;
    position: fixed;
    right: 10px;
    text-align: center;
    text-decoration: none;
    width: 100px;
}
.scrollToTop:hover {
    text-decoration: none;
}

    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ForLeftSideMenuList" runat="server">
    <div class="common-content">
    <div class="month">
        <div id="divYearlyHoliday" runat="server" class="month_s">    
          
            
        </div>
    </div>
</div>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="ForFoterSlider" runat="server">
</asp:Content>
