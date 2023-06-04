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
            Species species = repo.GetSpecies(id);
            if (species == null)
            {
                return View("SpeciesNotFound");
            }
            return View(species);
        }

        [HttpPost]
        public IActionResult UpdateSpecies(Species species)
        {
<<<<<<< HEAD
            if (ModelState.IsValid)
            {
                repo.UpdateSpecies(species);
                return RedirectToAction("ViewSpecies", new { id = species.SpeciesID });
            }
            return View(species);
=======
            repo.UpdateSpecies(species);
            return RedirectToAction("ViewSpecies", new { id = species.SpeciesID });
>>>>>>> 4e3fad0a6267a2a5b2b5017802810497202d550a
        }

        public IActionResult InsertSpecies()
        {
            var spec = repo.AssignGenus();
            return View(spec);
        }

        [HttpPost]
        public IActionResult InsertSpecies(Species speciesToInsert)
        {
            if (ModelState.IsValid)
            {
                repo.InsertSpecies(speciesToInsert);
                return RedirectToAction("Index");
            }
            return View(speciesToInsert);
        }

        public IActionResult DeleteSpecies(int id)
        {
            var species = repo.GetSpecies(id);
            if (species == null)
            {
                return View("SpeciesNotFound");
            }
            repo.DeleteSpecies(species);
            return RedirectToAction("Index");
        }

        public IActionResult GetImage(int id)
        {
            var species = repo.GetSpecies(id);
            if (species != null && species.Image != null)
            {
                return File(species.Image, "image/jpeg");
<<<<<<< HEAD
            }
            return NotFound();
        }
=======
    }
<<<<<<< HEAD
            return NotFound();
}
>>>>>>> ImageData1
    }
}


        //public IActionResult GetImage(int id)
        //{
        //    var species = repo.GetSpecies(id);
        //    return File(species.Image, "image/jpeg");
        //}
=======
}
>>>>>>> 4e3fad0a6267a2a5b2b5017802810497202d550a
