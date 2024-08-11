<%@ Page Title="Admission Details" Language="C#" MasterPageFile="~/main.Master" AutoEventWireup="true" CodeBehind="AdmissionDetails.aspx.cs" Inherits="DS.UI.Academics.Students.AdmissionDetails" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style>        
        .tgPanel {
            width: 100%;
        }
        .controlLength {
            width: 150px;
        }
        .tbl-controlPanel td:nth-child(1),
        .tbl-controlPanel td:nth-child(3),
        .tbl-controlPanel td:nth-child(5) {
            padding: 0px 5px;
        }
        .litleMargin {
            margin-left: 5px;
        }
        .btn {
            /*margin: 3px;*/
        }
        /*.tbl-controlPanel {
            width:762px;
        }*/
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
                    <a runat="server" href="~/Dashboard.aspx">
                        <i class="fa fa-dashboard"></i>
                        Dashboard
                    </a>
                </li> 
                <li><a runat="server" href="~/UI/Academic/AcademicHome.aspx">Academic Module</a></li>
                <li><a runat="server" href="~/UI/Academic/Students/StdHome.aspx">Student Module</a></li> 
                <li class="active">Admission Details</li>
            </ul>
            <!--breadcrumbs end -->
        </div>
    </div>
      <asp:UpdatePanel ID="UpdatePanel2" runat="server" UpdateMode="Conditional">                   
                <ContentTemplate>    
    <div class="tgPanel">
        <div class="row tbl-controlPanel">
            <div class="col-sm-10 col-md-offset-1">
                <div class="">
                    <div class="col-sm-3">
                        <label class="col-sm-4">Class</label>
                        <div class="col-sm-8">
                            <asp:DropDownList ID="dlClass" runat="server" CssClass="input controlLength" ClientIDMode="Static" AutoPostBack="false" >                       
                            </asp:DropDownList>
                        </div>
                    </div>
                    <div class="col-sm-3">
                        <label class="col-sm-4">Shift</label>
                        <div class="col-sm-8">
                          <asp:DropDownList ID="dlShift" runat="server" CssClass="input controlLength" ClientIDMode="Static" AutoPostBack="false">                       
                            </asp:DropDownList>
                        </div>
                    </div>
                    <div class="col-sm-3">
                        <label class="col-sm-4">Payment Status</label>
                        <div class="col-sm-8">
                          <asp:DropDownList ID="ddlStatus" runat="server" CssClass="input controlLength" ClientIDMode="Static" AutoPostBack="false">
                            <asp:ListItem Selected="True" Value="0">...Select...</asp:ListItem>
                            <asp:ListItem Value="1">All</asp:ListItem>
                            <asp:ListItem Value="2">Paid</asp:ListItem>
                            <asp:ListItem Value="3">Unpaid</asp:ListItem>
                        </asp:DropDownList>
                        </div>
                    </div>
                    <div class="col-sm-3">
                        <asp:Button ID="btnSearch" Text="Search" CssClass="btn btn-primary litleMargin" ClientIDMode="Static"
                        runat="server" OnClientClick="return validationInput();" OnClick="btnSearch_Click" />
                        <asp:Button ID="btnPrint" Text="Print" CssClass="btn btn-primary litleMargin" ClientIDMode="Static"
                        runat="server" OnClientClick="return validationInput();" OnClick="btnPrint_Click"  />
                    </div>
                </div>
            </div>
        </div>
    </div>  
                    </ContentTemplate>
          </asp:UpdatePanel>                  
    <div class="tgPanel">
        <div class="widget">
           <%-- <div class="head">
                <img src="/Images/action/refresh.png" class="refresh" onclick="$('#btnSearch').click();" />
                <div class="dataTables_filter_New" style="float: right;">
                    <label>
                        Search:
                    <input type="text" class="Search_New" placeholder="type here name/mobile" />
                    </label>
                </div>
            </div>--%>
            <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="btnSearch" EventName="Click" />
                </Triggers>
                <ContentTemplate>
                    <div id="divStudentDetails" class="datatables_wrapper" runat="server" style="width: 100%; height: auto"></div>
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
    </div>  
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ScriptContent" runat="server">
    <script type="text/javascript">
        $(document).ready(function () {
            // $("#dlClass").select2();           
            $('#tblStudentInfo').dataTable({
                "iDisplayLength": 10,
                "lengthMenu": [10, 20, 30, 40,50, 100]
            });
        });
        function loadStudentInfo() {
            $('#tblStudentInfo').dataTable({
                "iDisplayLength": 10,
                "lengthMenu": [10, 20, 30, 40, 50, 100]
            });
        }
        function validationInput() {
            if (validateCombo('dlClass', "0", 'Select a Class Name') == false) return false;
            if (validateCombo('dlShift', "0", 'Select a Shift') == false) return false;
            if (validateCombo('ddlStatus', "0", 'Select Payment Status') == false) return false;
            return true;
        }   
        function viewStudent(studentId) {
            goURL('/UI/Academic/Students/StdProfile.aspx?StudentId=' + studentId);
        }
        function PayNow(callID) {            
            var getID = callID.id;
            var ClsAdmID=getID.split("_");           
            goURL('/UI/Administration/Finance/FeeManaged/AdmFeesCollection.aspx?admissionNo=' +
                ClsAdmID[1] + "&classId=" + ClsAdmID[0] + "&Page=stdadmdetails");
        }
        function onMouseOver(rowIndex) {
            var gv = document.getElementById("gvAdmissionDetails");
            var rowElement = gv.rows[rowIndex];
            rowElement.style.backgroundColor = "#c8e4b6";
            //rowElement.cells[1].style.backgroundColor = "green";
        }
        function onMouseOut(rowIndex) {
            var gv = document.getElementById("gvAdmissionDetails");
            var rowElement = gv.rows[rowIndex];
            rowElement.style.backgroundColor = "#fff";
            //rowElement.cells[1].style.backgroundColor = "#fff";
        }
        function load() {
            $("#dlClass").select2();           
        }
    </script>
</asp:Content>
