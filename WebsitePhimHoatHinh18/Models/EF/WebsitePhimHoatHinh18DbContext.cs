using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;

namespace WebsitePhimHoatHinh18.Models.EF
{
    public partial class WebsitePhimHoatHinh18DbContext : DbContext
    {
        public WebsitePhimHoatHinh18DbContext()
            : base("name=WebsitePhimHoatHinh18DbContext")
        {
        }

        public virtual DbSet<Comments> Comments { get; set; }
        public virtual DbSet<Countries> Countries { get; set; }
        public virtual DbSet<Favorites> Favorites { get; set; }
        public virtual DbSet<Genres> Genres { get; set; }
        public virtual DbSet<Movies> Movies { get; set; }
        public virtual DbSet<Reports> Reports { get; set; }
        public virtual DbSet<Users> Users { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
        }
    }
}
