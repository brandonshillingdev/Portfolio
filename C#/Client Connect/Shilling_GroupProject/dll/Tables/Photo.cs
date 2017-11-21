namespace DbOps2
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Photo
    {
        public int Id { get; set; }

        public int Client { get; set; }

        [Column("Photo")]
        [Required]
        public byte[] Photo1 { get; set; }

        public int PhotographerId { get; set; }

        public int Released { get; set; }

        public virtual User User { get; set; }
    }
}
