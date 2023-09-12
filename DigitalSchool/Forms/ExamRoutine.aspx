<%@ Page Title="Exam Routine" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ExamRoutine.aspx.cs" Inherits="DS.Forms.ExamRoutine" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:UpdatePanel ID="uplMessage" runat="server" UpdateMode="Conditional">
        <Triggers>
            <asp:AsyncPostBackTrigger ControlID="btnSave" />
            <asp:AsyncPostBackTrigger ControlID="btnAdd" />
        </Triggers>
        <ContentTemplate>
            <p class="message" id="lblMessage" clientidmode="Static" runat="server"></p>
        </ContentTemplate>
    </asp:UpdatePanel>
    <div class="leftContentRoutine">
        <div class="divTop">
            <asp:UpdatePanel runat="server" ID="up1">
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="dlBatch" />
                    <asp:AsyncPostBackTrigger ControlID="dlExamType" />
                </Triggers>
                <ContentTemplate>
                    <table>
                        <tr>
                            <td>Batch</td>
                            <td>
                                <asp:DropDownList runat="server" ID="dlBatch" ClientIDMode="Static" CssClass="dropDownListRoutine" AutoPostBack="true" OnSelectedIndexChanged="dlBatch_SelectedIndexChanged"></asp:DropDownList></td>
                        </tr>
                        <tr>
                            <td>Shift</td>
                            <td>
                                <asp:DropDownList runat="server" ID="dlShift" ClientIDMode="Static" CssClass="dropDownListRoutine">
                                    <asp:ListItem>Morning</asp:ListItem>
                                    <asp:ListItem>Day</asp:ListItem>
                                </asp:DropDownList></td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label runat="server" ID="lblSectionOrGroup" Text="Section"></asp:Label></td>
                            <td>
                                <asp:DropDownList runat="server" ID="dlSection" ClientIDMode="Static" CssClass="dropDownListRoutine"></asp:DropDownList></td>
                        </tr>
                        <tr>
                            <td>Session Type</td>
                            <td>
                                <asp:DropDownList runat="server" ID="dlExamType" ClientIDMode="Static" CssClass="dropDownListRoutine" AutoPostBack="true">
                                    <asp:ListItem>--Select--</asp:ListItem>
                                </asp:DropDownList></td>
                        </tr>
                        <tr>
                            <td>Routine For :</td>
                            <td>
                                <asp:Label runat="server" ID="lblRoutineId" Text="" Style="color: #FF0000; font-size: 12px;"></asp:Label></td>
                        </tr>
                    </table>
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
        <div class="divTop">
            <asp:UpdatePanel runat="server" ID="upStTime" UpdateMode="Conditional">
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="btnAdd" />
                    <asp:AsyncPostBackTrigger ControlID="dlDay" />
                    <asp:AsyncPostBackTrigger ControlID="btnSave" />
                </Triggers>
                <ContentTemplate>
                    <table>
                        <tr>
                            <td>Period</td>
                            <td>
                                <asp:DropDownList runat="server" ID="dlPeriod" ClientIDMode="Static" CssClass="dropDownListRoutine">
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td>Date</td>
                            <td>
                                <asp:TextBox runat="server" ID="txtDate" ClientIDMode="Static" CssClass="dropDownListRoutine"></asp:TextBox>
                                <asp:CalendarExtender runat="server" ID="clndr" TargetControlID="txtDate"></asp:CalendarExtender>
                            </td>
                        </tr>
                        <tr>
                            <td>Day</td>
                            <td>
                                <asp:DropDownList runat="server" ID="dlDay" ClientIDMode="Static" CssClass="dropDownListRoutine" AutoPostBack="true">
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
                            <td></td>
                            <td></td>
                        </tr>
                    </table>
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
        <div class="divTop" style="background-color: FloralWhite">
            <asp:UpdatePanel runat="server" ID="UpdatePanel1" UpdateMode="Conditional">
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="dlBatch" />
                    <asp:AsyncPostBackTrigger ControlID="btnSave" />
                </Triggers>
                <ContentTemplate>
                    <table>
                        <tr>
                            <td>Subject</td>
                            <td>
                                <asp:DropDownList runat="server" ID="dlSubject" ClientIDMode="Static" CssClass="dropDownListRoutine"></asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td></td>
                            <td>
                                <asp:Button runat="server" ID="btnAdd" ClientIDMode="Static" Text="Add" CssClass="greenBtn" OnClick="btnAdd_Click" /></td>
                        </tr>
                    </table>
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
        <div style="padding: 10px;">
            <asp:UpdatePanel runat="server" ID="upCountDay" UpdateMode="Conditional">
                <Triggers>
                </Triggers>
                <ContentTemplate>
                    <p>
                        <asp:Label runat="server" ID="lblTeacherName" ClientIDMode="Static" Text="" BackColor="YellowGreen" CssClass="lblFontStyle" Font-Size="18px"></asp:Label><br />
                        <br />
                    </p>
                    <asp:Label runat="server" ID="lblClassDay" ClientIDMode="Static" Text="" CssClass="lblFontStyle" Font-Size="18px"></asp:Label><br />
                    <asp:Label runat="server" ID="lblClassWeek" ClientIDMode="Static" Text="" CssClass="lblFontStyle" Font-Size="18px"></asp:Label>
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
    </div>
    <div class="rightContentRoutine">
        <div style="height: 490px; overflow: auto;">
            <asp:UpdatePanel runat="server" ID="upbatch" UpdateMode="Conditional">
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="dlBatch" />
                    <asp:AsyncPostBackTrigger ControlID="btnAdd" />
                    <asp:AsyncPostBackTrigger ControlID="btnSave" />
                    <asp:AsyncPostBackTrigger ControlID="dlExamType" />
                </Triggers>
                <ContentTemplate>
                    <div class="headerRoutine">
                        EXAM ROUTINE FOR
                        <asp:Label runat="server" ID="lblBatchName" Text="" Style="font-family: 'Times New Roman'"></asp:Label>
                    </div>
                    <div style="padding: 10px; width: auto">
                        <asp:GridView ID="gvExamRoutine" runat="server" Width="100%" ClientIDMode="Static" AutoGenerateColumns="True">
                        </asp:GridView>
                    </div>
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
        <div style="height: 30px; text-align: right; padding: 05px">
            <asp:Button runat="server" ID="btnSave" Text="Save" ClientIDMode="Static" CssClass="greenBtn" />
        </div>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ScriptContent" runat="server">

</asp:Content>

