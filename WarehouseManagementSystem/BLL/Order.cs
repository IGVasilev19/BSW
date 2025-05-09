namespace Domain
{
    public class Order
    {
        public int OrderId { get; private set; }
        public int CreatedById { get; private set; }
        public int AssignedToId { get; private set; }
        public OrderStatus OrderStatus { get; private set; }
        public DateTime OrderDate { get; private set; }

        public Order(int orderId, int createdById, OrderStatus orderStatus, DateTime orderDate)
        {
            this.OrderId = orderId;
            this.CreatedById = createdById;
            this.AssignedToId = 0;
            this.OrderStatus = orderStatus;
            this.OrderDate = orderDate;
        }

        public Order(int orderId, int createdById, int assignedToId, OrderStatus orderStatus, DateTime orderDate)
        {
            this.OrderId = orderId;
            this.CreatedById = createdById;
            this.AssignedToId = assignedToId;
            this.OrderStatus = orderStatus;
            this.OrderDate = orderDate;
        }
    }
}