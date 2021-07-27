using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ReactServer2.Data;
using ReactServer2.Models;
using ReactServer2.Utility;

namespace ReactServer2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentsController : ControllerBase
    {
        private readonly ReactServer2Context _context;

        public StudentsController(ReactServer2Context context)
        {
            _context = context;
        }
        // GET: api/Students
        [HttpGet]
        public async Task<ActionResult<StudentDto>> GetStudent(string txtSearch , int? pageNumber)
        {
            StudentDto studentDto = new StudentDto();
            List<Student> students = new List<Student>();
            bool hanext = true;
            try
			{
                    pageNumber = pageNumber == null ? 1 : pageNumber;
				if (string.IsNullOrEmpty(txtSearch))
				{
                    if(await _context.Student.Skip((pageNumber.Value - 1) * 2).Take(2).CountAsync() == 0)
					{
                        pageNumber -= 1;
                                   hanext = false; 
                    }
				
                    students.AddRange(await _context.Student.Skip((pageNumber.Value - 1) * 2).Take(10).ToListAsync());
                    studentDto.students = students; 
                  studentDto.hasNext = hanext;
                    return studentDto;

                }
				else
				{
                    students.AddRange(await _context.Student.ToListAsync());
                    studentDto.students = students.Where(s=>s.name.Contains(txtSearch)).ToList();
                    studentDto.hasNext = hanext;

                    return studentDto;

                }
            }
            catch(Exception ex)
			{
                return Content(ex.Message);
			}
        }

        // GET: api/Students/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Student>> GetStudent(int id)
        {
            var student = await _context.Student.FindAsync(id);

            if (student == null)
            {
                return NotFound();
            }

            return student;
        }

        // PUT: api/Students/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost()]
        [Route("EditStudent")]

        public async Task<IActionResult> PutStudent( Student student)
        {
          

            _context.Entry(student).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
               
                    throw;
                
            }

            return NoContent();
        }

        // POST: api/Students
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Student>> PostStudent([FromForm]Student student , IFormFile file)
        {
            _context.Student.Add(student);
            if (file != null)
            {
                var fileName = Path.GetFileName(file.FileName);
                var filePath = Path.Combine(Directory.GetCurrentDirectory(), @"wwwroot\Upload\Student", fileName);
                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    file.CopyTo(stream);
                }
                student.url = "/Upload/Student/File/" + fileName;
            }
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetStudent", new { id = student.id }, student);
        }

        // DELETE: api/Students/5
        [HttpPost]
        [Route("DeleteStudent/{id}")]
        public async Task<IActionResult> DeleteStudent(int id)
        {
            var student = await _context.Student.FindAsync(id);
            if (student == null)
            {
                return NotFound();
            }

            _context.Student.Remove(student);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool StudentExists(int id)
        {
            return _context.Student.Any(e => e.id == id);
        }
    }
}
