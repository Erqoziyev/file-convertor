using FileConvertor.Desktop.Dtos.Convertor;
using FileConvertor.Desktop.Enums;
using FileConvertor.Desktop.Interfaces.Convertor;
using System.IO;
using System.Threading.Tasks;

namespace FileConvertor.Desktop.Services.Convertor;
public class ConvertService : IConvertService
{
    private readonly IExcelConvertorService _excelConvertor;
    private readonly IWordConvertorService _wordConvertor;
    private readonly IPdfConvertorService _pdfConvertor;

    public ConvertService()
    {
        this._excelConvertor = new ExcelConvertorService();
        this._wordConvertor = new WordConvertorService();
        this._pdfConvertor = new PdfConvertorService();
    }

    public async Task<(bool Result, string path)> ConvertAsync(ConvertorDto dto)
    {
        FileInfo fileInfo = new FileInfo(dto.fileName);
        string extension = fileInfo.Extension.Substring(1);

        FileInfo fileInfo2 = new FileInfo(dto.fileType);
        string fileType = fileInfo2.Extension.Substring(1);

        if (extension == FileType.xlsx.ToString())
        {
            if (fileType == FileType.pdf.ToString())
            {
                var result = await _excelConvertor.ExcelToPdfAsync(dto);

                return result;
            }
            else if (fileType == FileType.docx.ToString())
            {
                var result = await _excelConvertor.ExcelToWordAsync(dto);

                return result;
            }
            else
            {
                return (false, "");
            }
        }
        else if(extension == FileType.docx.ToString())
        {
            if(fileType == FileType.pdf.ToString())
            {
                var result = await _wordConvertor.WordToPdfAsync(dto);

                return result;
            }
            else if(fileType == FileType.xlsx.ToString())
            {
                var result = await _wordConvertor.WordToExcelAsync(dto);

                return result;
            }
            else
            {
                return (false, "");
            }
        }
        else if(extension == FileType.pdf.ToString())
        {
            if(fileType == FileType.docx.ToString())
            {
                var result = await _pdfConvertor.PdfToWordAsync(dto);

                return result;
            }
            else if(fileType == FileType.xlsx.ToString())
            {
                var result = await _pdfConvertor.PdfToExcelAsync(dto);

                return result;
            }
            else 
            { 
                return (false, "");
            }
        }
        else 
        {
            return (false, "");
        }
    }
}
