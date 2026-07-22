namespace WebApplication1.Models
{
    public class Module
    {
        public int ModuleId { get; set; }
        public int CourseId { get; set; }
        public string ModuleName { get; set; } = "";
        public int SortOrder { get; set; }
        public List<Topic> Topics { get; set; } = new();

    }
}
