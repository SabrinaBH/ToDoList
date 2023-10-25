using System.Data;
using System.Data.SqlClient;
using ToDoList.Models;

namespace ToDoList.Handlers
{
    public class HandlerObtenerDatos : HandlerBase
    {

        public HandlerObtenerDatos() { }

        public Usuario ObtenerUsuario(int identificadorUsuario)
        {
            Usuario usuario = new();

            string consultaBaseDatos = "SELECT * FROM dbo.ObtenerUsuario('" + (identificadorUsuario - 48) + "')";

            using (SqlCommand command = new(consultaBaseDatos, conexion))
            {

                if (conexion.State != System.Data.ConnectionState.Open)
                {
                    conexion.Open();
                }

                using (SqlDataReader reader = command.ExecuteReader())
                {
                    usuario = ObtenerUsuarioDesdeReader(reader);
                }

                conexion.Close();
            }
            return usuario;
        }

        public List<Estado> ObtenerEstadosUsuario(int identificadorUsuario)
        {
            List<Estado> estados = new();

            string consultaBaseDatos = "SELECT * FROM dbo.ObtenerEstados('" + (identificadorUsuario - 48) + "')";

            using (SqlCommand command = new(consultaBaseDatos, conexion))
            {

                if (conexion.State != System.Data.ConnectionState.Open)
                {
                    conexion.Open();
                }

                using (SqlDataReader reader = command.ExecuteReader())
                {
                    estados = ObtenerEstadosDesdeReader(reader);
                }

                conexion.Close();
            }
            return estados;
        }

        public List<Categoria> ObtenerCategoriasUsuario(int identificadorUsuario)
        {
            List<Categoria> categorias = new();

            string consultaBaseDatos = "SELECT * FROM dbo.ObtenerCategorias('" + (identificadorUsuario -48) + "')";

            using (SqlCommand command = new(consultaBaseDatos, conexion))
            {

                if (conexion.State != System.Data.ConnectionState.Open)
                {
                    conexion.Open();
                }
                    
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    categorias = ObtenerCategoriasDesdeReader(reader);
                }
                conexion.Close();

               
            }
            return categorias;
        }

        public List<Tarea> ObtenerTareasUsuario(int identificadorUsuario)
        {
            List<Tarea> tareas = new();

            string consultaBaseDatos = "SELECT * FROM dbo.ObtenerTareasUsuario('"+(identificadorUsuario-48)+"')";

            using (SqlCommand command = new(consultaBaseDatos, conexion))
            {


                if (conexion.State != System.Data.ConnectionState.Open)
                {
                    conexion.Open();
                }

                using (SqlDataReader reader = command.ExecuteReader()) 
                {
                    tareas = ObtenerTareasDesdeReader(reader);
                }

                conexion.Close();
            }
                return tareas;
        }

        private Usuario ObtenerUsuarioDesdeReader(SqlDataReader reader)
        {
            Usuario usuario = new();
            while (reader.Read())
            {
                usuario.Id = reader.GetInt32(reader.GetOrdinal("IdentificadorUsuario"));
                usuario.Nombre = reader.GetString(reader.GetOrdinal("Nombre"));
                usuario.PrimerApellido = reader.GetString(reader.GetOrdinal("PrimerApellido"));
                usuario.SegundoApellido = reader.GetString(reader.GetOrdinal("SegundoApellido"));
                usuario.Nickname = reader.GetString(reader.GetOrdinal("Nickname"));
                usuario.Email = reader.GetString(reader.GetOrdinal("Email"));

            }
            return usuario;
        }

        private List<Tarea> ObtenerTareasDesdeReader (SqlDataReader reader)
        {
            List<Tarea> tareas = new();
            while (reader.Read())
            {
                Tarea tarea = new();
                tarea.Id = reader.GetInt32(reader.GetOrdinal("IdentificadorTarea"));
                tarea.Titulo = reader.GetString(reader.GetOrdinal("Titulo"));
                tarea.Descripcion = reader.GetString(reader.GetOrdinal("Descripcion"));
                tarea.FechaInicial = Convert.ToDateTime(reader.GetDateTime(reader.GetOrdinal("FechaInicial")));
                tarea.FechaFinal = Convert.ToDateTime(reader.GetDateTime(reader.GetOrdinal("FechaFinal")));
                tarea.Dificultad = reader.GetInt16(reader.GetOrdinal("Dificultad"));
                tarea.Prioridad = reader.GetInt16(reader.GetOrdinal("Prioridad"));
                tarea.UsuarioCreador = reader.GetInt32(reader.GetOrdinal("IdentificadorUsuarioCreador"));
                tarea.Categoria = reader.GetInt32(reader.GetOrdinal("IdentificadorCategoria"));
                tarea.Estado = reader.GetInt32(reader.GetOrdinal("IdentificadorEstado"));

                tareas.Add (tarea);
            }
            return tareas;
        }

        private List<Categoria> ObtenerCategoriasDesdeReader(SqlDataReader reader)
        {
            List<Categoria> categorias = new();
            while (reader.Read())
            {
                Categoria categoria = new();
                categoria.Id = reader.GetInt32(reader.GetOrdinal("IdentificadorCategoria"));
                categoria.Nombre = reader.GetString(reader.GetOrdinal("NombreCategoria"));
                categoria.UsuarioCreador = reader.GetInt32(reader.GetOrdinal("IdentificadorUsuarioCreador"));

                categorias.Add(categoria);
            }
            return categorias;
        }


        private List<Estado> ObtenerEstadosDesdeReader(SqlDataReader reader)
        {
            List<Estado> estados = new();
            while (reader.Read())
            {
                Estado estado = new();
                estado.Id = reader.GetInt32(reader.GetOrdinal("IdentificadorEstado"));
                estado.Nombre = reader.GetString(reader.GetOrdinal("NombreEstado"));
                estado.UsuarioCreador = reader.GetInt32(reader.GetOrdinal("IdentificadorUsuarioCreador"));

                estados.Add(estado);
            }
            return estados;
        }

    }
}

