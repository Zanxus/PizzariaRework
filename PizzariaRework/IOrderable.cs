namespace PizzariaRework
{
    public interface IOrderable
    {
        string Name { get; set; }
        decimal Price { get; set; }
        decimal CalculatePrice();
    }
}