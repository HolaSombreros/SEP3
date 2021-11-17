using System.ComponentModel.DataAnnotations;

namespace SEP3Library.Models {
    public class Address {
        
        [Required(ErrorMessage = "Please enter a city")]
        public string City { get; set; }
        
        [Required(ErrorMessage = "Please enter a street name")]
        public string Street { get; set; }
          
        [Required(ErrorMessage = "Please enter a number")]
        public string Number { get; set; }
        
        [Range(1000,9999,ErrorMessage="Zipcode must be between 1000 and 9999")]
        [Required(ErrorMessage = "Please enter a zipcode")]
        public int ZipCode { get; set; }
        
    }
}