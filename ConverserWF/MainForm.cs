using ConverserLibrary;
using ConverserLibrary.Dto;
using ConverserLibrary.Interfaces;
using ConverserLibrary.Models;
using Microsoft.Extensions.Logging;
using OfficeOpenXml;

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

        /// <summary>
        /// Инициализарует подсказки для контролов формы
        /// </summary>
        private void InitializeTooltips()
        {
            var toolTip = new ToolTip
            {
                ToolTipTitle = "Подсказка"
            };
            toolTip.SetToolTip(DataLoadButton, "Загрузить данные из файла");
            toolTip.SetToolTip(ResetButton, "Сбросить данные");
        }

        /// <summary>
        /// Словарь, содержащий ключи - кнопки radioButton, и значения - делегаты.
        /// </summary>
        private readonly Dictionary<RadioButton, Action> RadioButtonActions = [];

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
            InitializeTooltips();
            SetEnableStateRadioButton(false);
            _logger = logger;
            _parser = parser;
            _separator = separator;
            _yandexFeedCreatorService = yandexFeedCreatorService;
            _twoGisFeedCreatorService = twoGisFeedCreatorService;
            _vkFeedCreatorService = vkFeedCreatorService;
            RadioButtonActions.Add(RadioButtonYandex, () => HandleFeedButtonClick(_yandexFeedCreatorService.CreateXml));
            RadioButtonActions.Add(RadioButton2gis, () => HandleFeedButtonClick(_twoGisFeedCreatorService.CreateXml));
            RadioButtonActions.Add(RadioButtonVK, () => HandleFeedButtonClick(_vkFeedCreatorService.CreateXml));
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

        private RadioButton _selectedRadioButton;

        /// <summary>
        /// Обработчик события Click для Radio Button.
        /// </summary>
        /// <param name="sender">Объект, вызвавший событие.</param>
        /// <param name="e">Аргументы события Click.</param>
        private void RadioButton_CheckedChanged(object sender, EventArgs e)
        {
            if (sender is RadioButton radioButton && radioButton.Checked)
            {
                _selectedRadioButton = radioButton;
            }
        }

        /// <summary>
        /// Обоработчик события Click для кнопки экспорта.
        /// </summary>
        /// <param name="sender">Объект, вызвавший событие.</param>
        /// <param name="e">Аргументы события Click.</param>
        private void ExportButton_Click(object sender, EventArgs e)
        {
            if (_selectedRadioButton != null && RadioButtonActions
                .TryGetValue(_selectedRadioButton, out Action value))
            {
                value.Invoke();
            }
            else
            {
                MessageBox.Show(this, "Не выбран целевой сервис для создания фидов.", "Внимание",
                            MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        /// <summary>
        /// Обработчик для кнопок генерации фидов. Принимает делегат, соответствующий
        /// сигнатуре метода CreateXml.
        /// </summary>
        /// <param name="createXml">Метод создания XML-файлов</param>
        private void HandleFeedButtonClick(Action<string, CitySeparatorResult> createXml)
        {
            var selectedCategories = GetAllCheckedNodes(CategoryTreePanel.Nodes)
            .Select(node => node.Tag as Category)
            .Where(category => category is not null)
            .ToList();

            if (selectedCategories.Count > 0)
            {
                _logger.LogInformation($"Старт обработки. Файл: {_filePathImport}");

                var citySeparatorResult = new CitySeparatorResult();

                foreach (var selectedCategory in selectedCategories)
                {
                    FeedCreatorProgressBar.Maximum = selectedCategories.Count;
                    FeedCreatorProgressBar.Visible = true;

                    foreach (var city in _cityDictionary.CityProducts.Keys)
                    {
                        foreach (var product in _cityDictionary.CityProducts[city])
                        {
                            if (selectedCategory is not null && product.CategoryId == selectedCategory.ID)
                            {
                                if (!citySeparatorResult.CityProducts.TryGetValue(city, out List<Product> value))
                                {
                                    value = new List<Product>();
                                    citySeparatorResult.CityProducts[city] = value;
                                }

                                value.Add(product);
                            }
                        }
                    }

                    FeedCreatorProgressBar.Value++;
                    citySeparatorResult.Categories.Add(selectedCategory); 
                }

                createXml(Path.GetDirectoryName(_filePathExport), citySeparatorResult);
                FeedCreatorProgressBar.Value = 0;
                _logger.LogInformation("Генерация XML-файлов завершена.");
            }
            else
            {
                MessageBox.Show(this, "Нет выбранных категорий для создания фидов.", "Внимание",
                            MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        /// <summary>
        /// Рекурсивно обходит дерево и получает все выбранные узлы категорий.
        /// </summary>
        /// <param name="nodes">Коллекция узлов для обхода.</param>
        /// <returns>Перечисление выбранных узлов.</returns>
        private static IEnumerable<TreeNode> GetAllCheckedNodes(TreeNodeCollection nodes)
        {
            foreach (TreeNode node in nodes)
            {
                if (node.Checked)
                    yield return node;

                foreach (var childNode in GetAllCheckedNodes(node.Nodes))
                    yield return childNode;
            }
        }

        /// <summary>
        /// Обработчик события Click для кнопки DataLoadButton.
        /// </summary>
        /// <param name="sender">Объект, вызвавший событие.</param>
        /// <param name="e">Аргументы события Click.</param>
        private void DataLoadButton_Click(object sender, EventArgs e)
        {
            CategoryTreePanel.Nodes.Clear();
            CategoryTreePanel.CheckBoxes = true;

            CheckAll.Enabled = true;
            CheckAll.Checked = false;

            var products = _parser.GetXLSXFile(_filePathImport);
            _cityDictionary = _separator.SeparateByCity(products);

            DataLoadProgressBar.Visible = true;
            DataLoadProgressBar.Maximum = _cityDictionary.Categories
                .Count(c => string.IsNullOrEmpty(c.ParentID));

            if (IsVlidExcelFile(_filePathImport))
            {
                foreach (var category in _cityDictionary.Categories.Where(c => c.ParentID == null))
                {
                    var rootNode = new TreeNode($"{category.Value} [{category.ID}]")
                    {
                        Tag = category
                    };

                    BuildCategoryTree(category, rootNode.Nodes);

                    CategoryTreePanel.Nodes.Add(rootNode);

                    DataLoadProgressBar.Value++;
                }

                DataLoadProgressBar.Hide();
                DataLoadButton.Text = "Загружено успешно!";
                FileSizeLabel.Text = $"Размер файла: {GetFormatFileSize(_filePathImport)}";
                CategoryTreePanel.ExpandAll();
                DataLoadButton.Enabled = false;
                CheckAll.Text = $"Выбрать все (всего: {_cityDictionary.Categories.Count})";
            }
            else
            {
                MessageBox.Show(this, "Выбранный файл Excel не подходит для создания фидов.", "Внимание",
                            MessageBoxButtons.OK, MessageBoxIcon.Warning);
                ResetButton_Click(this, new EventArgs());
            }
        }

        /// <summary>
        /// Представляет размер файла в надлежащем формате
        /// </summary>
        /// <param name="filePath">Путь к файлу.</param>
        /// <returns>Размер файла типа string.</returns>
        private static string GetFormatFileSize(string filePath)
        {
            long fileSize = new FileInfo(filePath).Length;
            string[] sizes = { "B", "KB", "MB", "GB", "TB" };
            int sizeUnitPosition = 0;
            double length = fileSize;

            while (length >= 1024 && sizeUnitPosition < sizes.Length - 1)
            {
                sizeUnitPosition++;
                length /= 1024;
            }

            return $"{Math.Round(length, 2)} {sizes[sizeUnitPosition]}";
        }

        /// <summary>
        /// Построение дерева категорий для указанной родительской категории.
        /// </summary>
        /// <param name="parentCategory">Родительскася категория.</param>
        /// <param name="nodes">Коллекция узлов дерева, к которой добавляются дочерние узлы.</param>
        private static void BuildCategoryTree(Category parentCategory, TreeNodeCollection nodes)
        {
            var childCategories = _cityDictionary.Categories.Where(c => c.ParentID == parentCategory.ID);

            foreach (var childCategory in childCategories)
            {
                var childNode = new TreeNode($"{childCategory.Value} [{childCategory.ID}]")
                {
                    Tag = childCategory
                };

                if (!nodes.Cast<TreeNode>().Any(existNode => existNode.Tag == childNode.Tag))
                {
                    nodes.Add(childNode);
                }

                BuildCategoryTree(childCategory, childNode.Nodes);
            }
        }

        /// <summary>
        /// Управляет галочками в дочерних и родительских узлах.
        /// </summary>
        /// <param name="sender">Объект, вызвавший событие.</param>
        /// <param name="e">Аргументы события AfterCheck.</param>
        private void CategoryTree_AfterCheck(object sender, TreeViewEventArgs e)
        {
            if (e.Action is not TreeViewAction.Unknown)
            {
                if (e.Node.Nodes.Count > 0)
                {
                    foreach (TreeNode childNode in e.Node.Nodes)
                    {
                        childNode.Checked = e.Node.Checked;
                    }
                }

                if (e.Node.Parent is not null)
                {
                    e.Node.Parent.Checked = e.Node.Parent.Nodes.Cast<TreeNode>().Any(node => node.Checked);
                }

                UpdateRadioButtonEnableState();
            }
        }

        /// <summary>
        /// Обработчик события CheckAll_Click. Устанавливает/снимает все галочки
        /// </summary>
        /// <param name="sender">Объект, вызвавший событие.</param>
        /// <param name="e">Аргументы события CheckAll_Click.</param>
        private void CheckAll_Click(object sender, EventArgs e)
        {
            bool selectAll = !CategoryTreePanel.Nodes.Cast<TreeNode>().All(node => node.Checked);

            foreach (TreeNode node in CategoryTreePanel.Nodes)
            {
                SetCheckChildNode(node, selectAll);
            }

            UpdateRadioButtonEnableState();
        }

        /// <summary>
        /// Рекурсивно устанавливает галочку для дочерних категорий, начиная с указанного узла.
        /// </summary>
        /// <param name="node">Начальный узел</param>
        /// <param name="isChecked">Признак выбора категории</param>
        private static void SetCheckChildNode(TreeNode node, bool isChecked)
        {
            node.Checked = isChecked;

            foreach (TreeNode childNode in node.Nodes)
            {
                SetCheckChildNode(childNode, isChecked);
            }
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

                ClearUIState();

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

                    ClearUIState();

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
                DataLoadButton.Enabled = false;
            }
        }

        /// <summary>
        /// Обработчик события Click для контрола сброса данных в полях формы либо присваивания им 
        /// значений по умолчанию.
        /// </summary>
        // <param name="sender">Объект, вызвавший событие.</param>
        /// <param name="e">Аргументы события Click.</param>
        private void ResetButton_Click(object sender, EventArgs e)
        {
            _filePathExport = null;
            _filePathImport = null;
            BrowseDirectoryExportField.Text = string.Empty;
            BrowseDirectoryImportField.Text = string.Empty;
            DataLoadButton.Enabled = false;          
            ClearUIState();
        }

        /// <summary>
        /// 
        /// </summary>
        private void ClearUIState()
        {
            DataLoadButton.Text = "Загрузить данные"; 
            CheckAll.Checked = false; 
            CheckAll.Enabled = false; 
            CheckAll.Text = "Выбрать все"; 
            CategoryTreePanel.Nodes.Clear(); 
            DataLoadProgressBar.Visible = false; 
            DataLoadProgressBar.Value = 0; 
            FeedCreatorProgressBar.Value = 0; 
            FileSizeLabel.Text = "";
            SetEnableStateRadioButton(false);
        }

        /// <summary>
        /// Устанавливает для каждой RadioButton состояние активности/неактивности.
        /// </summary>
        private void SetEnableStateRadioButton(bool enableStatus)
        {
            foreach (RadioButton radioButton in RadioButtonPanel.Controls.OfType<RadioButton>())
            {
                radioButton.Enabled = enableStatus;
                radioButton.Checked = enableStatus; // многократно вызывает RadioButton_CheckedChanged 
            }
        }

        /// <summary>
        /// Обновляет состояние активности RadioButton в зависимости от того, 
        /// выбран ли хотя бы один узел категории.
        /// </summary>
        private void UpdateRadioButtonEnableState()
        {
            bool anyCategorySelected = GetAllCheckedNodes(CategoryTreePanel.Nodes).Any();
            SetEnableStateRadioButton(anyCategorySelected);
        }

        /// <summary>
        /// Проверка валидности файла Excel.
        /// </summary>
        /// <param name="filePath">Путь к файлу Excel.</param>
        /// <returns>True, если файл является файлом Excel; в противном случае - false.</returns>
        private static bool IsExcelFile(string filePath)
        {
            return Path.GetExtension(filePath).Equals(".xlsx", StringComparison.OrdinalIgnoreCase)
                || Path.GetExtension(filePath).Equals(".xls", StringComparison.OrdinalIgnoreCase);
        }

        /// <summary>
        /// Проверка совместимости файла Excel с возможностью создания фидов
        /// </summary>
        /// <param name="filePath"></param>
        /// <returns></returns>
        private static bool IsVlidExcelFile(string filePath)
        {
            using (var package = new ExcelPackage(new FileInfo(filePath)))
            {
                var worksheet = package.Workbook.Worksheets[0];

                if (worksheet == null ||
                    worksheet.Cells["A1"].Text != "Город" ||
                    worksheet.Cells["C1"].Text != "КартинкаКрупная" ||
                    worksheet.Cells["D1"].Text != "Название_блюда" ||
                    worksheet.Cells["G1"].Text != "ИдМпМенюКатегория" ||
                    worksheet.Cells["I1"].Text != "Описание" ||
                    worksheet.Cells["M1"].Text != "ВесГраммы" ||
                    worksheet.Cells["AA1"].Text != "ИдКатегория")
                {
                    return false;
                }

                else return true;
            }
        }
    }
}
