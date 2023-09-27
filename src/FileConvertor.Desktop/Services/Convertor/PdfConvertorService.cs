using FileConvertor.Desktop.Dtos.Convertor;
using FileConvertor.Desktop.Interfaces.Convertor;
using Spire.Pdf;
using System.Threading.Tasks;

namespace FileConvertor.Desktop.Services.Convertor;

public class PdfConvertorService : IPdfConvertorService
{
    public PdfConvertorService()
    {
    }

    public async Task<(bool Result, string path)> PdfToExcelAsync(ConvertorDto dto)
    {
        await Task.Run(() =>
        {
            PdfDocument pdf = new PdfDocument();
            pdf.LoadFromFile(dto.fileName);
            pdf.SaveToFile(dto.fileType, FileFormat.XLSX);
        });

        return (Result: true, path: dto.fileType);
    }

    public async Task<(bool Result, string path)> PdfToWordAsync(ConvertorDto dto)
    {
        await Task.Run(() =>
        {
            PdfDocument pdf = new PdfDocument();
            pdf.LoadFromFile(dto.fileName);
            pdf.SaveToFile(dto.fileType, FileFormat.DOCX);
        });

        return (Result: true, path: dto.fileType);
    }
}
