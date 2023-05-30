using Dapper;
using System.Collections.Generic;
using System.Data;
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
            _conn.Execute("UPDATE species SET SpeciesName = @name, Pronunciation = @pronunciation, Meaning = @meaning WHERE SpeciesID = @id",
             new { name = species.SpeciesName, pronunciation = species.Pronunciation, meaning = species.Meaning, id = species.SpeciesID });
        }

        public void InsertSpecies(Species speciesToInsert)
        {
            _conn.Execute("INSERT INTO species (SpeciesName, Pronunciation, Meaning, GenusID) VALUES (@name, @pronunciation, @meaning, @genusID);",
                new { name = speciesToInsert.SpeciesName, pronunciation = speciesToInsert.Pronunciation, meaning = speciesToInsert.Meaning, genusID = speciesToInsert.GenusID });
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
            _conn.Execute("DELETE FROM location WHERE SpeciesID = @id;", new { id = species.SpeciesID });
            _conn.Execute("DELETE FROM description WHERE SpeciesID = @id;", new { id = species.SpeciesID });
            _conn.Execute("DELETE FROM species WHERE SpeciesID = @id;", new { id = species.SpeciesID });
            _conn.Execute("DELETE FROM image WHERE SpeciesID = @id;", new { id = species.SpeciesID });
        }

    }
}
