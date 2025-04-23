namespace BLL
{
    public class OrderProduct
    {
        public int OrderProductId { get; private set; }
        public int OrderId { get; private set; }
        public int ProductId { get; private set; }
        public int Quantity { get; private set; }
        public UnitType UnitType { get; private set; }
        public decimal TotalPrice { get; private set; }

        public OrderProduct(int orderProduct, int orderId, int productId, int quantity, UnitType unitType, decimal totalPrice)
        {
            this.OrderProductId = orderProduct;
            this.OrderId = orderId;
            this.ProductId = productId;
            this.Quantity = quantity;
            this.UnitType = unitType;
            this.TotalPrice = totalPrice;
        }
    }
}