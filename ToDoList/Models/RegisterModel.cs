using System.ComponentModel.DataAnnotations;

namespace ToDoList.Models
{
  public class RegisterModel
  {
    [Key]
    public int Id { get; set; }

    [Required]
    [Display(Name = "Name")]
    public string? Name { get; set; }

    [Required]
    [DataType(DataType.EmailAddress)]
    public string? Email { get; set; }

    [Required]
    [DataType(DataType.Password)]
    public string? Password { get; set; }
  }
}
