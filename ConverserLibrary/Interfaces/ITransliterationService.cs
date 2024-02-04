namespace ConverserLibrary.Interfaces
{
    /// <summary>
    /// Интерфейс для сервиса транслитерации.
    /// </summary>
    public interface ITransliterationService
    {
        /// <summary>
        /// Транслитерирует кириллицу в латиницу.
        /// </summary>
        /// <param name="text">Текст на кириллице.</param>
        /// <returns>Транслитерированный текст на латинице в нижнем регистре.</returns>
        string Transliterate(string text);
    }
}
