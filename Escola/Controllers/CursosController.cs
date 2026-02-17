using APIEscola.Context;
using APIEscola.Domain;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace APIEscola.Controllers;

[Route("[controller]")]
[ApiController]
public class CursosController : ControllerBase
{
    private readonly APIEscolaContext _context;

    public CursosController(APIEscolaContext context)
    {
        _context = context;
    }

    [HttpGet]
    public ActionResult<IEnumerable<Curso>> Get()
    {
        var cursos = _context.Cursos.ToList();

        if (cursos.IsNullOrEmpty())
        {
            return NotFound("Cursos não encontrados");
        }
        
        return cursos;
    }

    [HttpGet("{Id}", Name = "ObterCurso")]
    public ActionResult<Curso> GetId(int Id)
    {
        var curso = _context.Cursos.FirstOrDefault(c => c.Id == Id);

        if (curso == null)
        {
            return NotFound("Curso não encontrado");
        }
        
        return curso;
    }

    [HttpPost]
    public ActionResult Post(Curso curso)
    {
        if(curso == null)
        {
            return BadRequest("Dados inválidos");
        }
        _context.Cursos.Add(curso);
        _context.SaveChanges();

        return new CreatedAtRouteResult("ObterCurso", new { Id = curso.Id }, curso);
    }

     [HttpPut("{Id}")]
     public ActionResult Put(int Id, Curso curso)
     {
        if(Id != curso.Id)
        {
            return BadRequest("Id do Body diferente do Id da URI");
        }

        _context.Entry(curso).State = EntityState.Modified;
        _context.SaveChanges();
        
        return Ok(curso);
    }

    [HttpDelete("{Id}")]
    public ActionResult Delete(int Id)
    {
        var curso = _context.Cursos.FirstOrDefault(c => c.Id == Id);

        if (curso == null)
        {
            return NotFound("Curso não encontrado");
        }

        _context.Cursos.Remove(curso);
        _context.SaveChanges();

        return Ok(curso);
    }
}
