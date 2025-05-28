namespace Condorcet.B2.AspnetCore.MVC.Application.Core.Domain
{
    public class Order
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string Username { get; set; }
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }
        public DateTime OrderDate { get; set; }

        public decimal Total => Quantity * UnitPrice;
    }
}
