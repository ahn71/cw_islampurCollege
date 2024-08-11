<%@ Page Title="" Language="C#" MasterPageFile="~/main.Master"  AutoEventWireup="true" CodeBehind="IndividualAbsentDetails.aspx.cs" Inherits="DS.UI.Reports.Attendance.IndividualAbsentDetails" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style>
        input[type="radio"] {
            margin: 10px;
        }
        .tgPanel {
            width: 100%;
        }
        .controlLength {
            width: 150px;
        }
        .tbl-controlPanel{
            width: 500px;
        }
        .tbl-controlPanel td:nth-child(1),
        .tbl-controlPanel td:nth-child(3),
        .tbl-controlPanel td:nth-child(5) {
            padding: 0px 5px;
        }
        .litleMargin {
            margin-left: 5px;
        }
        .btn {
            margin: 3px;
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
        <asp:UpdatePanel ID="UpdatePanel2" runat="server" UpdateMode="Conditional">
            <Triggers>
                <asp:AsyncPostBackTrigger ControlID="dlSheetName" />
                 <asp:AsyncPostBackTrigger ControlID="ddlShiftList" />
            <asp:AsyncPostBackTrigger ControlID="ddlMonths" />
            </Triggers>
            <ContentTemplate>
                <table class="tbl-controlPanel">
                    <tr>
                        <td>Shift
                            </td>
                            <td>
                                <asp:DropDownList ID="ddlShiftList" Width="100px" runat="server" class="input controlLength" AutoPostBack="True" OnSelectedIndexChanged="ddlShiftList_SelectedIndexChanged">
                                   
                                </asp:DropDownList>
                            </td>
                            <td>Batch
                            </td>
                            <td>
                                <asp:DropDownList ID="ddlBatch"  runat="server" class="input controlLength" Width="100px" AutoPostBack="True" OnSelectedIndexChanged="ddlBatch_SelectedIndexChanged" ></asp:DropDownList>
                            </td>
                             <td>Group
                            </td>
                            <td>
                                <asp:DropDownList ID="ddlgroup" runat="server" class="input controlLength" Width="100px" ClientIDMode="Static"  Enabled="true"  AutoPostBack="True" OnSelectedIndexChanged="ddlgroup_SelectedIndexChanged"></asp:DropDownList>
                            </td>
                            <td>Section
                            </td>
                            <td>
                                <asp:DropDownList ID="ddlSection" runat="server"  class="input controlLength" Width="100px" AutoPostBack="True" OnSelectedIndexChanged="ddlSection_SelectedIndexChanged"></asp:DropDownList>
                            </td>
                        <td>Month</td>
                        <td><asp:DropDownList Visible="false" ID="dlSheetName" runat="server" ClientIDMode="Static"></asp:DropDownList>
                            <asp:DropDownList ID="ddlMonths" runat="server" ClientIDMode="Static" CssClass="input controlLength" Width="150px"
                                AutoPostBack="True" >
                            </asp:DropDownList>
                        </td>
                        <td>Roll No</td>
                        <td>
                            <asp:DropDownList ID="dlRollNo" Width="100px" runat="server" CssClass="input controlLength" ClientIDMode="Static">                                
                            </asp:DropDownList>
                        </td>
                        <td>
                            <asp:Button ID="btnSearch" Text="Search" CssClass="btn btn-primary litleMargin"
                                ClientIDMode="Static" runat="server" OnClick="btnSearch_Click" />
                        </td>
                    </tr>
                </table>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
    <div class="tgPanel">
        <div class="tgPanelHead">Searching Result</div>
        <div class="widget">
            <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                <ContentTemplate>
                    <div class="head">
                        <asp:Button runat="server" ID="btnPrintPreview" Text="Print Preview"
                            Width="120px" CssClass="btn btn-success pull-right" OnClick="btnPrintPreview_Click" />
                        <div class="dataTables_filter" style="float: right;">
                        </div>
                    </div>
                </ContentTemplate>
            </asp:UpdatePanel>
            <div class="clearfix"></div>
            <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="btnSearch" EventName="Click" />
                </Triggers>
                <ContentTemplate>
                    <div id="lblSectionDiv" runat="server"></div>
                    <div id="divIndividualAbsent" class="datatables_wrapper" runat="server"></div>
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
    </script>
</asp:Content>

