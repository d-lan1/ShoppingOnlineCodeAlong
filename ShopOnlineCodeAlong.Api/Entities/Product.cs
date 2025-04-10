using System.ComponentModel.DataAnnotations.Schema;

namespace ShopOnlineCodeAlong.Api.Entities
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string ImageURL{ get; set; }
        public decimal Price { get; set; }
        public int Qty { get; set; }
        public int CategoryId { get; set; }
        // Navigation property to the ProductCategory entity which enables entity framework to create a foreign key relationship
        // between the Product and ProductCategory entities. This can result in a single query (by using using Include()) to the database instead of two separate queries.
        // this is called eager loading. Eager loading retrieves related data upfront (e.g., using Include()),
        // see product repository GetItem method for an example.
        // Conversely, Lazy loading fetches related data only when accessed, potentially causing extra queries.
        [ForeignKey("CategoryId")]
        public ProductCategory ProductCategory { get; set; }
    }
}
