<%@ Page Title="" Language="C#" MasterPageFile="~/main.Master" AutoEventWireup="true" CodeBehind="website-general-settings.aspx.cs" Inherits="DS.UI.Administration.DSWS.website_general_settings" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
   
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
                <li><a runat="server" href="~/UI/Administration/DSWS/DSWSHome.aspx">Website Settings Module</a></li>  --%>    
                <li>
                     <a runat="server" id="aDashboard" >
                        <i class="fa fa-dashboard"></i>
                        Dashboard
                    </a>
                </li>
                <li><a runat="server" id="aAdministration">Administration Module</a></li>
                <li><a runat="server" id="aWebsite">Website Settings Module</a></li>
                <li class="active">General Settings</li>
            </ul>
            <!--breadcrumbs end -->
        </div>
    </div>
      <div class="">
        <div class="tgPanel">
            <div class="tgPanelHead">Website General Settings</div>
            <asp:UpdatePanel ID="UpdatePanel2" runat="server" UpdateMode="Conditional">
                <Triggers>                   
                    <asp:AsyncPostBackTrigger ControlID="ckbIsAdmissionOpen"/>
                </Triggers>
                <ContentTemplate>
                    <table class="tbl-controlPanel table table-bordered" style="text-align:center">                    
                        <tr>
                            <td>Admission Form Status</td>
                            <td>
                                <asp:TextBox runat="server" ClientIDMode="Static" ID="txtAdmissionMsg" class="form-control" placeholder="enter the admission message"></asp:TextBox>
                            </td>
                            <td>
                                <asp:CheckBox ID="ckbIsAdmissionOpen" runat="server" OnCheckedChanged="ckbIsAdmissionOpen_CheckedChanged"  ClientIDMode="Static" 
                                    AutoPostBack="true" />
                            </td>
                            <td>
                                 <asp:Button runat="server" ID="btnSubmit" Text="Submit" CssClass="btn btn-success" OnClick="btnSubmit_Click"/>
                            </td>
                        </tr>                    
                    </table>

                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ScriptContent" runat="server">
</asp:Content>
