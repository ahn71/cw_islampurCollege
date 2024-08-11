<%@ Page Title="ক্লাস রুটিন" Language="C#" EnableEventValidation="false" MasterPageFile="~/DSWS.Master" AutoEventWireup="true" CodeBehind="WSClassRoutine.aspx.cs" Inherits="DS.UI.DSWS.WSClassRoutine" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">  
    <link href="/Styles/gridview.css" rel="stylesheet" />

    <style type="text/css">
        .printbutton {
            background-image: url("../../Images/printicon/pdf.png");
            background-repeat: no-repeat;
            height: 24px;
            text-align: center;
            width: 24px;
        }
          .table-bordered {
            border:1px solid gray;
            margin:0px auto;           
        }
        .droppable{
            border:1px solid red;
        }
           .table-defination th,
        .table-defination td:first-child {
           /*background-color: #f6eded;*/
            border:1px solid gray;
        }
        .table thead > tr > th, .table tbody > tr > th, .table tfoot > tr > th, .table thead > tr > td, .table tbody > tr > td, .table tfoot > tr > td {
            border: 1px solid gray;
        }
        #droppable > ul {
          padding-left: 9px;
        }

    </style>
    <script type="text/javascript">//<![CDATA[ 

        function pdfprint() {
            var pdf = new jsPDF('p', 'pt', 'letter');
            // source can be HTML-formatted string, or a reference
            // to an actual DOM element from which the text will be scraped.
            pdf.setFontSize(9);
            source = $('#ForLeftSideMenuList_upRoutine')[0];

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
                top: 80,
                bottom: 60,
                left: 40,
                width: 896
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
                pdf.save('ClassRoutine.pdf');
            }, margins);
        }
        //]]>  

</script>
 
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
     <asp:UpdatePanel ID="uplMessage" runat="server">
        <ContentTemplate>
            <p class="message" id="lblMessage" clientidmode="Static" runat="server"></p>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ForLeftSideMenuList" runat="server">
       
    <div class="col-md-9">
        <div class="row">
        <div class="col-md-12" style="border:1px solid #cccccc; height: auto; padding: 0px;margin:0px; width:98%;">
            <div class="tgPanelHead">ক্লাস রুটিন</div>
        <div  class="routine">
<div style="text-align:center; border-bottom:1px solid #D2D2D2; padding:10px;">
  
    <asp:UpdatePanel runat="server" ID="upPrint">
        <ContentTemplate> 
            Shift
            <asp:DropDownList runat="server" ID="ddlShift" ClientIDMode="Static" Width="120px" CssClass="input"  AutoPostBack="true" OnSelectedIndexChanged="ddlShift_SelectedIndexChanged"></asp:DropDownList>
            Batch
            <asp:DropDownList runat="server" ID="ddlBatch" ClientIDMode="Static" Width="120px" CssClass="input"  AutoPostBack="true" OnSelectedIndexChanged="ddlBatch_SelectedIndexChanged"></asp:DropDownList>
            Group
            <asp:DropDownList runat="server" ID="ddlGroup" ClientIDMode="Static" Width="120px" CssClass="input"  AutoPostBack="true" OnSelectedIndexChanged="ddlGroup_SelectedIndexChanged"></asp:DropDownList>
            Section
            <asp:DropDownList runat="server" ID="ddlSection" ClientIDMode="Static" Width="120px" CssClass="input"  AutoPostBack="true" OnSelectedIndexChanged="ddlSection_SelectedIndexChanged"></asp:DropDownList>
            <%--<asp:Button ID="btnprintRoutine" runat="server" CssClass="btn printbutton" OnClick="btnprintRoutine_Click" />--%>
            <button  class='btn printbutton' onclick="javascript:pdfprint();"/>
        </ContentTemplate>
    </asp:UpdatePanel>

</div><br />

<asp:UpdatePanel runat="server" ID="upRoutine" UpdateMode="Conditional">
    <Triggers>
        <asp:AsyncPostBackTrigger ControlID="ddlShift" />
        <asp:AsyncPostBackTrigger ControlID="ddlBatch" />
        <asp:AsyncPostBackTrigger ControlID="ddlGroup" />
        <asp:AsyncPostBackTrigger ControlID="ddlSection" />
      <%--  <asp:AsyncPostBackTrigger ControlID="btnprintRoutine" />--%>
    </Triggers>
    <ContentTemplate>
        <asp:Panel ID="pnlRoutine" runat="server">
        <asp:Panel runat="server" class="reportHeader" style="height: 102px!important">
            <h3 style="font-size:12px" runat="server" id="hSchoolName"></h3>
            <a style="font-size:12px" runat="server" id="aAddress"></a>
            <h3 style="margin: 0px;font-size:12px">
                <asp:Label runat="server" ID="lblBatch"></asp:Label>
            </h3>
            <asp:Label runat="server" ID="lblShit"></asp:Label>
            <p runat="server" id="pTitle" style="margin: 0px; font-size: 12px; font-weight: bold;">Class Routine</p>
            <asp:Label ID="lblYear" runat="server"></asp:Label>
        </asp:Panel>
        <asp:Panel id="divClassRoutine" style="margin:0px auto; font-family:'Times New Roman';" runat="server">

        </asp:Panel> 
            </asp:Panel>
    </ContentTemplate>
</asp:UpdatePanel>
</div>
            </div>
            </div>
        </div>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="ForFoterSlider" runat="server">
</asp:Content>

