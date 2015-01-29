using System.ComponentModel.DataAnnotations.Schema;

namespace OdataService.Models
{

    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }

        [ForeignKey("Category")]
        public int CategoryId { get; set; }

        
        [ForeignKey("Supplier")]
        public int? SupplierId { get; set; }

        public virtual Supplier Supplier { get; set; }

        public virtual Category Category { get; set; }
    }
}