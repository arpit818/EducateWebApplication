using System.ComponentModel.DataAnnotations;

namespace WebApplication1.Models
{
    public class Users
    {
        [Key]
        public int UsersID { get; set; }

        [Required]
        [StringLength(50)]
        public string? Useranme { get; set; }

        [Required]
        [StringLength(50)]
        public string? Password { get; set; }

        [StringLength(70)]
        public string? Email { get; set; }

    }
}
