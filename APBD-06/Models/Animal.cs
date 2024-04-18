using System.ComponentModel.DataAnnotations;

namespace APBD_06.Models;

public class Animal(int idAnimal, string name, string description, string category, string area)
{
    public int IdAnimal { get; set; } = idAnimal;

    [Required]
    [MaxLength(200)]
    public string Name { get; set; } = name;

    [MaxLength(200)]
    public string Description { get; set; } = description;

    [Required]
    [EmailAddress]
    public string Category { get; set; } = category;

    [Required]
    [MaxLength(200)]
    public string Area { get; set; } = area;
}