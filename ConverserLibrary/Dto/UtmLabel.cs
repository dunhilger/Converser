namespace ConverserLibrary.Dto
{
    public class UtmLabel
    {
        public string CategoryName { get; set; }

        public string CategoryId { get; set; }

        public string CategoryUTMLabel { get; set; }

        public UtmLabel(string categoryName, string categoryId, string categoryUTMLabel) 
        {
            CategoryName = categoryName;
            CategoryId = categoryId;
            CategoryUTMLabel = categoryUTMLabel;
        }
    }
}
