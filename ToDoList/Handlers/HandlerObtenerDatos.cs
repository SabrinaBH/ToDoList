using System.Data;
using System.Data.SqlClient;
using ToDoList.Models;

namespace ToDoList.Handlers
{
    public class HandlerObtenerDatos : HandlerBase
    {

        public HandlerObtenerDatos() { }   

        public List<Estado> ObtenerEstadosDefault()
        {
            List<Estado> estados = new List<Estado>();

            string consultaBaseDeDatos = "SELECT * FROM Estado WHERE IdentificadorUsuarioCreador = '1';";
            DataTable tablaDeEstados = CrearTablaConsulta(consultaBaseDeDatos);
            foreach(DataRow columna in tablaDeEstados.Rows)
            {
                estados.Add(new Estado
                {
                    Id = Convert.ToInt32(columna["IdentificadorEstado"]),
                    Nombre = Convert.ToString(columna["NombreEstado"]),
                    UsuarioCreador = Convert.ToInt32(columna["IdentificadorUsuarioCreador"])
                });
            }
            return estados;
        }

        public List<Categoria> ObtenerCategorias() {

            List<Categoria> categorias = new List<Categoria>();

            string consultaBaseDeDatos = "SELECT * FROM Categoria WHERE IdentificadorUsuarioCreador = '1';";
            DataTable tablaDeCategorias = CrearTablaConsulta(consultaBaseDeDatos);
            foreach (DataRow columna in tablaDeCategorias.Rows)
            {
                categorias.Add(new Categoria
                {
                    Id = Convert.ToInt32(columna["IdentificadorCategoria"]),
                    Nombre = Convert.ToString(columna["NombreCategoria"]),
                    UsuarioCreador = Convert.ToInt32(columna["IdentificadorUsuarioCreador"])
                });
            }
            return categorias;
        }

        public List<Tarea> ObtenerTareasUsuario(int identificadorUsuario)
        {
            List<Tarea> tareas = new List<Tarea>();

            string consultaBaseDatos = "SELECT * FROM dbo.ObtenerTareasUsuario(" + identificadorUsuario+ ")";
            DataTable tablaDeTareas = CrearTablaConsulta(consultaBaseDatos);
            foreach (DataRow columna in tablaDeTareas.Rows)
            {
                tareas.Add(new Tarea
                {
                    Id = Convert.ToInt32(columna["IdentificadorTarea"]),
                    Titulo = Convert.ToString(columna["Titulo"]),
                    Descripcion = Convert.ToString(columna["Descripcion"]),
                    FechaInicial = Convert.ToDateTime(columna["FechaInicial"]),
                    FechaFinal = Convert.ToDateTime(columna["FechaFinal"]),
                    Dificultad = Convert.ToInt32(columna["Dificultad"]),
                    Prioridad = Convert.ToInt32(columna["Prioridad"]),
                    UsuarioCreador = Convert.ToInt32(columna["IdentificadorUsuarioCreador"]),
                    Categoria = Convert.ToInt32(columna["IdentificadorCategoria"]),
                    Estado = Convert.ToInt32(columna["IdentificadorEstado"])
                }) ;
            }   

            return tareas;
        }

    }
}

