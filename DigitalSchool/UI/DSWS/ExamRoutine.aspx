<%@ Page Title="পরীক্ষা রুটিন" Language="C#" MasterPageFile="~/DSWS.Master" AutoEventWireup="true" CodeBehind="ExamRoutine.aspx.cs" Inherits="DS.UI.DSWS.ExamRoutine" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style>
         /**/
        .display td:nth-child(2), td:nth-child(3),td:nth-child(4)
        , td:nth-child(5), td:nth-child(6), td:nth-child(7){
            width:250px;
            
        }
        .display td:first-child {
            width:100px;
        }
        .display th
        {
            background-color:black;
            color:white;
        }           
         /**/
        .controlLength{
            width: 200px;
        }
        .tbl-controlPanel1{
            width: 670px;
            color:gray;
            font-size:large;
            font-family: 'Times New Roman';       
       margin: 10px auto;
       padding: 5px;
        }
         .table tr th:first-child, tr td:first-child, tr th:nth-child(1), tr td:nth-child(1),tr th:nth-child(2), tr td:nth-child(2), tr th:nth-child(3),tr td:nth-child(3), tr th:nth-child(4),tr td:nth-child(4),tr th:nth-child(5),tr td:nth-child(5), tr th:nth-child(6),tr td:nth-child(6), tr th:nth-child(7),tr td:nth-child(7),tr th:nth-child(8),tr td:nth-child(8) {
            text-align: center;        
        }
        .radioButtonLists label {
            margin-left:5px;
        }
        .printbutton {
            background-image: url("../../Images/printicon/pdf.png");
            background-repeat: no-repeat;
            height: 24px;
            text-align: center;
            width: 24px;
        }
    </style>
     <script type="text/javascript">//<![CDATA[ 

         function pdfprint() {
             var pdf = new jsPDF('p', 'pt', 'a4');
             // source can be HTML-formatted string, or a reference
             // to an actual DOM element from which the text will be scraped.
             source = $('#ForLeftSideMenuList_divGv')[0];

             // we support special element handlers. Register them with jQuery-style 
             // ID selector for either ID or node name. ("#iAmID", "div", "span" etc.)
             // There is no support for any other type of selectors 
             // (class, of compound) at this time.
             specialElementHandlers = {
                 // element with id of "bypass" - jQuery style selector
                 '#bypassme': function (element, renderer) {
                     // true = "handled elsewhere, bypass text extraction"
                     return true
                 }
             };
             margins = {
                 top: 10,
                 bottom: 10,
                 left: 10,
                 width: 850
             };
             // all coords and widths are in jsPDF instance's declared units
             // 'inches' in this case
             pdf.fromHTML(
             source, // HTML string or DOM elem ref.
             margins.left, // x coord
             margins.top, { // y coord
                 'width': margins.width, // max width of content on PDF
                 'elementHandlers': specialElementHandlers
             },

             function (dispose) {
                 // dispose: object with X, Y of the last line add to the PDF 
                 //          this allow the insertion of new lines after html
                 pdf.save('ExamRoutine.pdf');
             }, margins);
         }
         //]]>  

</script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ForLeftSideMenuList" runat="server">
         <asp:UpdatePanel ID="uplMessage" runat="server">
        <ContentTemplate>
            <p class="message" id="lblMessage" clientidmode="Static" runat="server"></p>
        </ContentTemplate>
    </asp:UpdatePanel>
    <div class="row">
         <div runat="server" id="divBoardDirContacts" class="main-content">
        <div class="">
            <div class="tgPanel">
                <div class="tgPanelHead">পরীক্ষা রুটিন</div>
                <div class="routine">
                    <div style="text-align:center; border-bottom:1px solid #D2D2D2; padding:10px;">
                        <asp:UpdatePanel runat="server" ID="upPrint" UpdateMode="Conditional">
                            
                            <ContentTemplate>
                                Exam
                                <asp:DropDownList runat="server" ID="ddlExam" ClientIDMode="Static" Width="220px" CssClass="input" AutoPostBack="true" OnSelectedIndexChanged="ddlExam_SelectedIndexChanged"></asp:DropDownList>
                                Batch
                                <asp:DropDownList runat="server" ID="ddlBatch" ClientIDMode="Static" Width="220px" CssClass="input" AutoPostBack="true" OnSelectedIndexChanged="ddlBatch_SelectedIndexChanged"></asp:DropDownList>
                                <button  class='btn printbutton' onclick="javascript:pdfprint();"></button>
                                 </ContentTemplate>
                                 </asp:UpdatePanel>
                        </div>
                    <br />
                    <asp:UpdatePanel runat="server" ID="UpdatePanel1" UpdateMode="Conditional">                       
                        <ContentTemplate>
                            <div class="row">
                                <div class="col-lg-1"></div>
                                <div class="col-lg-10">
                                    <div runat="server" id="divGv" class="col-md-12" style="margin-top: 10px">

                                        <asp:GridView ID="gvExamSchedule" CssClass="tbl-controlPanel1 display" runat="server" GridLines="Both" AutoGenerateColumns="true" EditRowStyle-HorizontalAlign="Center">
                                            <HeaderStyle Height="45px" HorizontalAlign="Center" />
                                        </asp:GridView>

                                    </div>
                                </div>
                                <div class="col-lg-1"></div>
                            </div>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </div>
                    <br />
            </div>
        </div>
             </div>
    </div>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="ForFoterSlider" runat="server">
</asp:Content>

