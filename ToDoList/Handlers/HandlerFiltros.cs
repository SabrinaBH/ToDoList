using System.Data;
using System.Data.SqlClient;
using System.Reflection.PortableExecutable;
using System.Threading;
using ToDoList.Models;

namespace ToDoList.Handlers
{
    public class HandlerFiltros : HandlerBase
    {

        public HandlerFiltros() { }

        public List<Tarea> FiltroPorDificultad(string identificadorUsuario, int dificultad)
        {
            List<Tarea> tareas = new List<Tarea>();

            if (identificadorUsuario != "" && identificadorUsuario != null)
            {

                string consultaBaseDatos = "EXEC FiltroPorDificultad @IdentificadorUsuario = '" + identificadorUsuario + "', @NivelDificultad = " + dificultad+";";

                using (SqlCommand command = new SqlCommand(consultaBaseDatos, conexion))
                {


                    DataTable tablaDeDesglose = CrearTablaConsulta(consultaBaseDatos);
                    tareas = LlenarListaTareas(tablaDeDesglose);
                }
            }
            return tareas;
        }

        public List<Tarea> FiltroPorCategoria(string identificadorUsuario, int categoria)
        {
            List<Tarea> tareas = new List<Tarea>();

            if (identificadorUsuario != "" && identificadorUsuario != null)
            {

                string consultaBaseDatos = "EXEC FiltroPorCategoria @IdentificadorUsuario = '" + identificadorUsuario + "', @IdentificadorCategoria = " + categoria + ";";

                using (SqlCommand command = new SqlCommand(consultaBaseDatos, conexion))
                {

                    DataTable tablaDeDesglose = CrearTablaConsulta(consultaBaseDatos);
                    tareas = LlenarListaTareas(tablaDeDesglose);
                }
            }
            return tareas;
        }


        public List<Tarea> FiltroPorCategoriaDificultad(string identificadorUsuario, int categoria, int dificultad)
        {
            List<Tarea> tareas = new List<Tarea>();

            if (identificadorUsuario != "" && identificadorUsuario != null)
            {

                string consultaBaseDatos = "EXEC FiltroPorCategoriaDificultad @IdentificadorUsuario = '" + identificadorUsuario + "', @IdentificadorCategoria = " + categoria + ", @NivelDificultad= "+dificultad+";";

                using (SqlCommand command = new SqlCommand(consultaBaseDatos, conexion))
                {

                    DataTable tablaDeDesglose = CrearTablaConsulta(consultaBaseDatos);
                    tareas = LlenarListaTareas(tablaDeDesglose);
                }
            }
            return tareas;
        }
    }
}
