using System.ComponentModel.DataAnnotations;

namespace LostAndFoundMVC.Models
{
    public class Reports
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string VisitorName { get; set; } = null!; // Name of the person reporting the item
        [Required]
        public string ReportType { get; set; } = null!; // "Lost" or "Found"
        [Required]
        public string ItemType { get; set; } = null!; // e.g., "Wallet", "Phone"
        [Required]
        public string Location { get; set; } = null!; // Location where the item was lost or found
        [Required]
        public string Description { get; set; } = null!; // Description of the item
        [Required]
        public string ImageUrl { get; set; } = null!; // URL of the image of the item, if available
        [Required]
        [EmailAddress]
        public string Email { get; set; } = null!; //  contacts information for follow-up
        [Required]
        public string PhoneNumber { get; set; } = null!; //  contacts information for follow-up
        public DateTime ReportDate { get; set; } = DateTime.Now; // Date when the report was made
    }
}
