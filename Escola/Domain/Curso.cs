using System.ComponentModel.DataAnnotations;

namespace APIEscola.Domain;

public class Curso
{
    public Curso()
    {
        Turmas = new List<Turma>();
    }
    [Key]
    public int Id { get; set; }
    [Required]
    [StringLength(20)]
    public string? Nome { get; set; }
    public ICollection<Turma>? Turmas { get; set; }
}
