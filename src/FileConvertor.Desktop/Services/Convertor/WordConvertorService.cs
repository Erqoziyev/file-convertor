using FileConvertor.Desktop.Dtos.Convertor;
using FileConvertor.Desktop.Interfaces.Convertor;
using Spire.Doc;
using Spire.Doc.Documents;
using Spire.Doc.Fields;
using Spire.Xls;
using System;
using System.Drawing;
using System.Threading.Tasks;

namespace FileConvertor.Desktop.Services.Convertor;

public class WordConvertorService : IWordConvertorService
{
    public WordConvertorService()
    {
    }

    public async Task<(bool Result, string path)> WordToExcelAsync(ConvertorDto dto)
    {
        await Task.Run(() =>
        {
            var doc = new Spire.Doc.Document();
            doc.LoadFromFile(dto.fileName);
            var wb = new Workbook();
            wb.Worksheets.Clear();
            Worksheet worksheet = wb.CreateEmptySheet("WordToExcel");
            int row = 1;
            int column = 1;
            foreach (Section section in doc.Sections)
            {
                foreach (DocumentObject documentObject in section.Body.ChildObjects)
                {
                    if (documentObject is Paragraph)
                    {
                        CellRange cell = worksheet.Range[row, column];
                        Paragraph paragraph = documentObject as Paragraph;
                        CopyTextAndStyle(cell, paragraph);
                        row++;
                    }
                    if (documentObject is Table)
                    {
                        Table table = documentObject as Table;
                        int currentRow = ExportTableInExcel(worksheet, row, table);
                        row = currentRow;
                    }
                }
            }
            worksheet.AllocatedRange.AutoFitRows();
            worksheet.AllocatedRange.AutoFitColumns();
            worksheet.AllocatedRange.IsWrapText = true;
            wb.SaveToFile(dto.fileType, ExcelVersion.Version2013);
        });
        
        return (Result: true, path: dto.fileType);
    }

    private static int ExportTableInExcel(Worksheet worksheet, int row, Table table) 
    {
        CellRange cell;
        int column;

        foreach (TableRow tbRow in table.Rows)
        {
            column = 1;
            foreach (TableCell tbCell in tbRow.Cells)
            {
                cell = worksheet.Range[row, column];
                cell.BorderAround(LineStyleType.Thin, Color.Black);
                CopyContentInTable(tbCell, cell);
                column++;
            }
            row++;
        }

        return row;
    }

    private static void CopyContentInTable(TableCell tbCell, CellRange cell)
    {
        Paragraph newPara = new Paragraph(tbCell.Document);
        for (int i = 0; i<tbCell.ChildObjects.Count; i++)
        {
            DocumentObject documentObject = tbCell.ChildObjects[i];
            if (documentObject is Paragraph)
            {
                Paragraph paragraph = documentObject as Paragraph;
                foreach (DocumentObject cObj in paragraph.ChildObjects)
                {
                    newPara.ChildObjects.Add(cObj.Clone());
                }
                if (i<tbCell.ChildObjects.Count - 1)
                {
                    newPara.AppendText("\n");
                }
            }
        }
        CopyTextAndStyle(cell, newPara);
    }

    private static void CopyTextAndStyle(CellRange cell, Paragraph paragraph)
    {
        RichText richText = cell.RichText;
        richText.Text = paragraph.Text;
        int startIndex = 0;
        foreach (DocumentObject documentObject in paragraph.ChildObjects)
        {

            if (documentObject is TextRange)
            {
                TextRange textRange = documentObject as TextRange;
                string fontName = textRange.CharacterFormat.FontName;
                bool isBold = textRange.CharacterFormat.Bold;
                Color textColor = textRange.CharacterFormat.TextColor;
                float fontSize = textRange.CharacterFormat.FontSize;
                string textRangeText = textRange.Text;
                int strLength = textRangeText.Length;
                ExcelFont font = cell.Worksheet.Workbook.CreateFont();
                font.Color = textColor;
                font.IsBold = isBold;
                font.Size = fontSize;
                font.FontName = fontName;
                int endIndex = startIndex + strLength;
                richText.SetFont(startIndex, endIndex, font);
                startIndex += strLength;
            }
            if (documentObject is DocPicture)
            {
                DocPicture picture = documentObject as DocPicture;
                cell.Worksheet.Pictures.Add(cell.Row, cell.Column, picture.Image);
                cell.Worksheet.SetRowHeightInPixels(cell.Row, 1, picture.Image.Height);
            }
        }
        switch (paragraph.Format.HorizontalAlignment)
        {
            case HorizontalAlignment.Left:
                cell.Style.HorizontalAlignment = HorizontalAlignType.Left;
                break;
            case HorizontalAlignment.Center:
                cell.Style.HorizontalAlignment = HorizontalAlignType.Center;
                break;
            case HorizontalAlignment.Right:
                cell.Style.HorizontalAlignment = HorizontalAlignType.Right;
                break;
        }
    }


    public async Task<(bool Result, string path)> WordToPdfAsync(ConvertorDto dto)
    {
        await Task.Run(() =>
        {
            using (var converter = new GroupDocs.Conversion.Converter(dto.fileName))
            {
                var convertOptions = converter.GetPossibleConversions()["pdf"].ConvertOptions;

                converter.Convert(dto.fileType, convertOptions);
            }
        });

        return (Result: true, path: dto.fileType);
    }
}
