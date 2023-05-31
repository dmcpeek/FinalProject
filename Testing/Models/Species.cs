using System.Collections.Generic;

namespace Testing.Models
{
    public class Species
    {
        public Species()
        {
        }

        public int SpeciesID { get; set; }
        public string SpeciesName { get; set; }
        public string Pronunciation { get; set; }
        public int GenusID { get; set; }
        public string Meaning { get; set; }
        public IEnumerable<Genus> Genus { get; set; }
        public Dictionary<int,string> GenusNames { get; set; }
        public string Taxonomist { get; set; }
        public int DiscoveryYear { get; set; }
        public bool MottledLeaf { get; set;}
        public byte[] Image { get; set; }
        public string ImageName { get; set; }
        public string CountryOfOrigin { get; set; }
        public decimal Latitude { get; set; }
        public decimal Longitude { get; set; }
    }
}
