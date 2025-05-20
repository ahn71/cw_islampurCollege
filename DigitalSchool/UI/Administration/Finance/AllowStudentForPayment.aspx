<%@ Page Title="" Language="C#" MasterPageFile="~/main.Master" AutoEventWireup="true" CodeBehind="AllowStudentForPayment.aspx.cs" Inherits="DS.UI.Administration.Finance.AllowStudentForPayment" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
<style>
    table#MainContent_gvstudentList {
    background: white;
}

    th {
    background: #8fbe42;
    color: white;
}
      
        .tgPanel{
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
                <li><a runat="server" href="~/UI/Administration/Finance/FinanceHome.aspx">Finance Module</a></li>
                <li><a runat="server" href="~/UI/Administration/Finance/FeeManaged/FeeHome.aspx">Fee Management</a></li>
                <li class="active">Allow student fo payment</li>
            </ul>
            <!--breadcrumbs end -->
        </div>
    </div>
           <div class="tgPanel">
               <div class="tgPanelHead">Allow Student for Payement </div>
             </div>
    <div class=" container">
 <div class="row">
       <div class="col-lg-3">
           <label>Batch</label>
           <asp:DropDownList runat="server" ID="ddlBatch" CssClass="form-control" OnSelectedIndexChanged="ddlBatch_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList>
       </div>
          <div class="col-lg-3">
           <label>Group</label>
           <asp:DropDownList runat="server" ID="ddlGroup" CssClass="form-control" OnSelectedIndexChanged="ddlGroup_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList>
       </div>

         <div class="col-lg-3">
           <label>Section</label>
           <asp:DropDownList runat="server" ID="ddlSection" CssClass="form-control"></asp:DropDownList>
       </div>
             <div class="col-lg-3">
           <label style="opacity:0;">Section</label><br />
           <asp:Button runat="server" ID="btnSearch"  OnClick="btnSearch_Click" Text="SEARCH" CssClass="btn btn-success" />
       </div>
    

       <div class="col-log-3">

       </div>
   </div>
        <div class="row">
         <div class="col-lg-3">
           <label>Category</label>
           <asp:DropDownList runat="server" ID="ddlCategory" CssClass="form-control"></asp:DropDownList>
       </div>
        </div>
    </div>
  
    <asp:UpdatePanel runat="server" ID="up1">
        <ContentTemplate>
               <asp:GridView runat="server" ID="gvstudentList" CssClass="table" AutoGenerateColumns="false" style="margin-top:20px" DataKeyNames="BatchId,StudentId,ClsSecId,ClsGrpID" OnRowDataBound="gvstudentList_RowDataBound"  OnRowCommand="gvstudentList_RowCommand">
        <Columns>
              <asp:TemplateField HeaderText="SL">
                    <ItemTemplate>
                        <%# Container.DataItemIndex + 1 %>
                    </ItemTemplate>
                </asp:TemplateField>
             <asp:BoundField DataField="AdmissionNo" HeaderText="AdmissionNo" Visible="true" ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center" />
             <asp:BoundField DataField="FullName" HeaderText="FullName" Visible="true" ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center" />
             <asp:BoundField DataField="ClassName" HeaderText="ClassName" Visible="true" ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center" />

                <asp:BoundField DataField="GroupName" HeaderText="GroupName" />
         
                <asp:BoundField DataField="SectionName" HeaderText="SectionName"/>
                <asp:BoundField DataField="ShiftName" HeaderText="ShiftName" Visible="true" ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center" />
                <asp:BoundField DataField="RollNo" HeaderText="RollNo" Visible="true" ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center" />
                
                           
            
            <asp:TemplateField HeaderText="Remarks">
                <ItemTemplate>
                    <asp:TextBox ID="txRemarks" runat="server" ></asp:TextBox>
                </ItemTemplate>
               </asp:TemplateField>

                <asp:TemplateField HeaderText="Select" ItemStyle-HorizontalAlign="Center">
                                                                    <HeaderTemplate>
                                                                        <asp:CheckBox runat="server" ItemStyle-HorizontalAlign="Center" ID="hdChk" Text="All" Checked="true" AutoPostBack="True"  OnCheckedChanged="hdChk_CheckedChanged"/><br />
                                                                    </HeaderTemplate>
                                                                    <ItemTemplate>
                                                                        <asp:CheckBox ID="chkStatus" ItemStyle-HorizontalAlign="Center" runat="server" AutoPostBack="true" Checked="true" OnCheckedChanged="chkStatus_CheckedChanged"/>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
               <asp:TemplateField HeaderText="Action">
            <ItemTemplate>
                <asp:Button ID="btnSave" runat="server" Text="SUBMIT" CommandName="SaveRow" CommandArgument='<%# Container.DataItemIndex %>' CssClass="btn btn-primary" />
            </ItemTemplate>
        </asp:TemplateField>
        </Columns>
    </asp:GridView>
        </ContentTemplate>
    </asp:UpdatePanel>

   <asp:Button style="float:right;" runat="server" ID="btnSubmit" CssClass="btn btn-success" Text="SUBMIT" OnClick="btnSubmit_Click" />
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ScriptContent" runat="server">
   

</asp:Content>
