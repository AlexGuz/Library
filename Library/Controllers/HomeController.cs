using System.Collections.Generic;
using System.Web.Mvc;
using LibraryDB;
using LibraryDB.Models;
using System.Linq;
using System;

namespace Library.Controllers
{
    public class HomeController : Controller
    {
        private LibraryRepository<LibraryStorageUnit> _unitRepo;

        public HomeController()
        {
            _unitRepo = new LibraryRepository<LibraryStorageUnit>(new LibraryDBContext());
        }
        public ActionResult Index()
        {
            return View();
        }

        
        public ActionResult SaveToFile(string path, string name)
        {
            ViewBag.FileName = name;
            ViewBag.FilePath = path;
            return View();
        }
        
    }
}