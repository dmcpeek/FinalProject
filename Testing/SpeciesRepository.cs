using Dapper;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using Testing.Models;

namespace Testing
{
    public class SpeciesRepository : ISpeciesRepository
    {
        private readonly IDbConnection _conn;
        public SpeciesRepository(IDbConnection conn)
        {
            _conn = conn;
        }

        public IEnumerable<Species> GetAllSpecies()
        {
            return _conn.Query<Species>("SELECT * FROM species;");
        }

        public Species GetSpecies(int id)
        {
            return _conn.QuerySingle<Species>("SELECT * FROM species WHERE SpeciesID = @id", new { id = id });
        }

        public void UpdateSpecies(Species species)
        {
            _conn.Execute("UPDATE species SET " +
                "SpeciesName = @name, " +
                "Pronunciation = @pronunciation, " +
                "Meaning = @meaning, " +
                "Taxonomist = @taxonomist, " +
                "DiscoveryYear = @year, " +
                "CountryOfOrigin = @country, " +
                "Latitude = @lat, " +
                "Longitude = @lon, " +
                "ImageName = @imagename, " +
                "MottledLeaf = @leaf " +
                "WHERE SpeciesID = @id;",
             new { name = species.SpeciesName, 
                 pronunciation = species.Pronunciation, 
                 meaning = species.Meaning,                  
                 taxonomist = species.Taxonomist, 
                 year = species.DiscoveryYear, 
                 country = species.CountryOfOrigin, 
                 lat = species.Latitude, 
                 lon = species.Longitude,
                 imagename = species.ImageName,
                 leaf = species.MottledLeaf,
                 id = species.SpeciesID
             }); 

        }

        public void InsertSpecies(Species speciesToInsert)
        {
            _conn.Execute("INSERT INTO species (" +
                "GenusID, " +
                "SpeciesName, " +
                "Pronunciation, " +
                "Meaning, " +
                "Taxonomist, " +
                "DiscoveryYear, " +
                "CountryOfOrigin, " +
                "Latitude, " +
                "Longitude, " +
                "ImageName, " +
                "MottledLeaf )" + 
                "VALUES (@genusID, @name, @pronunciation, @meaning, @taxonomist, @year, @country, @lat, @lon, @imagename, @leaf);",
                new {
                    genusID = speciesToInsert.GenusID, 
                    name = speciesToInsert.SpeciesName,
                    pronunciation = speciesToInsert.Pronunciation,
                    meaning = speciesToInsert.Meaning, 
                    taxonomist = speciesToInsert.Taxonomist, 
                    year = speciesToInsert.DiscoveryYear, 
                    country = speciesToInsert.CountryOfOrigin, 
                    lat = speciesToInsert.Latitude, 
                    lon = speciesToInsert.Longitude,
                    imagename = speciesToInsert.ImageName,
                    leaf = speciesToInsert.MottledLeaf
                });
        }

        public IEnumerable<Genus> GetGenus()
        {
            return _conn.Query<Genus>("SELECT * FROM genus;");
        }
        public Species AssignGenus()
        {
            var genusList = GetGenus();
            var species = new Species();
            species.Genus = genusList;
            return species;
        }

        public void DeleteSpecies(Species species)
        {
            _conn.Execute("DELETE FROM species WHERE SpeciesID = @id;", new { id = species.SpeciesID });
        }

        public Dictionary<int, string> PickGenusName()
        {
            var genusType = GetGenus();
            var res = new Dictionary<int, string>();
            foreach (Genus g in genusType)
            {
                res.Add(g.GenusID, g.GenusName);
            }
            return res;
        }
    }
}
