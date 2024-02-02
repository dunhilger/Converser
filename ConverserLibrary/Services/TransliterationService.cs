using Microsoft.Extensions.Logging;
using System.Text;

namespace ConverserLibrary.Services
{
    /// <summary>
    /// Сервис для транслитерации.
    /// </summary>
    public class TransliterationService
    {
        private Dictionary<char, string> transliterationMap;

        private readonly ILogger<TransliterationService> _logger;

        public TransliterationService()
        {
            InitializeTransliterationMap();
        }

        public void InitializeTransliterationMap()
        {
            transliterationMap = new Dictionary<char, string>
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
            };
        }

        /// <summary>
        /// Транслитерирует кириллицу в латиницу.
        /// </summary>
        /// <param name="text">Текст на кириллице.</param>
        /// <returns>Транслитерированный текст на латинице в нижнем регистре.</returns>
        public string Transliterate(string text)
        {
            if (string.IsNullOrEmpty(text))
                _logger.LogError("Ошибка: Название блюда '{text}' отсутствует.", text);

            var result = new StringBuilder();

            foreach (char symbol in text)
            {
                if (char.IsDigit(symbol))
                {
                    result.Append(symbol);
                    continue;
                }

                if (IsLatinLetter(symbol))
                {
                    result.Append(symbol);
                    continue;
                }

                if (transliterationMap.TryGetValue(symbol, out string value))
                {
                    result.Append(value);
                }
                else
                {
                    result.Append(symbol);
                }
            }

            return result.ToString().ToLower();
        }

        private bool IsLatinLetter(char symbol)
        {
            return (symbol >= 'A' && symbol <= 'Z') || (symbol >= 'a' && symbol <= 'z');
        }
    }
}
