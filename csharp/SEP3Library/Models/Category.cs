using System.ComponentModel.DataAnnotations;

namespace SEP3Library.Models {
    public class Category {
        public int Id { get; set; }
        
        [Required(ErrorMessage = "Please specify a category name")]
        [StringLength(30, MinimumLength = 2, ErrorMessage = "Please specify a category name between {2} and {1} characters")]
        public string Name { get; set; }
    }
}