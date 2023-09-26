using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Security.Permissions;

namespace ToDoList.Models
{
    public class LoginModel
    {
        [Key]
        public int id {get; set;}
        [Required]
        [DataType(DataType.EmailAddress)]
        [DisplayName(Name = "Email")]
        public string Email { get; set;}

        [Required]
        [DataType(DataType.Password)]
        [DisplayName(Name = "Password")]
        public string Password { get; set; }
    }
}