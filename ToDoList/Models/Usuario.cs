namespace ToDoList.Models
{
    public class Usuario
    {
        public String Id { get; set; }
        public string Nombre { get; set; }
        public string PrimerApellido { get; set; }
        public string SegundoApellido { get; set; }
        public string Email { get; set; }

        public bool EsUsuarioDeJuego { get; set;}
    }
}