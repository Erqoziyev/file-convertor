using FileConvertor.Desktop.Dtos.Convertor;
using System.Threading.Tasks;

namespace FileConvertor.Desktop.Interfaces.Convertor;

public interface IConvertService
{
    public Task<(bool Result, string path)> ConvertAsync(ConvertorDto dto);
}