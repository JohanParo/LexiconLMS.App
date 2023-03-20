using AutoMapper;
using LexiconLMS.App.Server.Data;
using LexiconLMS.App.Server.Models;
using LexiconLMS.App.Shared;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Security.Claims;

namespace LexiconLMS.App.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class CoursesController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;
        public CoursesController(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        // GET: api/Courses
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CourseDto>>> GetCourse()
        {


            if (_context.Course == null)
            {
                return NotFound();
            }
            var result = await _context.Course.Include(c=>c.Users).Include(c => c.Modules)
                .ThenInclude(m => m.Activities).ThenInclude(a => a.ActivityType)
                .ToListAsync();

            var mappedResult = _mapper.Map<IEnumerable<CourseDto>>(result);
            var userRole = HttpContext.User.Claims.FirstOrDefault(u => u.Type.Contains("role"))?.Value;
            if (userRole != "Admin")
            {
                // mappedResult = mappedResult.Where(r => r.Published && r.Modules.Any(m => m.Published) && r.Modules.SelectMany(m => m.Activities).Any(a => a.Published))
                //.Select(r => new CourseDto
                //{
                //    Id = r.Id,
                //    Title = r.Title,
                //    Description = r.Description,
                //    StartTime = r.StartTime,
                //    EndTime = r.EndTime,
                //    Published = r.Published,
                //    Modules = r.Modules.Where(m => m.Published)
                //                       .Select(m => new ModuleDto
                //                       {
                //                           Id = m.Id,
                //                           Title = m.Title,
                //                           Description = m.Description,
                //                           StartTime = m.StartTime,
                //                           EndTime = m.EndTime,
                //                           CourseId = m.CourseId,
                //                           Published = m.Published,
                //                           Activities = m.Activities.Where(a => a.Published).ToList()
                //                       }).ToList()
                //}).ToList();

                var amappedResult = mappedResult.Where(r => r.Published && r.Modules.Any(m => m.Published) && r.Modules.SelectMany(m => m.Activities).Any(a => a.Published))
               .Select(r => _mapper.Map<StudentCourseDto>(r))
               .ToList();

                mappedResult = _mapper.Map<IEnumerable<CourseDto>>(amappedResult);
            } 
            

            return Ok(mappedResult);
        }

        // GET: api/Courses/5
        [HttpGet("{id}")]
        public async Task<ActionResult<CourseDto>> GetCourse(int id)
        {
            if (_context.Course == null)
            {
                return NotFound();
            }
            //var course = await _context.Course.FindAsync(id);
            var course = await _context.Course
                .Include(c => c.Users)
                .Include(c => c.Modules).ThenInclude(m => m.Activities).ThenInclude(a => a.ActivityType)
                .FirstOrDefaultAsync(i => i.Id == id);

            if (course == null)
            {
                return NotFound();
            }
            var temp = _mapper.Map<CourseDto>(course);

            return Ok(temp);
            //return course;
        }

        // PUT: api/Courses/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCourse(int id, Course course)
        {
            if (id != course.Id)
            {
                return BadRequest();
            }

            _context.Entry(course).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CourseExists(id))
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

        // POST: api/Courses
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Course>> PostCourse(Course course)
        {
            if (_context.Course == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Course'  is null.");
            }
            _context.Course.Add(course);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetCourse", new { id = course.Id }, course);
        }

        // DELETE: api/Courses/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCourse(int id)
        {
            if (_context.Course == null)
            {
                return NotFound();
            }
            var course = await _context.Course.FindAsync(id);
            if (course == null)
            {
                return NotFound();
            }

            _context.Course.Remove(course);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool CourseExists(int id)
        {
            return (_context.Course?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
