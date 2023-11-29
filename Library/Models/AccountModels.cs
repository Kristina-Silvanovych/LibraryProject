using DataLayer.Entityes;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using System.Text.RegularExpressions;

namespace Library.Models
{
    public class AddAuthorBindingModel
    {
        [Display(Name = "Name")]
        [Required(ErrorMessage = "Field must be set!")]
        public string? Name { get; set; }
        [Display(Name = "Surname")]
        [Required(ErrorMessage = "Field must be set!")]
        public string? Surname { get; set; }

        [Display(Name = "FathersName")]
        public string? FathersName { get; set; }

        [Display(Name = "BirthDate")]
        [Required(ErrorMessage = "Field must be set!")]
        public DateTime BirthDate { get; set; }
    }

    public class AddBookBindingModel
    {
        [Display(Name = "Title")]
        [Required(ErrorMessage = "Field must be set!")]
        public string Title { get; set; } = null!;
        [Display(Name = "CountOfPages")]
        [Required(ErrorMessage = "Field must be set!")]
        public int CountOfPages { get; set; }
        [Display(Name = "Genre")]
        [Required(ErrorMessage = "Field must be set!")]
        public string Genre { get; set; } = string.Empty;
        [Display(Name = "AuthorId")]
        [Required(ErrorMessage = "Field must be set!")]
        public int AuthorId { get; set; }
    }

    public class ChangeAuthorsDataBindingModel
    {
        [Display(Name = "Name")]
        public string? Name { get; set; }
        [Display(Name = "Surname")]
        public string? Surname { get; set; }

        [Display(Name = "FathersName")]
        public string? FathersName { get; set; }

        [Display(Name = "BirthDate")]
        public DateTime BirthDate { get; set; }
    }

    public class WhichAuthorWantChangeBindingModel
    {
        [Display(Name = "Surname")]
        [Required(ErrorMessage = "Field must be set!")]
        public string? Surname { get; set; }
        [Display(Name = "Name")]
        public string? Name { get; set; }
    }

    public class ChangeBooksDataBindingModel
    {
        [Display(Name = "Title")]
        public string? Title { get; set; }
        [Display(Name = "PagesCount")]
        public int PagesCount { get; set; }

        [Display(Name = "Genre")]
        public string? Genre { get; set; }

        [Display(Name = "AuthorId")]
        public int AuthorId { get; set; }
    }

    public class WhatBookWantChangeBindingModel
    {
        [Display(Name = "Title")]
        [Required(ErrorMessage = "Field must be set!")]
        public string? Title { get; set; }
    }

    public class ViemModelAddBook
    {
        public IEnumerable<Author> authors { get; set; }
        public AddBookBindingModel bookModel { get; set; }
    }
}
