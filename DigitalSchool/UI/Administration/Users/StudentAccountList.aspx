<%@ Page Title="" Language="C#" MasterPageFile="~/main.Master" AutoEventWireup="true" CodeBehind="StudentAccountList.aspx.cs" Inherits="DS.UI.Administration.Users.StudentAccountList" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
     <style>
        .tgPanel{
            width:100%;
        }
        .controlLength {
            width: 150px;
        }
        .tbl-controlPanel td:first-child{
            text-align:right;
            padding-right: 5px;
        }
        .tbl-controlPanel {
            width:740px;
        }
        .table tr th{
            background-color: #23282C;
            color: white;
        }
        .tgbutton{            
            padding: 10px 0;
            margin-left: 46%;
        }
        .datatables_wrapper{
            min-height: 0;
            max-height: 400px;
        }
         input[type="radio"]{
            margin: 5px;
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
                <li><a runat="server" href="~/UI/Administration/AdministrationHome.aspx">Administration Module</a></li>
                <li><a runat="server" href="~/UI/Administration/Users/UsersHome.aspx">Control Panel</a></li>
                <li class="active">Student Account Change</li>                
            </ul>
            <!--breadcrumbs end -->
        </div>
    </div>
        <div class="">
          <div class="row">
            <div class="col-md-6">                
                <h4 class="text-left">Student Account List</h4>
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
                 <asp:GridView ID="gvAccountList" runat="server" AutoGenerateColumns="false" DataKeyNames="UserId,Password" 
                     CssClass="table table-bordered" BackColor="White" OnRowCommand="gvAccountList_RowCommand"
                      AllowPaging="true" PageSize="25" OnPageIndexChanging="gvAccountList_PageIndexChanging"  >
                     <PagerStyle CssClass="gridview" />
            <Columns>
                
               <asp:BoundField DataField="FullName" HeaderText="Name" />                
                <asp:BoundField DataField="UserName" HeaderText="User Name" />                
                <asp:BoundField DataField="Password" HeaderText="Password" />
                <asp:BoundField DataField="CreatedAt" HeaderText="CreatedOn" />
                <asp:BoundField DataField="IsActive" HeaderText="Status" />                
                 <asp:TemplateField HeaderStyle-HorizontalAlign="Center">
                    <HeaderTemplate>
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
              
            </Columns>
        </asp:GridView>
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
