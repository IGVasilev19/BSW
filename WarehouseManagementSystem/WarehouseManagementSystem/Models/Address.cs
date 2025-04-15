namespace WarehouseManagementSystem.Models
{
    public class Address
    {
        public int AddressId { get; private set; }
        public string Country {  get; private set; }
        public string City { get; private set; }
        public string StreetName { get; private set; }
        public int StreetNumber { get; private set; }
        public int Zip {  get; private set; }
    }
}
