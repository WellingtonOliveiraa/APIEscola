using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace APIEscola.Domain;

public class Professor
{
    [Key]
    public int Id { get; set; }

    [Required(ErrorMessage = "Nome do professor é obrigatório")]
    [StringLength(80)]
    public string? Nome { get; set; }

    [Required]
    public int TurmaId { get; set; }

    [JsonIgnore]
    public Turma? Turma { get; set; }
}
