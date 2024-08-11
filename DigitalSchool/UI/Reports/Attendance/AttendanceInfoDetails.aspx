<%@ Page Title="" Language="C#" MasterPageFile="~/main.Master"  AutoEventWireup="true" CodeBehind="AttendanceInfoDetails.aspx.cs" Inherits="DS.UI.Reports.Attendance.AttendanceInfoDetails" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
     <style>
         .display thead th {
             background:#23282C;
         }
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
        .HeaderTitle{
            padding: 5px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="tgPanel">
        <div class="tgInput" style="width: 477px;">
            <table>
                <tr>
                    <td>
                        <asp:Button runat="server" ID="btnAllClassAttendance" Text="All Class Attendance List"
                            CssClass="btn btn-success litleMargin" OnClick="btnAllClassAttendance_Click" />
                    </td>
                    <td>
                        <asp:Button runat="server" ID="btnTodayAllClassAttendance" Text="All Class Attendance Summary"
                            CssClass="btn btn-success litleMargin" OnClick="btnTodayAllClassAttendance_Click" />
                    </td>
                </tr>
            </table>
        </div>
    </div>
    <div class="tgPanel">
        <div class="tgPanelHead">Searching Result</div>
        <asp:UpdatePanel runat="server" UpdateMode="Conditional">
            <Triggers>
                <asp:AsyncPostBackTrigger ControlID="btnAllClassAttendance" />
            </Triggers>
            <ContentTemplate>
                <div id="divAttendanceInfoDetails" runat="server"></div>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ScriptContent" runat="server">

</asp:Content>
