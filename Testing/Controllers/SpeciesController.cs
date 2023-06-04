using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Dapper;
using Microsoft.AspNetCore.Mvc;
using MySql.Data.MySqlClient;
using Testing.Models;
using System.IO;

namespace Testing.Controllers
{
    public class SpeciesController : Controller
    {
        private readonly ISpeciesRepository repo;
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

        //public IActionResult UpdateSpecies(int id)
        //{
        //    Species species = repo.GetSpecies(id);
        //    if (species == null)
        //    {
        //        return View("SpeciesNotFound");
        //    }
        //    return View(species);
        //}

        public IActionResult UpdateSpecies(int id)
        {
            Species species = repo.GetSpecies(id);
            if (species == null)
            {
                return View("SpeciesNotFound");
            }
            species.GenusNames = repo.PickGenusName();
            return View(species);
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

        //public IActionResult GetImage(int id)
        //{
        //    var species = repo.GetSpecies(id);
        //    if (species != null && species.Image != null)
        //    {
        //        return File(species.Image, "image/jpeg");
        //    }
        //    return NotFound();

        //}

        public IActionResult GetImage(int id)
        {
            var species = repo.GetSpecies(id);
            if (species != null && species.Image != null)
            {
                // Set the appropriate content type based on the image format
                string contentType = "image/jpeg"; // Modify if your images are in a different format

                // Return the image as a file
                return File(species.Image, contentType);
            }
            return NotFound();
        }

    }
}