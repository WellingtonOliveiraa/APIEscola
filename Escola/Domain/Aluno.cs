using System.ComponentModel.DataAnnotations;

namespace Escola.Domain;

public class Aluno
{
    [Key]
    public int Id { get; set; }
    [Required]
    [StringLength(80)]
    public string? Nome { get; set; }
}
