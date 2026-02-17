using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using APIEscola.Domain;

namespace Escola.Domain;

public class Aluno
{
    [Key]
    public int Id { get; set; }
    [Required]
    [StringLength(80)]
    public string? Nome { get; set; }
    [Required]
    public int TurmaId { get; set; }
    [JsonIgnore]
    public Turma? Turma { get; set; }
}
