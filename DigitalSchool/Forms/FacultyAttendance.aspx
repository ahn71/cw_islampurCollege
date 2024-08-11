<%@ Page Title="Daily Attendance Count of Faculty" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="FacultyAttendance.aspx.cs" Inherits="DS.Forms.FacultyAttendance" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">    
    <style>
        .controlLength{
            width:150px;
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
            margin-left: 5px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:UpdatePanel ID="UpdatePanel3" runat="server">
        <ContentTemplate>
            <p class="message" id="lblMessage" clientidmode="Static" runat="server"></p>
        </ContentTemplate>
    </asp:UpdatePanel>
    <div class="container">
        <div class="row">
            <div class="tgPanel">
                <div class="tgPanelHead">Daily Attendance Count of Faculty</div>
                <table class="tbl-controlPanel">
                    <tr>
                        <td>Departments</td>
                        <td>
                            <asp:DropDownList ID="dlDepartments" runat="server" CssClass="input controlLength"></asp:DropDownList>
                        </td>
                        <td>Month</td>
                        <td>
                            <asp:DropDownList ID="dlMonths" runat="server" CssClass="input controlLength">
                            </asp:DropDownList>
                        </td>
                        <td>
                            <asp:Button ID="btnProcess" Text="Process" ClientIDMode="Static" runat="server" CssClass="btn btn-primary litleMargin"
                                OnClick="btnProcess_Click" />
                        </td>
                        <td>
                            <asp:Button ID="btnPreview" Text="Preview" ClientIDMode="Static" runat="server" CssClass="btn btn-success litleMargin"
                                OnClick="btnPreview_Click" />
                        </td>
                    </tr>
                </table>
                <asp:UpdatePanel ID="UpdatePanel2" runat="server" UpdateMode="Conditional">
                    <ContentTemplate>
                        <asp:UpdateProgress ID="UpdateProgress1" runat="server">
                            <ProgressTemplate>
                                <br />
                                <span style="font-family: 'Times New Roman'; font-size: 20px; color: green; font-weight: bold; float: left">
                                    <p>Wait attendance sheet is processing</p>
                                </span>
                                <img style="width: 26px; height: 26px; cursor: pointer; float: left" src="/images/wait.gif" />
                            </ProgressTemplate>
                        </asp:UpdateProgress>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>
        </div>
    </div>
    <br />
    <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
        <Triggers>
            <asp:AsyncPostBackTrigger ControlID="btnProcess" />
        </Triggers>
        <ContentTemplate>
            <div runat="server" id="AttendanceSheetTitle" style="font-weight: bold; font-size: 22px;"></div>
            <br />
            <div runat="server" id="divTable" style="width: 100%; height: 0 auto; overflow-x: scroll; overflow-y: scroll" visible="false"></div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ScriptContent" runat="server">
    <script type="text/javascript">
        function saveData(celldata) {
            var dataID = celldata.id;
            var splitedData = dataID.split(":");
            var dataValue = celldata.value;
            jx.load('ForUpdate.aspx?tbldata=' + splitedData + '&val=' + dataValue + '&do=attUpdate', function (data) {
                //jx.load('ForUpdate.aspx?tbldata=' + splitedData + ', function (data) {
                //  alert(data);
            });
        }
    </script>
</asp:Content>

