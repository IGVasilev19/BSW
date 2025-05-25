namespace Domain
{
    public class Category
    {
        public int CategoryId { get; private set; }
        public string Name { get; private set; }

        public Category(string name)
        {
            this.Name = name;
        }

        public Category(int categoryId, string name)
        {
            this.CategoryId = categoryId;
            this.Name = name;
        }
    }
}