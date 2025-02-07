using iText.Kernel.Pdf;
using iText.Layout;
using iText.Layout.Element;
using OfficeOpenXml;

namespace CloudPOS.Utils
{
    public static class ReportHelper
    {

        public static byte[] ExportToExcel<T>(IList<T> table, string filename)
        {
            using ExcelPackage pack= new ExcelPackage();
            ExcelWorksheet ws = pack.Workbook.Worksheets.Add(filename);
            ws.Cells["A1"].LoadFromCollection(table, true, OfficeOpenXml.Table.TableStyles.Light1);
            return pack.GetAsByteArray();
        }
        public static byte[] ExportToPdf<T>(IList<T> table, string fileName)
        {
            using (MemoryStream stream = new MemoryStream())
            {
                PdfWriter writer = new PdfWriter(stream);
                PdfDocument pdf = new PdfDocument(writer);
                Document document = new Document(pdf);

                document.Add(new Paragraph(fileName).SetFontSize(16));  // Add file name as a title in pdf
                if (table.Any())
                {
                    var properties = typeof(T).GetProperties(); // Create a table with columns equal to the number of properties
                    iText.Layout.Element.Table pdfTable = new iText.Layout.Element.Table(properties.Length);

                    foreach (var headers in properties) // Add Header row
                    {
                        pdfTable.AddHeaderCell(headers.Name);
                    }
                    foreach(var item in table) // Add Data Rows
                    {
                        foreach(var prop in properties)
                        {
                            var value = prop.GetValue(item)?.ToString() ?? "";
                            pdfTable.AddCell(value);
                        }
                    }
                    document.Add(pdfTable);
                }
                document.Close();
                return stream.ToArray();
            }
       
            
        }
    }
}
