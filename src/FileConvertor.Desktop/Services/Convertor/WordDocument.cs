using System;

namespace FileConvertor.Desktop.Services.Convertor
{
    internal class WordDocument
    {
        private string fileName;

        public WordDocument(string fileName)
        {
            this.fileName = fileName;
        }

        internal void ConvertToExcel(string v)
        {
            throw new NotImplementedException();
        }

        internal void ConvertToPdf(string v)
        {
            throw new NotImplementedException();
        }
    }
}