namespace SalesApi.Model
{
    public class Item
    {
        public int recordNo { get; set; }
        public string productCode { get; set; }
        public double amount { get; set; }
        public string discountType { get; set; }
    }
    public class ProductList
    {
        public double totalDiscount { get; set; }

        public double totalAmount { get; set; }
        public List<Item> items { get; set; }

    }
}
