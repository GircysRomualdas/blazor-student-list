using BlazorCrud.Shared;
using Microsoft.AspNetCore.Components;
using System.Net.Http.Json;
using System.Runtime.InteropServices;

namespace BlazorCrud.Client.Services.StudentService
{
    public class StudentService : IStudentService
    {
        private readonly HttpClient http;
        private readonly NavigationManager navigationManager;

        public StudentService(HttpClient http, NavigationManager navigationManager)
        {

            this.http = http;
            this.navigationManager = navigationManager;
        }

        public List<Student> Students { get; set; } = new List<Student>();
        public List<StudentGroup> StudentGroups { get; set; } = new List<StudentGroup>();

        public async Task CreateStudent(Student student)
        {
            var result = await this.http.PostAsJsonAsync("api/student", student);

            await SetStudent(result);

        }

        private async Task SetStudent(HttpResponseMessage result)
        {
            var response = await result.Content.ReadFromJsonAsync<List<Student>>();
            Students = response;
            this.navigationManager.NavigateTo("students");
        }

        public async Task DeleteStudent(int id)
        {
            var result = await this.http.DeleteAsync($"api/student/{id}");

            await SetStudent(result);
        }

        public async Task<Student> GetSingleStudet(int id)
        {
            var result = await this.http.GetFromJsonAsync<Student>($"api/student/{id}");
            if (result != null)
                return result;
            throw new Exception("Not Found");
        }

        public async Task GetStudentGroups()
        {
            var result = await this.http.GetFromJsonAsync<List<StudentGroup>>("api/student/studentgroups");
            if (result != null)
                StudentGroups = result;
        }

        public async Task GetStudents()
        {
            var result = await this.http.GetFromJsonAsync<List<Student>>("api/student");
            if (result != null)
                Students = result;
        }

        public async Task UpdateStudent(Student student)
        {
            var result = await this.http.PutAsJsonAsync($"api/student/{student.Id}", student);

            await SetStudent(result);
        }
    }
}
