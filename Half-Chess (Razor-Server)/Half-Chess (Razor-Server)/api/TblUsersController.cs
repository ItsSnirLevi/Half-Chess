using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Half_Chess__Razor_Server_.Data;
using Half_Chess__Razor_Server_.Models;

namespace Half_Chess__Razor_Server_.api
{
    [Route("api/[controller]")]
    [ApiController]
    public class TblUsersController : ControllerBase
    {
        private readonly Half_Chess__Razor_Server_Context _context;

        public TblUsersController(Half_Chess__Razor_Server_Context context)
        {
            _context = context;
        }

        /*[HttpPost("validateMove")]
        public IActionResult ValidateMove([FromBody] MoveRequest moveRequest)
        {
            // Initialize the board or get the current game state
            var board = GameBoard.GetCurrentBoard();

            // Check if the move is valid
            bool isValid = board.IsMoveValid(moveRequest.Piece, moveRequest.TargetPosition);

            return Ok(new { isValid });
        }*/

        // GET: api/TblUsers
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TblUsers>>> GetTblUsers()
        {
          if (_context.TblUsers == null)
          {
              return NotFound();
          }
            return await _context.TblUsers.ToListAsync();
        }

        // GET: api/TblUsers/5
        [HttpGet("{id}")]
        public async Task<ActionResult<TblUsers>> GetTblUsers(int id)
        {
          if (_context.TblUsers == null)
          {
              return NotFound();
          }
            var tblUsers = await _context.TblUsers.FindAsync(id);

            if (tblUsers == null)
            {
                return NotFound();
            }

            return tblUsers;
        }

        // PUT: api/TblUsers/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTblUsers(int id, TblUsers tblUsers)
        {
            if (id != tblUsers.Id)
            {
                return BadRequest();
            }

            _context.Entry(tblUsers).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TblUsersExists(id))
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

        // POST: api/TblUsers
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<TblUsers>> PostTblUsers(TblUsers tblUsers)
        {
          if (_context.TblUsers == null)
          {
              return Problem("Entity set 'Half_Chess__Razor_Server_Context.TblUsers'  is null.");
          }
            _context.TblUsers.Add(tblUsers);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetTblUsers", new { id = tblUsers.Id }, tblUsers);
        }

        // DELETE: api/TblUsers/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTblUsers(int id)
        {
            if (_context.TblUsers == null)
            {
                return NotFound();
            }
            var tblUsers = await _context.TblUsers.FindAsync(id);
            if (tblUsers == null)
            {
                return NotFound();
            }

            _context.TblUsers.Remove(tblUsers);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool TblUsersExists(int id)
        {
            return (_context.TblUsers?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
