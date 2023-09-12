<%@ Page Title="" Language="C#" MasterPageFile="~/Student.Master" AutoEventWireup="true" CodeBehind="StudentFinance.aspx.cs" Inherits="DS.UI.StudentManage.StudentFinance" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style>
        .form-inline {
            margin-left:31px;
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
                <li class="active">Finance</li>                               
            </ul>
            <!--breadcrumbs end -->
        </div>
    </div>
   <div class="row"> 
        <div class="col-md-12">
        <div class="tgPanel" style="width:100%">        
              <div class="row"> 
                  <div class="col-md-4">                    
                       <div class="form-inline">
                          <div class="form-group">
                                    <label for="exampleInputName2">Fees Category</label>
                               <asp:DropDownList ID="ddlFeesCat" runat="server" Width="260px" ClientIDMode="Static" CssClass="input controlLength"></asp:DropDownList>
                             </div>                          
                          </div>  
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
                    <a id="A1" runat="server" onclick="return validateDropDown();" onserverclick="A1_ServerClick">
                        <div class="mini-stat sub_mini_stat clearfix btn3d btn custom_menu_btn">
                            <span>
                                <img width="30" alt="feeCategorie" src="../../../../Images/moduleicon/Finance/fee Categorie.ico">
                            </span>
                            <span>Category Wise Fees</span>
                        </div>
                    </a>
                </div>
                <div class="col-md-3">
                    <a id="A2" runat="server" onserverclick="A2_ServerClick">
                        <div class="mini-stat sub_mini_stat clearfix btn3d btn custom_menu_btn">
                            <span>
                                <img width="30" alt="feeCategorie" src="../../../../Images/moduleicon/Finance/fee Categorie.ico">
                            </span>
                            <span>Due List</span>                            
                        </div>
                    </a>                  
                </div>               
                <div class="col-md-3">
                    <a id="A3" runat="server"  onserverclick="A3_ServerClick">
                        <div class="mini-stat sub_mini_stat clearfix btn3d btn custom_menu_btn">
                            <span>
                                <img width="30" alt="finemenagment" src="../../../../Images/moduleicon/Finance/fine menagment.ico">
                            </span>
                            <span>Fine List</span>
                        </div>
                    </a>
                </div>
                 <div class="col-md-3">
                    <a id="A4" runat="server" href="~/UI/StudentManage/PaymentDetails.aspx">
                         <div class="mini-stat sub_mini_stat clearfix btn3d btn custom_menu_btn">
                            <span>
                                <img width="30" alt="feeCategorie" src="../../../../Images/moduleicon/Finance/fee Categorie.ico">
                            </span>
                            <span>Payment Details</span>                            
                        </div>
                    </a>                  
                </div>                             
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ScriptContent" runat="server">
    <script type="text/javascript">
        function validateDropDown() {           
            if (validateCombo('ddlFeesCat', 0, 'Select Fees Category') == false) return false;
            return true;
        }
    </script>
</asp:Content>
