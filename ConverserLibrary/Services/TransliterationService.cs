using ConverserLibrary.Interfaces;
using Microsoft.Extensions.Logging;
using System.Text;
using System.Text.RegularExpressions;

namespace ConverserLibrary.Services
{
    /// <summary>
    /// Сервис для транслитерации.
    /// </summary>
    public class TransliterationService : ITransliterationService
    {
        private static readonly Dictionary<char, string> _transliterationMap;

        static TransliterationService()
        {
            _transliterationMap = new Dictionary<char, string>
            {
                {'а', "a"},
                {'б', "b"},
                {'в', "v"},
                {'г', "g"},
                {'д', "d"},
                {'е', "e"},
                {'ё', "ye"},
                {'ж', "zh"},
                {'з', "z"},
                {'и', "i"},
                {'й', "y"},
                {'к', "k"},
                {'л', "l"},
                {'м', "m"},
                {'н', "n"},
                {'о', "o"},
                {'п', "p"},
                {'р', "r"},
                {'с', "s"},
                {'т', "t"},
                {'у', "u"},
                {'ф', "f"},
                {'х', "kh"},
                {'ц', "ts"},
                {'ч', "ch"},
                {'ш', "sh"},
                {'щ', "shch"},
                {'ъ', ""},
                {'ы', "y"},
                {'ь', ""},
                {'э', "e"},
                {'ю', "yu"},
                {'я', "ya"},
                {' ', "_"},
                {'/', "_"},
                {'-', "_"},
                {',', "_"},
                {'.', "_"},
                //{'(', ""},
                //{')', ""},
                //{':', ""},
                //{'+', ""},
                //{'?', ""},
                //{'!', ""},
                //{'"', ""},
            };
        }

        private readonly ILogger<TransliterationService> _logger;

        public TransliterationService(ILogger<TransliterationService> logger)
        {
            _logger = logger ??
                throw new ArgumentNullException(nameof(logger));
        }

        /// <summary>
        /// Транслитерирует кириллицу в латиницу.
        /// </summary>
        /// <param name="text">Текст на кириллице.</param>
        /// <returns>Транслитерированный текст на латинице в нижнем регистре.</returns>
        public string Transliterate(string text)
        {
            var result = new StringBuilder();

            foreach (char symbol in text.ToLower())
            {
                if (char.IsDigit(symbol) || IsLatinLetter(symbol))
                {
                    result.Append(symbol);
                }
                else if (_transliterationMap.TryGetValue(symbol, out string value))
                {
                    result.Append(value);
                }
                else
                {
                    result.Append("");
                }
            }

            return Regex.Replace(result.ToString().TrimEnd('_'), @"_+", "_");
        }

        private bool IsLatinLetter(char symbol)
        {
            return (symbol >= 'A' && symbol <= 'Z') || (symbol >= 'a' && symbol <= 'z');
        }
    }
}
