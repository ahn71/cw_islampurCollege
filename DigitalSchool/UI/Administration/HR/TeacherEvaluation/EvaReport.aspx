<%@ Page Title="" Language="C#" MasterPageFile="~/main.Master" AutoEventWireup="true" CodeBehind="EvaReport.aspx.cs" Inherits="DS.UI.Administration.HR.TeacherEvaluation.EvaReport" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style>
        .FinalGradeSheet tbody tr th{
            border:1px solid #CCCCCC;
            background:#ebebe0;
             padding: 3px;
        }
         .FinalGradeSheet tbody tr td {
            border: 1px solid #CCCCCC;
            font-size: 12px;
            padding: 3px;
            font-weight: normal;
            color: #000;
            background:#fff;
        }
        span.radio-btn label {
    margin: 10px;
}
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
 <asp:UpdatePanel ID="uplMessage" runat="server">
        <ContentTemplate>
            <p class="message" id="lblMessage" clientidmode="Static" runat="server"></p>
        </ContentTemplate>
    </asp:UpdatePanel>    
    <div class="row">
        <div class="col-md-12">
            <!--breadcrumbs start -->
            <ul class="breadcrumb">
                <li>
                    <a id="A1" runat="server" href="~/Dashboard.aspx">
                        <i class="fa fa-dashboard"></i>
                        Dashboard
                    </a>
                </li>
                <li><a id="A2" runat="server" href="~/UI/Administration/AdministrationHome.aspx">Administration Module</a></li>
                <li><a id="A3" runat="server" href="~/UI/Administration/HR/hrHome.aspx">Human Resource Module</a></li>  
                <li><a id="A4" runat="server" href="~/UI/Administration/HR/TeacherEvaluation/EvaHome.aspx">Teacher Evaluation</a></li> 
                <li class="active">Evaluation Reports</li>              
            </ul>
            <!--breadcrumbs end -->
        </div>
    </div>
     <asp:UpdatePanel runat="server" ID="up2" UpdateMode="Conditional">
                <Triggers>
                  
                </Triggers>
                <ContentTemplate>
    <div class="conainer">
    <div class="" id="divSearchPanel" runat="server">
      
          <div class="row tbl-controlPanel" style="background-color:white" >
             
                            <div class="col-md-8 col-md-offset-2"  runat="server" id="tblOp">
                                 <br />
                                <center >                                
                                <asp:RadioButtonList runat="server"  ID="rblReport"  RepeatDirection="Horizontal" AutoPostBack="true" OnSelectedIndexChanged="rblReport_SelectedIndexChanged">

                                    <asp:ListItem Selected="True" class="radio-btn" Value="0">Final Grade Sheet</asp:ListItem>
                                    <asp:ListItem  class="radio-btn" Value="4">Final Grade Sheet (Own) </asp:ListItem>
                                    <asp:ListItem  class="radio-btn" Value="5">Evaluators' Comparative Report</asp:ListItem>
                                    <asp:ListItem class="radio-btn" Value="1">College Rank Report</asp:ListItem>
                                    <asp:ListItem class="radio-btn" Value="2">Department Rank Report</asp:ListItem>
                                    <asp:ListItem class="radio-btn" Value="3">Individual Perpormance Report</asp:ListItem>
                                    <asp:ListItem class="radio-btn" Value="6">Dpartment Perpormance Report</asp:ListItem>
                                    <asp:ListItem class="radio-btn" Value="7">Sub Indicator Based Perpormance Report</asp:ListItem>
                                </asp:RadioButtonList>
                                    </center>

                                <div class="col-sm-6 col-sm-offset-1">
                                <div class="form-group row">
                                    <label class="col-sm-5">Evaluation Session</label>
                                    <div class="col-sm-7">
                                        <asp:DropDownList ID="ddlEvaSession"  runat="server" CssClass="input controlLengthMin form-control" ClientIDMode="Static">                                    
                                    </asp:DropDownList>
                                        </div>                                                                    
                                    
                                </div>
                                    </div>
                                  <div class="col-sm-5">
                                <div class="form-group row">
                                    <asp:Button ID="btnSearch" CssClass="btn btn-primary" runat="server" Text="Search" OnClick="btnSearch_Click" ClientIDMode="Static"
                                         /> 
                                     <asp:Button ID="btnPreview" CssClass="btn btn-primary" runat="server" Text="Preview" OnClick="btnPreview_Click" ClientIDMode="Static"
                                         />    
                                    
                                    <button id="btnExport" runat="server" visible="false" class="btn btn-primary" onclick="fnExcelReport('tblFinalGradeSheet');"> Export to Excel </button>
                                    <iframe id="txtArea1" style="display:none"></iframe>                          
                                    <%--<button id="exportpdf" > EXPORT PDF</button>--%>
                                </div>
                                    </div>
                                 
                             
                            </div>
                        </div>
      </div>
        
           <div class="row tbl-controlPanel" >
        <center>
          <%-- <table runat="server" id="tbltest" class="table table-bordered ">
                
               <tr>
                   <th rowspan="2" class="text-center ">A1</th>
                   <th rowspan="2" class="text-center">A1</th>
                   <th colspan="3" class="text-center">A1</th>
                   <th colspan="3" class="text-center">A2</th>
                   <th colspan="3" class="text-center">A3</th>
               </tr>
               <tr>
                   
                   <th class="text-center ">A</th>
                   <th>A</th>
                   <th>A</th>
                   <th>A</th>
                   <th>A</th>
                   <th>A</th>
                   <th>A</th>
                   <th>A</th>
                   <th>A</th>
               </tr>
              
           </table>--%>
            
        <div id="divMarksheet" runat="server" 
                        style="width: 100%; height:auto; overflow-x: auto; overflow-y: auto; float: left;" ></div>
                     
            </center>
            </div>
      

    </div>
                    </ContentTemplate>
            </asp:UpdatePanel>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ScriptContent" runat="server">
    <script type="text/javascript">
      
        function goToNewTab(url) {
            window.open(url);
          
        }
        function demoFromHTML() {
        var pdf = new jsPDF('p', 'pt', 'letter');
        // source can be HTML-formatted string, or a reference
        // to an actual DOM element from which the text will be scraped.
        source = $('#MainContent_divMarksheet')[0];

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
            width: 522
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
                pdf.save('Test.pdf');
            }, margins
        );
    }
    </script>

</asp:Content>