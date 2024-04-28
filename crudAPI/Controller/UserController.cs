using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using crudAPI.Domain.Model;
using crudAPI.Domain;

namespace crudAPI.Controller;

[ApiController]
[Route("[controller]")]
public class UserController : ControllerBase
{
    private AppDbContext _context;
    public UserController(AppDbContext context)
    {
        _context = context;
    }
    
    private bool UserExists(int id)
    {
        return _context.Users.Any(e => e.Id == id);
    }

    [HttpPost("create")]
    public async Task<ActionResult<User>> AddUser(User user)
    {
        _context.Users.Add(user);
        await _context.SaveChangesAsync();
        return CreatedAtAction(nameof(GetUser), new { id = user.Id }, user);
    }

    [HttpGet("read")]
    public async Task<ActionResult<IEnumerable<User>>> GetUsers()
    {
        return await _context.Users.ToListAsync();
    }

    [HttpPut("{id}/update")]
    public async Task<IActionResult> UpdateUser(int id, User user)
    {
        if (id != user.Id)
        {
            return BadRequest();
        }

        _context.Entry(user).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!UserExists(id))
            {
                return NotFound();
            }
            else
            {
                throw;
            }
        }
        return NoContent();
    }

    [HttpDelete("{id}/vasco")]
    public async Task<IActionResult> DeleteUser(int id)
    {
        var user = await _context.Users.FindAsync(id);
        if (user == null)
        {
            return NotFound();
        }

        _context.Users.Remove(user);
        await _context.SaveChangesAsync();
        return NoContent();
    }

    [HttpGet("{id}findUser")]
    public async Task<ActionResult<User>> GetUser(int id)
    {
        var user = await _context.Users.FindAsync(id);
        if (user == null)
        {
            return NotFound();
        }
        return user;
    }
}
