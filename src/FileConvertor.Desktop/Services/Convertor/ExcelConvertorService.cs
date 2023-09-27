using FileConvertor.Desktop.Dtos.Convertor;
using FileConvertor.Desktop.Interfaces.Convertor;
using GroupDocs.Conversion.Options.Convert;
using System.Threading.Tasks;

namespace FileConvertor.Desktop.Services.Convertor;

public class ExcelConvertorService : IExcelConvertorService
{
    public ExcelConvertorService()
    {
        
    }

    public async Task<(bool Result, string path)> ExcelToPdfAsync(ConvertorDto dto)
    {
        await Task.Run(() =>
        {
            using (var converter = new GroupDocs.Conversion.Converter(dto.fileName))
            {
                var options = new PdfConvertOptions();
                converter.Convert(dto.fileType, options);
            }
        });

        return (Result: true, path: dto.fileType);
    }
    public async Task<(bool Result, string path)> ExcelToWordAsync(ConvertorDto dto)
    {
        await Task.Run(() =>
        {
            using (var converter = new GroupDocs.Conversion.Converter(dto.fileName))
            {
                var options = new WordProcessingConvertOptions();
                converter.Convert(dto.fileType, options);
            }
        });
        
        return (Result: true, path: dto.fileType);
    }
}
