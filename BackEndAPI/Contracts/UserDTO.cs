using System.ComponentModel.DataAnnotations;

namespace BackEndAPI.Contracts
{
    public class UserDTO
    {
        [Required]
        public string Username { get; set; }
        [Required]
        [StringLength(10)]
        public string Password { get; set; }
    }
}