<%@ Page Title="Set Optional Subject" Language="C#" MasterPageFile="~/main.Master" AutoEventWireup="true" CodeBehind="SetOptionalSubject.aspx.cs" Inherits="DS.UI.Academic.Students.SetOptionalSubject" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style>
        .controlLength{
            width:200px;
            margin: 5px;
        }
        .tgPanel
        {
            width: 100%;
        } 
        #tblSetRollOptionalSubject
        {
            width:100%;            
        }
        #tblSetRollOptionalSubject th,  
        #tblSetRollOptionalSubject td, 
        #tblSetRollOptionalSubject td input,
        #tblSetRollOptionalSubject td select
        {
            padding: 5px 5px;
            margin-left: 10px;
            text-align: center;
        }
        .litleMargin{
            margin-right: 5px;
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
                    <a runat="server" href="~/Dashboard.aspx">
                        <i class="fa fa-dashboard"></i>
                        Dashboard
                    </a>
                </li> 
                <li><a id="A1" runat="server" href="~/UI/Academic/AcademicHome.aspx">Academic Module</a></li>
                <li><a id="A2" runat="server" href="~/UI/Academic/Examination/ExamHome.aspx">Examination Module</a></li>
                <li><a id="A3" runat="server" href="~/UI/Academic/Examination/ManagedSubject/SubjectManageHome.aspx">Subject Management</a></li>
                <li class="active">Set Optional Subject</li>                              
            </ul>
            <!--breadcrumbs end -->
        </div>
    </div>
    <div class="">
        <div class="row">
            <div class="col-md-12">
                <div class="tgPanel">
                    <div class="tgPanelHead">Set  Optional Subject</div>
                    <asp:UpdatePanel ID="UpdatePanel2" runat="server" UpdateMode="Conditional">
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="dlBatch" />
                            <asp:AsyncPostBackTrigger ControlID="btnProcess" />
                        </Triggers>
                        <ContentTemplate>
                            <table class="tbl-controlPanel">
                                <tr>
                                    <td>Shift</td>
                                    <td>
                                        <asp:DropDownList ID="dlShift" runat="server" ClientIDMode="Static" CssClass="input controlLength">                                            
                                        </asp:DropDownList></td>
                                    <td>Batch</td>
                                    <td>
                                        <asp:DropDownList ID="dlBatch" runat="server" AutoPostBack="true" ClientIDMode="Static"
                                             OnSelectedIndexChanged="dlBatch_SelectedIndexChanged" CssClass="input controlLength">
                                        </asp:DropDownList>
                                    </td>
                                    <td>Group</td>
                                    <td>
                                        <asp:DropDownList ID="dlGroup" runat="server" ClientIDMode="Static" CssClass="input controlLength"></asp:DropDownList></td>

                                    <td>
                                        <asp:Button ID="btnProcess" Text="Process" ClientIDMode="Static" runat="server"
                                           OnClientClick="return validateInputs();"  CssClass="btn btn-primary" OnClick="btnProcess_Click" />
                                        <span style="position: absolute">
                                            <asp:UpdateProgress ID="UpdateProgress2" runat="server" ClientIDMode="Static" AssociatedUpdatePanelID="UpdatePanel2">
                                                <ProgressTemplate>
                                                    <img class="LoadingImg" src="../../../AssetsNew/images/input-spinner.gif" />
                                                </ProgressTemplate>
                                            </asp:UpdateProgress>                                           
                                        </span>
                                    </td>
                                </tr>
                            </table>                            
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </div>
            </div>
        </div>       
        <div class="row">
            <div class="col-md-12">
                <div class="tgPanel">
                    <div class="widget">
                    <div class="head">
                        <img src="/Images/action/refresh.png" class="refresh" onclick="$('#btnProcess').click();" />
                        <div class="dataTables_filter_New" style="float: right;">
                            <label>
                                Search:
                    <input type="text" class="Search_New" placeholder="type here" />
                            </label>
                        </div>
                    </div>
                    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                        <Triggers>
                        <asp:AsyncPostBackTrigger ControlID="btnProcess" />
                            </Triggers>
                        <ContentTemplate>
                            <div runat="server" id="divStduentInfo" class="datatables_wrapper" style="width: 100%; height: auto"></div>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                        </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ScriptContent" runat="server">
    <script type="text/javascript">
        $(document).ready(function () {
            $(document).on("keyup", '.Search_New', function () {
                searchTable($(this).val(), 'tblStudentInfo', '');
            });
        });
        function validateInputs() {
            if (validateCombo('dlShift', '0', 'Select Shift Name') == false) return false;
            if (validateCombo('dlBatch', '0', 'Select Batch Name') == false) return false;
            //if (validateCombo('dlGroup', '0', 'Select Group Name') == false) return false;
            return true;
        }
        function setOS(celldata) // os= Optional Subject
        {            
            var getId = celldata.id;
            var splitedData = getId.split("_");
            var getVal = celldata.value;            
            $('#opt_' + getId).val(getVal);           
            jx.load('ForUpdate.aspx?tbldata=' + splitedData + '&val=' + getVal + '&do=attUpdate', function (data) {
            });
        }     
    </script>
</asp:Content>