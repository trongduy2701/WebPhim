namespace WebsitePhimHoatHinh18.Models.EF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Comments
    {
        [Key]
        public int CommentID { get; set; }

        public int? UserID { get; set; }

        public int? MovieID { get; set; }

        public string CommentText { get; set; }

        [Range(1, 5, ErrorMessage = "Rating must be between 1 and 5.")]
        public int? Rating { get; set; }

        public DateTime? CreatedDate { get; set; }

        public virtual Movies Movies { get; set; }

        public virtual Users Users { get; set; }
    }
}
