<%@ Page Title="Teacher Partial Info" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="TeacherPartialInfo.aspx.cs" Inherits="DS.Forms.TeacherPartialInfo" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <style>
        .tgPanel
        {
            width:100%;
        }
        input[type="checkbox"]
        {
            margin-left:10px;
            margin-right:5px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">    
    <asp:UpdatePanel ID="uplMessage" runat="server">
        <ContentTemplate>
            <p class="message" id="lblMessage" clientidmode="Static" runat="server"></p>
        </ContentTemplate>
    </asp:UpdatePanel>
    <div class="tgPanel">
        <div class="tgPanelHead">
            <asp:Label ID="lblTitle" runat="server" Text="" ClientIDMode="Static"></asp:Label>
            Information
        </div>
        <div class="widget">
            <div class="head">
                <img src="/Images/action/refresh.png" class="refresh" onclick="$('#btnSearch').click();" />
                <div class="head_title">
                    <asp:UpdatePanel ID="UpdatePanel2" runat="server" UpdateMode="Conditional">
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="chkIsTeacher" />
                        </Triggers>
                        <ContentTemplate>
                            <div>
                                <asp:CheckBox runat="server" ID="chkIsTeacher" Text="Is Teacher" ClientIDMode="Static" AutoPostBack="true" Checked="true" 
                                    OnCheckedChanged="chkIsTeacher_CheckedChanged" />                     
                            </div>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </div>
                <div class="dataTables_filter" style="float: right;">
                    <label>
                        Search:
                    <input type="text" class="search" placeholder="type here..." />
                    </label>
                </div>
            </div>
            <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="chkIsTeacher" />
                </Triggers>
                <ContentTemplate>
                    <div id="divTeacherInfo" class="datatables_wrapper" runat="server" style="width: 100%; height: auto"></div>
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
    </div>
    
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ScriptContent" runat="server">
    <script type="text/javascript">
        $(document).ready(function () {
            $(document).on("keyup", '.search', function () {
                searchTable($(this).val(), 'tblTeacherInfo', '');
            });
        });
        function editTeacher(teacherId) {
            goURL('/Forms/EmployeeInformation.aspx?TeacherId=' + teacherId + "&Edit=True");
        }
        function viewTeacher(teacherId) {
            goURL('/Forms/EmployeeView.aspx?TeacherId=' + teacherId);
        }
        function setSalary(teacherId) {
            goURL('/Forms/TeacherPartialInfo.aspx?TeacherId=' + teacherId + "&Save=True");
        }
        function existData() {
            alert("Salary Already Set");
            showMessage('Salary Already Set', 'warning');
        }
    </script>
</asp:Content>
