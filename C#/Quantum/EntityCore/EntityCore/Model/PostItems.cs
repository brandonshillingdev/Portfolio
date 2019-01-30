using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EntityCore.Model
{
    public partial class PostItems
    {
        [Key]
        public int PostItemId { get; set; }
        public int PostId { get; set; }
        [Required]
        public byte[] Item { get; set; }
        [Required]
        [StringLength(50)]
        public string ItemType { get; set; }

        [ForeignKey("PostId")]
        [InverseProperty("PostItems")]
        public Posts Post { get; set; }
    }
}
