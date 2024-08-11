<%@ Page Title="Student Details" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="AdmissionDetails.aspx.cs" Inherits="DS.Forms.AdmissionDetails" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">    
    <style>
        .litleMargin{
            margin-left: 5px;
        }
        .tgPanel{
            width:100%;
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
        <div class="tgInput" style="width: 680px; margin: 20px auto;">
            <table>
                <tr>
                    <td>Class</td>
                    <td>
                        <asp:DropDownList ID="dlClass" Style="width: 120px;" runat="server" ClientIDMode="Static" AutoPostBack="false">
                            <asp:ListItem>All</asp:ListItem>
                        </asp:DropDownList>
                    </td>
                    <td>Section</td>
                    <td>
                        <asp:DropDownList ID="dlSection" Style="width: 120px;" runat="server" ClientIDMode="Static" AutoPostBack="false">
                            <asp:ListItem>All</asp:ListItem>
                        </asp:DropDownList>
                    </td>
                    <td>Shift</td>
                    <td>
                        <asp:DropDownList ID="dlShift" Style="width: 120px;" runat="server" ClientIDMode="Static" AutoPostBack="false">
                            <asp:ListItem>All</asp:ListItem>
                            <asp:ListItem>Morning</asp:ListItem>
                            <asp:ListItem>Day</asp:ListItem>
                        </asp:DropDownList>
                    </td>
                    <td>
                        <asp:Button ID="btnSearch" Text="Search" CssClass="btn btn-primary litleMargin" ClientIDMode="Static" runat="server" OnClick="btnSearch_Click" />
                    </td>
                </tr>
            </table>
        </div>
    </div> 
    <div class="tgPanel">
        <div class="widget">
            <div class="head">
                <img src="/Images/action/refresh.png" class="refresh" onclick="$('#btnSearch').click();" />
                <div class="dataTables_filter" style="float: right;">
                    <label>
                        Search:
                        <input type="text" class="search" placeholder="type here name/roll/mobile" />
                    </label>
                </div>
            </div>
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
            $(document).on("keyup", '.search', function () {
                searchTable($(this).val(), 'tblStudentInfo', '');
            });
        });
        function editStudent(studentId) {
            goURL('/Forms/Admission.aspx?StudentId=' + studentId + "&Edit=True");
        }
        function viewStudent(studentId) {
            goURL('/Forms/StudentView.aspx?StudentId=' + studentId);
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
    </script>
</asp:Content>
