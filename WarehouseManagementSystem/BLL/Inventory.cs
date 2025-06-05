namespace Domain
{
    public class Inventory
    {
        public int InventoryId { get; private set; }
        public int ProductId { get; private set; }
        public int ZoneId { get; private set; }
        public int Quantity { get; private set; }
        public DateTime LastUpdate { get; private set; }

        public Inventory()
        {
        }
        public Inventory(int productId, int zoneId, int quantity, DateTime lastUpdate)
        {
            this.ProductId = productId;
            this.ZoneId = zoneId;
            this.Quantity = quantity;
            this.LastUpdate = lastUpdate;
        }
        public Inventory(int inventoryId, int productId, int zoneId, int quantity, DateTime lastUpdate)
        {
            this.InventoryId = inventoryId;
            this.ProductId = productId;
            this.ZoneId = zoneId;
            this.Quantity = quantity;
            this.LastUpdate = lastUpdate;
        }
    }
}