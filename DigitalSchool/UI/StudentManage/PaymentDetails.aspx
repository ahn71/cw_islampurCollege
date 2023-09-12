<%@ Page Title="" Language="C#" MasterPageFile="~/Student.Master" AutoEventWireup="true" CodeBehind="PaymentDetails.aspx.cs" Inherits="DS.UI.StudentManage.PaymentDetails" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style>
       .tbl-controlPanel{
           width:666px;
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
                    <a runat="server" href="~/UI/StudentManage/StudentManage.aspx">
                        <i class="fa fa-dashboard"></i>
                        <asp:Label ID="lblName" runat="server"></asp:Label>
                    </a>
                </li> 
                <li>
                    <a runat="server" href="~/UI/StudentManage/StudentFinance.aspx">
                       Finance
                    </a>
                </li>                 
                <li class="active">Payment Details</li>                               
            </ul>
            <!--breadcrumbs end -->
        </div>
    </div>
    <div class="row"> 
        <div class="col-md-12">
        <div class="tgPanel" style="width:100%">        
              <div class="row"> 
                  <div class="col-md-7"> 
                       <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                           <Triggers>
                               <asp:AsyncPostBackTrigger ControlID="ddlBatch" />
                           </Triggers>
        <ContentTemplate>        
            <table class="tbl-controlPanel" style="text-align:left">
                <tr>
                    <td>
                        Batch
                    </td>
                    <td>:</td>
                    <td>
                        <asp:DropDownList ID="ddlBatch" runat="server" Width="260px"
                             OnSelectedIndexChanged="ddlBatch_SelectedIndexChanged" AutoPostBack="true" 
                            ClientIDMode="Static" CssClass="input controlLength"></asp:DropDownList>
                    </td>
                     <td>
                        Fees Category
                    </td>
                    <td>:</td>
                    <td>
                        <asp:DropDownList ID="ddlFeesCat" runat="server" Width="260px" ClientIDMode="Static" CssClass="input controlLength"></asp:DropDownList>
                    </td>
                </tr>
            </table> 
            </ContentTemplate>
                           </asp:UpdatePanel>
                      </div>
                  </div>                         
        </div>       
        </div>           
    </div>
   <br />
    <div class="row">
        <asp:UpdatePanel ID="UpdatePanel1" runat="server" >
            <ContentTemplate>               
                 <div class="col-md-3">
                    <a id="A4" runat="server" onserverclick="A4_ServerClick">
                        <div class="panel">
                            <div class="panel-body">
                                <h5 class="text-center">Payment Details</h5>
                            </div>
                        </div>
                    </a>                  
                </div>                             
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ScriptContent" runat="server">
</asp:Content>
