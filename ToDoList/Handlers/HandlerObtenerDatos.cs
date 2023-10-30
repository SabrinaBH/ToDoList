using System.Data;
using System.Data.SqlClient;
using System.Reflection.PortableExecutable;
using System.Threading;
using ToDoList.Models;

namespace ToDoList.Handlers
{
    public class HandlerObtenerDatos : HandlerBase
    {

        public HandlerObtenerDatos() { }

        public String ObtenerIDUsuarioAdmin()
        {
            string consulta = "ObtenerIDUsuarioAdmin";
            string resultado = " ";
            SqlCommand comando = new SqlCommand(consulta, conexion);
            comando.CommandType = CommandType.StoredProcedure;
            SqlParameter id = new SqlParameter("@IDAdmin", SqlDbType.UniqueIdentifier);
            id.Direction = ParameterDirection.Output;
            comando.Parameters.Add(id);
            if (conexion.State == System.Data.ConnectionState.Open)
            {
                comando.ExecuteNonQuery();
            }
            else
            {
                conexion.Open();
                comando.ExecuteNonQuery();
                conexion.Close();
            }

            if (id.Value != null){
                resultado = id.Value.ToString();
            }

            return resultado;
        }

        public String ObtenerIDUsuario(String email)
        {
            string consulta = "ObtenerIDUsuario";
            string resultado = " ";
            SqlCommand comando = new SqlCommand(consulta, conexion);
            comando.CommandType = CommandType.StoredProcedure;
            comando.Parameters.AddWithValue("@EmailUsuario", email);
            SqlParameter id = new SqlParameter("@IDUsuario", SqlDbType.UniqueIdentifier);
            id.Direction = ParameterDirection.Output;
            comando.Parameters.Add(id);
            if (conexion.State == System.Data.ConnectionState.Open)
            {
                comando.ExecuteNonQuery();
            }
            else
            {
                conexion.Open();
                comando.ExecuteNonQuery();
                conexion.Close();
            }

            if(id.Value != null) {
                resultado = id.Value.ToString();
            }

            return resultado;
        }

        public List<Usuario> ObtenerUsuario(String identificadorUsuario)
        {
            List<Usuario> usuario = new List<Usuario>();

            string consultaBaseDatos = "SELECT * FROM Usuario WHERE Usuario.IdentificadorUsuario = '" + identificadorUsuario + "';";

            DataTable tablaDeDesglose = CrearTablaConsulta(consultaBaseDatos);
            foreach (DataRow columna in tablaDeDesglose.Rows)
            {
                usuario.Add(
                new Usuario
                {
                    // Id = Guid.Parse((string)columna["IdentificadorUsuario"]),
                    Id = Convert.ToString(columna["IdentificadorUsuario"]),
                    Nombre = Convert.ToString(columna["Nombre"]),
                    PrimerApellido = Convert.ToString(columna["PrimerApellido"]),
                    SegundoApellido = Convert.ToString(columna["SegundoApellido"]),
                    Email = Convert.ToString(columna["Email"]),
                    EsUsuarioDeJuego = Convert.ToBoolean(columna["EsUsuarioDeJuego"])
                });
            }
            return usuario;
        }


        public List<Estado> ObtenerEstadosUsuario(String identificadorUsuario)
        {
            List<Estado> estados = new List<Estado>();

            string consultaBaseDatos = "SELECT * FROM Estado WHERE Estado.IdentificadorUsuarioCreador = '" + identificadorUsuario + "';";

            DataTable tablaDeDesglose = CrearTablaConsulta(consultaBaseDatos);
            foreach (DataRow columna in tablaDeDesglose.Rows)
            {
                estados.Add(
                new Estado
                {
                    Id = Convert.ToInt32(columna["IdentificadorEstado"]),
                    Nombre = Convert.ToString(columna["NombreEstado"]),
                    UsuarioCreador = Convert.ToString(columna["IdentificadorUsuarioCreador"])
                });
            }
            return estados;
        }

        public List<Categoria> ObtenerCategoriasUsuario(String identificadorUsuario)
        {
            List<Categoria> categorias = new List<Categoria>();

            string consultaBaseDatos = "SELECT * FROM Categoria WHERE Categoria.IdentificadorUsuarioCreador = '" + identificadorUsuario + "';";

            DataTable tablaDeDesglose = CrearTablaConsulta(consultaBaseDatos);
            foreach (DataRow columna in tablaDeDesglose.Rows)
            {
                categorias.Add(
                new Categoria
                {
                    Id = Convert.ToInt32(columna["IdentificadorCategoria"]),
                    Nombre = Convert.ToString(columna["NombreCategoria"]),
                    UsuarioCreador = Convert.ToString(columna["IdentificadorUsuarioCreador"])
                });
            }
            return categorias;
        }

        public List<Tarea> ObtenerTareasUsuario(String identificadorUsuario)
        {
            List<Tarea> tareas = new List<Tarea>();

            string consultaBaseDatos = "SELECT * FROM Tarea WHERE Tarea.IdentificadorUsuarioCreador = '" + identificadorUsuario + "';";

            using (SqlCommand command = new SqlCommand(consultaBaseDatos, conexion))
            {


                DataTable tablaDeDesglose = CrearTablaConsulta(consultaBaseDatos);
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
}

