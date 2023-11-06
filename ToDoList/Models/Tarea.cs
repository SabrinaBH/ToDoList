using System.ComponentModel.DataAnnotations;

namespace ToDoList.Models
{
    public class Tarea
    {
        public String Id { get; set; }
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
        public int UsuarioCreador { get; set; }
        public int Categoria { get; set; }
        public int Estado { get; set; }



    }
}
