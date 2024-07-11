using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MiniSimulator.Data;
using MiniSimulator.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace MiniSimulator.Controllers
{
    public class TeamController : Controller
    {
        private readonly ApplicationDbContext _db;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public TeamController(ApplicationDbContext db, IWebHostEnvironment webHostEnvironment)
        {

        _db = db;
         _webHostEnvironment = webHostEnvironment;
        }
    
        public IActionResult Index()
        {
            IEnumerable<Team> objList = _db.Team;
            return View(objList);
        }

        //GET for EDIT
        public IActionResult Edit(int? Id)
        {
            if (Id == null || Id == 0)
            {
                return NotFound();
            }
            var obj = _db.Team.Find(Id);

            if (obj == null)
            {
                return NotFound();
            }

            return View(obj);
        }

        //Post for EDIT
        [HttpPost]        
        public IActionResult Edit(Team obj)
        {
            if (ModelState.IsValid)
            {
                var files = HttpContext.Request.Form.Files;
                string webRootPath = _webHostEnvironment.WebRootPath;
                string imagePath = webRootPath + WC.ImagePath;
                var objFromDb = _db.Team.AsNoTracking().FirstOrDefault(u => u.Id == obj.Id);

                if (files.Count > 0) //If there is a file that need to update
                {
                    string upload = webRootPath + WC.ImagePath;
                    string fileName = Guid.NewGuid().ToString();
                    string extension = Path.GetExtension(files[0].FileName);

                    var oldFile = Path.Combine(upload, objFromDb.Image);

                    if (System.IO.File.Exists(oldFile))
                    {
                        System.IO.File.Delete(oldFile); //Delete the old image
                    }

                    using (var fileStream = new FileStream(Path.Combine(upload, fileName + extension), FileMode.Create))
                    {
                        files[0].CopyTo(fileStream);
                    }

                    obj.Image = fileName + extension;
                }
                else
                {
                    obj.Image = objFromDb.Image;
                }



                _db.Team.Update(obj);
                _db.SaveChanges();
                
            }
            return RedirectToAction("Index", "Home");
        }
    }
}
