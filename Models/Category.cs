using System;
using System.Collections.Generic;

namespace Paredes_Gabriel_LibraryManagement.Models;

public partial class Category
{
    public int GenreId { get; set; }

    public string GenreName { get; set; } = null!;

    public virtual ICollection<Book> Books { get; set; } = new List<Book>();
}
