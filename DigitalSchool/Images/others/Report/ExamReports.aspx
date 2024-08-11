<%@ Page Title="Exam Report" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ExamReports.aspx.cs" Inherits="DS.Report.ExamReports" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">   
    <style type="text/css">
        .controlLength{
            width:195px;            
        }
        .tgPanel
        {
            width: 100%;
            min-height: 300px;
        }
        .tbl-controlPanel tr td:first-child {
            width: 30%;
            text-align: right;
            padding-right: 8px;               
        }
        
        .littleMargin{
            margin-right: 5px;
        }
        .titleHeader{
            padding : 10px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">   
    <asp:UpdatePanel ID="uplMessage" runat="server">
        <ContentTemplate>
            <p class="message" id="lblMessage" clientidmode="Static" runat="server"></p>
        </ContentTemplate>
    </asp:UpdatePanel>
    <div class="container">
        <div class="row">
            <div class="col-md-1"></div>
            <div class="col-md-10">
                <div class="col-md-12">
                    <div class="tgPanelHead">Exam Reports</div>
                </div>                
                <div class="col-md-6">
                    <div class="tgPanel">
                        <asp:UpdatePanel ID="UpdatePanel2" runat="server" UpdateMode="Conditional">
                            <Triggers>
                                <asp:AsyncPostBackTrigger ControlID="ddlExamType" />
                                <asp:AsyncPostBackTrigger ControlID="ddlSectionName" />
                                <asp:AsyncPostBackTrigger ControlID="ddlBatch" />
                                <asp:AsyncPostBackTrigger ControlID="ddlRoll" />
                                <asp:AsyncPostBackTrigger ControlID="ddlShift" />
                            </Triggers>
                            <ContentTemplate>
                                <table class="tbl-controlPanel">
                                    <tr>
                                        <td>Exam Type</td>
                                        <td>
                                            <asp:DropDownList ID="ddlExamType" runat="server" CssClass="input controlLength"
                                                ClientIDMode="Static" AutoPostBack="True" OnSelectedIndexChanged="ddlExamType_SelectedIndexChanged">
                                            </asp:DropDownList>
                                            <asp:DropDownList ID="ddlBatch" runat="server" ClientIDMode="Static" CssClass="input controlLength"
                                                AutoPostBack="True" OnSelectedIndexChanged="ddlBatch_SelectedIndexChanged">
                                            </asp:DropDownList>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>Exam Id</td>
                                        <td>
                                            <asp:DropDownList ID="ddlExamId" runat="server" ClientIDMode="Static" CssClass="input controlLength">
                                            </asp:DropDownList>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>Shift</td>
                                        <td>
                                            <asp:DropDownList ID="ddlShift" runat="server" ClientIDMode="Static" CssClass="input controlLength"
                                                AutoPostBack="True" OnSelectedIndexChanged="ddlShift_SelectedIndexChanged">
                                                <asp:ListItem Value="0">Morning</asp:ListItem>
                                                <asp:ListItem Value="1">Day</asp:ListItem>
                                            </asp:DropDownList>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>Section</td>
                                        <td>
                                            <asp:DropDownList ID="ddlSectionName" runat="server" ClientIDMode="Static" AutoPostBack="True" CssClass="input controlLength"
                                                OnSelectedIndexChanged="ddlSectionName_SelectedIndexChanged">
                                            </asp:DropDownList>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>Roll</td>
                                        <td>
                                            <asp:DropDownList ID="ddlRoll" runat="server" ClientIDMode="Static" CssClass="input controlLength"
                                                AutoPostBack="True">
                                            </asp:DropDownList>
                                        </td>
                                    </tr>
                                </table>
                                <div style="text-align: center; font-size: -25px">
                                    <p runat="server" id="MarkSheetTitle" visible="false"></p>
                                </div>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </div>
                </div>
                <div class="col-md-6">
                    <div class="tgPanel">
                        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                            <Triggers>
                            </Triggers>
                            <ContentTemplate>
                                <table class="tbl-controlPanel">
                                    <tr>
                                        <td>
                                            <asp:Button Text="Student Wise" CssClass="btn btn-primary" Width="195" 
                                                ID="btnStudentWiseMarkList" runat="server"
                                                OnClick="btnStudentWiseMarkList_Click" />
                                        </td>
                                        <td>                                            
                                            <asp:Button ID="btnResultSummary" runat="server" CssClass="btn btn-primary" Width="195" 
                                                OnClick="btnResultSummary_Click" Text="Result Summary"/>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:Button Text="Pass List" CssClass="btn btn-primary" ID="btnPassList" Width="195" 
                                                runat="server" OnClick="btnPassList_Click" />
                                        </td>
                                        <td>                                            
                                            <asp:Button Text="Fail List" CssClass="btn btn-primary" ID="btnFailList" Width="195" 
                                                runat="server" OnClick="btnFailList_Click" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:TextBox ID="txtTopStudent" runat="server" CssClass="input controlLength" 
                                                PlaceHolder="Number of Student"></asp:TextBox>
                                        </td>
                                        <td>                                            
                                            <asp:Button Text="Find Top Student" ClientIDMode="Static" CssClass="btn btn-primary" Width="195" 
                                                ID="btnFindTopStudent" runat="server" OnClick="btnFindTopStudent_Click" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:TextBox ID="txtStudentRoll" runat="server" CssClass="input controlLength" Width="195" 
                                                PlaceHolder="Roll of Student"></asp:TextBox>
                                        </td>
                                        <td>                                            
                                            <asp:Button ID="btnFindGPA" runat="server" CssClass="btn btn-primary" Width="195" 
                                                Text="Find GPA" OnClick="btnFindGPA_Click" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td></td>
                                        <td>
                                            <asp:Button ID="btnDetailsMarks" runat="server" CssClass="btn btn-primary" Width="195" 
                                                Text="Details Select Exam" Visible="false" />
                                        </td>
                                    </tr>
                                </table>
                                <div style="text-align: center; font-size: 20px">
                                    <p runat="server" id="P1" visible="false"></p>
                                </div>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </div>
                </div>
            </div>
            <div class="col-md-1"></div>
        </div>
        <div class="row">
            <div class="tgPanel">
                <div class="tgPanelHead">Searching Result</div>
                <asp:UpdatePanel ID="UpdatePanel4" runat="server" UpdateMode="Conditional">
                    <ContentTemplate>
                        <asp:Button ID="btnPrintPreview" runat="server" Text="Print Preview"
                            CssClass="btn btn-success pull-right" Width="110px" OnClick="btnPrintPreview_Click" />
                    </ContentTemplate>
                </asp:UpdatePanel>
                <asp:UpdatePanel ID="UpdatePanel3" runat="server" UpdateMode="Conditional">
                    <Triggers>
                        <asp:AsyncPostBackTrigger ControlID="btnStudentWiseMarkList" />
                        <asp:AsyncPostBackTrigger ControlID="btnFailList" />
                        <asp:AsyncPostBackTrigger ControlID="btnResultSummary" />
                        <asp:AsyncPostBackTrigger ControlID="btnPassList" />
                        <asp:AsyncPostBackTrigger ControlID="btnFindTopStudent" />
                        <asp:AsyncPostBackTrigger ControlID="btnFindGPA" />
                    </Triggers>
                    <ContentTemplate>
                        <div style="width: auto" runat="server" id="divProgressReport" visible="False">
                            <div class="titleHeader">
                                <asp:Label ID="lblProgressReport" runat="server" Text="" Font-Bold="True" Font-Size="15px"></asp:Label><br />
                                <asp:Label ID="lblName" runat="server" Text="" Font-Bold="True" Font-Size="15px"></asp:Label><br />
                                <asp:Label ID="lblClass" runat="server" Text="" Font-Bold="True" Font-Size="15px"></asp:Label><br />
                                <asp:Label ID="lblShift" runat="server" Text="" Font-Bold="True" Font-Size="15px"></asp:Label>
                            </div> 
                            <br />                           
                        </div>
                    </ContentTemplate>
                </asp:UpdatePanel>
                <div style="height: auto; width: 100%;"></div>
            </div>
        </div>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ScriptContent" runat="server">
</asp:Content>
