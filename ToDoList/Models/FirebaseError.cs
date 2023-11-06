namespace ToDoList.Models
{
  public class FirebaseError
  {
    public Error? error { get; set; }
  }


  public class Error
  {
    public int Code { get; set; }
    public string? Message { get; set; }
    public List<Error>? Errors { get; set; }
  }
}