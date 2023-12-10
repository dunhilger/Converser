namespace Converser
{
    public class Product
    {
        public string CityName { get; set; }

        public string BitrixCode { get; set; }

        public string Type { get; } = "vendor.model";

        public string Available { get; set; } = "true";

        public string Vendor { get; } = "MYBOX";

        public string Model { get; set; }

        public string Url { get; set; }

        public string Price { get; set; }

        public string Currency { get; set; } = "RUR";

        public string Rate { get; set; } = "1";

        public string Picture { get; set; }

        public string Pickup { get; set; } = "true";

        public string Weight { get; set; }

        public string Quantity { get; set; }

        public string Description { get; set; }

        public string ManufacturerWarranty { get; set; } = "true";

        public string CountryOfOrigin { get; set; } = "Россия";

        public int? JapaneseCuisineStation { get; set; }

        public int? PanasianCuisineStation { get; set; }

        public string CategoryId { get; set; }

        public string CategoryName { get; set; }

        public string ParentCategoryId { get; set; }

        public string ParentCategoryName { get; set; }

    }
}


