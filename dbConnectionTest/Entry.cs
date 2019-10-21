namespace dbConnectionTest
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("entry")]
    public partial class Entry
    {
        [Key]
        public int EntryId { get; set; }

        [Required]
        [StringLength(20)]
        public string Descr { get; set; }

        [Required]
        [StringLength(15)]
        public string ContactNum { get; set; }

        public int ContactId { get; set; }

        public virtual Contact Contact { get; set; }
    }
}
