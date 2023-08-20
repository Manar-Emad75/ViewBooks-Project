using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace ViewBooks.Models{
	public class Book{
		public int Id { get; set; }

		[Required(ErrorMessage = "You have to provide a valid Book name")]
		[MinLength(1, ErrorMessage = "Book name must be greater than 1 characters.")]
		[MaxLength(50, ErrorMessage = "Book Name must be 1ess than 50 characters.")]
		[DisplayName("Book Name")]
		public string BookName { get; set; }

		public decimal Price { get; set; }

		[DisplayName("Number Of Pages")]
		public int NumberOfPages { get; set; }

		[Required(ErrorMessage = "You have to provide a valid Author name")]
		[MinLength(1, ErrorMessage = "Book name must be greater than 1 characters.")]
		[MaxLength(70, ErrorMessage = "Book Name must be 1ess than 70 characters.")]
		[DisplayName("Author Name")]
		public string AuthorName { get; set; }

		[DisplayName("Date Of Publication")]
		public DateTime DateOfPublication { get; set; }

		[DisplayName("Image")]
		[ValidateNever]
		public string ImageUrl { get; set; }

		[Range(1,int.MaxValue,ErrorMessage ="Choose a valid dapartment!")]
		[DisplayName("Book Type")]
		public int CategoryId { get; set; }

		[ValidateNever]
		public Category category { get; set; }
	}
}
