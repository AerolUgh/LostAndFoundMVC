using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Http;

namespace LostAndFoundMVC.Models
{
    public class LostItems
    {
        [Key]
        public int LostItemId { get; set; }

        [Required]
        public string Name { get; set; } = null!;

        [Required]
        public string ItemType { get; set; } = null!;

        [Required]
        public string Description { get; set; } = null!;

        [Required]
        public string LocationLost { get; set; } = null!;

        public string ImageUrl { get; set; } = ""; // Don't require, set in controller

        [Required]
        [EmailAddress]
        public string Email { get; set; } = null!;

        [Required]
        public string PhoneNumber { get; set; } = null!;

        public DateTime DateLost { get; set; } = DateTime.Now;

        [NotMapped]
        public IFormFile? ImageFile { get; set; }
    }
}
