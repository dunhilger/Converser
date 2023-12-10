namespace Converser
{
    public interface ICitySeparatorService
    {
        CitySeparatorResult SeparateByCity(List<Product> products);
    }
}
