﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BlazorCrud.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentController : ControllerBase
    {
//        public static List<StudentGroup> studentGroups = new List<StudentGroup> {
//            new StudentGroup { Id = 1, Name = "PI20A" },
//            new StudentGroup { Id = 2, Name = "PI20B" },
//            new StudentGroup { Id = 3, Name = "PI20C" }
//        };

//        public static List<Student> students = new List<Student> {
//            new Student {
//                Id = 1,
//                FirstName = "Romas",
//                LastName = "Gircys",
//                Email = "romas@gmail.com",
//                StudentGroup = studentGroups[0],
//                StudentGroupId = 1
//            },
//            new Student {
//                Id = 2,
//                FirstName = "Vaidas",
//                LastName = "Katin",
//                Email = "vaidas@gmail.com",
//                StudentGroup = studentGroups[1],
//                StudentGroupId = 2
//},
//            new Student {
//                Id = 3,
//                FirstName = "Rudis",
//                LastName = "Ackas",
//                Email = "rudis@gmail.com",
//                StudentGroup = studentGroups[2],
//                StudentGroupId = 3
//            }
//        };


        private readonly DataContext _context;

        public StudentController(DataContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<List<Student>>> GetStudents()
        {
            //var students = await _context.Students.ToListAsync();
            var students = await _context.Students.Include(sh => sh.StudentGroup).ToListAsync();
            return Ok(students);
        }

        [HttpGet("studentgroups")]
        public async Task<ActionResult<List<StudentGroup>>> GetStudentGroups()
        {
            var studentGroups = await _context.StudentGroups.ToListAsync();
            return Ok(studentGroups);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<List<Student>>> GetStudent(int id)
        {
            var student = await _context.Students
                .Include(h => h.StudentGroup)
                .FirstOrDefaultAsync(h => h.Id == id);

            //var student = students.FirstOrDefault(s => s.Id == id);

            if (student == null)
            {
                return NotFound("Not Found");
            }
            return Ok(student);
        }

        [HttpPost]
        public async Task<ActionResult<List<Student>>> CreateStudent(Student student)
        {
            student.StudentGroup = null;
            _context.Students.Add(student);
            await _context.SaveChangesAsync();

            return Ok(await GetDbStudents());
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<List<Student>>> UpdateStudent(Student student, int id)
        {
            var dbstudent = await _context.Students
                .Include(sh => sh.StudentGroup)
                .FirstOrDefaultAsync(sh => sh.Id == id);
            if (dbstudent == null)
                return NotFound("Not Found");

            dbstudent.FirstName = student.FirstName;
            dbstudent.LastName = student.LastName;
            dbstudent.Email = student.Email;
            dbstudent.StudentGroupId = student.StudentGroupId;

            await _context.SaveChangesAsync();

            return Ok(await GetDbStudents());
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<List<Student>>> DeleteStudent(int id)
        {
            var bstudent = await _context.Students
                .Include(sh => sh.StudentGroup)
                .FirstOrDefaultAsync(sh => sh.Id == id);
            if (bstudent == null)
                return NotFound("Not Found");

            _context.Students.Remove(bstudent);
            await _context.SaveChangesAsync();

            return Ok(await GetDbStudents());
        }

        private async Task<List<Student>> GetDbStudents()
        {
            return await _context.Students.Include(sh => sh.StudentGroup).ToListAsync();
        }

    }
}
