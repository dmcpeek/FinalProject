using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using Testing.Models;

namespace Testing
{
    public interface ISpeciesRepository
    {
        public IEnumerable<Species> GetAllSpecies();
        public Species GetSpecies(int id);
        public void UpdateSpecies(Species species);
        public void InsertSpecies(Species speciesToInsert);
        public IEnumerable<Genus> GetGenus();
        public Species AssignGenus();
        public void DeleteSpecies(Species species);
    }
}
