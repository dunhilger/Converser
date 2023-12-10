using Microsoft.Extensions.Logging;
using OfficeOpenXml;

namespace Converser
{
    public class ExcelParserService : IExcelParserService
    {
        // перечисление со списком полей, ожидаемым из файла Excel
        enum Field
        {
            Model,
            Url,
            Price,
            CategoryId,
            Picture,
            Weight,
            Quantity,
            Description,
            JapaneseCuisineStation,
            BitrixCode,
            PanasianCuisineStation,
            CategoryName,
            ParentCategoryId,
            ParentCategoryName,
        }

        // словарь для сопоставления полей из Enum Field с объектами типа FielaData
        private readonly Dictionary<Field, FieldData> _fields;

        private readonly ILogger<ExcelParserService> _logger;

        public ExcelParserService(ILogger<ExcelParserService> logger)
        {
            _logger = logger ??
                throw new ArgumentNullException(nameof(logger));

            var model = new FieldData(5, "Model", (v, p) => p.Model = v);
            var url = new FieldData(32, "Url", (v, p) => p.Url = v);
            var price = new FieldData(6, "Price", (v, p) => p.Price = GetNumericValue(v));
            var categoryId = new FieldData(26, "CategoryId", (v, p) => p.CategoryId = v);
            var picture = new FieldData(3, "Picture", (v, p) => p.Picture = v);
            var weight = new FieldData(13, "Weight", (v, p) => p.Weight = GetNumericValue(v));
            var bitrixCode = new FieldData(20, "BitrixCode", (v, p) => p.BitrixCode = v);
            var quantity = new FieldData(14, "Quantity", (v, p) => p.Quantity = GetNumericValue(v));
            var description = new FieldData(9, "Description", (v, p) => p.Description = v);
            var japaneseCuisineStation = new FieldData(24, "JapaneseCuisineStation",
                (v, p) => p.JapaneseCuisineStation = GetIntValue(v));
            var panasianCuisineStation = new FieldData(25, "PanasianCuisineStation",
                (v, p) => p.PanasianCuisineStation = GetIntValue(v));
            var categoryName = new FieldData(8, "CategoryName", (v, p) => p.CategoryName = v);
            var parentCategoryId = new FieldData(27, "ParentCategoryId", (v, p) => p.ParentCategoryId = v);
            var parentCategoryName = new FieldData(28, "ParentCategoryName", (v, p) => p.ParentCategoryName = v);

            _fields = new Dictionary<Field, FieldData>()
            {
                { Field.Model, model },
                { Field.Url, url },
                { Field.Price, price },
                { Field.CategoryId, categoryId },
                { Field.Picture, picture },
                { Field.Weight, weight },
                { Field.BitrixCode, bitrixCode },
                { Field.Quantity, quantity },
                { Field.Description, description },
                { Field.JapaneseCuisineStation, japaneseCuisineStation },
                { Field.PanasianCuisineStation, panasianCuisineStation },
                { Field.CategoryName, categoryName },
                { Field.ParentCategoryId, parentCategoryId },
                { Field.ParentCategoryName, parentCategoryName },
            };
        }

        // преобразование текстового значения в числовое значение с фиксированным форматом
        private string GetNumericValue(string value)
        {
            var dotIndex = value.IndexOf('.');
            var commaIndex = value.IndexOf(',');
            int index = dotIndex > 0 ? dotIndex : (commaIndex > 0 ? commaIndex : value.Length);
            value = value.Substring(0, index);
            if (double.TryParse(value, out double numericValue))
            {
                return numericValue.ToString("0.#");
            }
            else
            {
                //Console.WriteLine($"Ошибка при преобразовании значения {value} в число.");
                _logger.LogError("Ошибка преобразования значения '{0a}' в число.", value);
            }

            return string.Empty;
        }

        // преобразование строкового значения в целочисленное значение int
        private int? GetIntValue(string value)
        {
            if (int.TryParse(value, out int intValue))
            {
                return intValue;
            }
            else
            {
                //Console.WriteLine($"Ошибка при преобразовании значения {value} в int.");
                _logger.LogError("Ошибка при преобразовании значения '{0a}' в int", value);
            }

            return null;
        }

        // парсинг файла Excel
        public List<Product> GetXLSXFile(string path)
        {
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

            var products = new List<Product>();

            if (!CheckDirectory(path))
            {
                return null;
            }

            using (var package = new ExcelPackage(new FileInfo(path)))
            {
                ExcelWorksheet worksheet = package.Workbook.Worksheets[0];
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

        // получение значения из ячейки файла Excel
        private static string GetCellValue(ExcelWorksheet worksheet, int row, int column)
        {
            var cellValue = worksheet.Cells[row, column].Value?.ToString().Trim() ?? string.Empty;

            return cellValue.Equals("NULL", StringComparison.OrdinalIgnoreCase) ? string.Empty : cellValue;
        }

        // установка значения в поле Product в соответствии с данными из поля FieldData
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
                //Console.WriteLine($"Отсутствует значение. Поле эксель: {worksheet.Cells[row, column].Address}");
                _logger.LogError("Отсутствует значение в ячейке Excel '{a0}'", worksheet.Cells[row, column].Address);
            }
        }

        // проверка существования файла Excel по указанной директории
        private static bool CheckDirectory(string path)
        {
            if (!File.Exists(path))
            {
                Console.WriteLine("Указанный файл не существует");
                Console.WriteLine("Нажмите любую клавишу для завершения...");
                Console.ReadKey();
                return false;
            }
            return true;
        }

        // содержит данные о поле Excel(позиция, имя, метод для обработки данных этого поля)
        class FieldData
        {
            public FieldData(int position, string name, Action<string, Product> handler)
            {
                Position = position;
                Name = name;
                Handler = handler;
            }

            public void HandleValue(string value, Product product)
            {
                Handler?.Invoke(value, product);
            }

            public Action<string, Product> Handler { get; }

            public int Position { get; }

            public string Name { get; }
        }
    }
}


