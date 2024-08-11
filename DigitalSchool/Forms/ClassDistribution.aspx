<%@ Page Title="Class Distribution" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ClassDistribution.aspx.cs" Inherits="DS.Forms.ClassDistribution" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div style="width: 100%; height: 230px">
        <div class="clasDistribution" style="float: left; margin-left: 100px;">
            <div style="height: 200px">
                <asp:UpdatePanel ID="uplMessage" runat="server" UpdateMode="Conditional">
                    <Triggers>
                        <asp:AsyncPostBackTrigger ControlID="dlDepartment" />
                    </Triggers>
                    <ContentTemplate>
                        <fieldset style="height: 209px; width: 49%; float: left; border: 1px solid #D2D2D2;">
                            <legend>Day Wise</legend>
                            <table style="margin-left: 10px">
                                <tr>
                                    <td>Routine Id</td>
                                    <td>
                                        <asp:DropDownList runat="server" ID="dlRoutineIdDay" ClientIDMode="Static" CssClass="dropDownListRoutine"></asp:DropDownList></td>
                                </tr>
                                <tr>
                                    <td>Day</td>
                                    <td>
                                        <asp:DropDownList runat="server" ID="dlDay" ClientIDMode="Static" CssClass="dropDownListRoutine">
                                            <asp:ListItem>Saturday</asp:ListItem>
                                            <asp:ListItem>Sunday</asp:ListItem>
                                            <asp:ListItem>Monday</asp:ListItem>
                                            <asp:ListItem>Tuesday</asp:ListItem>
                                            <asp:ListItem>Wednesday</asp:ListItem>
                                            <asp:ListItem>Thursday</asp:ListItem>
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                                <tr>
                                    <td>Department</td>
                                    <td>
                                        <asp:DropDownList runat="server" ID="dlDepartment" ClientIDMode="Static" CssClass="dropDownListRoutine" AutoPostBack="true" OnSelectedIndexChanged="dlDepartment_SelectedIndexChanged">
                                            <asp:ListItem>--Select--</asp:ListItem>
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                                <tr>
                                    <td>Teacher</td>
                                    <td>
                                        <asp:DropDownList runat="server" ID="dlTeacher" ClientIDMode="Static" CssClass="dropDownListRoutine"></asp:DropDownList>
                                    </td>
                                </tr>
                                <tr>
                                    <td>Index Number</td>
                                    <td>
                                        <asp:TextBox runat="server" ID="txtIndexNo" ClientIDMode="Static" CssClass="dropDownListRoutine"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td></td>
                                    <td>
                                        <asp:Button runat="server" Style="display: none" Text="Search" ID="btnSearchDayWise" ClientIDMode="Static" CssClass="greenBtn" Width="153px" OnClick="btnSearchDayWise_Click" />
                                        <img style="width: 60px; height: 26px; cursor: pointer;" src="/images/action/search.gif" onclick="$('#btnSearchDayWise').click();" />
                                    </td>
                                </tr>
                            </table>
                        </fieldset>
                    </ContentTemplate>
                </asp:UpdatePanel>
                <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
                    <Triggers>
                        <asp:AsyncPostBackTrigger ControlID="dlDepartmentWeek" />
                    </Triggers>
                    <ContentTemplate>
                        <fieldset style="height: 209px; width: 47%; float: right; border: 1px solid #D2D2D2;">
                            <legend>Weekly</legend>
                            <table style="margin-left: 10px">
                                <tr>
                                    <td>Routine Id</td>
                                    <td>
                                        <asp:DropDownList runat="server" ID="dlRoutineIdWeek" ClientIDMode="Static" CssClass="dropDownListRoutine"></asp:DropDownList></td>
                                </tr>
                                <tr>
                                    <td>Department</td>
                                    <td>
                                        <asp:DropDownList runat="server" ID="dlDepartmentWeek" ClientIDMode="Static" CssClass="dropDownListRoutine" AutoPostBack="true" OnSelectedIndexChanged="dlDepartmentWeek_SelectedIndexChanged">
                                            <asp:ListItem>--Select--</asp:ListItem>
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                                <tr>
                                    <td>Teacher</td>
                                    <td>
                                        <asp:DropDownList runat="server" ID="dlTeacherWeek" ClientIDMode="Static" CssClass="dropDownListRoutine"></asp:DropDownList>
                                    </td>
                                </tr>
                                <tr>
                                    <td>Index Number</td>
                                    <td>
                                        <asp:TextBox runat="server" ID="txtIndexNoWeek" ClientIDMode="Static" CssClass="dropDownListRoutine"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td></td>
                                    <td>
                                        <asp:Button runat="server" Style="display: none" Text="Search" ID="btnSearchWeek" Width="153px"
                                            ClientIDMode="Static" CssClass="greenBtn" OnClick="btnSearchWeek_Click" />
                                        <img style="width: 60px; height: 26px; cursor: pointer;" src="/images/action/search.gif"
                                            onclick="$('#btnSearchWeek').click();" />
                                    </td>
                                </tr>
                            </table>
                        </fieldset>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>
        </div>
        <div style="border: 1px solid #D2D2D2; float: right; height: 122px; margin-top: 18px; padding: 10px; width: 248px; margin-right: 65px;">
            <table>
                <tr>
                    <td>Id
                    </td>
                    <td>:</td>
                    <td>
                        <asp:DropDownList runat="server" ID="dlRoutineId" ClientIDMode="Static" Width="220px" CssClass="input"
                            OnSelectedIndexChanged="dlClass_SelectedIndexChanged" AutoPostBack="true">
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td></td>
                    <td></td>
                    <td></td>
                </tr>
            </table>
        </div>
        <div style="border: 1px solid #D2D2D2; float: right; height: 26px; margin-right: 64px; margin-top: 7px; padding: 7px 10px 17px; width: 249px;">
            <asp:UpdatePanel runat="server" ID="printP">
                <ContentTemplate>
                    <asp:Button runat="server" Text="Print Preview" ID="btnPrintPreview" Width="100px" CssClass="greenBtn" OnClick="btnPrintPreview_Click" /><br />
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
    </div>
    <div style="width: 100%; height: auto; margin-left: 110px;">
        <div style="height: auto; width: 100%">
            <asp:UpdatePanel ID="UpdatePanel2" runat="server" UpdateMode="Conditional">
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="btnSearchDayWise" />
                    <asp:AsyncPostBackTrigger ControlID="btnSearchWeek" />
                    <asp:AsyncPostBackTrigger ControlID="dlRoutineId" />
                </Triggers>
                <ContentTemplate>
                    <div id="divClassInfo" runat="server" class="display" style="font-family: monospace;"></div>
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ScriptContent" runat="server">

</asp:Content>

