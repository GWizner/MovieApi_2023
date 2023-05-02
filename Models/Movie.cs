using System;
using System.Collections.Generic;

namespace MovieApi_2023.Models;

public partial class Movie
{
    public int Id { get; set; }

    public string? Title { get; set; }

    public string? Category { get; set; }
}
