using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using SEP3Library.Models;

namespace SEP3Library.UIModels {
    public class BookModel : ItemModel {
        
        [Required(ErrorMessage = "Please enter an isbn")]
        [MaxLength(20, ErrorMessage = "The first name cannot exceed 20 characters")]
        public string Isbn { get; set; }
        
        [MaxLength(50, ErrorMessage = "The first name cannot exceed 50 characters")]
        [Required(ErrorMessage = "Please enter a first name for the author")]
        public string AuthorFirstName { get; set; }
        
        [Required(ErrorMessage = "Please enter a last name for the author")]
        [MaxLength(50, ErrorMessage = "The first name cannot exceed 50 characters")]
        public string AuthorLastName { get; set; }
        
        [Required(ErrorMessage = "Please enter a publication date for the author")]
        public MyDateTime PublicationDate { get; set; }
        
        [Required(ErrorMessage = "Please enter a language")]
        [MaxLength(20, ErrorMessage = "The first name cannot exceed 20 characters")]
        public string Language { get; set; }
        
        [Required(ErrorMessage = "Please enter at least a genre")]
        public List<Genre> Genre { get; set; }
    }
}