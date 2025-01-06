using Microsoft.AspNetCore.Identity;

namespace InnoProducts.Models;

public class User : IdentityUser
{
    public string? Initials { get; set; }
}