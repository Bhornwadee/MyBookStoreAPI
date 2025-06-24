namespace MyBookStoreAPI.Models
{
    public class OrderDetailModel
    {
        public int Id { get; set; }
        public string OrderNumber { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public int Price { get; set; }
        public string Store { get; set; } = string.Empty;
        public int TotalPaid { get; set; }
    }
}