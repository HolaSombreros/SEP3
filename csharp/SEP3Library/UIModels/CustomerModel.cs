using System.ComponentModel.DataAnnotations;
using System.IO;

namespace SEP3Library.UIModels {
    public class CustomerModel {
        
        [DataType(DataType.EmailAddress)]
        [Required(ErrorMessage = "Please enter an email address")]
        public string Email {
            get => email;
            set {
                if (!new EmailAddressAttribute().IsValid(value)) throw new InvalidDataException("Please enter a valid email address");
                email = value;
            }
        }
        private string email;
        
        [Required(ErrorMessage = "Please enter a first name")]
        [MaxLength(100, ErrorMessage = "The first name cannot exceed 100 characters")]
        [MinLength(2, ErrorMessage = "The first name has to be longer than 2 characters")]
        public string FirstName { get; set; }
        
        [Required(ErrorMessage = "Please enter a last name")]
        [MaxLength(100, ErrorMessage = "The last name cannot exceed 100 characters")]
        [MinLength(2, ErrorMessage = "The last name has to be longer than 2 characters")]
        public string LastName { get; set; }
        
        [Required(ErrorMessage = "Please enter a city")]
        public string City { get; set; }
        
        [Required(ErrorMessage = "Please enter a street name")]
        public string Street { get; set; }
          
        [Required(ErrorMessage = "Please enter a street number")]
        public string Number { get; set; }
        
        [Range(1000,9999,ErrorMessage="Zipcode must be between 1000 and 9999")]
        [Required(ErrorMessage = "Please enter a zipcode")]
        public int ZipCode { get; set; }
        
        [Required(ErrorMessage = "Please enter a password")]
        [MaxLength(20, ErrorMessage = "The password cannot exceed 20 characters")]
        [MinLength(6, ErrorMessage = "The password has to be longer than 6 characters")]
        public string Password { get; set; }
        
        [MaxLength(11, ErrorMessage = "The phone number has to be maximum 11 characters")]
        [MinLength(8, ErrorMessage = "The phone number has to be minimum 8 characters")]
        public string PhoneNumber { get; set; }
        
        [Compare("Password", ErrorMessage = "Passwords must match")]
        public string ConfirmPassword { get; set; }

        public string Role { get; set; }
    }
}
