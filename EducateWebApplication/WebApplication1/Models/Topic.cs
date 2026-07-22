namespace WebApplication1.Models
{
    public class Topic
    {
        public int TopicId { get; set; }
        public int ModuleId { get; set; }
        public string TopicName { get; set; } = "";
        public string? PdfPath { get; set; }
        public int SortOrder { get; set; }
    }
}
