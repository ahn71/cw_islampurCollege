<%@ Page Title="Add Exam Type" Language="C#" MasterPageFile="~/main.Master" AutoEventWireup="true" CodeBehind="AddExam.aspx.cs" Inherits="DS.UI.Academics.Examination.AddExam" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style>
        .tgPanel {
            width: 100%;
        }
        .tbl-controlPanel td:first-child{
            text-align: right;
            padding-right: 5px;
        }
        .dataTables_length, .dataTables_filter{
            display: none;
            padding: 15px;
        }
        #tblClassList_info {
             display: none;
            padding: 15px;
        }
           input[type="checkbox"]{
            margin: 5px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:UpdatePanel ID="uplMessage" runat="server">
        <ContentTemplate>
            <p class="message" id="lblMessage" clientidmode="Static" runat="server"></p>
        </ContentTemplate>
    </asp:UpdatePanel>
    <asp:HiddenField ID="lblExId" ClientIDMode="Static" runat="server" />
    <div class="row">
        <div class="col-md-12">
            <!--breadcrumbs start -->
            <ul class="breadcrumb">
                <li>
                   <%-- <a runat="server" href="~/Dashboard.aspx">--%>
                    <a runat="server" id="aDashboard">
                        <i class="fa fa-dashboard"></i>
                        Dashboard
                    </a>
                </li> 
                <li>
                    <%--<a runat="server" href="~/UI/Academic/AcademicHome.aspx">Academic Module</a>--%>
                    <a runat="server" id="aAcademicHome" >Academic Module</a>
                </li>
                <%--<li><a runat="server" href="~/UI/Academic/Examination/ExamHome.aspx">Examination Module</a></li>--%>
                <li><a runat="server" id="aExamHome">Examination Module</a></li>
                <li class="active">Add Exam Type</li>
            </ul>
            <!--breadcrumbs end -->
        </div>
    </div>
    <div class="">
        <div class="row">
            <div class="col-md-2"></div>
            <div class="col-md-4">
                <h4 class="text-right"  style="float: left;">Exam Type List</h4>
                <div class="dataTables_filter_New" style="float: right;margin-right:0px;">
                    <label>
                        Search:
                        <input type="text" class="Search_New" placeholder="type here" />
                    </label>
                </div>
            </div>
            <div class="col-md-6"></div>
        </div>
        <div class="row">
            <div class="col-md-2"></div>
            <div class="col-md-4">
                <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
                    <Triggers>
                        <asp:AsyncPostBackTrigger ControlID="btnSave" />
                    </Triggers>
                    <ContentTemplate>
                        <div class="tgPanel">
                        <div id="divExamList" class="datatables_wrapper" runat="server"
                            style="width: 100%; height: auto; max-height: 350px; overflow: auto; overflow-x: hidden;">
                        </div>
                            </div>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>
            <div class="col-md-6">
                <div class="tgPanel">
                    <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                        <ContentTemplate>
                            <div class="tgPanelHead">Add Exam Type</div>
                            <table class="tbl-controlPanel">
                                <tr>
                                    <td>Exam Name
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtEx_Name" runat="server" ClientIDMode="Static" CssClass="input form-control"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td>Serial
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtSerial" runat="server" ClientIDMode="Static" CssClass="input form-control"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        Type
                                    </td>
                                    <td>
                                        <asp:RadioButtonList runat="server" ID="rblType" RepeatDirection="Horizontal">
                                            <asp:ListItem Selected="True" Value="1">Semester Exam</asp:ListItem>
                                             <asp:ListItem Value="2">Quiz Exam</asp:ListItem>
                                            <asp:ListItem Value="0">Others</asp:ListItem>
                                        </asp:RadioButtonList>
                                         <%--<asp:CheckBox ID="chksemesterexam" class="radio-inline" runat="server" Text="Semester Exam" Checked="True" RepeatLayout="Flow" CssClass="radiobuttonlist" RepeatDirection="Horizontal"/>--%>
                                    </td>
                                </tr>  
                                <tr>
                                    <td>
                                        
                                    </td>
                                    <td>
                                        <asp:CheckBox ID="chkIsActive" ClientIDMode="Static" runat="server" Text="Is Active" />
                                    </td>
                                </tr>
                               
                                <tr>
                                    <td></td>
                                    <td>
                                        <asp:Button CssClass="btn btn-primary" ID="btnSave" runat="server" Text="Save" ClientIDMode="Static"
                                            OnClientClick="return validateInputs();" OnClick="btnSave_Click" />
                                        &nbsp;<input type="button" class="btn btn-default" value="Clear" onclick="clearIt();" />
                                    </td>
                                </tr>
                            </table>                            
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ScriptContent" runat="server">
    <script type="text/javascript">
        $(document).ready(function () {
            loaddatatable();
            $(document).on("keyup", '.Search_New', function () {
                searchTable($(this).val(), 'tblClassList', '');
            });
        });
        function loaddatatable() {
            $('#tblClassList').dataTable({
                "iDisplayLength": 10,
                "lengthMenu": [10, 20, 30, 40, 50, 100],
                "order": [[ 2, "desc" ]]
            });
        }
        function validateInputs() {
            if (validateText('txtEx_Name', 1, 50, 'Enter a Exam Name') == false) return false;
            return true;
        }
        function editExam(ExamId) {
            $('#lblExId').val(ExamId);
            var strExam = $('#exname' + ExamId).html();
            $('#txtEx_Name').val(strExam);
            var strordering = $('#ordering' + ExamId).html();
            $('#txtSerial').val(strordering);

            var status = $('#semesterexam' + ExamId).html();
            if (status == "Semester") {
                $('#<%=rblType.ClientID %>').find("input[value='1']").prop("checked", true);
            }
            else if (status == "Quiz") {
                $('#<%=rblType.ClientID %>').find("input[value='2']").prop("checked", true);}
            else $('#<%=rblType.ClientID %>').find("input[value='0']").prop("checked", true);
            var IsActive = $('#IsActive' + ExamId).html();
            if (IsActive == "Yes")
            {
                $('#chkIsActive').removeProp('checked');
                $('#chkIsActive').click(); 
            }
            else $('#chkIsActive').removeProp('checked');
            $("#btnSave").val('Update');
        }
        function clearIt() {
            $('input[type=text]').val('');
            $('#lblExId').val('');
           
            $("#btnSave").val('Save');
            setFocus('txtEx_Name');
        }
        function updateSuccess() {
            loaddatatable();
            showMessage('Updated successfully', 'success');
            clearIt();
        }
        function SavedSuccess() {
            showMessage('Saved successfully', 'success');
            clearIt();
            loaddatatable();
        }
    </script>
</asp:Content>
