using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace ToDoList.Views.ViewModels
{
    public class TareaModel
    {
        public string Titulo { get; set; }
        public string Descripcion { get; set; }
        public DateTime FechaInicial { get; set; }
        public DateTime FechaFinal { get; set; }
        public int Dificultad { get; set; }
        public int Prioridad { get; set; }
        public string? UsuarioCreador { get; set; }
        public int Categoria { get; set; }
        public int Estado { get; set; }

        public TareaModel()
        {
            Titulo = "";
            Descripcion = "";
            FechaInicial = DateTime.Now;
            FechaFinal = DateTime.Now.AddDays(1);
            Estado = 1;
            Categoria = 1;
            Prioridad = 1;
            Dificultad = 1;
        }

        public TareaModel(string titulo, string descripcion, DateTime inicial, DateTime final, int estado, int categoria, int prioridad, int dificultad, string usuario)
        {
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
