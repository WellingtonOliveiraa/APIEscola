using APIEscola.Context;
using APIEscola.Domain;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace APIEscola.Controllers;

[Route("[controller]")]
[ApiController]
public class TurmasController : ControllerBase
{
    private readonly APIEscolaContext _context;

    public TurmasController(APIEscolaContext context)
    {
        _context = context;
    }

    [HttpGet]
    public ActionResult<IEnumerable<Turma>> Get()
    {
        try
        {
            var turma = _context.Turmas.AsNoTracking().ToList();

            if (turma.IsNullOrEmpty())
            {
                return NotFound("Turmas não encontradas");
            }

            return turma;
        }
        catch (Exception)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, "Houve um erro ao processar sua requisição.");
        }
    }

    [HttpGet("{Id:int}", Name = "ObterTurma")]
    public ActionResult<Turma> GetId(int Id)
    {
        try
        {
            var turma = _context.Turmas.FirstOrDefault(t => t.Id == Id);

            if (turma == null)
            {
                return NotFound("Turma não encontrada");
            }

            return turma;
        }
        catch (Exception) 
        {
            return StatusCode(StatusCodes.Status500InternalServerError, "Houve um erro ao processar sua requisição.");
        }
    }

    [HttpPost]
    public ActionResult Post(Turma turma)
    {
        try
        {
            if (turma == null)
            {
                return BadRequest("Dados inválidos");
            }

            _context.Turmas.Add(turma);
            _context.SaveChanges();

            return new CreatedAtRouteResult("ObterTurma", new { Id = turma.Id }, turma);
        }
        catch (Exception) 
        {
            return StatusCode(StatusCodes.Status500InternalServerError, "Houve um erro ao processar sua requisição.");
        }
    }

    [HttpPut("{Id:int}")]
    public ActionResult Put(int Id, Turma turma)
    {
        try
        {
            if (Id != turma.Id)
            {
                return BadRequest("Id do Body diferente do Id da URI");
            }

            _context.Turmas.Entry(turma).State = EntityState.Modified;
            _context.SaveChanges();

            return Ok(turma);
        }
        catch (Exception)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, "Houve um erro ao processar sua requisição.");
        }
    }

    [HttpDelete("{Id:int}")]
    public ActionResult Delete(int Id)
    {
        try
        {
            var turma = _context.Turmas.FirstOrDefault(t => t.Id == Id);

            if (turma == null)
            {
                return BadRequest("Turma não encontrada");
            }

            _context.Turmas.Remove(turma);
            _context.SaveChanges();

            return Ok(turma);
        }
        catch (Exception) 
        {
            return StatusCode(StatusCodes.Status500InternalServerError, "Houve um erro ao processar sua requisição.");
        }
    }
}
