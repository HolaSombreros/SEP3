using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using SEP3Library.Models;

namespace SEP3Library.UIModels {
    public class BookModel {
        [Required(ErrorMessage = "Please enter a name")]
        [MaxLength(100, ErrorMessage = "The name cannot exceed 100 characters")]
        [MinLength(1, ErrorMessage = "The name has to be longer than 1 character")]
        public string Name { get; set; }
        
        [Required(ErrorMessage = "Please enter a description")]
        [MaxLength(500, ErrorMessage = "The description cannot exceed 500 characters")]
        [MinLength(5,ErrorMessage = "The description has to be longer than 5 character")]
        public string Description { get; set; }
        
        [Required(ErrorMessage = "Please enter a price")]
        [Range(1, double.PositiveInfinity)]
        public decimal Price { get; set; }
        
        [Range(0, 100)]
        public int Discount { get; set; }

        [Required(ErrorMessage = "Please enter a quantity")]
        [Range(0, int.MaxValue)]
        public int Quantity { get; set; }
        
        [Required(ErrorMessage = "Please select a status")]
        [JsonConverter(typeof(JsonStringEnumConverter))]
        public ItemStatus Status { get; set; }
        
        [Required(ErrorMessage = "Please select a category")]
        public Category Category { get; set; }
     
        [Required(ErrorMessage = "Please enter an isbn")]
        [MaxLength(20, ErrorMessage = "The first name cannot exceed 20 characters")]
        public string Isbn { get; set; }

        [Required(ErrorMessage = "Please enter at least an author")]
        public List<Author> Authors { get; set; } = new();

        [Required(ErrorMessage = "Please enter a publication date for the author")]
        public DateTime PublicationDate { get; set; }
        
        [Required(ErrorMessage = "Please enter a language")]
        [MaxLength(20, ErrorMessage = "The first name cannot exceed 20 characters")]
        public string Language { get; set; }

        [Required(ErrorMessage = "Please enter at least a genre")]
        public List<Genre> Genre { get; set; } = new();

        public string FilePath { get; set; }
    }
}