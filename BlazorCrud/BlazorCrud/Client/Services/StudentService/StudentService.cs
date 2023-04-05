using System.Net.Http.Json;
using System.Runtime.InteropServices;

namespace BlazorCrud.Client.Services.StudentService
{
    public class StudentService : IStudentService
    {
        private readonly HttpClient http;

        public StudentService(HttpClient http)
        {

            this.http = http;
        }

        public List<Student> Students { get; set; } = new List<Student>();
        public List<StudentGroup> StudentGroups { get; set; } = new List<StudentGroup>();



        public async Task<Student> GetSingleStudet(int id)
        {
            var result = await this.http.GetFromJsonAsync<Student>($"api/student/{id}");
            if (result != null)
                return result;
            throw new Exception("Not Found");
        }

        public Task GetStudentGroups()
        {
            throw new NotImplementedException();
        }

        public async Task GetStudents()
        {
            var result = await this.http.GetFromJsonAsync<List<Student>>("api/student");
            if (result != null)
                Students = result;
        }
    }
}
