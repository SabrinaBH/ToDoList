namespace ToDoList.Models
{
    public class Usuario
    {
        public String Id { get; set; }
        public string Nombre { get; set; }
        public string? PrimerApellido { get; set; }
        public string? SegundoApellido { get; set; }
        public string? Nickname { get; set; }
        public string? Email { get; set; }
        public string? NombreJuego { get; set;}

        public Usuario() {
            Id = Guid.NewGuid();
            Nombre = "";
            PrimerApellido = "";
            SegundoApellido = "";
            Nickname = "";
            Email = "";
            NombreJuego = "";
        }

        public Usuario(string nombre, string primerApellido, string segundoApellido, string nickname, string email, string nombreJuego) {
            Id = Guid.NewGuid();
            Nombre = nombre;
            PrimerApellido = primerApellido;
            SegundoApellido = segundoApellido;
            Nickname = nickname;
            Email = email;
            NombreJuego = nombreJuego;
        }
    }
}