namespace WebApplication1.Models
{
    public class UserProgress
    {
        public int ProgressId { get; set; }
        public int UserId { get; set; }
        public int TopicId { get; set; }
        public bool PdfViewed { get; set; }
        public bool QuizPassed { get; set; }
        public int? QuizScore { get; set; }
        public bool ExamPassed { get; set; }
        public int? ExamScore { get; set; }
        public DateTime UpdatedOn { get; set; } = DateTime.Now;
    }
}
