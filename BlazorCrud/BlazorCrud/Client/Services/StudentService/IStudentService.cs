namespace BlazorCrud.Client.Services.StudentService
{
    public interface IStudentService
    {
        List<Student> Students { get; set; }
        List<StudentGroup> StudentGroups { get; set; }

        Task GetStudents();
        Task GetStudentGroups();
        Task<Student> GetSingleStudet(int id);

    }
}
