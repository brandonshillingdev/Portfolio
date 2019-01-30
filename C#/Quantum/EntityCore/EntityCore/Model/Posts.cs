using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EntityCore.Model
{
    public partial class Posts
    {
        public Posts()
        {
            PostItems = new HashSet<PostItems>();
        }

        [Key]
        public int PostId { get; set; }
        public int UserId { get; set; }
        [Required]
        [StringLength(50)]
        public string Title { get; set; }
        [Required]
        [StringLength(5000)]
        public string Description { get; set; }
        [Required]
        [StringLength(20)]
        public string Privacy { get; set; }
        [Required]
        [StringLength(20)]
        public string Status { get; set; }

        [ForeignKey("UserId")]
        [InverseProperty("Posts")]
        public Users User { get; set; }
        [InverseProperty("Post")]
        public ICollection<PostItems> PostItems { get; set; }
    }
}
