using ConverserLibrary;
using ConverserLibrary.Interfaces;
using Microsoft.Extensions.Logging;

namespace ConverserWF
{
    public partial class MainForm : Form
    {
        private readonly ILogger<MainForm> _logger;
        private readonly IExcelParserService _parser;
        private readonly ICitySeparatorService _separator;
        private readonly IYandexFeedCreatorService _yandexFeedCreatorService;
        private readonly ITwoGisFeedCreatorService _twoGisFeedCreatorService;
        private readonly IVKFeedCreatorService _vkFeedCreatorService;
        // private readonly IInformationService _information;

        /// <summary>
        /// �������������� ��������� ������ MainService.
        /// </summary>
        /// <param name="logger">��������� �������</param>
        /// <param name="parser">��������� ������� �������</param>
        /// <param name="separator">��������� ������� ����������</param>
        /// <param name="yandexFeedCreatorService">��������� ������� ��� �������� XML-����� ������</param>
        /// <param name="twoGisFeedCreatorService">��������� ������� ��� �������� XML-����� 2���</param>
        public MainForm(ILogger<MainForm> logger,
            IExcelParserService parser,
            ICitySeparatorService separator,
            IYandexFeedCreatorService yandexFeedCreatorService,
            ITwoGisFeedCreatorService twoGisFeedCreatorService,
            IVKFeedCreatorService vkFeedCreatorService)
        {
            InitializeComponent();
            _logger = logger;
            _parser = parser;
            _separator = separator;
            _yandexFeedCreatorService = yandexFeedCreatorService;
            _twoGisFeedCreatorService = twoGisFeedCreatorService;
            _vkFeedCreatorService = vkFeedCreatorService;

            YandexFeedButton.Enabled = false;
            VKFeedButton.Enabled = false;
            TwoGisFeedButton.Enabled = false;

            _yandexFeedCreatorService.XmlCreated += OnXmlCreated;
        }

        private void OnXmlCreated(object sender, XmlCreatedEventArgs e)
        {
            //SendInformation($"������ ���� {e.FileName}.");
        }

        private static string filePathImport;

        private static string filePathExport;

        /// <summary>
        /// ���������� ������� Click ��� ������ YandexFeedButton.
        /// </summary>
        /// <param name="sender">������, ��������� �������.</param>
        /// <param name="e">��������� ������� Click.</param>
        private void YandexFeedButton_Click(object sender, EventArgs e)
        {
            HandleFeedButtonClick(_yandexFeedCreatorService.CreateXml);
        }

        /// <summary>
        /// ���������� ������� Click ��� ������ VKFeedButton.
        /// </summary>
        /// <param name="sender">������, ��������� �������.</param>
        /// <param name="e">��������� ������� Click.</param>
        private void VKFeedButton_Click(object sender, EventArgs e)
        {
            HandleFeedButtonClick(_vkFeedCreatorService.CreateXml);
        }

        /// <summary>
        /// ���������� ������� Click ��� ������ TwoGisFeedButton.
        /// </summary>
        /// <param name="sender">������, ��������� �������.</param>
        /// <param name="e">��������� ������� Click.</param>
        private void TwoGisFeedButton_Click(object sender, EventArgs e)
        {
            HandleFeedButtonClick(_twoGisFeedCreatorService.CreateXml);
        }

        /// <summary>
        /// ���������� ��� ������ ��������� �����. ��������� �������, ���������������
        /// ��������� ������ CreateXml.
        /// </summary>
        /// <param name="createXml">����� �������� XML-������</param>
        private void HandleFeedButtonClick(Action<string, CitySeparatorResult> createXml)
        {
            _logger.LogInformation($"����� ���������. ����: {filePathImport}");

            // ������� ������, ��������� ������ ���� �������  
            var products = _parser.GetXLSXFile(filePathImport);

            // �������� ������ �� �������
            var cityDictionary = _separator.SeparateByCity(products);

            // �������� �����
            createXml(Path.GetDirectoryName(filePathExport), cityDictionary);

            _logger.LogInformation("��������� XML-������ ���������.");
        }

        /// <summary>
        /// ���������� ������� DragEnter ��� ��������, ����������� ������������� �����.
        /// </summary>
        /// <param name="sender">������, ��������� �������.</param>
        /// <param name="e">��������� ������� DragEnter.</param>
        private void OnDragEnter(object sender, DragEventArgs e)
        {
            string[] files = (string[])e.Data.GetData(DataFormats.FileDrop);

            if (files.Length > 0)
            {
                filePathImport = files[0];

                if (IsExcelFile(filePathImport))
                {
                    e.Effect = DragDropEffects.Copy;
                    Cursor = Cursors.Default;
                }
                else
                {
                    e.Effect = DragDropEffects.None;
                    Cursor = Cursors.Cross;
                }
            }
        }

        /// <summary>
        /// ���������� ������� DragDrop ��� ��������, ������������� ��� �������������� ����� � ������� ��������.
        /// </summary>
        /// <param name="sender">������, ��������� �������.</param>
        /// <param name="e">��������� ������� DragDrop.</param>
        private void OnDragDrop(object sender, DragEventArgs e)
        {
            string[] files = (string[])e.Data.GetData(DataFormats.FileDrop);
            if (files.Length > 0)
            {
                filePathImport = files[0];  

                if (string.IsNullOrEmpty(filePathExport))
                {
                    filePathExport = filePathImport;
                    BrowseDirectoryExportField.Text = Path.GetDirectoryName(filePathImport);
                }

                ProcessFile(filePathImport);

                BrowseDirectoryImportField.Text = filePathImport;
            }
        }

        /// <summary>
        /// ���������� ������� Click ��� ��������, ������������� ��� ����� �� ������� BrowseFileImportDirectory.
        /// </summary>
        /// <param name="sender">������, ��������� �������.</param>
        /// <param name="e">��������� ������� Click.</param>
        private void BrowseFileImportDirectory_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.Filter = "����� Excel (*.xlsx;*.xls)|*.xlsx;*.xls|��� ����� (*.*)|*.*";
                openFileDialog.Title = "�������� ���� Excel";

                DialogResult dialogResult = openFileDialog.ShowDialog();

                if (dialogResult == DialogResult.OK)
                {
                    filePathImport = openFileDialog.FileName;

                    if (string.IsNullOrEmpty(filePathExport))
                    {
                        filePathExport = filePathImport;
                        BrowseDirectoryExportField.Text = Path.GetDirectoryName(filePathImport);
                    }

                    ProcessFile(filePathImport);

                    BrowseDirectoryImportField.Text = filePathImport;
                }
                else
                {
                    BrowseDirectoryImportField.Text = !string.IsNullOrEmpty(filePathImport) ? 
                                                      filePathImport : "";
                }
            }
        }

        /// <summary>
        /// ���������� ������� Click ��� ��������, ������������� ��� ����� �� ������� BrowseExportDirectory.
        /// </summary>
        /// <param name="sender">������, ��������� �������.</param>
        /// <param name="e">��������� ������� Click.</param>
        private void BrowseExportDirectory_Click(object sender, EventArgs e)
        {
            using (FolderBrowserDialog folderBrowserDialog = new FolderBrowserDialog())
            {
                folderBrowserDialog.SelectedPath = BrowseDirectoryExportField.Text;

                DialogResult result = folderBrowserDialog.ShowDialog();

                if (result == DialogResult.OK)
                {
                    BrowseDirectoryExportField.Text = folderBrowserDialog.SelectedPath;
                }

                filePathExport = folderBrowserDialog.SelectedPath;
            }
        }

        /// <summary>
        /// ��������� ���������� �����.
        /// </summary>
        /// <param name="selectedFile">���� � ���������� �����.</param>
        private void ProcessFile(string selectedFile)
        {
            if (IsExcelFile(selectedFile))
            {
                YandexFeedButton.Enabled = true;
                VKFeedButton.Enabled = true;
                TwoGisFeedButton.Enabled = true;
            }
            else
            {
                MessageBox.Show($"��������� ���� �� �������� ������ Excel:\n{selectedFile}");
                YandexFeedButton.Enabled = false;
                VKFeedButton.Enabled = false;
                TwoGisFeedButton.Enabled = false;
            }
        }

        /// <summary>
        /// ���������� ������� Click ��� �������� ������ ������ � ���������� �����.
        /// </summary>
        // <param name="sender">������, ��������� �������.</param>
        /// <param name="e">��������� ������� Click.</param>
        public void FieldDataResetButton_Click(object sender, EventArgs e)
        {
            filePathExport = null;
            filePathImport = null;
            BrowseDirectoryExportField.Text = "";
            BrowseDirectoryImportField.Text = "";
            YandexFeedButton.Enabled = false;
            VKFeedButton.Enabled = false;
            TwoGisFeedButton.Enabled = false;
        }

        /// <summary>
        /// �������� ���������� ����� Excel.
        /// </summary>
        /// <param name="filePath">���� � ����� Excel.</param>
        /// <returns>True, ���� ���� �������� ������ Excel; � ��������� ������ - false.</returns>
        private bool IsExcelFile(string filePath)
        {
            return Path.GetExtension(filePath).Equals(".xlsx", StringComparison.OrdinalIgnoreCase)
                || Path.GetExtension(filePath).Equals(".xls", StringComparison.OrdinalIgnoreCase);
        }

        // private void SendInformation(string text)
        // {
        //     InformationTextBox.Text += text;
        // }
    }
}
