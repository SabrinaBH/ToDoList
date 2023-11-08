using System.ComponentModel.DataAnnotations;

namespace ToDoList.Models
{
  public class LoginModel
  {
    [Key]
    public int Id { get; set; }
    [Required]
    [DataType(DataType.EmailAddress)]
    public string? Email { get; set; }

    [Required]
    [DataType(DataType.Password)]
    public string? Password { get; set; }
  }
}