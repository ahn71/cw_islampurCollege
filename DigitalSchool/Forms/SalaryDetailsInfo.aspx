<%@ Page Title="Salary Set Details Info" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="SalaryDetailsInfo.aspx.cs" Inherits="DS.Forms.SalaryDetailsInfo" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <style>
        .controlLength {
            width: 120px;
            margin: 5px;
        }

        .tgPanel {
            width: 100%;
        }

        #tblSetRollOptionalSubject {
            width: 100%;
        }

            #tblSetRollOptionalSubject th,
            #tblSetRollOptionalSubject td,
            #tblSetRollOptionalSubject td input,
            #tblSetRollOptionalSubject td select {
                padding: 5px 5px;
                margin-left: 10px;
                text-align: center;
            }

        .litleMargin {
            margin-left: 5px;
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
            <div class="col-md-12">
                <div class="col-md-6">
                    <div class="tgPanel">
                        <div class="tgPanelHead">Salary Details Information</div>
                        <table class="tbl-controlPanel">
                            <tr>
                                <td>Department
                                </td>
                                <td>
                                    <asp:DropDownList runat="server" ID="dlDepartment" ClientIDMode="Static" CssClass="input controlLength"
                                        AutoPostBack="true" OnSelectedIndexChanged="dlDepartment_SelectedIndexChanged">
                                        <asp:ListItem>--Select--</asp:ListItem>
                                    </asp:DropDownList>
                                </td>
                                <td>Name
                                </td>
                                <td>
                                    <asp:UpdatePanel ID="up1" runat="server">
                                        <Triggers>
                                            <asp:AsyncPostBackTrigger ControlID="dlDepartment" />
                                        </Triggers>
                                        <ContentTemplate>
                                            <asp:DropDownList ID="dlTeacher" runat="server" CssClass="input controlLength"></asp:DropDownList>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </td>
                                <td>
                                    <asp:Button runat="server" ID="btnSearch" ClientIDMode="Static" Text="Search" CssClass="btn btn-success"
                                        OnClick="btnSearch_Click" />
                                </td>
                            </tr>
                        </table>
                        <br />
                    </div>
                </div>
                <div class="col-md-6">
                    <div class="tgPanel">
                        <div class="tgPanelHead">Searching by Salary</div>
                        <table class="tbl-controlPanel">
                            <tr>
                                <td>
                                    <asp:DropDownList ID="dlSalaryType" runat="server" CssClass="input controlLength">
                                        <asp:ListItem>Basic Salary</asp:ListItem>
                                        <asp:ListItem>School Salary</asp:ListItem>
                                        <asp:ListItem>Total Salary</asp:ListItem>
                                    </asp:DropDownList>
                                </td>
                                <td>
                                    <asp:TextBox runat="server" ID="txtFromSalary" CssClass="input controlLength" PlaceHolder="From Salary"></asp:TextBox>
                                </td>
                                <td>
                                    <asp:TextBox runat="server" ID="txtToSalary" CssClass="input controlLength" PlaceHolder="To Salary"></asp:TextBox>
                                </td>
                                <td>
                                    <asp:Button runat="server" ID="btnSalaryRange" ClientIDMode="Static" CssClass="btn btn-success" Text="Search"
                                        OnClick="btnSalaryRange_Click" />
                                </td>
                            </tr>
                        </table>
                        <br />
                    </div>
                </div>
            </div>
            <div class="col-md-12">
                <div class="col-md-10"></div>
                <div class="col-md-2">
                    <asp:UpdatePanel runat="server" ID="up2">
                        <ContentTemplate>
                            <asp:Button runat="server" ID="btnPrintPreview" ClientIDMode="Static" Text="Print Preview"
                                CssClass="btn btn-success pull-right" OnClick="btnPrintPreview_Click" />
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </div>
            </div>
        </div>
        <div class="col-md-12">
            <div class="tgPanel">
                <div class="tgPanelHead">Details</div>
                <hr style="margin: 0px;" />
                <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
                    <Triggers>
                        <asp:AsyncPostBackTrigger ControlID="btnSearch" />
                        <asp:AsyncPostBackTrigger ControlID="btnSalaryRange" />
                    </Triggers>
                    <ContentTemplate>
                        <div id="divSalaryDetailsInfo" class="datatables_wrapper" runat="server" style="width: 100%; height: auto"></div>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>
        </div>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ScriptContent" runat="server">
</asp:Content>
