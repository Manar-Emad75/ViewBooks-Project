using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace ViewBooks.Models
{
	public class Category
	{
		public int Id { get; set; }

		[Required(ErrorMessage = "you have to provide a valid Book Type")]
		[MinLength(2, ErrorMessage = "Type must be greater than 1 characters.")]
		[MaxLength(70, ErrorMessage = "Type must be less than 70 characters.")]
		[DisplayName("Book Type")]
		public string BookType { get; set; }

		[Required(ErrorMessage = "you have to provide a valid description for thi Type")]
		[MinLength(2, ErrorMessage = "Description must be greater than 1 characters.")]
		[MaxLength(70, ErrorMessage = "Description must be less than 70 characters.")]
		public string Description { get; set; }

		[ValidateNever]
		public List<Book> Books { get; set; }
	}
}
