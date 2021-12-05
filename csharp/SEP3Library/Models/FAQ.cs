using System.ComponentModel.DataAnnotations;

namespace SEP3Library.Models {
    public class FAQ {
        public int Id { get; set; }
        
        [Required(ErrorMessage = "Please specify the category")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "Please specify a category between {2} and {1} characters")]
        public string Category { get; set; }
        
        [Required(ErrorMessage = "Please specify the question")]
        [StringLength(200, MinimumLength = 10, ErrorMessage = "Please specify a question between {2} and {1} characters")]
        public string Question { get; set; }
        
        [Required(ErrorMessage = "Please specify the answer")]
        [StringLength(500, MinimumLength = 10, ErrorMessage = "Please specify an answer between {2} and {1} characters")]
        public string Answer { get; set; }
    }
}