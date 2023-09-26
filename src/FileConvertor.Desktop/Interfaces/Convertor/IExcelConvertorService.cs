using FileConvertor.Desktop.Dtos.Convertor;
using System.Threading.Tasks;

namespace FileConvertor.Desktop.Interfaces.Convertor;

public interface IExcelConvertorService
{
    public Task<(bool Result, string path)> ExcelToWordAsync(ConvertorDto dto);

    public Task<(bool Result, string path)> ExcelToPdfAsync(ConvertorDto dto);
}
