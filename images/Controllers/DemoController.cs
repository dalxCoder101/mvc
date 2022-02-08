using System.Diagnostics;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using shopContext.Models;
using MySql.Data;
using MySql;
using MySql.Data.MySqlClient;
using System.Dynamic;
using shopContext.ViewModel;

namespace shopContext.Controllers
{
    public class DemoController : Controller
    {

        private readonly shapiContext _context = new shapiContext();

        public DemoController(shapiContext context)
        {
            _context = context;
        }
        //Author
        [HttpGet]
        public IActionResult Author()
        {

            SampleVM vmodel = new SampleVM();

            vmodel.Authors= _context.Authors.ToList();
            return View(vmodel);
        }
        
        //Books
            public IActionResult Book()
        {
           var _author = _context.Authors.ToList();
           ViewData["Book"]=_author;//Send this to view

            SampleVM vmodel = new SampleVM();

            vmodel.Books= _context.Categories.ToList();
            return View(vmodel);
        }

        //Add author
        [HttpGet]
        public IActionResult Create_author()
        {
          
            return View();
        }

        [HttpPost]
        public IActionResult Create_author(Author aut)
        {
            _context.Authors.Add(aut);
            _context.SaveChanges();

            SampleVM vmodel = new SampleVM();
            vmodel.Authors = _context.Authors.ToList();

            return View("Author", vmodel);
        }


        //Add Books
        [HttpGet]
        public IActionResult Create_book()
        {
            
            var categories = _context.Authors.ToList();
            ViewData["Create_book"] = categories; // Send this list to the view

            return View();
        }

        [HttpPost]
        public IActionResult Create_book(Category ctg)
        {
            _context.Categories.Add(ctg);
            _context.SaveChanges();

            var _author = _context.Authors.ToList();
            ViewData["Book"]=_author;

            SampleVM vmodel = new SampleVM();
            vmodel.Books = _context.Categories.ToList();
            vmodel.Authors = _context.Authors.ToList();
            return View("Book", vmodel);
        }


        //Update/Delete for Books
        public IActionResult Edit_cat(int id, int auth)
        {
            var categories = _context.Authors.ToList();
            ViewData["Create_book"] = categories; // Send this list to the view

            var model = new Category();
            model.auth_ID = auth;


            ViewBag.Message = model; 


            return View(_context.Categories.Where(q => q.ID == id).FirstOrDefault());
        }

        [HttpPost]
        public IActionResult Edit_cat(Category category)
        {
            if (ModelState.IsValid)
            {
                _context.Categories.Update(category);
                _context.SaveChanges();

                var _author = _context.Authors.ToList();
                ViewData["Book"]=_author; 

                return RedirectToAction("Book");
            }
            return View(category);
        }


        public IActionResult Delete(int id)
        {
            var model = _context.Categories.Where(q => q.ID == id).FirstOrDefault();
            _context.Categories.Remove(model);
            _context.SaveChanges();
            return RedirectToAction("Book");
        }



        //Update/Delete for Authors
        public IActionResult Edit_auth(int id)
        {
            return View(_context.Authors.Where(q => q.ID == id).FirstOrDefault());
        }

        [HttpPost]
        public IActionResult Edit_auth(Author author)
        {
            if (ModelState.IsValid)
            {
                _context.Authors.Update(author);
                _context.SaveChanges();
                return RedirectToAction("Author");
            }
            return View(author);
        }


        public IActionResult Delete_auth(int id)
        {
            var model = _context.Authors.Where(q => q.ID == id).FirstOrDefault();
            _context.Authors.Remove(model);
            _context.SaveChanges();
            return RedirectToAction("Author");
        }



    }//end
}//end

