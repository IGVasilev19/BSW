namespace Domain
{
    public class Product
    {
        public int ProductId { get; private set; }
        public string Name { get; private set; }
        public decimal Price { get; private set; }
        public int CategoryId { get; private set; }

        public Product(string name, decimal price, int categoryId)
        {
            this.Name = name;
            this.Price = price;
            this.CategoryId = categoryId;
        }

        public Product(int productId, string name, decimal price, int categoryId)
        {
            this.ProductId = productId;
            this.Name = name;
            this.Price = price;
            this.CategoryId = categoryId;
        }
    }
}