using LostAndFoundMVC.Data;
using LostAndFoundMVC.Models;
using Microsoft.AspNetCore.Mvc;

namespace LostAndFoundMVC.Controllers
{
    

    public class ItemsController : Controller
    {
        private readonly LostAndFoundContext _context;

        public ItemsController(LostAndFoundContext context)
        {
            _context = context;
        }

        public IActionResult ViewClaimedItems()
        {
            var claimedItems = _context.Claimed.ToList();
            return View(claimedItems);
        }

        public IActionResult ViewFoundItems()
        {
            var foundItems = _context.NotClaimed
                .Where(nc => nc.ReportType == "Found")
                .ToList();
            return View(foundItems);
        }

        public IActionResult ViewLostItems()
        {
            var lostItems = _context.NotClaimed
                .Where(nc => nc.ReportType == "Lost")
                .ToList();
            return View(lostItems);
        }

        [HttpPost]
        public async Task<IActionResult> Claimed(int id)
        { 
            var notClaimedItem = await _context.NotClaimed.FindAsync(id);
            if (notClaimedItem == null)
            {
                return NotFound();
            }

            var claimedItem = new Claimed
            {
                VisitorName = notClaimedItem.VisitorName,
                ReportType = notClaimedItem.ReportType,
                ItemType = notClaimedItem.ItemType,
                Location = notClaimedItem.Location,
                Description = notClaimedItem.Description,
                ImageUrl = notClaimedItem.ImageUrl,
                Email = notClaimedItem.Email,
                PhoneNumber = notClaimedItem.PhoneNumber,
                ReportDate = notClaimedItem.ReportDate
            };
            _context.Claimed.Add(claimedItem);

            _context.NotClaimed.Remove(notClaimedItem);

            await _context.SaveChangesAsync();

            if(notClaimedItem.ReportType == "Found")
            {
                return RedirectToAction("ViewFoundItems");
            }
            else
            {
                return RedirectToAction("ViewLostItems");
            }
            
        }
    }
}
