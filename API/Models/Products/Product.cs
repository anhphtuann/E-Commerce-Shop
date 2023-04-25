using System;
using System.Collections.Generic;
using API.Models.Reviews;

namespace API.Models.Products;

[Table("Products")]
public class Product
{
    [Key]
    public int proId { get; set; }

    [MaxLength(50)]
    public string productName { get; set; }
    public string productDescription { get; set; }
    public string brand { get; set; }
    public bool status { get; set; } = false;
    public int cateId { get; set; }

    [ForeignKey("cateId")]
    public virtual Category category { get; set; }

    public virtual ICollection<Review> reviews { get; set; }
}

