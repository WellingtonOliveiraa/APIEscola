using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using Escola.Domain;

namespace APIEscola.Domain;

public class Turma
{
    public Turma()
    {
        Alunos = new List<Aluno>();
        Professores = new List<Professor>();
    }

    [Key]
    public int Id { get; set; }

    [Required(ErrorMessage = "Nome da turma é obrigatório")]
    [StringLength(20)]
    public string? Nome { get; set; }

    [Required(ErrorMessage = "Código do curso é obrigatório")]
    public int CursoId { get; set; }

    [JsonIgnore]
    public Curso? Curso { get; set; }
    public ICollection<Aluno>? Alunos { get; set; }
    public ICollection<Professor>? Professores { get; set; }
}
