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
    }
}
