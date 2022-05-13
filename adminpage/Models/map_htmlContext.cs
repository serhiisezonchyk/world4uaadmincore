using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using adminpage.Controllers;

namespace adminpage.Models
{
    public partial class map_htmlContext : DbContext
    {
        public const string SessionKeyStr = "_strconn";
        public string str { get; set; }
        public map_htmlContext()
        {

        }

        public map_htmlContext(DbContextOptions<map_htmlContext> options)
            : base(options)
        {

        }

        public virtual DbSet<WorldBoundary> WorldBoundaries { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) {

        }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasPostgresExtension("postgis");

            modelBuilder.Entity<WorldBoundary>(entity =>
            {
                entity.HasKey(e => e.Gid)
                    .HasName("world-administrative-boundaries_pkey");

                entity.ToTable("world_boundaries");

                entity.HasIndex(e => e.Geom, "world-administrative-boundaries_geom_idx")
                    .HasMethod("gist");

                entity.Property(e => e.Gid)
                    .HasColumnName("gid")
                    .HasDefaultValueSql("nextval('\"world-administrative-boundaries_gid_seq\"'::regclass)");

                entity.Property(e => e.AddInfo)
                    .HasMaxLength(1000)
                    .HasColumnName("add_info");

                entity.Property(e => e.Charity)
                    .HasMaxLength(1000)
                    .HasColumnName("charity");

                entity.Property(e => e.ColorCode)
                    .HasMaxLength(255)
                    .HasColumnName("color_code");

                entity.Property(e => e.Continent)
                    .HasMaxLength(255)
                    .HasColumnName("continent");

                entity.Property(e => e.EntryDoc)
                    .HasMaxLength(1000)
                    .HasColumnName("entry_doc");

                entity.Property(e => e.FrenchShor)
                    .HasMaxLength(255)
                    .HasColumnName("french_shor");

                entity.Property(e => e.FugitiveStatus).HasColumnName("fugitive_status");

                entity.Property(e => e.GeneralInfo)
                    .HasMaxLength(1000)
                    .HasColumnName("general_info");

                entity.Property(e => e.Geom)
                    .HasColumnType("geometry(Geometry)")
                    .HasColumnName("geom");

                entity.Property(e => e.Housing)
                    .HasMaxLength(1000)
                    .HasColumnName("housing");

                entity.Property(e => e.Iso3)
                    .HasMaxLength(255)
                    .HasColumnName("iso3");

                entity.Property(e => e.Iso31661)
                    .HasMaxLength(255)
                    .HasColumnName("iso_3166_1_");

                entity.Property(e => e.Name)
                    .HasMaxLength(255)
                    .HasColumnName("name");

                entity.Property(e => e.Nutrition)
                    .HasMaxLength(1000)
                    .HasColumnName("nutrition");

                entity.Property(e => e.Pets)
                    .HasMaxLength(1000)
                    .HasColumnName("pets");

                entity.Property(e => e.RegDoc)
                    .HasMaxLength(1000)
                    .HasColumnName("reg_doc");

                entity.Property(e => e.Region)
                    .HasMaxLength(255)
                    .HasColumnName("region");

                entity.Property(e => e.Status)
                    .HasMaxLength(255)
                    .HasColumnName("status");

                entity.Property(e => e.Transport)
                    .HasMaxLength(1000)
                    .HasColumnName("transport");

                entity.Property(e => e.Upddate)
                 .HasColumnName("upddate");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
