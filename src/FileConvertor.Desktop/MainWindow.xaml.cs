using FileConvertor.Desktop.Dtos.Convertor;
using FileConvertor.Desktop.Interfaces.Convertor;
using FileConvertor.Desktop.Services.Convertor;
using Microsoft.Win32;
using System;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace FileConvertor.Desktop;

/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : Window
{

    public string path = "";
    public int id;
    private readonly IConvertService _service;

    public MainWindow()
    {
        InitializeComponent();
        this._service = new ConvertService();
    }

    private OpenFileDialog GetFileDialog()
    {
        OpenFileDialog openFileDialog = new OpenFileDialog();
        return openFileDialog;
    }

    private void pdfBtn_Click(object sender, RoutedEventArgs e)
    {
        if (path != "" && path != null)
        {
            Button button = (Button)sender;
            if (button.Name == "pdfBtn")
            {
                FileInfo fileInfo = new FileInfo(path);
                if (fileInfo.Extension != ".pdf")
                {
                    SaveFileDialog saveFileDialog = new SaveFileDialog();
                    saveFileDialog.Filter = "PDF Fayllari (*.pdf)|*.pdf";
                    loader.Visibility = Visibility.Visible;
                    saveFileDialog.ShowDialog();
                    ConvertorDto dto = new ConvertorDto();
                    dto.fileName = path;
                    dto.fileType = saveFileDialog.FileName;
                    _service.ConvertAsync(dto);

                    loader.Visibility = Visibility.Collapsed;
                    MessageBox.Show("Siz tanlagan fayl PDF ga o'tkazib saqlandi");
                    txtFileName.Text = "";
                    path = "";
                }
                else
                {
                    MessageBox.Show("Siz tanlagan fayl shundoq ham PDF");
                }
            }
            else if (button.Name == "exelBtn")
            {
                FileInfo fileInfo = new FileInfo(path);
                if (fileInfo.Extension != ".xlsl")
                {
                    SaveFileDialog saveFileDialog = new SaveFileDialog();
                    saveFileDialog.Filter = "Excel Fayllari (*.xlsx)|*.xlsx";
                    loader.Visibility = Visibility.Visible;
                    saveFileDialog.ShowDialog();
                    ConvertorDto dto = new ConvertorDto();
                    dto.fileName = path;
                    dto.fileType = saveFileDialog.FileName;
                    _service.ConvertAsync(dto);
                    loader.Visibility = Visibility.Collapsed;
                    MessageBox.Show("Siz tanlagan fayl EXCEL ga o'tkazib saqlandi");
                    txtFileName.Text = "";
                    path = "";
                }
                else
                {
                    MessageBox.Show("Siz tanlagan fayl shundoq ham Excel");
                }
            }
            else if (button.Name == "docBtn")
            {
                FileInfo fileInfo = new FileInfo(path);
                if (fileInfo.Extension != ".docx")
                {
                    SaveFileDialog saveFileDialog = new SaveFileDialog();
                    saveFileDialog.Filter = "Microsoft Word Fayllari (*.docx)|*.docx";
                    loader.Visibility = Visibility.Visible;
                    saveFileDialog.ShowDialog();
                    ConvertorDto dto = new ConvertorDto();
                    dto.fileName = path;
                    dto.fileType = saveFileDialog.FileName;
                    _service.ConvertAsync(dto);
                    loader.Visibility = Visibility.Collapsed;
                    MessageBox.Show("Siz tanlagan fayl DOCUMENT ga o'tkazib saqlandi");
                    txtFileName.Text = "";
                    path = "";
                }
                else
                {
                    MessageBox.Show("Siz tanlagan fayl shundoq ham Document");
                }
            }
        }
        else
        {
            MessageBox.Show("Mos fayl turini tanlang!");
        }
    }

    private void ChangeBtn_Click(object sender, RoutedEventArgs e)
    {
        var openFileDialog = GetFileDialog();
        if (openFileDialog.ShowDialog() == true)
        {
            string pathName = openFileDialog.FileName;
            openFileDialog.FileName = "";
            string extension = System.IO.Path.GetExtension(pathName).ToLower();

            if (extension == ".pdf")
            {
                path = pathName;
                txtFileName.Text = pathName;
            }
            else if (extension == ".docx" || extension == ".doc")
            {
                path = pathName;
                txtFileName.Text = pathName;
            }
            else if (extension == ".xlsx" || extension == ".xls")
            {
                path = pathName;
                txtFileName.Text = pathName;
            }
            else
            {
                MessageBox.Show("Mos fayl turini tanlang!");
            }
        }
    }
}
