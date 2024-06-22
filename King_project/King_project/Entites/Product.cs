namespace King_project.Entites
{
    public class Product
    {
        public int Id { get; set; }
        public required string Title { get; set; }   
        public string Description { get; set; } = string.Empty;
        public float Price { get; set; }
        public string[]? Images { get; set; }

    }
}
