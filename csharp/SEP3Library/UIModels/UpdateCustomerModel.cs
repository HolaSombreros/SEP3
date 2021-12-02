using System.ComponentModel.DataAnnotations;

namespace SEP3Library.UIModels {
    public class UpdateCustomerModel {
        [Required(ErrorMessage = "Please enter a first name")]
        [MaxLength(100, ErrorMessage = "The first name cannot exceed {1} characters")]
        [MinLength(2, ErrorMessage = "The first name has to be longer than {1} characters")]
        public string FirstName { get; set; }
        
        [Required(ErrorMessage = "Please enter a last name")]
        [MaxLength(100, ErrorMessage = "The last name cannot exceed {1} characters")]
        [MinLength(2, ErrorMessage = "The last name has to be longer than {1} characters")]
        public string LastName { get; set; }
        
        [Required(ErrorMessage = "Please enter an email address")]
        [EmailAddress(ErrorMessage = "Please enter a valid email address")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [Phone(ErrorMessage = "Please enter a valid phone number")]
        [StringLength(30, MinimumLength = 8, ErrorMessage = "The password must be between {2} and {1} characters")]
        public string PhoneNumber { 
            get => phoneNumber;
            set {
                phoneNumber = value;
                if (phoneNumber != null && phoneNumber.Length == 0) phoneNumber = null;
            }
        }
        private string phoneNumber;
        
        [Required(ErrorMessage = "Please enter a street name")]
        public string Street { get; set; }
          
        [Required(ErrorMessage = "Please enter a street number")]
        public string Number { get; set; }
        
        [Required(ErrorMessage = "Please enter a zip code")]
        [Range(1000, 9999, ErrorMessage = "The zip code must be a number between {1} and {2}")]
        public int ZipCode { get; set; }
        
        [Required(ErrorMessage = "Please enter a city")]
        public string City { get; set; }
        
        [StringLength(20, MinimumLength = 6, ErrorMessage = "The password must be between {2} and {1} characters")]
        public string Password {
            get => password;
            set {
                password = value;
                if (password != null && password.Length == 0) password = null;
            }
        }
        private string password;
        
        [Compare("Password", ErrorMessage = "Passwords must match")]
        public string ConfirmPassword {
            get => confirmPassword;
            set {
                confirmPassword = value;
                if (confirmPassword != null && confirmPassword.Length == 0) confirmPassword = null;
            }
        }
        private string confirmPassword;
    }
}