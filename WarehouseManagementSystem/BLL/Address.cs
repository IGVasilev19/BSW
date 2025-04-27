namespace Entities
{
    public class Address
    {
        public int AddressId { get; private set; }
        public string Country { get; private set; }
        public string City { get; private set; }
        public string StreetName { get; private set; }
        public int StreetNumber { get; private set; }
        public int Zip { get; private set; }

        public Address() { }

        public Address(string Country, string City, string StreetName, int StreetNumber, int Zip)
        {
            this.Country = Country;
            this.City = City;
            this.StreetName = StreetName;
            this.StreetNumber = StreetNumber;
            this.Zip = Zip;
        }

        public Address(int AddressId, string Country, string City, string StreetName, int StreetNumber, int Zip)
        {
            this.Country = Country;
            this.City = City;
            this.StreetName = StreetName;
            this.StreetNumber = StreetNumber;
            this.Zip = Zip;
        }
    }
}
