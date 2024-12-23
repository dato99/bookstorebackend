namespace BookStore.Models
{
    public class Order
    {
        public int Id { get; set; }
        public int Quantity { get; set; }

        public decimal OrderPrice { get; set; }

    }
}
