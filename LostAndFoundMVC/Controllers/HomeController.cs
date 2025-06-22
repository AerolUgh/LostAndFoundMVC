using LostAndFoundMVC.Data;
using LostAndFoundMVC.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace LostAndFoundMVC.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly LostAndFoundContext _context;


        public HomeController(ILogger<HomeController> logger, LostAndFoundContext context)
        {
            _logger = logger;
            _context = context;
        }
        public IActionResult DashboardAdmin()
        {
            var reports = _context.Reports.ToList();

            return View(reports);
        }


        public IActionResult DashboardVisitors()
        {
            return View();
        }

            

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
