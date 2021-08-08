using DesktopClient.Models;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using OfficeOpenXml;
using System;
using System.Data;
using System.IO;
using System.Threading.Tasks;

namespace DesktopClient.Services
{
    /*
     EPPlus package used for export to Excel file operations.
     Its noncommercial usage is free. Otherwise lisence is required
     */
    public class ExcelExportService : IExportService
    {
        private static FileInfo saveFilePath;
        private readonly ILogger logger;

        public ExcelExportService()
        {
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
            logger = App.ServiceProvider.GetRequiredService<ILoggerProvider>().CreateLogger("ExcelExportService");
        }

        public async Task<string> ExportAsync(DataTable dataTable)
        {
            try
            {
                var folder = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
                saveFilePath = new FileInfo(Path.Combine(folder, "export.xlsx"));
                DeleteIfExists(saveFilePath);

                using (var package = new ExcelPackage(saveFilePath))
                {
                    var ws = package.Workbook.Worksheets.Add("ExprortResults");
                    var range = ws.Cells["A1"].LoadFromDataTable(dataTable, true, OfficeOpenXml.Table.TableStyles.Medium4);
                    range.AutoFitColumns();

                    await package.SaveAsync();
                }

                logger.LogInformation("Exported successfully");
                return null;
            }

            catch (Exception ex)
            {
                logger.LogError(ex, "Error while saving data to Excel");
                return ex.Message;
            }
           
        }

        private static void DeleteIfExists(FileInfo file)
        {
            if (file.Exists)
                file.Delete();
        }
    }
}
