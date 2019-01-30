using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EntityCore.Model
{
    public partial class Users
    {
        public Users()
        {
            Posts = new HashSet<Posts>();
        }

        [Key]
        public int UserId { get; set; }
        [Required]
        [StringLength(50)]
        public string Username { get; set; }
        [Required]
        [StringLength(50)]
        public string Password { get; set; }
        [Required]
        [StringLength(50)]
        public string Email { get; set; }
        [Required]
        [StringLength(50)]
        public string FirstName { get; set; }
        [Required]
        [StringLength(50)]
        public string LastName { get; set; }
        public byte[] ProfilePicture { get; set; }
        public int Admin { get; set; }

        [InverseProperty("User")]
        public ICollection<Posts> Posts { get; set; }
    }
}
