<%@ Page Title="" Language="C#" MasterPageFile="~/main.Master" AutoEventWireup="true" CodeBehind="AddNotice.aspx.cs" Inherits="DS.UI.Administration.DSWS.AddNotice" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style>
        .tgPanel {
            width: 100%;
        }

        .input {
            color: #000;
        }

        .tbl-controlPanel td:first-child {
            text-align: right;
            padding-right: 5px;
        }

        .tbl-controlPanel {
            width: auto;
        }


        .chkbox label {
            margin-left: 7px;
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
                <%-- <li>
                    <a id="A1" runat="server" href="~/Dashboard.aspx">
                        <i class="fa fa-dashboard"></i>
                        Dashboard
                    </a>
                </li>
                <li><a runat="server" href="~/UI/Administration/AdministrationHome.aspx">Administration Module</a></li>
                <li><a runat="server" href="~/UI/Administration/DSWS/DSWSHome.aspx">Website Settings Module</a></li>   --%>
                <li>
                    <a runat="server" id="aDashboard">
                        <i class="fa fa-dashboard"></i>
                        Dashboard
                    </a>
                </li>
                <li><a runat="server" id="aAdministration">Administration Module</a></li>
                <li><a runat="server" id="aWebsite">Website Settings Module</a></li>
                <li><a runat="server" id="aNotice">Notice</a></li>
                <li runat="server" id="liAddEdit" class="active"></li>
            </ul>
            <!--breadcrumbs end -->
        </div>
    </div>



    <div class="">
        <div class="row">
            <div class="col-md-5">
                <div class="tgPanelHead">Add Notice</div>
                <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                    <Triggers>
                        <asp:PostBackTrigger ControlID="btnSubmit" />
                        <asp:PostBackTrigger ControlID="btnClear" />
                    </Triggers>
                    <ContentTemplate>
                        <div class="tgPanel">
                            <div class="row tbl-controlPanel">
                                <div class="col-sm-10 col-sm-offset-1">
                                    <div class="row tbl-controlPanel">
                                        <label class="col-sm-4">Subject</label>
                                        <div class="col-sm-8">
                                            <asp:TextBox ID="txtNSubject" runat="server" ClientIDMode="Static" class="input controlLength form-control"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="row tbl-controlPanel">
                                        <label class="col-sm-4">Details</label>
                                        <div class="col-sm-8">
                                            <asp:TextBox ID="txtNDetails" runat="server" Height="300px" ClientIDMode="Static" class="input form-control" TextMode="MultiLine" Font-Names="SutonnyMJ"></asp:TextBox>
                                            <asp:HtmlEditorExtender ID="txtBody_HtmlEditorExtender" runat="server" TargetControlID="txtNDetails" EnableSanitization="false">
                                            </asp:HtmlEditorExtender>
                                        </div>
                                    </div>
                                    <div class="row tbl-controlPanel">
                                        <label class="col-sm-4">Publish Date</label>
                                        <div class="col-sm-8">
                                            <asp:TextBox ID="txtPublishdate" runat="server" ClientIDMode="Static" CssClass="input controlLength form-control"></asp:TextBox>
                                            <asp:CalendarExtender ID="CalendarExtender2" runat="server"
                                                Format="dd-MM-yyyy" TargetControlID="txtPublishdate">
                                            </asp:CalendarExtender>
                                        </div>
                                    </div>
                                    <div runat="server" visible="false" class="row tbl-controlPanel">
                                        <label class="col-sm-4">Order No</label>
                                        <div class="col-sm-8">
                                            <asp:TextBox ID="txtOrder" runat="server" ClientIDMode="Static" class="input controlLength form-control"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div runat="server" class="row tbl-controlPanel">
                                        <label class="col-sm-4">Attach File</label>
                                        <div class="col-sm-8">
                                            <asp:FileUpload ID="fileAttachment" runat="server" />

                                        </div>
                                    </div>
                                    <div class="row tbl-controlPanel">
                                        <label class="col-sm-4"></label>
                                        <div class="col-sm-8">

                                            <asp:CheckBox ID="chkIsActive" CssClass="chkbox" runat="server" ClientIDMode="Static" Text="Is Active" />
                                            <asp:CheckBox ID="chkIsImportantNews" CssClass="chkbox" runat="server" ClientIDMode="Static" Text="Is Important News" />
                                        </div>
                                    </div>
                                    <div class="row tbl-controlPanel">
                                        <div class="col-sm-offset-4 col-sm-8">
                                            <asp:Button CssClass="btn btn-primary" ID="btnSubmit" runat="server" Text="Save" ClientIDMode="Static"
                                                OnClientClick="return validateInputs();" OnClick="btnSubmit_Click" />
                                            &nbsp;<asp:Button  runat="server" ID="btnClear" Text="Clear" CssClass="btn btn-default" OnClick="btnClear_Click" />

                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </ContentTemplate>
                </asp:UpdatePanel>

            </div>
        </div>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ScriptContent" runat="server">
    <script type="text/javascript">      
        function validateInputs() {
            if (validateText('txtNSubject', 1, 50, 'Enter Notice Subject') == false) return false;
            if (validateText('txtNDetails', 0, 2000, 'Enter Valid Notice Details') == false) return false;
            if (validateText('txtPublishdate', 1, 20, 'Enter Publish Date') == false) return false;
            if (validateText('txtOrder', 1, 20, 'Enter Order No') == false) return false;
            return true;
        }     
    </script>
</asp:Content>
