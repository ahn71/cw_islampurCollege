<%@ Page Title="" Language="C#" MasterPageFile="~/main.Master" AutoEventWireup="true" CodeBehind="UserAccountList.aspx.cs" Inherits="DS.UI.Administration.Users.UserAccountList" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
         .table tr th{
            background-color: #23282C;
            color: white;
        }
        .tgPanel {
           width:100%;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
      <asp:UpdatePanel ID="uplMessage" runat="server">
        <ContentTemplate>
            <p class="message" id="lblMessage" clientidmode="Static" runat="server"></p>
        </ContentTemplate>
    </asp:UpdatePanel>
     <div class="col-md-12">
            <!--breadcrumbs start -->
            <ul class="breadcrumb">
                <li>
                    <a runat="server" href="~/Dashboard.aspx">
                        <i class="fa fa-dashboard"></i>
                        Dashboard
                    </a>
                </li>
                <li><a runat="server" href="~/UI/Administration/AdministrationHome.aspx">Administration Module</a></li>
                <li><a runat="server" href="~/UI/Administration/Users/UsersHome.aspx">Control Panel</a></li>             
                <li class="active">Page Setup & Set Privilege</li>
            </ul>
            <!--breadcrumbs end -->
        </div>
    <div class="">
          <div class="row">
            <div class="col-md-6">                
                <h4 class="text-left">User Account List</h4>
            </div>
            <div class="col-md-6">
                 <div class="dataTables_filter_New" style="float: right;margin-right:0px;">
                     <label>
                         Search:
                         <input type="text" class="Search_New"  placeholder="type here" />
                     </label>
                 </div>
            </div>
        </div>
         <div class="row">
    <div class="col-lg-12">
        <asp:UpdatePanel ID="up2" runat="server" UpdateMode="Conditional">
            <ContentTemplate>
                <div class="tgPanel">
                 <asp:GridView ID="gvAccountList" runat="server" AutoGenerateColumns="false" DataKeyNames="UserId" 
                     CssClass="table table-bordered" BackColor="White" OnRowCommand="gvAccountList_RowCommand" 
                      PageSize="25" AllowPaging="true" OnPageIndexChanging="gvAccountList_PageIndexChanging" >
                     <PagerStyle CssClass="gridview" />
            <Columns>
                
               <asp:BoundField DataField="FirstName" HeaderText="First Name" />
                <asp:BoundField DataField="LastName" HeaderText="Last Name" />
                <asp:BoundField DataField="Email" HeaderText="Email" />
                <asp:BoundField DataField="UserName" HeaderText="User Name" />
                
                <asp:BoundField DataField="UserPassword" HeaderText="Password" />

                <asp:BoundField DataField="CreatedOn" HeaderText="CreatedOn" />
                <asp:BoundField DataField="Status" HeaderText="Status" />
                <asp:BoundField DataField="UserType" HeaderText="User Type" />
                 <asp:TemplateField HeaderStyle-HorizontalAlign="Center">
                    <HeaderTemplate >
                       Id & Password
                    </HeaderTemplate>
                    <ItemTemplate>
                        <asp:ImageButton ID="btnShowUIP" runat="server"  ImageUrl="~/Images/datatables/viewPassword.png" Width="30px" CommandName="ShowUserIdPassord" CommandArgument='<%#((GridViewRow)Container).RowIndex %>' />                 
                         <asp:Label ID="lblUIP" runat="server" Text="Show" ForeColor="Green" style="cursor:pointer" ></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField>
                    <HeaderTemplate >
                       Edit
                    </HeaderTemplate>
                    <ItemTemplate>
                        <asp:ImageButton ID="btnEdit" runat="server" ImageUrl="~/Images/datatables/edit.png" Width="25px" CommandName="Change" CommandArgument='<%#((GridViewRow)Container).RowIndex %>' />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField>
                    <HeaderTemplate >
                       Is Evaluator
                    </HeaderTemplate>
                    <ItemTemplate>
                    
                        <asp:Button runat="server" ID="btnEvaluator" ClientIDMode="Static" Text='<%#Eval("Evaluator")%>' CssClass="btn btn-primary" CommandName="Evaluator" CommandArgument='<%#((GridViewRow)Container).RowIndex %>'  />
                        <%--<asp:CheckBox runat="server" ID="ckbEvaluator"  CommandName="Evaluator" CommandArgument='<%#((GridViewRow)Container).RowIndex %>' AutoPostBack="true"  />--%>
                       
                       
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
        </asp:GridView>
                    </div>
            </ContentTemplate>
        </asp:UpdatePanel>
       
    </div>
             </div>
        </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ScriptContent" runat="server">
    <script type="text/javascript">
        $(document).ready(function () {
            $(document).on("keyup", '.Search_New', function () {
                searchTable($(this).val(), 'MainContent_gvAccountList', '');
            });
        });
    </script>
</asp:Content>
