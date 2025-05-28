namespace Condorcet.B2.AspnetCore.MVC.Application.Core.Domain;

public class Cart
    {
        public List<ProductInCart> ProductsList { get; set; } = new List<ProductInCart>();

        public void AddProduct(Product p, int quantity)
        {
            var existingItem = ProductsList.FirstOrDefault(item => item.Product.Id == p.Id);
            if (existingItem != null)
            {
                existingItem.Quantity += quantity;
            }
            else
            {
                ProductsList.Add(new ProductInCart
                {
                    Product = p,
                    Quantity = quantity
                });
            }
        }
}