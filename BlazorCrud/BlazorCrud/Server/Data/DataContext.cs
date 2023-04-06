
namespace BlazorCrud.Server.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
            
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<StudentGroup>().HasData(
                new StudentGroup { Id = 1, Name = "PI20A" },
                new StudentGroup { Id = 2, Name = "PI20B" },
                new StudentGroup { Id = 3, Name = "PI20C" }
            );

            modelBuilder.Entity<Student>().HasData(
                new Student
                {
                    Id = 1,
                    FirstName = "Romas",
                    LastName = "Gircys",
                    Email = "romas@gmail.com",
                    StudentGroupId = 1
                },
                new Student
                {
                    Id = 2,
                    FirstName = "Vaidas",
                    LastName = "Katin",
                    Email = "vaidas@gmail.com",
                    StudentGroupId = 2
                },
                new Student
                {
                    Id = 3,
                    FirstName = "Rudis",
                    LastName = "Ackas",
                    Email = "rudis@gmail.com",
                    StudentGroupId = 3
                }
            );
        }

        public DbSet<Student> Students { get; set; }

        public DbSet<StudentGroup> StudentGroups { get; set; }

    }
}
