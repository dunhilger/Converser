﻿using ConverserLibrary.Models.JSON_Models;

namespace ConverserLibrary.Interfaces
{
    /// <summary>
    /// Интерфейс для сервиса чтения Json.
    /// </summary>
    public interface IJsonApiDataService
    {
        /// <summary>
        /// Получает список городов из Json по указанному url.
        /// </summary>
        /// <returns></returns>
        public Task<JsonRoot> GetCities(string url)
;    }
}
