﻿namespace EFDatabaseContext.Models;

public class User
{
    public Guid Id { get; set; }
    public string? Name { get; set; }
    public int Age { get; set; }
}