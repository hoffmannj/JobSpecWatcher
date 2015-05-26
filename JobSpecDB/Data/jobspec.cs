namespace JobSpecDB.Data
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("jobspecs.jobspecs")]
    public partial class jobspec
    {
        [Key]
        [Column(Order = 0, TypeName = "uint")]
        public long jobspecId { get; set; }

        [Column(Order = 1)]
        [StringLength(500)]
        public string title { get; set; }

        [Column(Order = 2)]
        [StringLength(1000)]
        public string link { get; set; }

        [Column(Order = 3)]
        [StringLength(1000)]
        public string source { get; set; }

        [Column(Order = 4)]
        public Guid guid { get; set; }

        [Column(Order = 5)]
        public DateTime pubdate { get; set; }

        [Column(Order = 6)]
        [StringLength(16777215)]
        public string description { get; set; }

        [Column(Order = 7)]
        [StringLength(16777215)]
        public string spectext { get; set; }
    }
}
