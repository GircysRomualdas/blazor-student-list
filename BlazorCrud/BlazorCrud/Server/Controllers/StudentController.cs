using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BlazorCrud.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentController : ControllerBase
    {
        public static List<StudentGroup> studentGroups = new List<StudentGroup> { 
            new StudentGroup { Id = 1, Name = "PI20A" },
            new StudentGroup { Id = 2, Name = "PI20B" },
            new StudentGroup { Id = 3, Name = "PI20C" }
        };

        public static List<Student> students = new List<Student> {
            new Student { 
                Id = 1, 
                FirstName = "Romas", 
                LastName = "Gircys", 
                Email = "romas@gmail.com",
                StudentGroup = studentGroups[0]
            },
            new Student {
                Id = 2,
                FirstName = "Vaidas",
                LastName = "Katin",
                Email = "vaidas@gmail.com",
                StudentGroup = studentGroups[1]
            },
            new Student {
                Id = 3,
                FirstName = "Rudis",
                LastName = "Ackas",
                Email = "rudis@gmail.com",
                StudentGroup = studentGroups[2]
            }
        };

        [HttpGet]
        public async Task<ActionResult<List<Student>>> GetStudents()
        {
            return Ok(students);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<List<Student>>> GetStudent(int id)
        {
            var student = students.FirstOrDefault(s => s.Id == id);
            if (student == null)
            {
                return NotFound("Not Found");
            }
            return Ok(student);
        }
    }
}
