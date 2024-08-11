<%@ Page Title="" Language="C#" MasterPageFile="~/main.Master" AutoEventWireup="true" CodeBehind="ResultPublish.aspx.cs" Inherits="DS.UI.Academic.Examination.Result_Publish" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
<asp:UpdatePanel ID="uplMessage" runat="server">
        <ContentTemplate>
            <p class="message" id="lblMessage" clientidmode="Static" runat="server"></p>
        </ContentTemplate>
    </asp:UpdatePanel>
    <asp:HiddenField ID="lblExId" ClientIDMode="Static" runat="server" />
    <div class="row">
        <div class="col-md-12">
            <!--breadcrumbs start -->
            <ul class="breadcrumb">
                <li>
                   <%-- <a runat="server" href="~/Dashboard.aspx">--%>
                    <a runat="server" id="aDashboard">
                        <i class="fa fa-dashboard"></i>
                        Dashboard
                    </a>
                </li>                
                 <%--<li><a runat="server" href="~/UI/Academic/AcademicHome.aspx">Academic Module</a></li>--%>                
                <%--<li><a runat="server" href="~/UI/Academic/Examination/ExamHome.aspx">Examination Module</a></li>--%>
                 <li>  <a runat="server" id="aAcademicHome" >Academic Module</a></li>
                <li><a runat="server" id="aExamHome">Examination Module</a></li> 
                <li class="active">Result Publish</li>
            </ul>
            <!--breadcrumbs end -->
        </div>
    </div>
    <div class="">
     <%--   <div class="row">
            <div class="col-md-2"></div>
            <div class="col-md-4">
                <h4 class="text-right"  style="float: left;">Exam Type List</h4>
                <div class="dataTables_filter_New" style="float: right;margin-right:0px;">
                    <label>
                        Search:
                        <input type="text" class="Search_New" placeholder="type here" />
                    </label>
                </div>
            </div>
            <div class="col-md-6"></div>
        </div>--%>
        <div class="row">
            <div class="col-md-2"></div>
            <div class="col-md-8">
                <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
                    <Triggers>
                     
                    </Triggers>
                    <ContentTemplate>
                        <div class="tgPanel">
                            <asp:GridView ID="gvExamList" CssClass="table table-bordered" BackColor="White" HeaderStyle-BackColor="Black" HeaderStyle-ForeColor="White"  runat="server"  Width="100%" AutoGenerateColumns="False" DataKeyNames="ExInSl" OnRowCommand="gvExamList_RowCommand">
<%--<HeaderStyle BackColor="#0057AE" Font-Bold="True" Font-Size="14px" ForeColor="White" Height="28px"></HeaderStyle>--%>
                              
                       <RowStyle HorizontalAlign="Center" />
                         <Columns>
                            <asp:BoundField DataField="SL"  HeaderText="SL" Visible="false"  ItemStyle-Height="28px" >
<ItemStyle Height="28px"></ItemStyle>
                             </asp:BoundField>
                              <asp:BoundField DataField="ExInId" HeaderStyle-HorizontalAlign="Left" HeaderText="Exam ID">
                             </asp:BoundField>
                        <asp:BoundField DataField="ExName" HeaderStyle-HorizontalAlign="Left" HeaderText="Exam">
                             </asp:BoundField>
                             <asp:BoundField DataField="GroupName" HeaderStyle-HorizontalAlign="Left" HeaderText="Group">                          
                             </asp:BoundField> 
                             <asp:BoundField DataField="Status" HeaderStyle-HorizontalAlign="Left" HeaderText="Status">                          
                             </asp:BoundField>
                            <asp:TemplateField HeaderText="Publish" >
                                <ItemTemplate>
                                    <asp:Button ID="btnPublish" runat="server" CommandName="Publish" CommandArgument="<%#((GridViewRow)Container).RowIndex%>" Text="Publish" CssClass="btn btn-primary" OnClientClick="return confirm('Are you sure to publish?');" />    </ItemTemplate>
                            </asp:TemplateField>
                               <asp:TemplateField HeaderText="Unpublish" >
                                <ItemTemplate>
                                    <asp:Button ID="btnUnPublish" runat="server" CommandName="Unpublish" CommandArgument="<%#((GridViewRow)Container).RowIndex%>" Text="Unpublish" CssClass="btn btn-danger" OnClientClick="return confirm('Are you sure to unpublish?');" />    </ItemTemplate>
                            </asp:TemplateField>

                        </Columns>
                    </asp:GridView>
                        <div id="divExamList" class="datatables_wrapper" runat="server"
                            style="width: 100%; height: auto; max-height: 350px; overflow: auto; overflow-x: hidden;">
                        </div>
                            </div>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>
         <div class="col-md-2"></div>
        </div>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ScriptContent" runat="server">
</asp:Content>
