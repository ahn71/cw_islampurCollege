<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="emp_attendance_list.aspx.cs" Inherits="DS.Forms.machine_attendance.emp_attendance_list" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">   
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="widget">
        <h3 style="text-align: center;">Staff/Faculty Attendance List</h3>
        <div class="head">
            <input id="btnBack" type="button" value="Back" class="back_Btn" style="margin-left: 4px" 
                onclick="window.location.href = 'manually_certain_emp_attendance_count.aspx'" />
            <div style="float: right;">
                <table>
                    <tr>
                        <td>Department</td>
                        <td>
                            <asp:DropDownList ID="dlDepartment" CssClass="input" Style="width: 150px;" runat="server" ClientIDMode="Static" AutoPostBack="True">
                            </asp:DropDownList>
                        </td>
                        <td>Designation</td>
                        <td>
                            <asp:DropDownList ID="dlDesignation" CssClass="input" Style="width: 150px;" runat="server" ClientIDMode="Static" AutoPostBack="True">
                            </asp:DropDownList>
                        </td>
                        <td>
                            <img style="width: 60px; height: 26px; cursor: pointer;" src="/images/action/Find.png" onclick="$('#btnSearch').click();" />
                            <asp:Button Style="display: none;" runat="server" ID="btnFind" Text="Find" ClientIDMode="Static" CssClass="search-box" />
                        </td>
                        <td>
                            <asp:Button runat="server" ID="btnRefresh" Text="Refresh" ClientIDMode="Static" CssClass="greenBtn tdWidth" />
                        </td>
                    </tr>
                </table>
            </div>
        </div>
        <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
            <Triggers>
                <asp:AsyncPostBackTrigger ControlID="btnFind" />
            </Triggers>
            <ContentTemplate>
                <asp:GridView ID="gvEmpAttendanceList" CssClass="display" runat="server" Width="100%" DataKeyNames="EId,AttDate" AutoGenerateColumns="false" AllowPaging="True" HeaderStyle-Font-Bold="true" HeaderStyle-HorizontalAlign="Center" HeaderStyle-Font-Size="13px" Style="margin-top: 1px" CellPadding="4" ForeColor="#333333" GridLines="Both" OnRowCommand="gvEmpAttendanceList_RowCommand">
                    <PagerStyle CssClass="gridview" />
                    <Columns>
                        <asp:BoundField DataField="EId" HeaderText="EmpId" Visible="false" ItemStyle-HorizontalAlign="Center" />
                        <asp:BoundField DataField="AttDate" HeaderText="AttDate" Visible="false" />
                        <asp:BoundField DataField="ECardNo" HeaderText="Card No" ItemStyle-HorizontalAlign="Center" />
                        <asp:BoundField DataField="EName" HeaderText="Name" />
                        <asp:BoundField DataField="AttMonth" HeaderText="Date" ItemStyle-HorizontalAlign="Center" />
                        <asp:BoundField DataField="AttStatus" HeaderText="Status" ItemStyle-HorizontalAlign="Center" />
                        <asp:BoundField DataField="AttManual" HeaderText="Count" ItemStyle-HorizontalAlign="Center" />
                        <asp:BoundField DataField="LoginTime" HeaderText="In Time" ItemStyle-HorizontalAlign="Center" />
                        <asp:BoundField DataField="LogOutTime" HeaderText="Out Time" ItemStyle-HorizontalAlign="Center" />
                        <asp:ButtonField ButtonType="Button" ControlStyle-CssClass="greenBtn tdWidth" CommandName="alter" Text="Alter" HeaderStyle-Height="28px" ItemStyle-Width="75px" />
                        <%--              <asp:ButtonField ButtonType="Button" ItemStyle-Width="30px" ControlStyle-CssClass="greenBtn tdWidth " CommandName="delete"  Text="Delete"  HeaderStyle-Height="28px"  />--%>
                        <%--  <asp:CommandField ShowDeleteButton="True" ButtonType="Button" />--%>
                        <asp:TemplateField AccessibleHeaderText="Choose" ItemStyle-Width="80px" ItemStyle-HorizontalAlign="center">
                            <ItemTemplate>
                                <asp:Button ID="btnDelete" runat="server" ControlStyle-CssClass="cancel_Btn" Width="70" Text="Delete" 
                                    CommandName="delete" CommandArgument='<%# Eval("EId")+","+Eval("AttDate") %>' OnClientClick="return IsDelete();" />
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ScriptContent" runat="server">
    <script type="text/javascript">
        function IsDelete() {
            var IsDelete = IsDelete = confirm('Are you sure, you want to delete the record?');
            if (!IsDelete) return false;
            else return true;
        }
    </script>
</asp:Content>

