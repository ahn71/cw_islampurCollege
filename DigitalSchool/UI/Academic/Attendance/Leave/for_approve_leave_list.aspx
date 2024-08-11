<%@ Page Title="For Leave Approved" Language="C#" MasterPageFile="~/main.Master" AutoEventWireup="true" CodeBehind="for_approve_leave_list.aspx.cs" Inherits="DS.UI.Academics.Attendance.Leave.for_approve_list" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style>
        .hand {
            cursor: pointer;        
        }
        .tgPanel {
            width: 100%;
        }
        .tbl-controlPanel{
            width: 448px;
        }        
        .tbl-controlPanel td:first-child {
            text-align: right;
            padding-right: 5px;
        }  
        .litleMargin {
            margin-left: 5px;
        } 
        .table tr th{
            background-color: #23282C;
            color: white;
        }
        .total_days_table{}   
        .total_days_table tr td{
            padding-top:0px !important;
            padding-bottom:0px !important;
        }   
    </style>    
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:UpdatePanel ID="uplMessage" runat="server">
        <ContentTemplate>
            <p class="message" id="lblMessage" clientidmode="Static" runat="server"></p>
        </ContentTemplate>
    </asp:UpdatePanel>
    <div class="row">
        <div class="col-md-12">
            <!--breadcrumbs start -->
            <ul class="breadcrumb">
                <li>
                    <a runat="server" href="~/Dashboard.aspx">
                        <i class="fa fa-dashboard"></i>
                        Dashboard
                    </a>
                </li>
                <li><a runat="server" href="~/UI/Academic/AcademicHome.aspx">Academic Module</a></li>
                <li><a runat="server" href="~/UI/Academic/Attendance/AttendanceHome.aspx">Attendance Module</a></li>
                <li><a runat="server" href="~/UI/Academic/Attendance/Leave/LeaveHome.aspx">Leave Management</a></li>
                <li class="active">For Leave Approved</li>
            </ul>
            <!--breadcrumbs end -->
        </div>
    </div>
    <div class="">
        <div class="tgPanel">
            <div class="tgPanelHead">Leave List For Confirmation</div>
            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="ddlShiftName" />
                    <asp:AsyncPostBackTrigger ControlID="ddlFindingType" />
                </Triggers>
                <ContentTemplate>
                    <table class="tbl-controlPanel">
                        <tr>
                            <td>Shift</td>                           
                            <td>
                                <asp:DropDownList ID="ddlShiftName" CssClass="input" runat="server" Width="150px" AutoPostBack="True"                                    
                                    OnSelectedIndexChanged="ddlShiftName_SelectedIndexChanged">
                                    <asp:ListItem Value="0">...Select...</asp:ListItem> 
                                </asp:DropDownList>
                            </td>
                            <td>Type</td>                           
                            <td>
                                <asp:DropDownList ID="ddlFindingType" runat="server" CssClass="input" Width="150px" AutoPostBack="True" 
                                    OnSelectedIndexChanged="ddlFindingType_SelectedIndexChanged">
                                    <asp:ListItem Value="Today">Today</asp:ListItem>
                                    <asp:ListItem Value="All">All</asp:ListItem>
                                </asp:DropDownList>
                            </td>
                            <td>
                                <asp:LinkButton runat="server" ID="lnkRefresh" Text="Refresh" CssClass="btn btn-default litleMargin" 
                                    OnClick="lnkRefresh_Click">                                   
                                </asp:LinkButton>
                                <span style="position: absolute; width: 25px">
                                    <asp:UpdateProgress ID="UpdateProgress1" runat="server" ClientIDMode="Static">
                                        <ProgressTemplate>
                                            <img class="LoadingImg" src="../../../../AssetsNew/images/input-spinner.gif" />
                                        </ProgressTemplate>
                                    </asp:UpdateProgress>
                                </span>
                            </td>
                        </tr>
                    </table>                    
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
    </div>
    <div class="">
        <div class="tgPanel">
            <div class="tgPanelHead">Searching Result</div>
            <asp:UpdatePanel runat="server" ID="up2">
                <Triggers>
                </Triggers>
                <ContentTemplate>
                    <asp:GridView ID="gvForApprovedList" runat="server" AutoGenerateColumns="false" AllowPaging="true" PageSize="40" 
                        Width="100%" DataKeyNames="LACode" CssClass="table table-bordered"
                        OnRowCommand="gvForApprovedList_RowCommand" 
                        OnRowDataBound="gvForApprovedList_RowDataBound">
                        <PagerStyle CssClass="gridview" />
                        <Columns>
                            <asp:BoundField DataField="LACode" HeaderText="LACode" Visible="false" />
                            <asp:BoundField DataField="EName" HeaderText="Name" Visible="true"/>
                            <asp:BoundField DataField="LeaveName" HeaderText="Leave" Visible="true" />
                            <asp:TemplateField HeaderText="From Date">
                                <ItemTemplate>
                                    <asp:TextBox ID="txtFromDate" runat="server" CssClass="input" Enabled="false" 
                                        Width="83px" Text='<%#(Eval("FromDate").ToString()) %>'></asp:TextBox>
                                    <asp:CalendarExtender runat="server" Format="dd-MM-yyyy"
                                        Enabled="True" TargetControlID="txtFromDate" ID="CExtApplicationDate">
                                    </asp:CalendarExtender>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="To Date">
                                <ItemTemplate>
                                    <asp:TextBox ID="txtToDate" runat="server" CssClass="input"
                                        Enabled="false" Width="83px" Text='<%#(Eval("ToDate").ToString()) %>'></asp:TextBox>
                                    <asp:CalendarExtender ID="CalendarExtender1" runat="server" TargetControlID="txtToDate" Format="dd-MM-yyyy">
                                    </asp:CalendarExtender>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Total Days" HeaderStyle-Width="100px">
                                <ItemTemplate>
                                    <asp:Table runat="server" CssClass="total_days_table">
                                        <asp:TableRow>
                                            <asp:TableCell>
                                                <asp:Button runat="server" ID="btnEqual" Height="30px" Text="=" Font-Bold="true" ForeColor="Red" Width="30px" CommandName="Calculation" CommandArgument='<%#((GridViewRow)Container).RowIndex%>' />
                                            </asp:TableCell>
                                            <asp:TableCell>
                                                <asp:TextBox ID="txtTotalDays" runat="server" Width="53px" Height="26px" Style="text-align: center" CssClass="form-control text_box_width" Enabled="false" Font-Bold="true" ForeColor="Red" Text='<%#(Eval("TotalDays").ToString()) %>'></asp:TextBox>
                                            </asp:TableCell>
                                        </asp:TableRow>
                                    </asp:Table>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:BoundField DataField="EntryDate" HeaderText="Requsted" Visible="true" ItemStyle-HorizontalAlign="Center" />
                            <asp:TemplateField HeaderText="Change" HeaderStyle-Width="30px">
                                <ItemTemplate>
                                    <asp:Button ID="btnEdit" runat="server" Text="Edit" CssClass="btn btn-primary" CommandName="Alter" CommandArgument='<%#((GridViewRow)Container).RowIndex%>' />
                                </ItemTemplate>
                            </asp:TemplateField>
                             <asp:TemplateField HeaderText="View" HeaderStyle-Width="30px">
                                <ItemTemplate>
                                    <asp:Button ID="btnView" runat="server" Text="View" CssClass="btn btn-primary" CommandName="View" CommandArgument='<%#((GridViewRow)Container).RowIndex%>' />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Approved" HeaderStyle-Width="80px">
                                <ItemTemplate>
                                    <asp:Table runat="server" CssClass="total_days_table">
                                        <asp:TableRow>
                                            <asp:TableCell>
                                                <asp:Button ID="btnYes" Text="Yes" CommandName="Yes" runat="server" CssClass="btn btn-success"
                                                    OnClientClick="return confirm('Do you want to approved ?')" 
                                                    CommandArgument='<%#((GridViewRow)Container).RowIndex%>'/>
                                            </asp:TableCell>
                                            <asp:TableCell>
                                                |
                                            </asp:TableCell>
                                            <asp:TableCell>
                                                <asp:Button ID="btnNot" Text="No" CommandName="No" runat="server" CssClass="btn btn-danger"
                                                    OnClientClick="return confirm('Do you want to not approved ?')" 
                                                    CommandArgument='<%#((GridViewRow)Container).RowIndex%>' />
                                            </asp:TableCell>
                                        </asp:TableRow>
                                    </asp:Table>
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>
                    <div>                        
                        <div id="divRecordMessage" runat="server" visible="false" style="padding:5px;">
                        </div>
                    </div>
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ScriptContent" runat="server">
    <script type="text/javascript">
        var oldgridcolor;
        function SetMouseOver(element) {
            oldgridcolor = element.style.backgroundColor;
            element.style.backgroundColor = '#ffeb95';
            element.style.cursor = 'pointer';
            // element.style.textDecoration = 'underline';
        }
        function SetMouseOut(element) {
            element.style.backgroundColor = oldgridcolor;
            // element.style.textDecoration = 'none';
        }
        function LeaveDaysCaclulate(ob) {
            var start = new Date("2014-06-01");
            var td = ob.value;
            var end = new Date(td);
            var diff = new Date(end - start);
            var days = diff / 1000 / 60 / 60 / 24;
            alert(days);
            var par = $(ob).closest('tr');
            var d = par[0];
            var r = d.find('td');
        }
        function goToNewTabandWindow(url) {
            window.open(url);
        }
    </script>

</asp:Content>

