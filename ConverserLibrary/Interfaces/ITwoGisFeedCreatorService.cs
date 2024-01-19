namespace ConverserLibrary.Interfaces
{
    /// <summary>
    /// Интерфейс для сервиса создания XML-фидов для 2ГИС.
    /// </summary>
    public interface ITwoGisFeedCreatorService
    {
        /// <summary>
        /// Создает XML-файл для 2ГИС на основе данных о разделении по городам.
        /// </summary>
        /// <param name="path">Путь, по которому будет создан XML-файл</param>
        /// <param name="citySeparatorResult">Результат разделения по городам</param>
        public void CreateXml(string path, CitySeparatorResult citySeparatorResult);
    }
}
