using System;
using System.Collections.Generic;


namespace API.Models.Products;

[Table("Categories")]
public class Category
{
    [Key]
    public int cateId { get; set; }

    [Required]
    [MaxLength(50)]
    public string categoryName { get; set; }

    [Required]
    public string location { get; set; }

    public ICollection<Product> products { get; set; }
}
