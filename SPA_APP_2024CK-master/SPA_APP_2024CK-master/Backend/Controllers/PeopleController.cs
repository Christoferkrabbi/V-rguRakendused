using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using backend.Model;
using Backend.Model;

namespace backend.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PeopleController : ControllerBase
{
    private readonly DataContext context;
    public PeopleController(DataContext c)
    {
        context = c;
    }
    [HttpGet]
    public IActionResult GetPeople()
    {
        var people = context.PeopleList!.AsQueryable();
        return Ok(people);

    }
    [HttpPost]
    public IActionResult CreatePerson([FromBody] Person e)
    {
        var dbPeople = context.PeopleList?.Find(e.Id);
        if (dbPeople == null)
        {
            context.PeopleList?.Add(e);
            context.SaveChanges();
            return CreatedAtAction(nameof(GetPeople), new { e.Id }, e);
        }
        return Conflict();
    }
    [HttpPut("{id}")]
    public IActionResult Update(int? id, [FromBody] Person e)
    {
        var dbPeople = context.PeopleList!.AsNoTracking().FirstOrDefault(PeopleInDB => PeopleInDB.Id == e.Id);
        if (id != e.Id || dbPeople == null) return NotFound();
        context.Update(e);
        context.SaveChanges();
        return NoContent();
    }



    [HttpDelete("{id}")]
    public IActionResult Delete(int id)
    {
        var personToDelete = context.PeopleList?.Find(id);
        if (personToDelete == null) return NotFound();
        context.PeopleList?.Remove(personToDelete);
        context.SaveChanges();
        return NoContent();
    }

}

