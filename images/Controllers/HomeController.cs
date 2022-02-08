using System.Diagnostics;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using shopContext.Models;
using MySql.Data;
using MySql;
using MySql.Data.MySqlClient;

namespace shopContext.Controllers
{
    public class HomeController : Controller
    {
        private readonly shapiContext _context = new shapiContext();

        public HomeController(shapiContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            var list_stores = _context.Authors.ToList();
            return View(list_stores);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
