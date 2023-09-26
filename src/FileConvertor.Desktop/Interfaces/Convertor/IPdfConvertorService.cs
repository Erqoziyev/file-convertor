using FileConvertor.Desktop.Dtos.Convertor;
using System.Threading.Tasks;

namespace FileConvertor.Desktop.Interfaces.Convertor;

public interface IPdfConvertorService
{
    public Task<(bool Result, string path)> PdfToWordAsync(ConvertorDto dto);

    public Task<(bool Result, string path)> PdfToExcelAsync(ConvertorDto dto);
}
