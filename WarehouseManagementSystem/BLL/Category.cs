namespace Domain
{
    public class Category
    {
        public int CategoryId { get; private set; }
        public string Name { get; private set; }
        public int WarehouseId { get; private set; }

        public Category(string name)
        {
            this.Name = name;
        }

        public Category(string name, int warehouseId)
        {
            this.Name = name;
            this.WarehouseId = warehouseId;
        }

        public Category(int categoryId, string name)
        {
            this.CategoryId = categoryId;
            this.Name = name;
        }

        public Category(int categoryId, string name, int warehouseId)
        {
            this.CategoryId = categoryId;
            this.Name = name;
            WarehouseId = warehouseId;
        }
    }
}