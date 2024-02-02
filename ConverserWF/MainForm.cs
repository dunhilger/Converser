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
        /// Инициализирует экземпляр класса MainService.
        /// </summary>
        /// <param name="logger">Интерфейс логгера</param>
        /// <param name="parser">Интерфейс сервиса парсера</param>
        /// <param name="separator">Интерфейс сервиса сепаратора</param>
        /// <param name="yandexFeedCreatorService">Интерфейс сервиса для создания XML-фидов Яндекс</param>
        /// <param name="twoGisFeedCreatorService">Интерфейс сервиса для создания XML-фидов 2ГИС</param>
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
            //SendInformation($"Создан файл {e.FileName}.");
        }

        private static string filePathImport;

        private static string filePathExport;

        /// <summary>
        /// Обработчик события Click для кнопки YandexFeedButton.
        /// </summary>
        /// <param name="sender">Объект, вызвавший событие.</param>
        /// <param name="e">Аргументы события Click.</param>
        private void YandexFeedButton_Click(object sender, EventArgs e)
        {
            HandleFeedButtonClick(_yandexFeedCreatorService.CreateXml);
        }

        /// <summary>
        /// Обработчик события Click для кнопки VKFeedButton.
        /// </summary>
        /// <param name="sender">Объект, вызвавший событие.</param>
        /// <param name="e">Аргументы события Click.</param>
        private void VKFeedButton_Click(object sender, EventArgs e)
        {
            HandleFeedButtonClick(_vkFeedCreatorService.CreateXml);
        }

        /// <summary>
        /// Обработчик события Click для кнопки TwoGisFeedButton.
        /// </summary>
        /// <param name="sender">Объект, вызвавший событие.</param>
        /// <param name="e">Аргументы события Click.</param>
        private void TwoGisFeedButton_Click(object sender, EventArgs e)
        {
            HandleFeedButtonClick(_twoGisFeedCreatorService.CreateXml);
        }

        /// <summary>
        /// Обработчик для кнопок генерации фидов. Принимает делегат, соответствующий
        /// сигнатуре метода CreateXml.
        /// </summary>
        /// <param name="createXml">Метод создания XML-файлов</param>
        private void HandleFeedButtonClick(Action<string, CitySeparatorResult> createXml)
        {
            _logger.LogInformation($"Старт обработки. Файл: {filePathImport}");

            // парсинг эксель, получение списка всех товаров  
            var products = _parser.GetXLSXFile(filePathImport);

            // разбивка списка по городам
            var cityDictionary = _separator.SeparateByCity(products);

            // создание фидов
            createXml(Path.GetDirectoryName(filePathExport), cityDictionary);

            _logger.LogInformation("Генерация XML-файлов завершена.");
        }

        /// <summary>
        /// Обработчик события DragEnter для контрола, позволяющий перетаскивать файлы.
        /// </summary>
        /// <param name="sender">Объект, вызвавший событие.</param>
        /// <param name="e">Аргументы события DragEnter.</param>
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
        /// Обработчик события DragDrop для контрола, выполняющийся при перетаскивании файла в область контрола.
        /// </summary>
        /// <param name="sender">Объект, вызвавший событие.</param>
        /// <param name="e">Аргументы события DragDrop.</param>
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
        /// Обработчик события Click для контрола, выполняющийся при клике на элемент BrowseFileImportDirectory.
        /// </summary>
        /// <param name="sender">Объект, вызвавший событие.</param>
        /// <param name="e">Аргументы события Click.</param>
        private void BrowseFileImportDirectory_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.Filter = "Файлы Excel (*.xlsx;*.xls)|*.xlsx;*.xls|Все файлы (*.*)|*.*";
                openFileDialog.Title = "Выберите файл Excel";

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
        /// Обработчик события Click для контрола, выполняющийся при клике на элемент BrowseExportDirectory.
        /// </summary>
        /// <param name="sender">Объект, вызвавший событие.</param>
        /// <param name="e">Аргументы события Click.</param>
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
        /// Обработка выбранного файла.
        /// </summary>
        /// <param name="selectedFile">Путь к выбранному файлу.</param>
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
                MessageBox.Show($"Выбранный файл не является файлом Excel:\n{selectedFile}");
                YandexFeedButton.Enabled = false;
                VKFeedButton.Enabled = false;
                TwoGisFeedButton.Enabled = false;
            }
        }

        /// <summary>
        /// Обработчик события Click для контрола сброса данных в диалоговых окнах.
        /// </summary>
        // <param name="sender">Объект, вызвавший событие.</param>
        /// <param name="e">Аргументы события Click.</param>
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
        /// Проверка валидности файла Excel.
        /// </summary>
        /// <param name="filePath">Путь к файлу Excel.</param>
        /// <returns>True, если файл является файлом Excel; в противном случае - false.</returns>
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
