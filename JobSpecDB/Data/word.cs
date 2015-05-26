namespace JobSpecDB.Data
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("jobspecs.words")]
    public partial class word
    {
        [Key]
        [Column(Order = 0, TypeName = "uint")]
        public long wordId { get; set; }

        [Column("word", Order = 1)]
        [StringLength(100)]
        public string word1 { get; set; }

        [Column(Order = 2, TypeName = "bit")]
        public bool isSkill { get; set; }

        [Column(Order = 3)]
        [StringLength(100)]
        public string skillName { get; set; }
    }
}
