<%@ Page Title="" Language="C#" MasterPageFile="~/main.Master" AutoEventWireup="true" CodeBehind="ExamineeSelection.aspx.cs" Inherits="DS.UI.Academic.Examination.ExamineeSelection" %>
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
                <li class="active">Examinee Selection</li>
            </ul>
            <!--breadcrumbs end -->
        </div>
    </div>
    <div class="row" id="divSearchPanel" runat="server">
        <div class="col-md-12">
            <div class="tgPanel">
                <div class="tgPanelHead">
                </div>
                <asp:UpdatePanel ID="UpdatePanel2" runat="server" UpdateMode="Conditional">
                    <Triggers>
                        <asp:AsyncPostBackTrigger ControlID="ddlBatch" />
                        <asp:AsyncPostBackTrigger ControlID="ddlGroup" />
                        <asp:AsyncPostBackTrigger ControlID="ddlExamId" />                        
                        <asp:AsyncPostBackTrigger ControlID="gvExamRoutine" />                        
                    </Triggers>
                    <ContentTemplate>
                        <div class="row tbl-controlPanel" >
                            <div class="col-sm-8 col-sm-offset-2" runat="server" id="tblOp">
                                <div class="form-group row">
                                    <label class="col-sm-2">Shift</label>
                                    <div class="col-sm-10">
                                        <asp:DropDownList ID="ddlShift" runat="server" CssClass="input controlLengthMin form-control" ClientIDMode="Static">                                    
                                    </asp:DropDownList>
                                        </div>                               
                                </div>
                                <div class="form-group row">
                                    <label class="col-sm-2">Batch</label>                                  
                                       <div class="col-sm-10"> 
                                            <asp:DropDownList ID="ddlBatch" runat="server" ClientIDMode="Static" CssClass="input controlLengthMin form-control" OnSelectedIndexChanged="ddlBatch_SelectedIndexChanged" AutoPostBack="True">
                                    </asp:DropDownList>
                                    </div>                                   
                                </div>
                                <div class="form-group row">
                                    <label class="col-sm-2">Group</label>
                                    <div class="col-sm-10">
                                        <asp:DropDownList ID="ddlGroup" runat="server" ClientIDMode="Static" AutoPostBack="true" OnSelectedIndexChanged="ddlGroup_SelectedIndexChanged" CssClass="input controlLengthMin form-control" Enabled="false" >
                                    </asp:DropDownList>
                                        </div>                                   
                                </div>                               
                                <div class="form-group row">
                                    <label class="col-sm-2">Exam Id</label>
                                    <div class="col-sm-10">
                                        <asp:DropDownList ID="ddlExamId" runat="server" ClientIDMode="Static" CssClass="input controlLength form-control"
                                         AutoPostBack="True" OnSelectedIndexChanged="ddlExamId_SelectedIndexChanged">
                                        <asp:ListItem Value="0">...Select Exam Id...</asp:ListItem>
                                    </asp:DropDownList>
                                    </div>
                                </div>
                                <div class="form-group row">
                                    <label class="col-sm-2">Section</label>
                                    <div class="col-sm-10">
                                        <asp:DropDownList ID="ddlSection" runat="server" ClientIDMode="Static" CssClass="input controlLength form-control"
                                         AutoPostBack="True" OnSelectedIndexChanged="ddlSection_SelectedIndexChanged">         
                                    </asp:DropDownList>
                                    </div>
                                </div>                               
                                <div class="form-group row">                                    
                                    <div class="col-sm-8 col-sm-offset-2">     
                                        <asp:CheckBox Visible="false" runat="server" ID="chkForCoutAsFinalResult" />
                                        <asp:Button ID="btnSave" Text="Save" ClientIDMode="Static" runat="server" 
                                          OnClick="btnSave_Click" CssClass="btn btn-primary" OnClientClick="return validateInputs();"/>            
                                        <asp:Button ID="btnPrintPreview" runat="server" Text="Print Full Marksheet" CssClass="btn btn-primary" ClientIDMode="Static" OnClick="btnPrintPreview_Click" />                             
                                    </div>
                                    <div class="col-sm-8 col-sm-offset-2">
                             <asp:UpdateProgress ID="UpdateProgress1" runat="server">
                                <ProgressTemplate>                           
                                    <span style="font-family: 'Times New Roman'; font-size: 1.2em; padding:5px; color: #1fb5ad; font-weight: bold; float: left">
                                        <p>Wait please,It's working...</p>
                                    </span>
                                    <img class="LoadingImg_" src="../../../../../AssetsNew/images/input-spinner.gif" />
                                    <div class="clearfix"></div>
                                </ProgressTemplate>                        
                            </asp:UpdateProgress>
                                    </div>                                    
                                </div>                         
                            </div>
                        </div>
                                                                      
                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>

            <div class="tgPanel">
                 <asp:UpdatePanel ID="up2" runat="server" UpdateMode="Conditional">
         <Triggers>
                <asp:AsyncPostBackTrigger ControlID="ddlExamId" />
                <asp:AsyncPostBackTrigger ControlID="btnSave" />
            </Triggers>
            <ContentTemplate>
                 <asp:GridView ID="gvExamRoutine" runat="server" AutoGenerateColumns="false" DataKeyNames="ExamineeID,StudentId,ClsSecID,ClsGrpID,BatchID" 
                     CssClass="table table-bordered" BackColor="White" HeaderStyle-BackColor="Black" HeaderStyle-ForeColor="White" OnRowCommand="gvExamRoutine_RowCommand">
                     <%--<PagerStyle CssClass="gridview" />--%>
            <Columns>
                  <asp:TemplateField HeaderText="SL"> 
        <ItemTemplate>
             <%#Container.DataItemIndex+1 %>
        </ItemTemplate>
    </asp:TemplateField>
                <asp:BoundField DataField="RollNo" HeaderText="Roll No" />
               <asp:BoundField DataField="FullName" HeaderText="Name" />
                <asp:BoundField DataField="SectionName" HeaderText="Section" />               
                <asp:BoundField DataField="GroupName" HeaderText="Group" />                
                <asp:BoundField DataField="Status" HeaderText="Status" />                                 
                 <asp:TemplateField HeaderText="Status" >
                     <HeaderTemplate>
      <asp:CheckBox ID="ckbAll" ClientIDMode="Static" Text="All" runat="server" AutoPostBack="true" OnCheckedChanged="ckbAll_CheckedChanged"
          />
    </HeaderTemplate>
                              <ItemTemplate>
    <asp:CheckBox ID="ckbStatus" ClientIDMode="Static" runat="server" 

       Checked='<%# Convert.ToBoolean(Eval("Status")) %>'/>
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
   
</asp:Content>