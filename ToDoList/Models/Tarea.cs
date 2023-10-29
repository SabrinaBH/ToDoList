namespace ToDoList.Models
{
    public class Tarea
    {
        public String Id { get; set; }
        public string Titulo { get; set; }
        public string Descripcion { get; set; }
        public DateTime FechaInicial { get; set; }
        public DateTime FechaFinal { get; set; }
        public int Dificultad { get; set; }
        public int Prioridad { get; set; }
        public String UsuarioCreador { get; set; }
        public int Categoria { get; set; }
        public int Estado { get; set; }
    }
}
