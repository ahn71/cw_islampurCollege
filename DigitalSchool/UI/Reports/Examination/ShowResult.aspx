<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ShowResult.aspx.cs" Inherits="DS.UI.Reports.Examination.ShowResult" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">

<head runat="server">

    <title></title>
    <link rel="stylesheet" href="https://cdn.jsdelivr.net/npm/bootstrap@5.3.0/dist/css/bootstrap.min.css"/>
    
    <!-- Optional: Include Bootstrap Icons CSS (if needed) -->
    <link rel="stylesheet" href="https://cdn.jsdelivr.net/npm/bootstrap-icons/font/bootstrap-icons.css"/>
     <script src="https://cdn.jsdelivr.net/npm/bootstrap@5.3.0/dist/js/bootstrap.bundle.min.js"></script>
    


    <style>
@media print {
	@page {
		margin-top: 0;
		margin-bottom: 0;
	}
	body {
		padding-top: 72px;
		padding-bottom: 72px ;
	}
    #download_btn{
        display:none;
    }
}


    </style>

    <script src="https://cdnjs.cloudflare.com/ajax/libs/jquery/3.6.0/jquery.min.js"></script>


       <script src="https://cdnjs.cloudflare.com/ajax/libs/jspdf/2.4.0/jspdf.umd.min.js"></script>

</head>
<body>
    <form id="form1" runat="server">
        <div class="container" id="pdf-content">
            <div class="row py-3">
                <div class="col-lg-12">
                     <div class="result_header">
                     <h4 class="text-center fw-bold">Institution: Islampur College (EIIN: 109857)</h4>
              
                   
                   <asp:Label runat="server" ID="lblExamName" CssClass="fw-bold d-block text-center"></asp:Label>
                  
                   <asp:Label runat="server" CssClass="text-center d-block border-bottom pb-3 fw-bold" ID="lblExamGroupName"></asp:Label>
                
                   <asp:Label ID="lblResultStatus" CssClass="d-block mt-3" runat="server"></asp:Label>
               
                   <asp:Label ID="lblShowpassResult" CssClass="d-block mt-3" runat="server"></asp:Label>
               
                   <asp:Label style="display:inline-block;margin:10px 0;line-height:30px;" ID="lblFailedData" runat="server"></asp:Label>

                         <button id="download_btn" class="border-0 btn btn-success" onclick="window.print(); download">Download</button>

              </div>
                </div>
            </div>

     <%--       <div id="pdfContainerS">
      <h2>I am nipun</h2>
    </div>--%>
    


        </div>
       <%--<button type="button" onclick="downloadPDF()">Download PDF</button>--%>
    </form>
</body>
 

<%--      <script>
          function downloadPDF() {
              const pdf = new jsPDF();

              const source = document.querySelector('.pdf-content'); 

              pdf.fromHTML(source, 15, 15, {
                  width: 180
              });

              pdf.save('download.pdf');
          }
         
      </script>--%>
    

 
</html>
