using ConverserLibrary.Dto;
using ConverserLibrary.Interfaces;
using Microsoft.Extensions.Logging;
using OfficeOpenXml;
using System.Globalization;

namespace ConverserLibrary
{
    /// <summary>
    /// Сервис для парсинга данных из файла Excel и создания списка продуктов.
    /// </summary>
    public class ExcelParserService : IExcelParserService
    {
        /// <summary>
        /// Перечисление со списком полей, ожидаемых из файла Excel
        /// </summary>
        enum Field
        {
            CommercialName,
            TechnicalName,
            Price,
            CategoryId,
            PictureLink,
            Weight,
            Quantity,
            Description,
            BitrixCode,
            CategoryName,
            ParentCategoryId,
            ParentCategoryName,
        }

        /// <summary>
        /// Содержит словарь для сопоставления полей из Enum Field с объектами типа FielaData.
        /// </summary>
        private readonly Dictionary<Field, FieldData> _fields;

        /// <summary>
        /// Поле для ведения логов в контексте сервиса парсинга Excel.
        /// </summary>
        private readonly ILogger<ExcelParserService> _logger;

        /// <summary>
        /// Инициализирует экземпляр класса ExcelParserService.
        /// </summary>
        /// <param name="logger">Интерфейс логгера</param>
        /// <exception cref="ArgumentNullException"></exception>
        public ExcelParserService(ILogger<ExcelParserService> logger)
        {
            _logger = logger ??
                throw new ArgumentNullException(nameof(logger));

            var commercialName = new FieldData("НаименованиеСайт", (v, p) => p.CommercialName = v);
            var technicalName = new FieldData("Название_блюда", (v, p) => p.TechnicalName = v);
            var price = new FieldData("Цена", (v, p) => p.Price = GetNumericValue(v));
            var categoryId = new FieldData("ИдКатегория", (v, p) => p.CategoryId = v);
            var pictureLink = new FieldData("КартинкаКрупная", (v, p) => p.PictureLink = v);
            var weight = new FieldData("ВесГраммы", (v, p) => p.Weight = GetNumericValue(v));
            var bitrixCode = new FieldData("БитриксКод", (v, p) => p.BitrixCode = v);
            var quantity = new FieldData("ПорцияКоличество", (v, p) => p.Quantity = GetNumericValue(v));
            var description = new FieldData("Описание", (v, p) => p.Description = v);
            var categoryName = new FieldData("Название_блюда_в_МП", (v, p) => p.CategoryName = v);
            var parentCategoryId = new FieldData("ИдКатегорияРодитель", (v, p) => p.ParentCategoryId = v);
            var parentCategoryName = new FieldData("НаименованиеРодитель", (v, p) => p.ParentCategoryName = v);

            _fields = new Dictionary<Field, FieldData>()
            {
                { Field.CommercialName, commercialName },
                { Field.TechnicalName, technicalName },
                { Field.Price, price },
                { Field.CategoryId, categoryId },
                { Field.PictureLink, pictureLink },
                { Field.Weight, weight },
                { Field.BitrixCode, bitrixCode },
                { Field.Quantity, quantity },
                { Field.Description, description },
                { Field.CategoryName, categoryName },
                { Field.ParentCategoryId, parentCategoryId },
                { Field.ParentCategoryName, parentCategoryName },
            };
        }

        /// <summary>
        /// Преобразует текстовое значения в числовое с фиксированным форматом
        /// </summary>
        /// <param name="value">Значение из ячейки Excel</param>
        /// <returns></returns>
        private string GetNumericValue(string value)
        {
            var dotIndex = value.IndexOf('.');
            var commaIndex = value.IndexOf(',');
            int index = dotIndex > 0 ? dotIndex : (commaIndex > 0 ? commaIndex : value.Length);
            value = value.Substring(0, index);

            if (double.TryParse(value, NumberStyles.Any, CultureInfo.InvariantCulture, out double numericValue))
            {
                return numericValue.ToString("0.#");
            }
            else
            {
                _logger.LogError("Ошибка преобразования значения '{value}' в число.", value);
            }

            return string.Empty;
        }

        /// <summary>
        /// Получает данные из файла Excel и возвращает список продуктов.
        /// </summary>
        /// <param name="path">Путь к файлу Excel</param>
        /// <returns></returns>
        public List<Product> GetXLSXFile(string path)
        {
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

            var products = new List<Product>();

            using (var package = new ExcelPackage(new FileInfo(path)))
            {
                ExcelWorksheet worksheet = package.Workbook.Worksheets[0];

                foreach (var field in _fields.Values)
                {
                    field.Position = 0;
                }

                var nameDict = _fields
                    .GroupBy(i => i.Value.Name)
                    .ToDictionary(i => i.Key, i => i.FirstOrDefault().Value);

                for (int col = 1; col <= worksheet.Dimension.Columns; col++)
                {
                    var columnName = worksheet.Cells[1, col].Text.Trim();

                    if (nameDict.TryGetValue(columnName, out var field))
                    {
                        field.Position = col;
                    }
                }

                foreach (var field in _fields.Values)
                {
                    if (field.Position == 0)
                    {
                        return null;
                    }
                }

                int rowCount = worksheet.Cells[worksheet.Dimension.End.Row, 1].End.Row;

                for (int row = 2; row <= rowCount; row++)
                {
                    string cityName = GetCellValue(worksheet, row, 1);

                    if (!string.IsNullOrEmpty(cityName))
                    {
                        var product = new Product
                        {
                            CityName = cityName
                        };

                        foreach (var field in _fields)
                        {
                            SetFieldValue(worksheet, row, field.Value, product);
                        }

                        products.Add(product);
                    }
                }
            }

            return products;
        }

        /// <summary>
        /// Получает значения из ячейки файла Excel.
        /// </summary>
        /// <param name="worksheet">Лист Excel</param>
        /// <param name="row">Строка</param>
        /// <param name="column">Столбец</param>
        /// <returns>Значение ячейки или пустая строка, если значение равно "NULL"</returns>
        private static string GetCellValue(ExcelWorksheet worksheet, int row, int column)
        {
            var cellValue = worksheet.Cells[row, column].Value?.ToString().Trim() ?? string.Empty;

            return cellValue.Equals("NULL", StringComparison.OrdinalIgnoreCase) ? string.Empty : cellValue;
        }

        /// <summary>
        /// Устанавливает значение поля в объекте Product, используя данные из указанной ячейки файла Excel из FieldData.
        /// </summary>
        /// <param name="worksheet">Лист Excel</param>
        /// <param name="row">Строка</param>
        /// <param name="fieldData">Данные о поле, включая позицию и метод обработки</param>
        /// <param name="product">Объект Product, в который будет установлено значение поля</param>
        private void SetFieldValue(ExcelWorksheet worksheet, int row, FieldData fieldData, Product product)
        {
            var column = fieldData.Position;

            string value = GetCellValue(worksheet, row, column);

            if (!string.IsNullOrEmpty(value))
            {
                fieldData.Handler(value, product);
            }
            else
            {
                _logger.LogError("Отсутствует значение в ячейке Excel '{adress}'", worksheet.Cells[row, column].Address);
            }
        }

        /// <summary>
        /// Класс, представляющий данные о поле Excel. Cодержит данные о поле Excel(позиция, имя, метод для обработки данных этого поля).
        /// </summary>
        class FieldData
        {
            /// <summary>
            /// Инициализирует экземпляр класса FieldData
            /// </summary>
            /// <param name="name">Имя поля</param>
            /// <param name="handler">Метод обработки данных этого поля</param>
            public FieldData(string name, Action<string, Product> handler)
            {
                Name = name;
                Handler = handler;
            }

            /// <summary>
            /// Обрабатывает значение поля, вызывая соответствующий метод обработки.
            /// </summary>
            /// <param name="value">Значение поля</param>
            /// <param name="product">Продукт, которому будет установлено значение</param>
            public void HandleValue(string value, Product product)
            {
                Handler?.Invoke(value, product);
            }

            /// <summary>
            /// Получает и устанавливает метод обработки данных этого поля.
            /// </summary>
            public Action<string, Product> Handler { get; }

            /// <summary>
            /// Получает позицию поля в файле Excel.
            /// </summary>
            public int Position { get; set; }

            /// <summary>
            /// Получает имя поля.
            /// </summary>
            public string Name { get; }
        }
    }
}

