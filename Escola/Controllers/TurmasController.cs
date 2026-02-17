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
        var turma = _context.Turmas.ToList();

        if (turma.IsNullOrEmpty())
        {
            return NotFound("Turmas não encontradas");
        }

        return turma;
    }

    [HttpGet("{Id:int}", Name = "ObterTurma")]
    public ActionResult<Turma> GetId(int Id)
    {
        var turma = _context.Turmas.FirstOrDefault(t => t.Id == Id);

        if (turma == null)
        {
            return NotFound("Turma não encontrada");
        }

        return turma;
    }

    [HttpPost]
    public ActionResult Post(Turma turma)
    {
        if (turma == null)
        {
            return BadRequest("Dados inválidos");
        }

        _context.Turmas.Add(turma);
        _context.SaveChanges();

        return new CreatedAtRouteResult("ObterTurma", new { Id = turma.Id }, turma);
    }

    [HttpPut("{Id:int}")]
    public ActionResult Put(int Id, Turma turma)
    {
        if (Id != turma.Id)
        {
            return BadRequest("Id do Body diferente do Id da URI");
        }

        _context.Turmas.Entry(turma).State = EntityState.Modified;
        _context.SaveChanges();

        return Ok(turma);
    }

    [HttpDelete("{Id:int}")]
    public ActionResult Delete(int Id)
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
}
