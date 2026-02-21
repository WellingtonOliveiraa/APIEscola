using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace APIEscola.Domain;

public class Curso
{
    public Curso()
    {
        Turmas = new List<Turma>();
    }

    [Key]
    public int Id { get; set; }

    [Required(ErrorMessage = "Nome do curso é obrigatório")]
    [StringLength(40)]
    public string? Nome { get; set; }

    [JsonIgnore]
    public ICollection<Turma>? Turmas { get; set; }
}
