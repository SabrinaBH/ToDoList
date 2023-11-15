using System.ComponentModel.DataAnnotations;

namespace ToDoList.Models
{
    public class Tarea
    {
        public string Id { get; set; }

        [Required(ErrorMessage = "Debe ingresar el titulo")]
        [MinLength(3, ErrorMessage = "Debe ingresar al menos 3 caracteres")]
        [Display(Name = "Titulo")]
        public string Titulo { get; set; }

        [Required(ErrorMessage = "Debe ingresar la descripcion")]
        [MinLength(5, ErrorMessage = "Debe ingresar al menos 5 caracteres")]
        [Display(Name = "Descripcion")]
        public string Descripcion { get; set; }

        [Required(ErrorMessage = "Debe ingresar la fecha de inicio")]
        [Display(Name = "Fecha de inicio")]
        public DateTime FechaInicial { get; set; }

        [Required(ErrorMessage = "Debe ingresar la fecha de finalizacion")]
        [Display(Name = "Fecha final")]
        public DateTime FechaFinal { get; set; }

        [Required(ErrorMessage = "Debe ingresar la dificultad")]
        [Range(1, 5)]
        [Display(Name = "Dificultad")]
        public int Dificultad { get; set; }

        [Required(ErrorMessage = "Debe ingresar la prioridad")]
        [Range(1, 3)]
        [Display(Name = "Prioridad")]
        public int Prioridad { get; set; }
        public string? UsuarioCreador { get; set; }

        [Required(ErrorMessage = "Debe seleccionar la categoria")]
        [Range(1, 5)]
        [Display(Name = "Categoria")]
        public int Categoria { get; set; }
        public int Estado { get; set; }

        public Tarea() {
            Id = Guid.NewGuid().ToString();
            Titulo = "";
            Descripcion = "";
            FechaInicial = DateTime.Now;
            FechaFinal = DateTime.Now.AddDays(1);
            Estado = 1;
            Categoria = 1;
            Prioridad = 1;
            Dificultad = 1;
        }

        public Tarea(string titulo, string descripcion, DateTime inicial, DateTime final, int estado, int categoria, int prioridad, int dificultad, string usuario)
        {
            Id = Guid.NewGuid().ToString();
            Titulo = titulo;
            Descripcion = descripcion;
            FechaInicial = inicial;
            FechaFinal = final;
            Estado = estado;
            Categoria = categoria;
            Prioridad = prioridad;
            Dificultad = dificultad;
            UsuarioCreador = usuario;
        }

    }
}
