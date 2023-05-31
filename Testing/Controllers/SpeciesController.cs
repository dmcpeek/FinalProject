using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Dapper;
using Microsoft.AspNetCore.Mvc;
using MySql.Data.MySqlClient;
using Testing.Models;

namespace Testing.Controllers
{
    public class SpeciesController : Controller
    {
        private readonly ISpeciesRepository repo;
        //private readonly object CountryOfOrigin;
        //private string _conn;
        //private object speciesName;
        //private object pronuciation;
        //private object meaning;
        //private object image;
        //private object taxonomist;
        //private object mottledLeaf;
        //private object country;
        //private object latitude;
        //private object longitude;

        //public object Latitude { get; private set; }
        //public object Longitude { get; private set; }
        //public object Taxonomist { get; private set; }
        //public object MottledLeaf { get; private set; }
        //public object SpeciesName { get; private set; }
        //public object Pronunciation { get; private set; }
        //public object Meaning { get; private set; }

        public SpeciesController(ISpeciesRepository repo)
        {
            this.repo = repo;
        }

        public IActionResult Index()
        {
            var species = repo.GetAllSpecies();
            foreach (var speciesItem in species)
            {
                speciesItem.GenusNames = repo.PickGenusName();
            }
            return View(species);
        }

        public IActionResult ViewSpecies(int id)
        {
            var species = repo.GetSpecies(id);
            species.GenusNames = repo.PickGenusName();
            return View(species);
        }

        public IActionResult UpdateSpecies(int id)
        {
            Species prod = repo.GetSpecies(id);
            if (prod == null)
            {
                return View("SpeciesNotFound");
            }
            return View(prod);
        }

        public IActionResult UpdateSpeciesToDatabase(Species species)
        {
            repo.UpdateSpecies(species);

            return RedirectToAction("ViewSpecies", new { id = species.SpeciesID });
        }

        public IActionResult InsertSpecies()
        {
            var spec = repo.AssignGenus();
            return View(spec);
        }

        public IActionResult InsertSpeciesToDatabase(Species speciesToInsert)
        {
            repo.InsertSpecies(speciesToInsert);
            return RedirectToAction("Index");
        }

        public IActionResult DeleteSpecies(Species species)
        {
            repo.DeleteSpecies(species);
            return RedirectToAction("Index");
        }

        //public async Task<IActionResult> InsertSpeciesToDatabase(Group entity)
        ////FROM: https://stackoverflow.com/questions/50650985/dapper-for-net-core-insert-into-a-table-and-return-id-of-inserted-row
        //{
        //    using (var connection = new MySqlConnection(_conn))
        //    {
        //        await connection.OpenAsync();
        //        using (var transaction = connection.BeginTransaction())
        //        {
        //            try
        //            {
        //                var insertGenusQuery = "INSERT INTO genus (GenusName, Pronunciation) VALUES (@GenusName, @GenusPronunciation); SELECT LAST_INSERT_ID();";
        //                var insertSpeciesQuery = "INSERT INTO species (GenusID, SpeciesName, Pronunciation, Meaning) VALUES (@GenusID, @SpeciesName, @SpeciesPronunciation, @SpeciesMeaning); SELECT LAST_INSERT_ID();";
        //                var insertImageQuery = "INSERT INTO image (SpeciesID, Image) VALUES (@SpeciesID, @Image);";
        //                var insertDescriptionQuery = "INSERT INTO description (SpeciesID, Taxonomist, DiscoveryYear, MottledLeaf) VALUES (@SpeciesID, @Taxonomist, @DiscoveryYear @MottledLeaf);";
        //                var insertLocationQuery = "INSERT INTO location (SpeciesID, CountryOfOrigin, Latitude, Longitude) VALUES (@SpeciesID, @CountryOfOrigin, @Latitude, @Longitude);";

        //                var genusId = await connection.ExecuteScalarAsync<int>(insertGenusQuery, entity, transaction);
        //                var speciesId = await connection.ExecuteScalarAsync<int>(insertSpeciesQuery, new { GenusID = genusId, SpeciesName, Pronunciation, Meaning }, transaction);
        //                await connection.ExecuteAsync(insertImageQuery, new { SpeciesID = speciesId, image }, transaction);
        //                await connection.ExecuteAsync(insertDescriptionQuery, new { SpeciesID = speciesId, Taxonomist, MottledLeaf }, transaction);
        //                await connection.ExecuteAsync(insertLocationQuery, new { SpeciesID = speciesId, CountryOfOrigin, Latitude, Longitude }, transaction);

        //                transaction.Commit();

        //                return RedirectToAction("Index");
        //            }
        //            catch
        //            {
        //                transaction.Rollback();
        //                throw; // Rethrow the exception to handle it at a higher level
        //            }
        //        }
        //    }
        //}

    }
}