<%@ Page Title="" Language="C#" MasterPageFile="~/main.Master" AutoEventWireup="true" CodeBehind="BlockStudentForPayment.aspx.cs" Inherits="DS.UI.Administration.Finance.BlockStudentForPayment" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style>
        .tgPanel{
            width:100%;
        }
         table#MainContent_gvstudentList {
    background: white;
}

    th {
    background: #8fbe42;
    color: white;
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
                <li class="active">Block student for payment</li>
            </ul>
            <!--breadcrumbs end -->
        </div>
    </div>
           <div class="tgPanel">
               <div class="tgPanelHead">Block Student for Payement </div>
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

         <div class="col-lg-2">
           <label>Section</label>
           <asp:DropDownList runat="server" ID="ddlSection" CssClass="form-control"></asp:DropDownList>
       </div>
       <div class="col-lg-2">
           <label>Exam</label>
           <asp:DropDownList runat="server" ID="ddlExam" CssClass="form-control"></asp:DropDownList>
       </div>
             <div class="col-lg-2">
           <label style="opacity:0;">Section</label>
           <asp:Button runat="server" ID="btnSearch"  OnClick="btnSearch_Click" Text="SEARCH" CssClass="btn btn-success" />
       </div>
    


   </div>
        <div class="row">
         <div class="col-lg-3">
           <label>Category</label>
           <asp:DropDownList runat="server" ID="ddlCategory" CssClass="form-control"></asp:DropDownList>
       </div>
      
        </div>
    </div>
    <asp:UpdatePanel runat="server" ID="up2">
        <ContentTemplate>
                <asp:GridView runat="server" ID="gvstudentList" CssClass="table" AutoGenerateColumns="false" style="margin-top:20px" DataKeyNames="BatchId,StudentId,ClsSecId,ClsGrpID" OnRowDataBound="gvstudentList_RowDataBound">
        <Columns>
              <asp:TemplateField HeaderText="SL">
                    <ItemTemplate>
                        <%# Container.DataItemIndex + 1 %>
                    </ItemTemplate>
                </asp:TemplateField>
             <asp:BoundField DataField="AdmissionNo" HeaderText="AdmissionNo" Visible="true" ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center" />
             <asp:BoundField DataField="FullName" HeaderText="FullName" Visible="true" ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center" />
             <asp:BoundField DataField="ClassName" HeaderText="ClassName" Visible="true" ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center" />
         
                <asp:BoundField DataField="SectionName" HeaderText="SectionName"/>
                <asp:BoundField DataField="RollNo" HeaderText="RollNo" Visible="true" ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center" />
                <asp:BoundField DataField="Grade" HeaderText="Grade" Visible="true" ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center" />
                <asp:BoundField DataField="NumberOfFailSubjectTotal" HeaderText="NumberOfFailSubjectTotal" Visible="true" ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center" />
                
                           
            
            <asp:TemplateField HeaderText="Remarks">
                <ItemTemplate>
                    <asp:TextBox ID="txRemarks" runat="server" ></asp:TextBox>
                </ItemTemplate>
               </asp:TemplateField>

                <asp:TemplateField HeaderText="Select" ItemStyle-HorizontalAlign="Center">
                                                                    <HeaderTemplate>
                                                                        <asp:CheckBox runat="server" ItemStyle-HorizontalAlign="Center" ID="hdChk" Text="All" Checked="false" AutoPostBack="True"  OnCheckedChanged="hdChk_CheckedChanged"/><br />
                                                                    </HeaderTemplate>
                                                                    <ItemTemplate>
                                                                        <asp:CheckBox ID="chkStatus" ItemStyle-HorizontalAlign="Center" runat="server" AutoPostBack="true" Checked="true" OnCheckedChanged="chkStatus_CheckedChanged"/>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
               
        </Columns>
    </asp:GridView>
        </ContentTemplate>
    </asp:UpdatePanel>
 <asp:Button style="float:right" runat="server" ID="btnSubmit" CssClass="btn btn-success" Text="SUBMIT"  OnClick="btnSubmit_Click"/>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ScriptContent" runat="server">
</asp:Content>
