namespace Domain
{
    public class Inventory
    {
        public int InventoryId { get; private set; }
        public int ProductId { get; private set; }
        public int ZoneId { get; private set; }
        public int Quantity { get; private set; }
        public DateTime LastUpdated { get; private set; }

        public Inventory(int inventoryId, int productId, int zoneId, int quantity, DateTime lastUpdated)
        {
            this.InventoryId = inventoryId;
            this.ProductId = productId;
            this.ZoneId = zoneId;
            this.Quantity = quantity;
            this.LastUpdated = lastUpdated;
        }
    }
}