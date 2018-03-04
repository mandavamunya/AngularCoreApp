using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Application.Core.Entities;
using Application.Infrastructure.Identity;

namespace Application.Web.Controllers.Api.Publication
{
    [Produces("application/json")]
    [Route("api/JournoRankings")]
    public class JournoRankingsController : Controller
    {
        private readonly AppIdentityDbContext _context;

        public JournoRankingsController(AppIdentityDbContext context)
        {
            _context = context;
        }

        // GET: api/JournoRankings
        [HttpGet]
        public IEnumerable<JournoRanking> GetJournoRankings()
        {
            return _context.JournoRankings;
        }

        // GET: api/JournoRankings/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetJournoRanking([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var journoRanking = await _context.JournoRankings.SingleOrDefaultAsync(m => m.Id == id);

            if (journoRanking == null)
            {
                return NotFound();
            }

            return Ok(journoRanking);
        }

        // PUT: api/JournoRankings/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutJournoRanking([FromRoute] int id, [FromBody] JournoRanking journoRanking)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != journoRanking.Id)
            {
                return BadRequest();
            }

            _context.Entry(journoRanking).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!JournoRankingExists(id))
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

        // POST: api/JournoRankings
        [HttpPost]
        public async Task<IActionResult> PostJournoRanking([FromBody] JournoRanking journoRanking)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.JournoRankings.Add(journoRanking);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetJournoRanking", new { id = journoRanking.Id }, journoRanking);
        }

        // DELETE: api/JournoRankings/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteJournoRanking([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var journoRanking = await _context.JournoRankings.SingleOrDefaultAsync(m => m.Id == id);
            if (journoRanking == null)
            {
                return NotFound();
            }

            _context.JournoRankings.Remove(journoRanking);
            await _context.SaveChangesAsync();

            return Ok(journoRanking);
        }

        private bool JournoRankingExists(int id)
        {
            return _context.JournoRankings.Any(e => e.Id == id);
        }
    }
}