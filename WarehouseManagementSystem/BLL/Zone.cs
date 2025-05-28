namespace Domain
{
    public class Zone
    {
        public int ZoneId { get; private set; }
        public string Name { get; private set; }
        public decimal Capacity { get; private set; }
        public int WarehouseId { get; private set; }

        public Zone(string name, decimal capacity, int warehouseId)
        {
            this.Name = name;
            this.Capacity = capacity;
            this.WarehouseId = warehouseId;
        }

        public Zone(int zoneId, string name, decimal capacity, int warehouseId)
        {
            this.ZoneId = zoneId;
            this.Name = name;
            this.Capacity = capacity;
            this.WarehouseId = warehouseId;
        }
    }

}