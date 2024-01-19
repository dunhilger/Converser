namespace ConverserLibrary.Interfaces
{
    /// <summary>
    /// Интерфейс для сервиса парсера файлов Excel.
    /// </summary>
    public interface IExcelParserService
    {
        /// <summary>
        /// Получает список товаров из файла Excel по указанному пути.
        /// </summary>
        /// <param name="path">Путь к файлу Excel</param>
        /// <returns>Список товаров</returns>
        List<Product> GetXLSXFile(string path);
    }
}