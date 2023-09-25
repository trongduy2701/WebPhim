namespace WebsitePhimHoatHinh18.Models.EF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Reports
    {
        [Key]
        public int ReportID { get; set; }

        public int? UserID { get; set; }

        public int? MovieID { get; set; }

        public string ReportText { get; set; }

        public DateTime? CreatedDate { get; set; }

        public virtual Movies Movies { get; set; }

        public virtual Users Users { get; set; }
    }
}
