<%@ Page Title="Evaluation Number Entry" Language="C#" MasterPageFile="~/main.Master" AutoEventWireup="true" CodeBehind="EvaNumberEntry.aspx.cs" Inherits="DS.UI.Administration.HR.TeacherEvaluation.EvaNumberEntry" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
     <style>
        .tgPanel{
            width: 100%;
        }
        #btnPrintPreview{
            margin: 3px;
        }
        .controlLength{
            /*width: 302px;*/
        }
        .controlLengthMin{
            /*width: 150px;*/
        }
        .LoadingImg{
            width: 80%;
            margin-left: 10px;
            padding-top: 5px;
        }
        .checkBox label{
            margin-left:4px;
        }
         /*#tblMarkEntryPoint th, td {
             width:40px;
         }*/
        @media only screen and (min-width: 320px) and (max-width: 479px) {

            #ddlSectionName {
            margin-top:10px;
            }

            #ddlBatch{
            margin-top:10px;
            }

            #MainContent_btnDetailsMarks{
            margin-top:10px;
            }
        
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
                <li class="active">Evaluation Number Entry</li>              
            </ul>
            <!--breadcrumbs end -->
        </div>
    </div>
     <asp:UpdatePanel runat="server" ID="up2" UpdateMode="Conditional">
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="ddlEvaSession" />
                    <asp:AsyncPostBackTrigger ControlID="ddlCommitteeMember" />
                </Triggers>
                <ContentTemplate>
    <div class="conainer">
    <div class="" id="divSearchPanel" runat="server">
      
          <div class="row tbl-controlPanel" style="background-color:white">
             
                            <div class="col-sm-8 col-sm-offset-2"  runat="server" id="tblOp">
                                 <br />
                                <div class="col-sm-6">
                                <div class="form-group row">
                                    <label class="col-sm-5">Evaluation Session</label>
                                    <div class="col-sm-7">
                                        <asp:DropDownList ID="ddlEvaSession"  runat="server" CssClass="input controlLengthMin form-control" ClientIDMode="Static" AutoPostBack="true"  OnSelectedIndexChanged="ddlEvaSession_SelectedIndexChanged">                                    
                                    </asp:DropDownList>
                                        
                                        </div>                                    
                                    
                                </div>
                                    </div>
                                <div class="col-sm-6">
                                <div class="form-group row">
                                    <label class="col-sm-5">Committee Member</label>
                                    <div class="col-sm-7">
                                        <asp:DropDownList ID="ddlCommitteeMember" runat="server" CssClass="input controlLengthMin form-control" ClientIDMode="Static" AutoPostBack="true" OnSelectedIndexChanged="ddlCommitteeMember_SelectedIndexChanged" >
                                    </asp:DropDownList>
                                        </div>     
                                    <%--<asp:Button runat="server" ID="btnExportToExcel" ClientIDMode="Static" Text="Export To Excel" OnClick="btnExportToExcel_Click" />  
                                     <asp:Button runat="server" ID="btnExport" ClientIDMode="Static" Text="Export"  OnClientClick="write_to_excel();" />--%>                       
                                </div>         
                                </div>         
                             
                            </div>
                        </div>
      </div>
        
          
      
    <div class="row tbl-controlPanel" >
        <center>
         
            
        <div id="divMarksheet" runat="server" 
                        style="width: 100%; height:70vh; overflow-x: scroll; overflow-y: scroll; float: left;" ></div>
                     
            </center>
            </div>
    </div>
                    </ContentTemplate>
            </asp:UpdatePanel>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ScriptContent" runat="server">  
    <script type="text/javascript">     
        $(document).ready(function () {
            $(document).on('keypress', function (e) {
                if (e.keyCode == 13) {
                    e.preventDefault();
                }
            })
        })
        function saveData(celldata) {
            var dataID = celldata.id;
            var splitedData = dataID.split(":");
            var dataValue = celldata.value;
            jx.load('ForNumberEntry.aspx?tbldata=' + splitedData + '&val=' + dataValue + '&do=attUpdate', function (data) {
                if (data == "Not Save")
                    alert("Please type valid marks");
                this.focus();
            });
        }
        function write_to_excel() {
            alert('ok');
            str = "";
            var mytable = document.getElementById("tblMarkEntryPoint");
            var rowCount = mytable.rows.length;
            var colCount = mytable.getElementsByTagName("tr")[0].getElementsByTagName("th").length;
            var ExcelApp = new ActiveXObject("Excel.Application");
            var ExcelSheet = new ActiveXObject("Excel.Sheet");
            //ExcelSheet.Application.Visible = true;
            for (var i = 0; i < rowCount; i++) {
                for (var j = 0; j < colCount; j++) {
                    if (i == 0) {
                        str = mytable.getElementsByTagName("tr")[i].getElementsByTagName("th")[j].innerText;
                    }
                    else {
                        str = mytable.getElementsByTagName("tr")[i].getElementsByTagName("td")[j].innerText;
                    }
                    ExcelSheet.ActiveSheet.Cells(i + 1, j + 1).Value = str;
                }
            }
            ExcelSheet.autofit;
            ExcelSheet.Application.Visible = true;
            DisplayAlerts = true;
            CollectGarbage();
        }
        </script>

</asp:Content>
