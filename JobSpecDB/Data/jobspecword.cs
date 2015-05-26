namespace JobSpecDB.Data
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("jobspecs.jobspecwords")]
    public partial class jobspecword
    {
        [Key]
        [Column(Order = 0, TypeName = "uint")]
        public long jobspecwordId { get; set; }

        [Column(Order = 1, TypeName = "uint")]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public long wordId { get; set; }

        [Column(Order = 2, TypeName = "uint")]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public long jobspecId { get; set; }
    }
}
