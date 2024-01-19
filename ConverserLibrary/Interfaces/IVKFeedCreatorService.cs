namespace ConverserLibrary.Interfaces
{
    /// <summary>
    /// Интерфейс для сервиса создания XML-фидов для VK.
    /// </summary>
    public interface IVKFeedCreatorService
    {
        /// <summary>
        /// Создает XML-файл для Яндекса на основе данных о разделении по городам.
        /// </summary>
        /// <param name="path">Путь, по которому будет создан XML-файл</param>
        /// <param name="citySeparatorResult">Результат разделения по городам</param>
        public void CreateXml(string path, CitySeparatorResult citySeparatorResult);
    }
}


