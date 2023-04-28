using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Models.Reviews;


namespace API.Models.Users
{
    [Table("Users")]
    public class User
    {
        [Key]
        public int userId { get; set; }
        [MaxLength(50)]
        public string firstName { get; set; }
        [MaxLength(50)]
        public string lastName { get; set; }
        [MaxLength(50)]
        public string account { get; set; }
        public byte[] passwordHash { get; set; } = new byte[0];
        public byte[] passwordSalt { get; set; } = new byte[0];
        public string role { get; set; }
        public string email { get; set; }
        public string contact { get; set; }
        public ICollection<Review> reviews { get; set; }
    }
}