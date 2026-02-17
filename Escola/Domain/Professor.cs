using System.ComponentModel.DataAnnotations;

namespace APIEscola.Domain;

public class Professor
{
    [Key]
    public int Id { get; set; }
    [Required]
    [StringLength(80)]
    public string? Nome { get; set; }
}
