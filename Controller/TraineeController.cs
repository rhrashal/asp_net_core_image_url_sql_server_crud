using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Evi_test
{
    public class TraineeController : Controller
    {
        IHostingEnvironment _env;
        TestDbContex db;
        public  TraineeController(TestDbContex Db, IHostingEnvironment env)
        {
            db = Db;
            _env = env;
        }
        // GET: /<controller>/
        public IActionResult Index()
        {
            TraineeView trainee = new TraineeView();
            trainee.Trainees = db.trainees.ToList();
            return View(trainee);
        }
        [HttpGet]
        public IActionResult Create()
        {
            
            return View();
        }
        [HttpPost]
        public IActionResult Create(Trainee trainee, IFormFile formFile)
        {
            try 
            {
                using (var strem = new FileStream(_env.WebRootPath + "\\Image\\" + formFile.FileName, FileMode.Create))
                {
                    formFile.CopyTo(strem);
                }
                trainee.ImageUrl = "~/Image/" + formFile.FileName;

                db.trainees.Add(trainee);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            catch (Exception ex) 
            {
                return View();
            }

            
        }

        [HttpGet]
        public IActionResult Edit(int Id)
        {
            var trainee = db.trainees.Find(Id);
            return View(trainee);
        }
        [HttpPost]
        public IActionResult Edit(Trainee trainee, IFormFile formFile)
        {
            try
            {
                if(formFile != null)
                {
                    using (var strem = new FileStream(_env.WebRootPath + "\\Image\\" + formFile.FileName, FileMode.Create))
                    {
                        formFile.CopyTo(strem);
                    }
                    trainee.ImageUrl = "~/Image/" + formFile.FileName;
                }
                

                db.trainees.Update(trainee);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                return View();
            }

            
        }

        public IActionResult Delete(int Id)
        {
            var trainee = db.trainees.Find(Id);
            db.trainees.Remove(trainee);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
