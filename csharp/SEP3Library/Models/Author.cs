using System.ComponentModel.DataAnnotations;

namespace SEP3Library.Models {
    public class Author {
        
        public int Id { get; set; }
        [MaxLength(50, ErrorMessage = "The first name cannot exceed 50 characters")]
        [Required(ErrorMessage = "Please enter a first name for the author")]
        public string FirstName { get; set; }
        [Required(ErrorMessage = "Please enter a last name for the author")]
        [MaxLength(50, ErrorMessage = "The first name cannot exceed 50 characters")]
        public string LastName { set; get; }
    }
}