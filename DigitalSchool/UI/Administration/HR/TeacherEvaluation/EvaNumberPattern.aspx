<%@ Page Title="Eva. Number Pattern" Language="C#" MasterPageFile="~/main.Master" AutoEventWireup="true" CodeBehind="EvaNumberPattern.aspx.cs" Inherits="DS.UI.Administration.HR.TeacherEvaluation.EvaNumberPattern" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
     <style>
        .tgPanel {
            width: 100%;
        }
        .tbl-controlPanel td:first-child{
            text-align: right;
            padding-right: 5px;
        }
        input[type="checkbox"]{
            margin: 7px;
        }
         .dataTables_length, .dataTables_filter {
          display: none;
          padding: 15px;
        }
        #tblClassList_info {
             display: none;
            padding: 15px;
        }
        #tblClassList_paginate {
            display: none;
            padding: 15px;
        }
        .no-footer {
           border-bottom: 1px solid #ecedee !important;
        }
         .txtWidth {
             width:50px;
             text-align:center;
             
         }
         .btnalt {
    color: white;
    background-color:#a6c1ee;
   
}
        #ckbAll {
    margin-top: 17px;
    display: block;
    position: absolute;
    margin-left: 27px;
    width: 15px;
    /* margin: 0 auto; */
}
        #ckbAll+label {
    margin-left: 10px;
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
                    <a id="A1" runat="server" href="~/Dashboard.aspx">
                        <i class="fa fa-dashboard"></i>
                        Dashboard
                    </a>
                </li>
                <li><a id="A2" runat="server" href="~/UI/Administration/AdministrationHome.aspx">Administration Module</a></li>
                <li><a id="A3" runat="server" href="~/UI/Administration/HR/hrHome.aspx">Human Resource Module</a></li>  
                <li><a id="A4" runat="server" href="~/UI/Administration/HR/TeacherEvaluation/EvaHome.aspx">Teacher Evaluation</a></li> 
                <li class="active">Set Number Pattern</li>              
            </ul>
            <!--breadcrumbs end -->
        </div>
    </div>
     <div class="">
        <div class="row">
            <div class="col-md-4">
                <h4 class="text-right" style="float:left">Number Pattern List</h4>
                 <div class="dataTables_filter_New" style="float: right;margin-right:0px;">
                     <label>
                         Search:
                         <input type="text" class="Search_New" placeholder="type here" />
                     </label>
                 </div>                
            </div>
            <div class="col-md-8"></div>
        </div>
        <div class="row">
            <div class="col-md-4">
                <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
                    <Triggers>
                        <asp:AsyncPostBackTrigger ControlID="btnSave" />
                    </Triggers>
                    <ContentTemplate>
                        <div class="tgPanel">
                            <%--<div id="divCategoryList" class="datatables_wrapper" runat="server" 
                            style="width: 100%; height: auto; max-height: 500px; overflow: auto; overflow-x: hidden;"></div>--%>
                            <asp:GridView ID="gvNumberPatternList" DataKeyNames="NumPatternID" CellPadding="3"
                                CssClass="table table-striped table-bordered table-condensed" HeaderStyle-BackColor="Black" HeaderStyle-ForeColor="White" runat="server" AutoGenerateColumns="false" OnRowCommand="gvNumberPatternList_RowCommand">
                                <Columns>
                                    <asp:TemplateField HeaderText="SL">
                                <ItemTemplate>
                                     <%# Container.DataItemIndex + 1 %>                                  
                                </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center"  />
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:TemplateField> 
                                    <asp:BoundField DataField="NumPattern" HeaderText="Number Pattern" />
                               
                                    <asp:TemplateField HeaderText="Edit">
                                <ItemTemplate>
                                    <asp:Button runat="server" CommandName="Alter" CommandArgument="<%# Container.DataItemIndex%> " ID="btnAlter" CssClass="btn btnalt" Text="Edit" />                              
                                </ItemTemplate>
                              
                            </asp:TemplateField>
                                </Columns>
                            </asp:GridView>
                        </div>
                        <asp:HiddenField ID="lblSubCategoryId" ClientIDMode="Static" runat="server" />
                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>
            <div class="col-md-8">
                <div class="tgPanel">
                <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                    <ContentTemplate>
                        <div class="tgPanelHead">Set Number Pattern</div>
                          <asp:TextBox ID="txtNumberPattern" runat="server" ClientIDMode="Static" CssClass="input form-control" placeholder="Enter Number Pattern Name"   Style="text-align:center" ></asp:TextBox> 
                        <div>
                            <asp:GridView ID="gvNumberPattern" DataKeyNames="SubCategoryID" CellPadding="3"  
     CssClass= "table table-striped table-bordered table-condensed"  runat="server" AutoGenerateColumns="false"
    >
    <Columns>
        <asp:BoundField DataField="Category" HeaderText="Category" />
          <asp:TemplateField HeaderText="SL">
                                <ItemTemplate>
                                     <%# Container.DataItemIndex + 1 %>                                  
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:TemplateField> 
        <asp:BoundField DataField="SubCategory" HeaderText="Sub Category" />  
        <asp:TemplateField HeaderText="Chosen">
            <HeaderTemplate>
                <asp:CheckBox ID="ckbAll" Text="Chosen" Checked="true" OnCheckedChanged="ckbAll_CheckedChanged" runat="server" ClientIDMode="Static" AutoPostBack="true" />
            </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:CheckBox runat="server" ID="ckbChosen" ClientIDMode="Static" Checked='<%# Eval("chk") %>' />                                 
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:TemplateField>
        <asp:TemplateField HeaderText="Full Number">
                                <ItemTemplate>
                                    <asp:TextBox ID="txtFullNumber" runat="server" ClientIDMode="Static" CssClass="input form-control txtWidth" Text='<%# Eval("FullNumber") %>'></asp:TextBox>  
                                 <cc1:FilteredTextBoxExtender  runat="server" FilterType="Custom,Numbers" ValidChars="."
                                     TargetControlID="txtFullNumber" />
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:TemplateField> 
        <asp:TemplateField HeaderText="Excellent">
                                <ItemTemplate>
                                    <asp:TextBox ID="txtExcellent" runat="server" ClientIDMode="Static" CssClass="input form-control txtWidth" Text='<%# Eval("Excellent") %>'></asp:TextBox> 
                                 <cc1:FilteredTextBoxExtender  runat="server" FilterType="Custom,Numbers" ValidChars="."
                                     TargetControlID="txtExcellent" />                                 
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:TemplateField> 
        <asp:TemplateField HeaderText="Good">
                                <ItemTemplate>
                                    <asp:TextBox ID="txtGood" runat="server" ClientIDMode="Static" CssClass="input form-control txtWidth" Text='<%# Eval("Good") %>'></asp:TextBox>    
                                 <cc1:FilteredTextBoxExtender  runat="server" FilterType="Custom,Numbers" ValidChars="."
                                     TargetControlID="txtGood" />                               
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:TemplateField> 
        <asp:TemplateField HeaderText="Medium">
                                <ItemTemplate>
                                   <asp:TextBox ID="txtMedium" runat="server" ClientIDMode="Static" CssClass="input form-control txtWidth" Text='<%# Eval("Medium") %>'></asp:TextBox>  
                                 <cc1:FilteredTextBoxExtender  runat="server" FilterType="Custom,Numbers" ValidChars="."
                                     TargetControlID="txtMedium" />                              
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:TemplateField>      
            <asp:TemplateField HeaderText="Weak">
                                <ItemTemplate>
                                   <asp:TextBox ID="txtWeak" runat="server" ClientIDMode="Static" CssClass="input form-control txtWidth" Text='<%# Eval("Weak") %>'></asp:TextBox>  
                                 <cc1:FilteredTextBoxExtender  runat="server" FilterType="Custom,Numbers" ValidChars="."
                                     TargetControlID="txtWeak" />                              
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:TemplateField>  
         <asp:TemplateField HeaderText="So Weak">
                                <ItemTemplate>
                                   <asp:TextBox ID="txtSoWeak" runat="server" ClientIDMode="Static" CssClass="input form-control txtWidth" Text='<%# Eval("SoWeak") %>'></asp:TextBox>  
                                 <cc1:FilteredTextBoxExtender  runat="server" FilterType="Custom,Numbers" ValidChars="."
                                     TargetControlID="txtSoWeak" />                              
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:TemplateField>  
    </Columns>
</asp:GridView>
                        </div>     
                        
                        <asp:Button ID="btnSave" CssClass="btn btn-primary" runat="server" Text="Save"  ClientIDMode="Static" OnClick="btnSave_Click" />
                        <asp:Button ID="btnReset" CssClass="btn btn-default" runat="server" Text="Reset"  ClientIDMode="Static" OnClick="btnReset_Click" />
                                         
                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>
            </div>
        </div>        
    </div> 
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ScriptContent" runat="server">
</asp:Content>
