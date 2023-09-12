<%@ Page Title="" Language="C#" MasterPageFile="~/main.Master" AutoEventWireup="true" CodeBehind="ChangePageInfo.aspx.cs" Inherits="DS.UI.Administration.Users.ChangePageInfo" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
      <style type="text/css">
         .table tr th{
            background-color: #23282C;
            color: white;
        }
    </style>
    <script src="../scripts/jquery-1.8.2.js"></script>
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

</script>
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

    <div class="col-lg-4"></div>
    <div class="col-lg-4">
        <asp:DropDownList ID="ddlUserTypeList" runat="server" ClientIDMode="Static" Width="65%" CssClass="input controlLength" AutoPostBack="True" OnSelectedIndexChanged="ddlUserTypeList_SelectedIndexChanged">
        </asp:DropDownList>
    </div>
    <div class="col-lg-4">
        <div class="dataTables_filter_New" style="float: right;margin-right:0px;">
                     <label>
                         Search:
                         <input type="text" class="Search_New"  placeholder="type here" />
                     </label>
                 </div>
    </div>
    <div class="col-lg-12">
        <asp:UpdatePanel ID="UpdatePanel4" runat="server">
            <Triggers>
                <asp:AsyncPostBackTrigger ControlID="ddlUserTypeList" />
                <%--      <asp:AsyncPostBackTrigger ControlID="btnSetPage" />  --%>
            </Triggers>
            <ContentTemplate>

                <div id="showParticular" runat="server" style="display: block; width: 100%; height: 100% auto; background-color: white; top: 60px">                  
                    <div style="background-color: #23282C; color: white">
                        <h3>Chosen page for setup user privilege by user type</h3>
                    </div>

                    <asp:Panel runat="server" ScrollBars="Vertical" Width="100%" Height="100%">
                        <asp:GridView ID="gvPageInfoList" runat="server" Width="100%" DataKeyNames="PageNameId" CssClass="table table-bordered" AutoGenerateColumns="False" Height="250px" OnRowDataBound="gvPageInfoList_RowDataBound">

                            <RowStyle HorizontalAlign="Center" />
                            <Columns>
                                <asp:TemplateField HeaderText="S.No" ItemStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                        <asp:HiddenField ID="hideSubId" runat="server"
                                            Value='<%# DataBinder.Eval(Container.DataItem, "PageNameId")%>' />
                                        <%# Container.DataItemIndex+1%>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Page Name">
                                    <ItemTemplate>
                                        <asp:Label ID="lblSubName" Style="float: left" runat="server"
                                            Text='<%# DataBinder.Eval(Container.DataItem, "PageTitle")%>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Module">
                                    <ItemTemplate>
                                        <asp:Label ID="txtPassMarks" Style="width: 50px; text-align: start;" runat="server"
                                            Text='<%# DataBinder.Eval(Container.DataItem, "ModuleType")%>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="View">
                                    <HeaderTemplate>
                                        <asp:CheckBox runat="server" ClientIDMode="Static" ID="hdChkViewAction" Style="margin-left: 42%" AutoPostBack="True" OnCheckedChanged="hdChkViewAction_CheckedChanged" /><br />
                                        <asp:Label runat="server" ID="lblHDTitle" Text="View"></asp:Label>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <asp:CheckBox ID="chkViewAction" runat="server" AutoPostBack="true" Checked='<%#bool.Parse(Eval("ViewAction").ToString())%>' OnCheckedChanged="chkViewAction_CheckedChanged" />
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Save">
                                    <HeaderTemplate>
                                        <asp:CheckBox runat="server" ID="hdChkSaveAction" Style="margin-left: 42%" AutoPostBack="true" OnCheckedChanged="hdChkSaveAction_CheckedChanged" /><br />
                                        <asp:Label runat="server" ID="lblSaveTitle" Text="Save"></asp:Label>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <asp:CheckBox ID="chkSaveAction" runat="server" AutoPostBack="true" Checked='<%#bool.Parse(Eval("SaveAction").ToString())%>' OnCheckedChanged="chkSaveAction_CheckedChanged" />
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Update">
                                    <HeaderTemplate>
                                        <asp:CheckBox runat="server" ID="hdChkUpdateAction" Style="margin-left: 42%" AutoPostBack="true" OnCheckedChanged="hdChkUpdateAction_CheckedChanged" /><br />
                                        <asp:Label runat="server" ID="lblChkUpdate" Text="Update"></asp:Label>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <asp:CheckBox ID="chkUpdateAction" runat="server" AutoPostBack="true" Checked='<%#bool.Parse(Eval("UpdateAction").ToString())%>' OnCheckedChanged="chkUpdateAction_CheckedChanged" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Delete">
                                    <HeaderTemplate>
                                        <asp:CheckBox runat="server" ID="hdChkDeleteAction" Style="margin-left: 42%" AutoPostBack="true" OnCheckedChanged="hdChkDeleteAction_CheckedChanged" /><br />
                                        <asp:Label runat="server" ID="lblDeleteTitle" Text="Delete"></asp:Label>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <asp:CheckBox ID="chkDeleteAction" runat="server" AutoPostBack="true" Checked='<%#bool.Parse(Eval("DeleteAction").ToString())%>' OnCheckedChanged="chkDeleteAction_CheckedChanged" />
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Generate">
                                    <HeaderTemplate>
                                        <asp:CheckBox runat="server" ID="hdChkGenerateAction" Style="margin-left: 42%" AutoPostBack="true" OnCheckedChanged="hdChkGenerateAction_CheckedChanged" /><br />
                                        <asp:Label runat="server" ID="lblGenerateTitle" Text="Generate"></asp:Label>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <asp:CheckBox ID="chkGenerateAction" runat="server" AutoPostBack="true" Checked='<%#bool.Parse(Eval("GenerateAction").ToString())%>' OnCheckedChanged="chkGenerateAction_CheckedChanged" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="All">
                                    <HeaderTemplate>
                                        <asp:CheckBox runat="server" ID="hdChkAllAction" Style="margin-left: 42%" AutoPostBack="true" OnCheckedChanged="hdChkAllAction_CheckedChanged" /><br />
                                        <asp:Label runat="server" ID="lblCheckAll" Text="All"></asp:Label>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <asp:CheckBox ID="chkAllAction" runat="server" AutoPostBack="true" Checked='<%#bool.Parse(Eval("AllAction").ToString())%>' OnCheckedChanged="chkAllAction_CheckedChanged" />
                                    </ItemTemplate>
                                </asp:TemplateField>

                            </Columns>
                        </asp:GridView>
                        <%--  </div>--%>
                    </asp:Panel>

                </div>

                </div>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ScriptContent" runat="server">
    <script type="text/javascript">
        $(document).ready(function () {
            $(document).on("keyup", '.Search_New', function () {
                searchTable($(this).val(), 'MainContent_gvPageInfoList', '');
            });
        });
    </script>
</asp:Content>
