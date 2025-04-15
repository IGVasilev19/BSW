namespace WarehouseManagementSystem.Models
{
    public class Zone
    {
        public int ZoneId { get; private set; }
        public string Name { get; private set; }
        public int Capacity { get; private set; }
        public int WarehouseId { get; private set; }

        public Zone(int zoneId, string name, int capacity, int warehouseId)
        {
            this.ZoneId = zoneId;
            this.Name = name;
            this.Capacity = capacity;
            this.WarehouseId = warehouseId;
        }
    }
}
