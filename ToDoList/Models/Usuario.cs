namespace ToDoList.Models
{
    public class Usuario
    {
        public string Id { get; set; }
        public string Nombre { get; set; }
        public string? PrimerApellido { get; set; }
        public string? SegundoApellido { get; set; }
        public string? Nickname { get; set; }
        public string? Email { get; set; }
        public bool EsUsuarioDeJuego { get; set;}

        public Usuario() {
            //Id = Guid.NewGuid();
            Id = "";
            Nombre = "";
            PrimerApellido = "";
            SegundoApellido = "";
            Nickname = "";
            Email = "";
            EsUsuarioDeJuego = false;
        }

        public Usuario(string id, string nombre, string primerApellido, string segundoApellido, string nickname, string email, bool esJuego) {
            Id = id;
            Nombre = nombre;
            PrimerApellido = primerApellido;
            SegundoApellido = segundoApellido;
            Nickname = nickname;
            Email = email;
            EsUsuarioDeJuego = esJuego;
        }
    }
}