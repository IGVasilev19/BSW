namespace Entities
{
    public class Warehouse
    {
        public int WarehouseId { get; private set; }
        public string Name { get; private set; }
        public int AddressId { get; private set; }

        public Warehouse(string name)
        {
            this.Name = name;
        }

        public Warehouse(string name, int addressId)
        {
            this.Name = name;
            this.AddressId = addressId;
        }

        public Warehouse(int WarehouseId, string name, int addressId)
        {
            this.WarehouseId = WarehouseId;
            this.Name = name;
            this.AddressId = addressId;
        }
    }
}