<%@ Page Title="Student Attendance List" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="student_attendance_list.aspx.cs" Inherits="DS.Forms.machine_attendance.student_attendance_list" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:UpdatePanel ID="uplMessage" runat="server">
        <ContentTemplate>
            <p class="message" id="lblMessage" clientidmode="Static" runat="server"></p>
        </ContentTemplate>
    </asp:UpdatePanel>
    <div class="widget">
        <h3 style="text-align: center;">Students Attendance List</h3>
        <div class="head">
            <input id="btnBack" type="button" value="Back" class="back_Btn" style="margin-left: 4px" onclick="window.location.href = 'manually_certain_attendance_count.aspx'" />
            <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="btnFind" />
                    <asp:AsyncPostBackTrigger ControlID="btnRefresh" />
                </Triggers>
                <ContentTemplate>

                    <div style="float: right;">
                        <table class="display">
                            <tr>
                                <td>Shift :
                                </td>
                                <td>
                                    <asp:DropDownList runat="server" ID="ddlShift" ClientIDMode="Static" CssClass="dropDownListRoutine"></asp:DropDownList>
                                </td>


                                <td>Batch
                                </td>
                                <td>
                                    <asp:DropDownList runat="server" ID="ddlBatch" ClientIDMode="Static" CssClass="dropDownListRoutine"></asp:DropDownList>
                                </td>

                                <td>Section
                                </td>
                                <td>
                                    <asp:DropDownList runat="server" ID="ddlSectionName" ClientIDMode="Static" CssClass="dropDownListRoutine"></asp:DropDownList>
                                </td>

                                <td>
                                    <img style="width: 60px; height: 26px; cursor: pointer;" src="/images/action/Find.png" onclick="$('#btnFind').click();" />
                                    <asp:Button Style="display: none;" runat="server" ID="btnFind" Text="Find" ClientIDMode="Static" CssClass="search-box" OnClick="btnFind_Click" />
                                </td>
                                <td>
                                    <asp:Button runat="server" ID="btnRefresh" Text="Refresh" ClientIDMode="Static" CssClass="greenBtn tdWidth" OnClick="btnRefresh_Click" />
                                </td>
                            </tr>

                        </table>
                    </div>
                </ContentTemplate>
            </asp:UpdatePanel>

        </div>

        <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
            <Triggers>
                <asp:AsyncPostBackTrigger ControlID="btnFind" />
                <asp:AsyncPostBackTrigger ControlID="btnRefresh" />
            </Triggers>
            <ContentTemplate>
                <asp:GridView ID="gvStudentAttendanceList" CssClass="display" runat="server" Width="100%" DataKeyNames="StudentId,AttDate" AutoGenerateColumns="false" AllowPaging="True" HeaderStyle-Font-Bold="true" HeaderStyle-HorizontalAlign="Center" HeaderStyle-Font-Size="13px" Style="margin-top: 1px" CellPadding="4" ForeColor="#333333" GridLines="Both" OnRowCommand="gvStudentAttendanceList_RowCommand" OnRowDeleting="gvStudentAttendanceList_RowDeleting">
                    <PagerStyle CssClass="gridview" />
                    <Columns>
                        <asp:BoundField DataField="StudentId" HeaderText="StudentId" Visible="false" ItemStyle-HorizontalAlign="Center" />
                        <asp:BoundField DataField="AttDate" HeaderText="AttDate" Visible="false" />
                        <asp:BoundField DataField="AdmissionNo" HeaderText="Card No" ItemStyle-HorizontalAlign="Center" />
                        <asp:BoundField DataField="FullName" HeaderText="Name" />
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
                                    CommandName="delete" CommandArgument='<%# Eval("StudentId")+","+Eval("AttDate") %>' OnClientClick="return IsDelete();" />
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


