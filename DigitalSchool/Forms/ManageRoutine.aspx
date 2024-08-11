<%@ Page Title="Manage Class Routine" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ManageRoutine.aspx.cs" Inherits="DS.Forms.ManageRoutine" %>
<%@ Register Assembly="AjaxControlToolkit"  Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <link href="../Assets/css/timetable.css" rel="stylesheet" />
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
    <div class="container-fluid">
        <div class="row">
            <div class="col-md-3">
                <asp:UpdatePanel runat="server" ID="up1">
                    <Triggers>
                        <asp:AsyncPostBackTrigger ControlID="dlBatch" />
                        <asp:AsyncPostBackTrigger ControlID="dlExamType" />
                    </Triggers>
                    <ContentTemplate>
                        <div class="form-group">
                            <label>Batch</label>
                            <asp:DropDownList runat="server" ID="dlBatch" ClientIDMode="Static" CssClass="form-control"
                                AutoPostBack="true" OnSelectedIndexChanged="dlBatch_SelectedIndexChanged">
                            </asp:DropDownList>
                            <label>Shift</label>
                            <asp:DropDownList runat="server" ID="dlShift" ClientIDMode="Static" CssClass="form-control">
                                <asp:ListItem>Morning</asp:ListItem>
                                <asp:ListItem>Day</asp:ListItem>
                            </asp:DropDownList>
                            <label>
                                <asp:Label runat="server" ID="lblSectionOrGroup" Text="Section"></asp:Label></label>
                            <asp:DropDownList runat="server" ID="dlSection" ClientIDMode="Static" CssClass="form-control"></asp:DropDownList>
                            <label>Session Type</label>
                            <asp:DropDownList runat="server" ID="dlExamType" ClientIDMode="Static" CssClass="form-control"
                                AutoPostBack="true" OnSelectedIndexChanged="dlExamType_SelectedIndexChanged">
                                <asp:ListItem>--Select--</asp:ListItem>
                            </asp:DropDownList>
                        </div>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>
            <div class="col-md-3">
                <asp:UpdatePanel runat="server" ID="upStTime" UpdateMode="Conditional">
                    <Triggers>
                        <asp:AsyncPostBackTrigger ControlID="btnAdd" />
                        <asp:AsyncPostBackTrigger ControlID="dlDay" />
                        <asp:AsyncPostBackTrigger ControlID="btnSave" />
                        <asp:AsyncPostBackTrigger ControlID="gvDay" />
                    </Triggers>
                    <ContentTemplate>
                        <div class="form-group">
                            <label>Routine For</label>
                            <asp:Label runat="server" ID="lblRoutineId" Text="" Style="color: #FF0000; font-size: 12px;"></asp:Label>
                        </div>
                        <div class="form-group">
                            <label>Day</label>
                            <asp:DropDownList runat="server" ID="dlDay" ClientIDMode="Static" CssClass="form-control" AutoPostBack="true"
                                OnSelectedIndexChanged="dlDay_SelectedIndexChanged">
                                <asp:ListItem>Saturday</asp:ListItem>
                                <asp:ListItem>Sunday</asp:ListItem>
                                <asp:ListItem>Monday</asp:ListItem>
                                <asp:ListItem>Tuesday</asp:ListItem>
                                <asp:ListItem>Wednesday</asp:ListItem>
                                <asp:ListItem>Thursday</asp:ListItem>
                            </asp:DropDownList>

                            <label>Start Time</label>
                            <div class="row">
                                <div class="col-sm-6">
                                    <asp:DropDownList runat="server" ID="dlHours" ClientIDMode="Static" CssClass="form-control"></asp:DropDownList>
                                </div>
                                <div class="col-sm-6">
                                    <asp:DropDownList runat="server" ID="dlMinute" ClientIDMode="Static" CssClass="form-control"></asp:DropDownList>
                                </div>
                            </div>
                        </div>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>
            <div class="col-md-3">
                <asp:UpdatePanel runat="server" ID="UpdatePanel1" UpdateMode="Conditional">
                    <Triggers>
                        <asp:AsyncPostBackTrigger ControlID="dlBatch" />
                        <asp:AsyncPostBackTrigger ControlID="dlDepartment" />
                        <asp:AsyncPostBackTrigger ControlID="btnSave" />
                        <asp:AsyncPostBackTrigger ControlID="gvDay" />
                    </Triggers>
                    <ContentTemplate>
                        <div class="form-group">
                            <label>Subject</label>
                            <asp:DropDownList runat="server" ID="dlSubject" ClientIDMode="Static" CssClass="form-control"></asp:DropDownList>
                            <label>Department</label>
                            <asp:DropDownList runat="server" ID="dlDepartment" ClientIDMode="Static" CssClass="form-control" AutoPostBack="true"
                                OnSelectedIndexChanged="dlDepartment_SelectedIndexChanged">
                                <asp:ListItem>--Select--</asp:ListItem>
                            </asp:DropDownList>
                            <label>Teacher</label>
                            <asp:DropDownList runat="server" ID="dlTeacher" ClientIDMode="Static" CssClass="form-control"
                                OnSelectedIndexChanged="dlTeacher_SelectedIndexChanged" AutoPostBack="true">
                            </asp:DropDownList>
                            <label>Duration</label>
                            <asp:TextBox runat="server" ID="txtDuration" ClientIDMode="Static" CssClass="form-control" PlaceHolder="Minute"></asp:TextBox>
                        </div>
                        <div class="col-sm-6">
                            <asp:Button runat="server" ID="btnAdd" ClientIDMode="Static" Text="Add" CssClass="btn btn-primary btn-lg" OnClick="btnAdd_Click" />
                        </div>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>
        </div>
    </div>
    <div style="padding: 10px;">
        <asp:UpdatePanel runat="server" ID="upCountDay" UpdateMode="Conditional">
            <Triggers>
                <asp:AsyncPostBackTrigger ControlID="dlTeacher" />
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
    <div>
        <asp:UpdatePanel runat="server" ID="upbatch" UpdateMode="Conditional">
            <Triggers>
                <asp:AsyncPostBackTrigger ControlID="dlBatch" />
                <asp:AsyncPostBackTrigger ControlID="btnAdd" />
                <asp:AsyncPostBackTrigger ControlID="btnSave" />
                <asp:AsyncPostBackTrigger ControlID="dlExamType" />
            </Triggers>
            <ContentTemplate>
                <%--<div class="headerRoutine">--%>
                <h5>CLASS ROUTINE FOR
                        <asp:Label runat="server" ID="lblBatchName" Text=""></asp:Label>
                </h5>
                <%--</div>--%>
                <table class="table-bordered custom_table">
                    <tr>
                        <th>Time/<br>
                            Date</th>
                        <th>Time</th>
                        <th>Time</th>
                        <th>Time</th>
                        <th>Time</th>
                    </tr>
                    <tr>
                        <td>Sat</td>
                        <td>
                            <div class="box">
                                <ul>
                                    <li>Web Design</li>
                                    <li>Akter Hossain</li>
                                    <li>Room-100</li>
                                </ul>
                            </div>
                        </td>
                        <td>
                            <div class="box">
                                <ul>
                                    <li>Web Design</li>
                                    <li>Akter Hossain</li>
                                    <li>Room-100</li>
                                </ul>
                            </div>
                        </td>
                        <td>
                            <div class="box">
                                <ul>
                                    <li>Web Design</li>
                                    <li>Akter Hossain</li>
                                    <li>Room-100</li>
                                </ul>
                            </div>
                        </td>
                        <td>
                            <div class="box">
                                <ul>
                                    <li>Web Design</li>
                                    <li>Akter Hossain</li>
                                    <li>Room-100</li>
                                </ul>
                            </div>
                        </td>
                    </tr>
                    <tr>
                        <td>Sun</td>
                        <td>
                            <div class="box">
                                <ul>
                                    <li>Web Design</li>
                                    <li>Akter Hossain</li>
                                    <li>Room-100</li>
                                </ul>
                            </div>
                        </td>
                        <td>
                            <div class="box">
                                <ul>
                                    <li>Web Design</li>
                                    <li>Akter Hossain</li>
                                    <li>Room-100</li>
                                </ul>
                            </div>
                        </td>
                        <td>
                            <div class="box">
                                <ul>
                                    <li>Web Design</li>
                                    <li>Akter Hossain</li>
                                    <li>Room-100</li>
                                </ul>
                            </div>
                        </td>
                        <td>
                            <div class="box">
                                <ul>
                                    <li>Web Design</li>
                                    <li>Akter Hossain</li>
                                    <li>Room-100</li>
                                </ul>
                            </div>
                        </td>
                    </tr>
                    <tr>
                        <td>Mon</td>
                        <td>
                            <div class="box">
                                <ul>
                                    <li>Web Design</li>
                                    <li>Akter Hossain</li>
                                    <li>Room-100</li>
                                </ul>
                            </div>
                        </td>
                        <td>
                            <div class="box">
                                <ul>
                                    <li>Web Design</li>
                                    <li>Akter Hossain</li>
                                    <li>Room-100</li>
                                </ul>
                            </div>
                        </td>
                        <td>
                            <div class="box">
                                <ul>
                                    <li>Web Design</li>
                                    <li>Akter Hossain</li>
                                    <li>Room-100</li>
                                </ul>
                            </div>
                        </td>
                        <td>
                            <div class="box">
                                <ul>
                                    <li>Web Design</li>
                                    <li>Akter Hossain</li>
                                    <li>Room-100</li>
                                </ul>
                            </div>
                        </td>
                    </tr>
                    <tr>
                        <td>Tue</td>
                        <td>
                            <div class="box">
                                <ul>
                                    <li>Web Design</li>
                                    <li>Akter Hossain</li>
                                    <li>Room-100</li>
                                </ul>
                            </div>
                        </td>
                        <td>
                            <div class="box">
                                <ul>
                                    <li>Web Design</li>
                                    <li>Akter Hossain</li>
                                    <li>Room-100</li>
                                </ul>
                            </div>
                        </td>
                        <td>
                            <div class="box">
                                <ul>
                                    <li>Web Design</li>
                                    <li>Akter Hossain</li>
                                    <li>Room-100</li>
                                </ul>
                            </div>
                        </td>
                        <td>
                            <div class="box">
                                <ul>
                                    <li>Web Design</li>
                                    <li>Akter Hossain</li>
                                    <li>Room-100</li>
                                </ul>
                            </div>
                        </td>
                    </tr>
                    <tr>
                        <td>Wed</td>
                        <td>
                            <div class="box">
                                <ul>
                                    <li>Web Design</li>
                                    <li>Akter Hossain</li>
                                    <li>Room-100</li>
                                </ul>
                            </div>
                        </td>
                        <td>
                            <div class="box">
                                <ul>
                                    <li>Web Design</li>
                                    <li>Akter Hossain</li>
                                    <li>Room-100</li>
                                </ul>
                            </div>
                        </td>
                        <td>
                            <div class="box">
                                <ul>
                                    <li>Web Design</li>
                                    <li>Akter Hossain</li>
                                    <li>Room-100</li>
                                </ul>
                            </div>
                        </td>
                        <td>
                            <div class="box">
                                <ul>
                                    <li>Web Design</li>
                                    <li>Akter Hossain</li>
                                    <li>Room-100</li>
                                </ul>
                            </div>
                        </td>
                    </tr>
                    <tr>
                        <td>Thur</td>
                        <td>
                            <div class="box">
                                <ul>
                                    <li>Web Design</li>
                                    <li>Akter Hossain</li>
                                    <li>Room-100</li>
                                </ul>
                            </div>
                        </td>
                        <td>
                            <div class="box">
                                <ul>
                                    <li>Web Design</li>
                                    <li>Akter Hossain</li>
                                    <li>Room-100</li>
                                </ul>
                            </div>
                        </td>
                        <td>
                            <div class="box">
                                <ul>
                                    <li>Web Design</li>
                                    <li>Akter Hossain</li>
                                    <li>Room-100</li>
                                </ul>
                            </div>
                        </td>
                        <td>
                            <div class="box">
                                <ul>
                                    <li>Web Design</li>
                                    <li>Akter Hossain</li>
                                    <li>Room-100</li>
                                </ul>
                            </div>
                        </td>
                    </tr>
                </table>
                <div style="padding: 10px; width: auto">
                    <asp:GridView ID="gvDay" runat="server" Width="100%" Visible="false" ClientIDMode="Static"
                        OnSelectedIndexChanged="gvDay_SelectedIndexChanged"
                        OnRowDataBound="gvDay_RowDataBound"
                        AutoGenerateColumns="False">
                        <Columns>
                            <asp:BoundField DataField="Day" HeaderText="Day">
                                <ItemStyle Width="100px" />
                            </asp:BoundField>
                            <asp:BoundField DataField="StartTime" HeaderText="StartTime">
                                <ItemStyle Width="100px" />
                            </asp:BoundField>
                            <asp:BoundField DataField="EndTime" HeaderText="EndTime">
                                <ItemStyle Width="100px" />
                            </asp:BoundField>
                            <asp:BoundField DataField="SubName" HeaderText="SubName">
                                <ItemStyle Width="200px" />
                            </asp:BoundField>
                            <asp:BoundField DataField="TCodeNo" HeaderText="	TCodeNo">
                                <ItemStyle Width="80px" />
                            </asp:BoundField>
                            <asp:BoundField DataField="OrderNo" HeaderText="OrderNo">
                                <ItemStyle Width="10px" />
                            </asp:BoundField>
                            <asp:CommandField ButtonType="Image" HeaderText="Edit" ItemStyle-Height="20px" SelectImageUrl="/Images/gridImages/edit.png" ShowSelectButton="True">
                                <ItemStyle Height="15px" Width="20px" />
                            </asp:CommandField>
                        </Columns>
                    </asp:GridView>
                    <div id="divRoutineInfo" style="width: 100%" runat="server"></div>
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
    <div style="height: 30px; text-align: right; padding: 05px">
        <asp:Button runat="server" ID="btnSave" Text="Save" ClientIDMode="Static" CssClass="greenBtn" OnClick="btnSave_Click" />
    </div>
    <asp:UpdatePanel runat="server" ID="upRoutine" UpdateMode="Conditional">
        <Triggers>
        </Triggers>
        <ContentTemplate>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ScriptContent" runat="server">
</asp:Content>
