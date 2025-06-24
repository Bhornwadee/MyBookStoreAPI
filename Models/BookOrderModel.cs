namespace MyBookStoreAPI.Models
{
    public class BookOrderModel
    {
        public string Name { get; set; } = string.Empty;
        public int Price { get; set; }
        public string Store { get; set; } = string.Empty;
    }
}