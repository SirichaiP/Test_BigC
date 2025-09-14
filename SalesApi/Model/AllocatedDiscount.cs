namespace SalesApi.Model
{
    public class ItemDiscount
    {
        public int recordNo { get; set; }
        public double discount { get; set; }
    }

    public class AllocatedDiscount
    {
        public List<ItemDiscount> allocatedDiscounts { get; set; }
        public double totalDiscount { get; set; }
    }


}
