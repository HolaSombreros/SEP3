using System.ComponentModel.DataAnnotations;
using SEP3Library.Models;

namespace SEP3Library.UIModels {
    public class ReviewModel {
        public int Id { get; set; }
        [Required]
        public int CustomerId { get; set; }
        public string CustomerFirstName { get; set; }
        public string CustomerLastName { get; set; }
        public int Rating { get; set; }
        
        [MaxLength(3000, ErrorMessage = "The comment cannot exceed 3000 characters")]
        public string Comment { get; set; }
        [Required]
        public int ItemId { get; set; }
        [Required(ErrorMessage = "Please enter a publication date for the review")]
        public MyDateTime DateTime {get; set; }
    }
}