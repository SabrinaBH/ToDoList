using System.Data;
using System.Data.SqlClient;
using System.Threading;
using ToDoList.Models;

namespace ToDoList.Handlers
{
    public abstract class HandlerBase
    {
        protected SqlConnection conexion;
        protected string rutaConexion;
        public HandlerBase()
        {
            var builder = WebApplication.CreateBuilder();
            rutaConexion =
            builder.Configuration.GetConnectionString("ContextoBaseDeDatosProyectoToDoList");
            conexion = new SqlConnection(rutaConexion);
        }

        //método para llenar una tabla a partir de la información obtenida de una consulta a la base de datos
        protected DataTable CrearTablaConsulta(string consulta)
        {

            SqlCommand comandoParaConsulta = new(consulta,
            conexion);
            SqlDataAdapter adaptadorParaTabla = new(comandoParaConsulta);
            DataTable consultaFormatoTabla = new();
            conexion.Open();
            adaptadorParaTabla.Fill(consultaFormatoTabla);
            conexion.Close();
            return consultaFormatoTabla;
        }

        public void ReiniciarConexion()
        {
            var builder = WebApplication.CreateBuilder();
            rutaConexion = builder.Configuration.GetConnectionString("ContextoBaseDeDatosProyectoToDoList");
            conexion = new SqlConnection(rutaConexion);
        }


        public List<Tarea> LlenarListaTareas(DataTable tablaDeDesglose)
        {

            List<Tarea> tareas = new List<Tarea>();

            foreach (DataRow columna in tablaDeDesglose.Rows)
            {
            tareas.Add(
                new Tarea
            {
                Id = Convert.ToString(columna["IdentificadorTarea"]),
                Titulo = Convert.ToString(columna["Titulo"]),
                Descripcion = Convert.ToString(columna["Descripcion"]),
                FechaInicial = Convert.ToDateTime(columna["FechaInicial"]),
                FechaFinal = Convert.ToDateTime(columna["FechaFinal"]),
                Dificultad = Convert.ToInt16(columna["Dificultad"]),
                Prioridad = Convert.ToInt16(columna["Prioridad"]),
                UsuarioCreador = Convert.ToString(columna["IdentificadorUsuarioCreador"]),
                Categoria = Convert.ToInt32(columna["IdentificadorCategoria"]),
                Estado = Convert.ToInt32(columna["IdentificadorEstado"])
                    });
            }
            return tareas;
        }
    }
}
