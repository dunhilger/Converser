﻿using ConverserLibrary;
using ConverserLibrary.Dto;
using ConverserLibrary.Interfaces;
using ConverserLibrary.Models;
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
            DataLoadButton.Enabled = false;
            CheckAll.Enabled = false;
        }

        private void OnXmlCreated(object sender, XmlCreatedEventArgs e)
        {
            //SendInformation($"Создан файл {e.FileName}.");
        }

        private static string _filePathImport;

        private static string _filePathExport;

        private static CitySeparatorResult _cityDictionary;

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
            _logger.LogInformation($"Старт обработки. Файл: {_filePathImport}");

            _cityDictionary.Categories.Clear();

            foreach (Category category in CategoriesListCheckBox.CheckedItems)
            {
                _cityDictionary.Categories.Add(category);// в списке категорий фидов остаются только выбранные категории
            }

            createXml(Path.GetDirectoryName(_filePathExport), _cityDictionary);

            _logger.LogInformation("Генерация XML-файлов завершена.");
        }

        /// <summary>
        /// Обработчик события Click для кнопки DataLoadButton.
        /// </summary>
        /// <param name="sender">Объект, вызвавший событие.</param>
        /// <param name="e">Аргументы события Click.</param>
        private void DataLoadButton_Click(object sender, EventArgs e)
        {
            CategoriesListCheckBox.Items.Clear();
            CheckAll.Enabled = true;
            CheckAll.Checked = false;

            var products = _parser.GetXLSXFile(_filePathImport);
            _cityDictionary = _separator.SeparateByCity(products);

            if (_cityDictionary.Categories.Count > 0 && 
                _cityDictionary.CityProducts.Count > 0 &&
                _cityDictionary.CityProducts.Keys.Count > 0)
            {
                DataLoadButton.Text = "Загружено успешно!";
                DataLoadButton.Enabled = false;
                YandexFeedButton.Enabled = true;
                VKFeedButton.Enabled = true;
                TwoGisFeedButton.Enabled = true;

                foreach (var category in _cityDictionary.Categories)
                {
                    CategoriesListCheckBox.Items.Add(new Category
                    {
                        ID = category.ID,
                        Value = category.Value,
                        ParentID = category.ParentID
                    });
                }
            }
            else
            {
                MessageBox.Show(this, "Выбранный файл Excel не подходит для создания фидов.", "Внимание",
                            MessageBoxButtons.OK, MessageBoxIcon.Warning);
                FieldDataResetButton_Click(this, new EventArgs());
            }
        }

        /// <summary>
        /// Обработчик события CheckAll_Click. Устанавливает/снимает все галочки
        /// </summary>
        /// <param name="sender">Объект, вызвавший событие.</param>
        /// <param name="e">Аргументы события DragEnter.</param>
        private void CheckAll_Click(object sender, EventArgs e)
        {
            CategoriesListCheckBox.SetItemChecked(0, !CategoriesListCheckBox.GetItemChecked(0));

            bool selectAll = CategoriesListCheckBox.GetItemChecked(0);

            for (int i = 1; i < CategoriesListCheckBox.Items.Count; i++)
            {
                CategoriesListCheckBox.SetItemChecked(i, selectAll);
            }
        }///установка галочек с клавиатуры - убрать

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
                _filePathImport = files[0];

                if (IsExcelFile(_filePathImport))
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
                _filePathImport = files[0];

                if (string.IsNullOrEmpty(_filePathExport))
                {
                    _filePathExport = _filePathImport;
                    BrowseDirectoryExportField.Text = Path.GetDirectoryName(_filePathImport);
                }

                ProcessFile(_filePathImport);

                BrowseDirectoryImportField.Text = _filePathImport;
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
                    _filePathImport = openFileDialog.FileName;
                    // частично дублирует FieldDataResetButton_Click →
                    DataLoadButton.Text = "Загрузить данные";
                    CheckAll.Checked = false;
                    CheckAll.Enabled = false;
                    CategoriesListCheckBox.Items.Clear();
                    YandexFeedButton.Enabled = false;
                    VKFeedButton.Enabled = false;
                    TwoGisFeedButton.Enabled = false;

                    if (string.IsNullOrEmpty(_filePathExport) || _filePathExport != _filePathImport)
                    {
                        _filePathExport = _filePathImport;
                        BrowseDirectoryExportField.Text = Path.GetDirectoryName(_filePathImport);
                    }

                    ProcessFile(_filePathImport);

                    BrowseDirectoryImportField.Text = _filePathImport;
                }
                else
                {
                    BrowseDirectoryImportField.Text = !string.IsNullOrEmpty(_filePathImport) ?
                                                      _filePathImport : "";
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

                _filePathExport = folderBrowserDialog.SelectedPath;
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
                DataLoadButton.Enabled = true;
            }
            else
            {
                MessageBox.Show($"Выбранный файл не является файлом Excel:\n{selectedFile}");
                YandexFeedButton.Enabled = false;
                VKFeedButton.Enabled = false;
                TwoGisFeedButton.Enabled = false;
                DataLoadButton.Enabled = false;
            }
        }

        /// <summary>
        /// Обработчик события Click для контрола сброса данных в диалоговых окнах.
        /// </summary>
        // <param name="sender">Объект, вызвавший событие.</param>
        /// <param name="e">Аргументы события Click.</param>
        public void FieldDataResetButton_Click(object sender, EventArgs e)
        {
            _filePathExport = null;
            _filePathImport = null;
            BrowseDirectoryExportField.Text = string.Empty;
            BrowseDirectoryImportField.Text = string.Empty;
            YandexFeedButton.Enabled = false;
            VKFeedButton.Enabled = false;
            TwoGisFeedButton.Enabled = false;
            DataLoadButton.Enabled = false;
            DataLoadButton.Text = "Загрузить данные";
            CategoriesListCheckBox.Items.Clear();
            CheckAll.Enabled = false;
            CheckAll.Checked = false;
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
