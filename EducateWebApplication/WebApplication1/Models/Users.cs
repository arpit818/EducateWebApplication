using System.ComponentModel.DataAnnotations;

namespace WebApplication1.Models
{
    public class Users
    {
       
        [Key]
        public int UsersID { get; set; }

        [Required]
        [StringLength(50)]
        public string? Username { get; set; }

        [Required]
        [StringLength(50)]
        public string? Password { get; set; }

        [StringLength(70)]
        public string? Email { get; set; }

        public int? ModuleId { get; set; }

        [Required]
        [StringLength(10)]
        public string? ModuleName { get; set; }

        public DateTime FromDate { get; set; }

        public DateTime ToDate { get; set; }

        public DateTime CreatedOn { get; set; } = DateTime.Now;

    }
}
