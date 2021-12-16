using System.ComponentModel.DataAnnotations;
using System.IO;

namespace SEP3Library.Models {
    public class Customer {
        public int Id { get; set; }

        [Required(ErrorMessage = "Please enter an email address")]
        [EmailAddress(ErrorMessage = "Please enter a valid email address")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [DataType(DataType.Password)]
        public string Password { get; set; }
        
        [Required(ErrorMessage = "Please enter a first name")]
        [MaxLength(100, ErrorMessage = "The first name cannot exceed 100 characters")]
        [MinLength(2, ErrorMessage = "The first name has to be longer than 2 characters")]
        public string FirstName { get; set; }
        
        [Required(ErrorMessage = "Please enter a last name")]
        [MaxLength(100, ErrorMessage = "The last name cannot exceed 100 characters")]
        [MinLength(2, ErrorMessage = "The last name has to be longer than 2 characters")]
        public string LastName { get; set; }
        public Address Address { get; set; } = new Address();
        public string Role { get; set; }
        public string PhoneNumber { get; set; }
    }
}