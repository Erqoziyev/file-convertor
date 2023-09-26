using FileConvertor.Desktop.Dtos.Convertor;
using System.Threading.Tasks;

namespace FileConvertor.Desktop.Interfaces.Convertor;

public interface IWordConvertorService
{
    public Task<(bool Result, string path)> WordToExcelAsync(ConvertorDto dto);

    public Task<(bool Result, string path)> WordToPdfAsync(ConvertorDto dto);
}
