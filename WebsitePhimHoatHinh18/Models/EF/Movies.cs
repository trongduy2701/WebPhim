namespace WebsitePhimHoatHinh18.Models.EF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Movies
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Movies()
        {
            Comments = new HashSet<Comments>();
            Favorites = new HashSet<Favorites>();
            Reports = new HashSet<Reports>();
        }

        [Key]
        public int MovieID { get; set; }

        public int? GenreID { get; set; }

        public int? CountryID { get; set; }

        public string Tilte { get; set; }

        public string Director { get; set; }

        public string Description { get; set; }

        public string Year { get; set; }

        public string Time { get; set; }

        public int? View { get; set; }

        public int? Rate { get; set; }

        public string Image { get; set; }

        public string Video { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Comments> Comments { get; set; }

        public virtual Countries Countries { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Favorites> Favorites { get; set; }

        public virtual Genres Genres { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Reports> Reports { get; set; }
    }
}
