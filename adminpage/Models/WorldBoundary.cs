using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using NetTopologySuite.Geometries;

namespace adminpage.Models
{
    public partial class WorldBoundary
    {
        public int Gid { get; set; }
        [NotMapped]
        public string? Status { get; set; }
        [NotMapped]

        public string? ColorCode { get; set; }
        [NotMapped]

        public string? Region { get; set; }
        [NotMapped]

        public string? Iso3 { get; set; }
        [NotMapped]

        public string? Continent { get; set; }
 
        public string? Name { get; set; }
        [NotMapped]

        public string? Iso31661 { get; set; }
        [NotMapped]

        public string? FrenchShor { get; set; }
        [NotMapped]

        public Geometry? Geom { get; set; }
        public int? FugitiveStatus { get; set; }
        public string? GeneralInfo { get; set; }
        public string? EntryDoc { get; set; }
        public string? RegDoc { get; set; }
        public string? Transport { get; set; }
        public string? Housing { get; set; }
        public string? Nutrition { get; set; }
        public string? Pets { get; set; }
        public string? Charity { get; set; }
        public string? AddInfo { get; set; }
    }
}
