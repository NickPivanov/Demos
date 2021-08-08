using System.Data;
using System.Threading.Tasks;

namespace DesktopClient.Models
{
    public interface IExportService
    {
        Task<string> ExportAsync(DataTable dataTable);
    }
}
