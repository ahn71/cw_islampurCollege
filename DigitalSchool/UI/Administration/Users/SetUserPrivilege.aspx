<%@ Page Title="" Language="C#" MasterPageFile="~/main.Master" AutoEventWireup="true" CodeBehind="SetUserPrivilege.aspx.cs" Inherits="DS.UI.Administration.Users.SetUserPrivilege" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .CheckBoxs label {
        padding-left: 2px;
             margin-right: 3px;
             margin-left: 2px;
             margin-top:15px
        }
        .HDHorizantalAlignment {
        margin-left:12px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:UpdatePanel ID="uplMessage" runat="server">
        <ContentTemplate>
            <p class="message" id="lblMessage" clientidmode="Static" runat="server"></p>
        </ContentTemplate>
    </asp:UpdatePanel>
    <div class="col-lg-4"></div>
    <div class="col-lg-4">
        <asp:DropDownList ID="ddlCurrentUserList" runat="server" ClientIDMode="Static" Width="65%" CssClass="input controlLength" AutoPostBack="True" OnSelectedIndexChanged="ddlCurrentUserList_SelectedIndexChanged">
        </asp:DropDownList>
    </div>
    <asp:UpdatePanel ID="up4" runat="server" UpdateMode="Always">
         <ContentTemplate>
             <div class="col-lg-4" id="divTitle" runat="server"></div>
     
      </ContentTemplate>

    </asp:UpdatePanel>
    <div class="col-lg-12">
         <asp:UpdatePanel ID="UpdatePanel4" runat="server">
                            <Triggers>                                
                                <asp:AsyncPostBackTrigger ControlID="ddlCurrentUserList" />   
                               <%-- <asp:AsyncPostBackTrigger ControlID="divTitle" />        --%>                  
                            </Triggers>
                            <ContentTemplate>
                      
                            <div id="showParticular" runat="server" style="display:block;width:100%; height:100% auto; background-color:white; top:60px"  >
                                <div style="background-color:#23282C;color:white; font-weight:bold"><h3>Chosen Page For Set User Privilege</h3></div>  
                                                
                               <asp:Panel runat="server" ScrollBars="Vertical" Width="100%" Height="100%">
                                  <asp:GridView ID="gvPageInfoList"  runat="server" Width="100%" DataKeyNames="PageNameId" CssClass="table table-bordered" AutoGenerateColumns="False" Height="250px" >

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
                                                            <asp:Label ID="lblSubName" style="float:left"  runat="server"
                                                                Text='<%# DataBinder.Eval(Container.DataItem, "PageTitle")%>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Module">
                                                        <ItemTemplate>
                                                            <asp:Label ID="txtPassMarks" style="width:50px;text-align:start;"  runat="server"
                                                                Text='<%# DataBinder.Eval(Container.DataItem, "ModuleType")%>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>   

                                                     <asp:TemplateField HeaderText="View" >
                                                         <HeaderTemplate>
                                                             <asp:CheckBox runat="server" ClientIDMode="Static" ID="hdChkViewAction"  style="margin-left:42%" AutoPostBack="True" OnCheckedChanged="hdChkViewAction_CheckedChanged"  /><br />
                                                            <asp:Label runat="server" ID="lblHDTitle" Text="View"></asp:Label>
                                                         </HeaderTemplate>
                                                        <ItemTemplate>
                                                            <asp:CheckBox ID="chkViewAction" runat="server"  AutoPostBack="true" Checked='<%#bool.Parse(Eval("ViewAction").ToString())%>' OnCheckedChanged="chkViewAction_CheckedChanged" />
                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                     <asp:TemplateField HeaderText="Save">
                                                         <HeaderTemplate>
                                                             <asp:CheckBox runat="server" ID="hdChkSaveAction"  style="margin-left:42%" AutoPostBack="true" OnCheckedChanged="hdChkSaveAction_CheckedChanged" /><br />
                                                            <asp:Label runat="server" ID="lblSaveTitle" Text="Save"></asp:Label>
                                                         </HeaderTemplate>
                                                        <ItemTemplate>
                                                            <asp:CheckBox ID="chkSaveAction" runat="server"  AutoPostBack="true" Checked='<%#bool.Parse(Eval("SaveAction").ToString())%>' OnCheckedChanged="chkSaveAction_CheckedChanged" />
                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                     <asp:TemplateField HeaderText="Update">
                                                         <HeaderTemplate>
                                                             <asp:CheckBox runat="server" ID="hdChkUpdateAction"  style="margin-left:42%" AutoPostBack="true" OnCheckedChanged="hdChkUpdateAction_CheckedChanged"  /><br />
                                                            <asp:Label runat="server" ID="lblChkUpdate" Text="Update"></asp:Label>
                                                         </HeaderTemplate>
                                                        <ItemTemplate>
                                                            <asp:CheckBox ID="chkUpdateAction" runat="server"  AutoPostBack="true" Checked='<%#bool.Parse(Eval("UpdateAction").ToString())%>' OnCheckedChanged="chkUpdateAction_CheckedChanged" />
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                     <asp:TemplateField HeaderText="Delete">
                                                         <HeaderTemplate>
                                                             <asp:CheckBox runat="server" ID="hdChkDeleteAction"  style="margin-left:42%" AutoPostBack="true" OnCheckedChanged="hdChkDeleteAction_CheckedChanged"  /><br />
                                                            <asp:Label runat="server" ID="lblDeleteTitle" Text="Delete" ></asp:Label>
                                                         </HeaderTemplate>
                                                        <ItemTemplate>
                                                            <asp:CheckBox ID="chkDeleteAction" runat="server"  AutoPostBack="true" Checked='<%#bool.Parse(Eval("DeleteAction").ToString())%>' OnCheckedChanged="chkDeleteAction_CheckedChanged" />
                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                      <asp:TemplateField HeaderText="Generate">
                                                          <HeaderTemplate>
                                                             <asp:CheckBox runat="server" ID="hdChkGenerateAction"  style="margin-left:42%" AutoPostBack="true" OnCheckedChanged="hdChkGenerateAction_CheckedChanged"  /><br />
                                                           <asp:Label runat="server" ID="lblGenerateTitle" Text="Generate"></asp:Label>
                                                         </HeaderTemplate>
                                                        <ItemTemplate>
                                                            <asp:CheckBox ID="chkGenerateAction" runat="server"  AutoPostBack="true" Checked='<%#bool.Parse(Eval("GenerateAction").ToString())%>' OnCheckedChanged="chkGenerateAction_CheckedChanged" />
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
</asp:Content>
