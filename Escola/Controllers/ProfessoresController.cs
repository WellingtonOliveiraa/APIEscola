using APIEscola.Context;
using APIEscola.Domain;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace APIEscola.Controllers;

[Route("[controller]")]
[ApiController]
public class ProfessoresController : ControllerBase
{
    private readonly APIEscolaContext _context;

    public ProfessoresController(APIEscolaContext context)
    {
        _context = context;
    }

     [HttpGet]
     public ActionResult<IEnumerable<Professor>> Get()
    {
        var professor = _context.Professores.ToList();

        if(professor.IsNullOrEmpty())
        {
            return NotFound("Professores não encontrados");
        }

        return professor;
    }

    [HttpGet("{Id:int}", Name = "ObterProfessor")]
    public ActionResult<Professor> GetId(int Id)
    {
        var professor = _context.Professores.FirstOrDefault(p => p.Id == Id);

        if(professor == null)
        {
            return NotFound("Professor não encontrado");
        }

        return professor;
    }

    [HttpPost]
    public ActionResult Post(Professor professor) 
    { 
        if(professor == null)
        {
            return BadRequest("Dados inválidos");
        }

        _context.Professores.Add(professor);
        _context.SaveChanges();

        return new CreatedAtRouteResult("ObterProfessor", new { Id = professor.Id }, professor);
    }

    [HttpPut("{Id:int}")]
    public ActionResult Put(int Id, Professor professor)
    {
        if (Id != professor.Id)
        {
            return BadRequest("Id do Body diferente do Id da URI");
        }

        _context.Professores.Entry(professor).State = EntityState.Modified;
        _context.SaveChanges();

        return Ok(professor);
    }

    [HttpDelete("{Id:int}")]
    public ActionResult Delete(int Id) 
    {
        var professor = _context.Professores.FirstOrDefault(p => p.Id == Id);

        if(professor == null)
        {
            return BadRequest("Professor não encontrado");
        }

        _context.Professores.Remove(professor);
        _context.SaveChanges();

        return Ok(professor);
    }
}
