using APIEscola.Domain;
using Escola.Domain;
using Microsoft.EntityFrameworkCore;

namespace APIEscola.Context;

public class APIEscolaContext : DbContext
{
    public APIEscolaContext(DbContextOptions<APIEscolaContext> options) : base(options)
    {
    }

    public DbSet<Aluno>? Alunos { get; set; }
    public DbSet<Professor>? Professores { get; set; }
}
