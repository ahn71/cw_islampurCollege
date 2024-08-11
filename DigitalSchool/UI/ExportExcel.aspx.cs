using DS.DAL;
using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using static DS.UI.ExportExcel;

namespace DS.UI
{
    public partial class ExportExcel : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnImportExcel_Click(object sender, EventArgs e)
        {
            if (exlFileUpload.HasFile)
            {
                try
                {
                    string filePath = Server.MapPath("~/App_Data/") + exlFileUpload.FileName;
                    exlFileUpload.SaveAs(filePath);

                    List<ExcelData> excelDataList = ReadExcelFile(filePath);
                   

                  
                }
                catch (Exception ex)
                {
                    
                }
            }
            else
            {
                
            }
        }

        private List<ExcelData> ReadExcelFile(string filePath)
        {
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
            var excelDataList = new List<ExcelData>();

            FileInfo fileInfo = new FileInfo(filePath);
            using (ExcelPackage package = new ExcelPackage(fileInfo))
            {
                ExcelWorksheet worksheet = package.Workbook.Worksheets[0];
                int rowCount = worksheet.Dimension.Rows;
                int colCount = worksheet.Dimension.Columns;
                var headerColumnIndex = new Dictionary<string, int>();

               
                for (int col = 1; col <= colCount; col++)
                {
                    var headerText = worksheet.Cells[1, col].Text.Trim();
                    if (!string.IsNullOrEmpty(headerText))
                    {
                        headerColumnIndex[headerText] = col;
                    }
                }

             
                for (int row = 2; row <= rowCount; row++)
                {
                   
                    var studentIdText = worksheet.Cells[row, headerColumnIndex["StudentId"]].Text.Trim();
                    var registrationNumberText = worksheet.Cells[row, headerColumnIndex["RegistrationNo"]].GetValue<string>();

                    if (string.IsNullOrEmpty(studentIdText) && string.IsNullOrEmpty(registrationNumberText))
                    {
                        break; 
                    }

                    if (!string.IsNullOrEmpty(studentIdText))
                    {
                        var studentId = int.Parse(studentIdText);
                        var registrationNumber = registrationNumberText;
                        string query = "UPDATE currentStudentInfo SET RegistrationNo =" + registrationNumber + " WHERE StudentId = " + studentId + "";
                        CRUD.ExecuteNonQuery(query);
                    }
                }
            }

            return excelDataList;
        }





        public class ExcelData
        {
            public int StudentId { get; set; }
            public string RegistrationNumber { get; set; }
        }
    }
}