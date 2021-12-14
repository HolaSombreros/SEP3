using System.ComponentModel.DataAnnotations;

namespace SEP3Library.Models {
    public class Review {
        [Required] public Customer Customer { get; set; }

        public int Rating { get; set; }

        [MaxLength(3000, ErrorMessage = "The comment cannot exceed 3000 characters")]
        public string Comment { get; set; }

        [Required] public int ItemId { get; set; }

        [Required(ErrorMessage = "Please enter a publication date for the review")]
        public MyDateTime DateTime { get; set; } = new MyDateTime(System.DateTime.Now);
    }
}