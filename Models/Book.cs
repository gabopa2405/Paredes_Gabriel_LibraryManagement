using System;
using System.Collections.Generic;

namespace Paredes_Gabriel_LibraryManagement.Models;

public partial class Book
{
    public int BookId { get; set; }

    public string Title { get; set; } = null!;

    public int AuthorId { get; set; }

    public string? Isbn { get; set; }

    public DateOnly? PublicationDate { get; set; }

    public int? GenreId { get; set; }

    public bool? AvailabilityStatus { get; set; }

    public virtual Author Author { get; set; } = null!;

    public virtual Category? Genre { get; set; }

    public virtual ICollection<Loan> Loans { get; set; } = new List<Loan>();
}
