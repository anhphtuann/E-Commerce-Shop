using System;
using System.Collections.Generic;
using API.Models.Products;
using API.Models.Users;

namespace API.Models.Reviews;

[Table("Reviews")]
public class Review
{
    [Key]
    public int reviewId { get; set; }
    public int userId { get; set; }
    public int proId { get; set; }
    public double rate { get; set; }
    public string comment { get; set; }
    public DateTime date { get; set; }
    
    [ForeignKey("userId")]
    public User user { get; set; }
    [ForeignKey("proId")]
    public Product product { get; set; }
}
