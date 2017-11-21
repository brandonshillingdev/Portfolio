namespace DbOps2
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Financial")]
    public partial class Financial
    {
        public int Id { get; set; }

        [Required]
        [StringLength(30)]
        public string Client { get; set; }

        public int ClientId { get; set; }

        public decimal Amount { get; set; }

        public int PhotographerId { get; set; }

        [Required]
        [StringLength(30)]
        public string Photographer { get; set; }

        [StringLength(10)]
        public string Paid { get; set; }

        public int AdminId { get; set; }

        [StringLength(50)]
        public string Company { get; set; }

        public DateTime? DueDate { get; set; }

        public virtual User User { get; set; }
    }
}
