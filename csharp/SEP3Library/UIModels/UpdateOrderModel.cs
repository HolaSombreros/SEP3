using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
using SEP3Library.Models;

namespace SEP3Library.UIModels {
    public class UpdateOrderModel {
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
          
        [Required(ErrorMessage = "Please enter a number")]
        public string Number { get; set; }
        
        [Range(1000,9999,ErrorMessage="Zipcode must be between 1000 and 9999")]
        [Required(ErrorMessage = "Please enter a zipcode")]
        public int ZipCode { get; set; }
        
        public int CustomerId { get; set; }
        
        public int OrderId { get; set; }
    }
}