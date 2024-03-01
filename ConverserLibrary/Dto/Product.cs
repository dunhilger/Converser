﻿namespace ConverserLibrary.Dto
{
    /// <summary>
    /// Единица товара/sku.
    /// </summary>
    public class Product
    {
        /// <summary>
        /// Получает название города.
        /// </summary>
        public string CityName { get; set; }

        /// <summary>
        /// Получает Bitrix-код товара.
        /// </summary>
        public string BitrixCode { get; set; }

        /// <summary>
        /// Получает тип товара.
        /// </summary>
        public string Type { get; } = "vendor.model";

        /// <summary>
        /// Получает признак наличия товара.
        /// </summary>
        public string Available { get; set; } = "true";

        /// <summary>
        /// Получает вендора товара.
        /// </summary>
        public string Vendor { get; } = "MYBOX";

        /// <summary>
        /// Получает коммерческое название товара.
        /// </summary>
        public string CommercialName { get; set; }

        /// <summary>
        /// Получает техническое название товара.
        /// </summary>
        public string TechnicalName { get; set; }         

        /// <summary>
        /// Получает цену товара.
        /// </summary>
        public string Price { get; set; }

        /// <summary>
        /// Получает валюту цены товара.
        /// </summary>
        public string Currency { get; set; } = "RUR";

        /// <summary>
        /// Получает курс валюты к рублю.
        /// </summary>
        public string Rate { get; set; } = "1";

        /// <summary>
        /// Получает изображение товара.
        /// </summary>
        public string PictureLink { get; set; }

        /// <summary>
        /// Получает возможность самовывоза товара.
        /// </summary>
        public string Pickup { get; set; } = "true";

        /// <summary>
        /// Получает вес товара.
        /// </summary>
        public string Weight { get; set; }

        /// <summary>
        /// Получает количество товара.
        /// </summary>
        public string Quantity { get; set; }

        /// <summary>
        /// Получает описание товара.
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Получает наличие гарантии от производителя.
        /// </summary>
        public string ManufacturerWarranty { get; set; } = "true";

        /// <summary>
        /// Получает страну производства товара.
        /// </summary>
        public string CountryOfOrigin { get; set; } = "Россия";

        /// <summary>
        /// Получает количество станций японской кухни.
        /// </summary>
        public int? JapaneseCuisineStationQuantity { get; set; }

        /// <summary>
        /// Получает количество станций паназийской кухни.
        /// </summary>
        public int? PanasianCuisineStationQuantity { get; set; }

        /// <summary>
        /// Получает ID категории товара.
        /// </summary>
        public string CategoryId { get; set; }

        /// <summary>
        /// Получает имя категории товара.
        /// </summary>
        public string CategoryName { get; set; }

        /// <summary>
        /// Получает ID родительской категории товара.
        /// </summary>
        public string ParentCategoryId { get; set; }

        /// <summary>
        /// Получает имя родительской категории товара.
        /// </summary>
        public string ParentCategoryName { get; set; }
    }
}
