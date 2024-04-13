using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SparkleAir.Front.API.Models;

namespace SparkleAir.Front.API.Controllers.Members
{
    [Route("api/[controller]")]
    [ApiController]
    public class MemberLoginLogsController : ControllerBase
    {
        //private readonly dbAirSparkleContext _context;

        //public MemberLoginLogsController(dbAirSparkleContext context)
        //{
        //    _context = context;
        //}

        //// GET: api/MemberLoginLogs
        //[HttpGet]
        //public async Task<ActionResult<IEnumerable<MemberLoginLog>>> GetMemberLoginLogs()
        //{
        //    return await _context.MemberLoginLogs.ToListAsync();
        //}

        //// GET: api/MemberLoginLogs/5
        //[HttpGet("{id}")]
        //public async Task<ActionResult<MemberLoginLog>> GetMemberLoginLog(int id)
        //{
        //    var memberLoginLog = await _context.MemberLoginLogs.FindAsync(id);

        //    if (memberLoginLog == null)
        //    {
        //        return NotFound();
        //    }

        //    return memberLoginLog;
        //}

        //// PUT: api/MemberLoginLogs/5
        //// To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        //[HttpPut("{id}")]
        //public async Task<IActionResult> PutMemberLoginLog(int id, MemberLoginLog memberLoginLog)
        //{
        //    if (id != memberLoginLog.Id)
        //    {
        //        return BadRequest();
        //    }

        //    _context.Entry(memberLoginLog).State = EntityState.Modified;

        //    try
        //    {
        //        await _context.SaveChangesAsync();
        //    }
        //    catch (DbUpdateConcurrencyException)
        //    {
        //        if (!MemberLoginLogExists(id))
        //        {
        //            return NotFound();
        //        }
        //        else
        //        {
        //            throw;
        //        }
        //    }

        //    return NoContent();
        //}

        //// POST: api/MemberLoginLogs
        //// To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        //[HttpPost]
        //public async Task<ActionResult<MemberLoginLog>> PostMemberLoginLog(MemberLoginLog memberLoginLog)
        //{
        //    _context.MemberLoginLogs.Add(memberLoginLog);
        //    await _context.SaveChangesAsync();

        //    return CreatedAtAction("GetMemberLoginLog", new { id = memberLoginLog.Id }, memberLoginLog);
        //}

        //// DELETE: api/MemberLoginLogs/5
        //[HttpDelete("{id}")]
        //public async Task<IActionResult> DeleteMemberLoginLog(int id)
        //{
        //    var memberLoginLog = await _context.MemberLoginLogs.FindAsync(id);
        //    if (memberLoginLog == null)
        //    {
        //        return NotFound();
        //    }

        //    _context.MemberLoginLogs.Remove(memberLoginLog);
        //    await _context.SaveChangesAsync();

        //    return NoContent();
        //}

        //private bool MemberLoginLogExists(int id)
        //{
        //    return _context.MemberLoginLogs.Any(e => e.Id == id);
        //}
    }
}
