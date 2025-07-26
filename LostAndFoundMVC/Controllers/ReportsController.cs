using LostAndFoundMVC.Data;
using LostAndFoundMVC.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LostAndFoundMVC.Controllers
{
    public class ReportsController : Controller
    {
        private readonly LostAndFoundContext _context;


        public ReportsController(LostAndFoundContext context)
        {
            _context = context;
        }

        public IActionResult ReportFoundItem()
        {
            return View();
        }

        public IActionResult ReportLostItem()
        {
            return View();
        }

        

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult ReportFoundItem(FoundItems foundItem)
        {
            if (ModelState.IsValid)
            {
                if (foundItem.ImageFile != null && foundItem.ImageFile.Length > 0)
                {
                    var uploadsDir = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/uploads");

                    // Ensure the folder exists
                    if (!Directory.Exists(uploadsDir))
                        Directory.CreateDirectory(uploadsDir);

                    var fileName = Path.GetFileName(foundItem.ImageFile.FileName);
                    var filePath = Path.Combine(uploadsDir, fileName);

                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        foundItem.ImageFile.CopyTo(stream);
                    }

                    // Save only the relative path
                    foundItem.ImageUrl = "/uploads/" + fileName;
                }

                _context.FoundItems.Add(foundItem);

                var report = new Reports
                {
                    VisitorName = foundItem.Name,
                    ReportType = "Found",
                    ItemType = foundItem.ItemType,
                    Location = foundItem.LocationFound,
                    Description = foundItem.Description,
                    Email = foundItem.Email,
                    PhoneNumber = foundItem.PhoneNumber,
                    ReportDate = DateTime.Now,
                    ImageUrl = foundItem.ImageUrl ?? ""
                };

                _context.Reports.Add(report);
                _context.SaveChanges();

                return RedirectToAction("VisitorViewFoundItems", "Items");
            }

            // DEBUG: Log model errors
            var errors = ModelState
                .Where(x => x.Value.Errors.Any())
                .Select(x => new { x.Key, x.Value.Errors })
                .ToList();

            foreach (var error in errors)
            {
                Console.WriteLine($"Field: {error.Key}, Error: {string.Join(", ", error.Errors.Select(e => e.ErrorMessage))}");
            }

            return View(foundItem);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult ReportLostItem(LostItems lostItem)
        {
            if (ModelState.IsValid)
            {
                if (lostItem.ImageFile != null && lostItem.ImageFile.Length > 0)
                {
                    var uploadsDir = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/uploads");

                    // Ensure the folder exists
                    if (!Directory.Exists(uploadsDir))
                        Directory.CreateDirectory(uploadsDir);

                    var fileName = Path.GetFileName(lostItem.ImageFile.FileName);
                    var filePath = Path.Combine(uploadsDir, fileName);

                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        lostItem.ImageFile.CopyTo(stream);
                    }

                    // Save only the relative path
                    lostItem.ImageUrl = "/uploads/" + fileName;
                }

                _context.LostItems.Add(lostItem);

                var report = new Reports
                {
                    VisitorName = lostItem.Name,
                    ReportType = "Lost",
                    ItemType = lostItem.ItemType,
                    Location = lostItem.LocationLost,
                    Description = lostItem.Description,
                    Email = lostItem.Email,
                    PhoneNumber = lostItem.PhoneNumber,
                    ReportDate = lostItem.DateLost,
                    ImageUrl = lostItem.ImageUrl ?? ""
                };

                _context.Reports.Add(report);
                _context.SaveChanges();

                return RedirectToAction("VisitorViewLostItems", "Items");
            }

            // DEBUG: Log model errors
            var errors = ModelState
                .Where(x => x.Value.Errors.Any())
                .Select(x => new { x.Key, x.Value.Errors })
                .ToList();

            foreach (var error in errors)
            {
                Console.WriteLine($"Field: {error.Key}, Error: {string.Join(", ", error.Errors.Select(e => e.ErrorMessage))}");
            }

            return View(lostItem);
        }

        [HttpPost]
        public async Task<IActionResult> Accept(int id)
        {
            var report = await _context.Reports.FindAsync(id);
            if (report == null)
            {
                return NotFound();
            }

            // Add to NotClaimed
            var notClaimed = new NotClaimed
            {
                VisitorName = report.VisitorName,
                ReportType = report.ReportType,
                ItemType = report.ItemType,
                Location = report.Location,
                Description = report.Description,
                Email = report.Email,
                PhoneNumber = report.PhoneNumber,
                ReportDate = report.ReportDate,
                ImageUrl = report.ImageUrl ?? ""
            };
            _context.NotClaimed.Add(notClaimed);

            // Optionally remove the report or mark as accepted
            _context.Reports.Remove(report);

            await _context.SaveChangesAsync();

            if(report.ReportType == "Found")
            {
                return RedirectToAction("ViewFoundItems", "Items");
            }
            else
            {
                return RedirectToAction("ViewLostItems", "Items");
            }
        }
    }
}
