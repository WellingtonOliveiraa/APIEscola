using APIEscola.Context;
using Escola.Domain;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

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
        try
        {
            var alunos = _context.Alunos.AsNoTracking().ToList();

            if (alunos.IsNullOrEmpty())
            {
                return NotFound("Alunos não encontrados");
            }

            return alunos;

        }
        catch (Exception)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, "Houve um erro ao processar sua requisição.");
        }
    }

    [HttpGet("{Id:int}", Name = "ObterAluno")]
    public ActionResult<Aluno> GetId(int Id)
    {
        try
        {
            var aluno = _context.Alunos.FirstOrDefault(a => a.Id == Id);

            if (aluno == null)
            {
                return NotFound("Aluno não encontrado");
            }

            return aluno;
        }
        catch (Exception) 
        {
            return StatusCode(StatusCodes.Status500InternalServerError, "Houve um erro ao processar sua requisição.");
        }
    }

    [HttpPost]
    public ActionResult Post(Aluno aluno)
    {
        try
        {
            if (aluno == null)
            {
                return BadRequest("Dados inválidos");
            }
            _context.Alunos.Add(aluno);
            _context.SaveChanges();

            return new CreatedAtRouteResult("ObterAluno", new { Id = aluno.Id }, aluno);
        }
        catch (Exception)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, "Houve um erro ao processar sua requisição.");
        }
    }

    [HttpPut("{Id:int}")]
    public ActionResult Put(int Id, Aluno aluno)
    {
        try
        {
            if (Id != aluno.Id)
            {
                return BadRequest("Id do Body diferente do Id da URI");
            }

            _context.Alunos.Entry(aluno).State = EntityState.Modified;
            _context.SaveChanges();

            return Ok(aluno);
        }
        catch (Exception) 
        {
            return StatusCode(StatusCodes.Status500InternalServerError, "Houve um erro ao processar sua requisição.");
        }
    }

    [HttpDelete("{Id:int}")]
    public ActionResult Put(int Id)
    {
        try
        {
            var aluno = _context.Alunos.FirstOrDefault(a => a.Id == Id);

            if (aluno == null)
            {
                return NotFound("Aluno não encontrado");
            }

            _context.Alunos.Remove(aluno);
            _context.SaveChanges();

            return Ok(aluno);
        }
        catch (Exception) 
        {
            return StatusCode(StatusCodes.Status500InternalServerError, "Houve um erro ao processar sua requisição.");
        }
    }
}
