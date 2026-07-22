using System.Reflection;

namespace WebApplication1.Models
{
    public class Course
    {
        public int CourseId { get; set; }
        public string CourseName { get; set; } = "";
        public string? Description { get; set; }
        public List<Module> Modules { get; set; } = new();
    }
}
