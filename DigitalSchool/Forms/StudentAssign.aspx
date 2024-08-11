<%@ Page Title="Student Assign" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="StudentAssign.aspx.cs" Inherits="DS.Forms.StudentAssign" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <style>
        .tgPanel
        {
            width:500px;
        }
        .controlLength
        {
            width:200px;
            margin-left:5px;
            margin-right:5px;
        }
        .litleMargin{
            margin-left: 5px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:HiddenField ID="lblStudentId" ClientIDMode="Static" Value="" runat="server" />
    <asp:UpdatePanel ID="uplMessage" runat="server">
        <ContentTemplate>
            <p class="message" id="lblMessage" clientidmode="Static" runat="server"></p>
        </ContentTemplate>
    </asp:UpdatePanel>
    <div class="container">
        <div class="row">
            <div class="col-md-4"></div>
            <div class="col-md-4">
                <asp:UpdatePanel ID="UpdatePanel3" runat="server" UpdateMode="Conditional">
                    <ContentTemplate>
                        <div class="tgPanel">
                            <div class="tgPanelHead">Promotion panel for failed student</div>
                            <table class="tbl-controlPanel">
                                <tr>
                                    <td>Name
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtStName" runat="server" CssClass="input controlLength" ClientIDMode="Static" ReadOnly="true"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td>Roll
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtStRoll" runat="server" CssClass="input controlLength" ClientIDMode="Static"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td></td>
                                    <td>
                                        <asp:Button ID="btnSsign" runat="server" Text="Assign" CssClass="btn btn-primary litleMargin"
                                            OnClientClick="return validateInputs();" OnClick="btnSsign_Click" />
                                        <input type="button" value="Reset" class="btn btn-default litleMargin" onclick="clearIt();" />
                                    </td>
                                </tr>
                            </table>
                        </div>
                    </ContentTemplate>
                    <Triggers>
                        <asp:AsyncPostBackTrigger ControlID="btnSearch" EventName="Click" />
                    </Triggers>
                </asp:UpdatePanel>
                <br />
            </div>
            <div class="col-md-4"></div>
        </div>
    </div>
    <%---------------------------Current Student List---------------------%>
    <div class="tgPanel" style="width: 100%">
        <div class="widget">
            <div class="head">
                <img src="/Images/action/refresh.png" class="refresh" onclick="$('#btnSearch').click();" />
                <div class="head_title">
                    <asp:UpdatePanel ID="UpdatePanel2" runat="server" UpdateMode="Conditional">
                        <ContentTemplate>
                            <table class="tbl-controlPanel">
                                <tr>
                                    <td>Batch</td>
                                    <td>
                                        <asp:DropDownList ID="dlOldBatchs" runat="server" CssClass="input controlLength" ClientIDMode="Static" AutoPostBack="false">
                                            <asp:ListItem Selected="True">---Select---</asp:ListItem>
                                        </asp:DropDownList>
                                    </td>
                                    <td>Section</td>
                                    <td>
                                        <asp:DropDownList ID="dlSection" runat="server" CssClass="input controlLength">
                                        </asp:DropDownList>
                                    </td>
                                    <td>
                                        <asp:Button ID="btnSearch" CssClass="btn btn-primary litleMargin"
                                            Text="Search" ClientIDMode="Static" runat="server" OnClick="btnSearch_Click" />
                                    </td>
                                </tr>
                            </table>
                        </ContentTemplate>
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="btnSearch" EventName="Click" />
                        </Triggers>
                    </asp:UpdatePanel>
                </div>
                <div class="dataTables_filter" style="float: right;">
                    <label>
                        Search:
                    <input type="text" class="search" placeholder="type here..." />
                    </label>
                </div>
            </div>
            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                <ContentTemplate>
                    <div id="divStudentDetails" class="datatables_wrapper" runat="server" style="width: 100%; height: auto"></div>
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ScriptContent" runat="server">
    <script type="text/javascript">
        function validateInputs() {
            if (validateText('txtStName', 1, 20, 'Select student') == false) return false;
            else if (validateText('txtStRoll', 1, 20, 'Enter roll') == false) return false;
            return true;
        }
        $(document).ready(function () {
            $(document).on("keyup", '.search', function () {
                searchTable($(this).val(), 'tblStudentInfo', '');
            });
        });
        function studentAssign(studentId) {
            $('#lblStudentId').val(studentId);
            $('#imgProfile').val();
            var strStname = $('#r_' + studentId + ' td:nth-child(3)').html();
            var strStRoll = $('#r_' + studentId + ' td:nth-child(4)').html();
            var strClass = $('#r_' + studentId + ' td:nth-child(1)').html();
            var strSection = $('#r_' + studentId + ' td:nth-child(2)').html();
            $('#txtStName').val(strStname);
            $('#txtStRoll').val(strStRoll);
            $('#dlClassName').val(strClass);
            $('#dlSectionName').val(strSection);
        }
        function clearIt() {
            $('#txtStName').val('');
            $('#txtStRoll').val('');
            $('dlClassName').val('');
            $('dlSectionName').val('');
        }
    </script>
</asp:Content>
