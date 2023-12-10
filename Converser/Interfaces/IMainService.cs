namespace Converser
{
    /// <summary>
    /// Интерфейс главного сервиса.
    /// </summary>
    public interface IMainService
    {
        /// <summary>
        /// Запускает основной процесс обработки данных.
        /// </summary>
        /// <param name="path">Путь к файлу Exscel</param>
        void Run(string path);
    }
}
