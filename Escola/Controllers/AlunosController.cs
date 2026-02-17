using APIEscola.Context;
using Escola.Domain;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace APIEscola.Controllers;

[Route("[controller]")]
[ApiController]
public class AlunosController : ControllerBase
{
    private readonly APIEscolaContext _context;

    public AlunosController(APIEscolaContext context)
    {
        _context = context;
    }

    [HttpGet]
    public ActionResult<IEnumerable<Aluno>> Get()
    {
        var alunos = _context.Alunos.ToList();

        if (alunos == null)
        {
            return NotFound("Alunos não encontrados");
        }

        return alunos;
    }

    [HttpGet("{Id:int}", Name = "ObterAluno")]
    public ActionResult<Aluno> GetId(int Id)
    {
        var aluno = _context.Alunos.FirstOrDefault(a => a.Id == Id);

        if(aluno == null)
        {
            return NotFound("Aluno não encontrado");
        }

        return aluno;
    }

    [HttpPost]
    public ActionResult Post(Aluno aluno)
    {
        if(aluno == null)
        {
            return BadRequest("Dados inválidos");
        }
        _context.Alunos.Add(aluno);
        _context.SaveChanges();

        return new CreatedAtRouteResult("ObterAluno", new { Id = aluno.Id }, aluno);
    }

    [HttpPut("{Id:int}")]
    public ActionResult Put(int Id, Aluno aluno)
    {
        if(Id != aluno.Id)
        {
            return BadRequest("Id do Body diferente do Id da URI");
        }

        _context.Alunos.Entry(aluno).State = EntityState.Modified;
        _context.SaveChanges();

        return Ok(aluno);
    }

    [HttpDelete("{Id:int}")]
    public ActionResult Put(int Id)
    {
        var aluno = _context.Alunos.FirstOrDefault(a => a.Id == Id);

        if(aluno == null)
        {
            return NotFound("Aluno não encontrado");
        }

        _context.Alunos.Remove(aluno);
        _context.SaveChanges();

        return Ok(aluno);
    }
}
