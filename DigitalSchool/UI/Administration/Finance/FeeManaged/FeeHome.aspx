<%@ Page Title="Fee Managed" Language="C#" MasterPageFile="~/main.Master" AutoEventWireup="true" CodeBehind="FeeHome.aspx.cs" Inherits="DS.UI.Administration.Finance.FeeManaged.FeeHome" %>

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
                    <a runat="server" href="~/Dashboard.aspx">
                        <i class="fa fa-dashboard"></i>
                        Dashboard
                    </a>
                </li>
                <li><a runat="server" href="~/UI/Administration/AdministrationHome.aspx">Administration Module</a></li>
                <li><a runat="server" href="~/UI/Administration/Finance/FinanceHome.aspx">Finance Module</a></li>
                <li class="active">Fee Management</li>
            </ul>
            <!--breadcrumbs end -->
        </div>
    </div>
    <div class="row">
        <div class="col-md-3">
            <a runat="server" href="~/UI/Administration/Finance/FeeManaged/AddParticular.aspx">
               <div class="mini-stat sub_mini_stat clearfix btn3d btn custom_menu_btn_administration">
                    <span>
                        <img width="45" src="../../../../Images/moduleicon/Finance/Particular.ico" alt="Particular" />
                    </span>
                    <span>Particulars</span>
                </div>
            </a>
        </div>
        <div class="col-md-3">
            <a runat="server" href="~/UI/Administration/Finance/FeeManaged/FeesCategoriesInfo.aspx">
                <div class="mini-stat sub_mini_stat clearfix btn3d btn custom_menu_btn_administration">
                    <span>
                        <img width="45" src="../../../../Images/moduleicon/Finance/fee Categorie.ico" alt="feeCategorie" />
                    </span>
                    <span>Fee Category</span>
                </div>
            </a>
        </div>
        <div class="col-md-3">
            <a runat="server" href="~/UI/Administration/Finance/FeeManaged/AdmissionFeesCategories.aspx">
               <div class="mini-stat sub_mini_stat clearfix btn3d btn custom_menu_btn_administration">
                    <span>
                        <img width="45" src="../../../../Images/moduleicon/Finance/admission fee.ico" alt="admissionfee" />
                    </span>
                    <span>Admission Fee Category</span>
                </div>
            </a>
        </div>
        <div class="col-md-3">
            <a runat="server" href="~/UI/Administration/Finance/FeeManaged/ParticularCategories.aspx">
                 <div class="mini-stat sub_mini_stat clearfix btn3d btn custom_menu_btn_administration">
                    <span>
                        <img width="45" src="../../../../Images/moduleicon/Finance/Particular fee.ico" alt="Particularfee" />
                    </span>
                    <span>Fee Particulars</span>
                </div>
            </a>
        </div>
        <div class="col-md-3">
            <a runat="server" href="~/UI/Administration/Finance/FeeManaged/AdmissionAssignParticular.aspx">
                <div class="mini-stat sub_mini_stat clearfix btn3d btn custom_menu_btn_administration">
                    <span>
                        <img width="45" src="../../../../Images/moduleicon/Finance/admission fee particular.ico" alt="admissionparticular" />
                    </span>
                    <span>Admission Fee Particulars</span>
                </div>
            </a>
        </div>
       <%-- <div class="col-md-3">
            <a runat="server" href="~/UI/Administration/Finance/FeeManaged/DateOfPayment.aspx">
                 <div class="mini-stat sub_mini_stat clearfix btn3d btn custom_menu_btn">
                    <span>
                        <img width="30" src="../../../../Images/moduleicon/Finance/fine menagment.ico" alt="finemenagment" />
                    </span>
                    <span>Set Fee Date</span>
                </div>
            </a>
        </div>--%>
        <div class="col-md-3">
            <a runat="server" href="~/UI/Administration/Finance/FeeManaged/DiscountSet.aspx">
               <div class="mini-stat sub_mini_stat clearfix btn3d btn custom_menu_btn_administration">
                    <span>
                        <img width="45" src="../../../../Images/moduleicon/Finance/discount.ico" alt="discount" />
                    </span>
                    <span>Discount</span>
                </div>
            </a>
        </div>
        <div class="col-md-3">
            <a runat="server" href="~/UI/Administration/Finance/FeeManaged/FeesCollection.aspx">
                 <div class="mini-stat sub_mini_stat clearfix btn3d btn custom_menu_btn_administration">
                    <span>
                        <img width="45" src="../../../../Images/moduleicon/Finance/collection.ico" alt="collection" />
                    </span>
                    <span>Fee Collection</span>
                </div>
            </a>
        </div>
        <div class="col-md-3">
            <a runat="server" href="~/UI/Administration/Finance/FeeManaged/AdmFeesCollection.aspx">
                <div class="mini-stat sub_mini_stat clearfix btn3d btn custom_menu_btn_administration">
                    <span>
                        <img width="45" src="../../../../Images/moduleicon/Finance/collection details.ico" alt="collectiondetails" />
                    </span>
                    <span>Admission Collection</span>
                </div>
            </a>
        </div>
        <div class="col-md-3">
            <a runat="server" href="~/UI/Administration/Finance/FeeManaged/FeeCollectionDetails.aspx">
                 <div class="mini-stat sub_mini_stat clearfix btn3d btn custom_menu_btn_administration">
                    <span>
                        <img width="45" src="../../../../Images/moduleicon/Finance/collection details.ico" alt="collectiondetails" />
                    </span>
                    <span>Fee Collection Details</span>
                </div>
            </a>
        </div>
        <div class="col-md-3">
            <a id="A1" runat="server" href="~/UI/Administration/Finance/FeeManaged/AdmCollectionDetails.aspx">
               <div class="mini-stat sub_mini_stat clearfix btn3d btn custom_menu_btn_administration">
                    <span>
                        <img width="45" src="../../../../Images/moduleicon/Finance/admission collection details.ico" alt="admissioncollectiondetails" />
                    </span>
                    <span>Admission Collection Details</span>
                </div>
            </a>
        </div>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ScriptContent" runat="server">
</asp:Content>
