namespace Converser
{
    public interface IExcelParserService
    {
        List<Product> GetXLSXFile(string path);
    }
}