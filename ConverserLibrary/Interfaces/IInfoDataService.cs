using ConverserLibrary.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
namespace ConverserLibrary.Interfaces
{
    /// <summary>
    /// 
    /// </summary>
    public interface IInfoDataService
    {
        /// <summary>
        /// Возвращает список городов из Json
        /// </summary>
        /// <returns>Список городов</returns>
        List<City> GetCities();

        /// <summary>
        /// Возвращает список utm меток
        /// </summary>
        /// <returns>Список utm меток</returns>
        List<UtmLabel> GetUtmLabels();
    }
}
