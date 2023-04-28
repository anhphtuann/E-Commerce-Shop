using System;
using System.Collections.Generic;

namespace API.Models.Products;

public class Stock
{
    public int cateId { get; set; }
    public int proId { get; set; }
    public int vendorUserId { get; set; }
    [Required]
    [Column(TypeName = "money")]
    public decimal priceUnit { get; set; }
    [Required]
    public long quantity { get; set; }
    public DateTime dateModify { get; set; }

    [ForeignKey("proId")]
    public Product product { get; set; }
    [ForeignKey("cateId")]
    public Category category { get; set; }
}
