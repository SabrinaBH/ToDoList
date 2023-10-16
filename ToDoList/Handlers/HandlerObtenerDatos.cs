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

        public List<Usuario> ObtenerUsuario(int identificadorUsuario)
        {
            List<Usuario> usuario = new List<Usuario>();

            string consultaBaseDatos = "SELECT * FROM Usuario WHERE Usuario.IdentificadorUsuario = '" + identificadorUsuario + "';";

            DataTable tablaDeDesglose = CrearTablaConsulta(consultaBaseDatos);
            foreach (DataRow columna in tablaDeDesglose.Rows)
            {
                usuario.Add(
                new Usuario
                {
                    Id = Convert.ToInt32(columna["IdentificadorUsuario"]),
                    Nombre = Convert.ToString(columna["Nombre"]),
                    PrimerApellido = Convert.ToString(columna["PrimerApellido"]),
                    SegundoApellido = Convert.ToString(columna["SegundoApellido"]),
                    Nickname = Convert.ToString(columna["Nickname"]),
                    Email = Convert.ToString(columna["Email"])
                });
            }
            return usuario;
        }

        public List<Estado> ObtenerEstadosUsuario(int identificadorUsuario)
        {
            List<Estado> estados = new List<Estado>();

            string consultaBaseDatos = "SELECT * FROM Estado WHERE Estado.IdentificadorUsuarioCreador = '" + identificadorUsuario + "';";

            DataTable tablaDeDesglose = CrearTablaConsulta(consultaBaseDatos);
            foreach (DataRow columna in tablaDeDesglose.Rows)
            {
                estados.Add(
                new Estado
                {
                    Id = Convert.ToInt32(columna["IdentificadorCategoria"]),
                    Nombre = Convert.ToString(columna["NombreEstado"]),
                    UsuarioCreador = Convert.ToInt32(columna["IdentificadorUsuarioCreador"])
                });
            }
            return estados;
        }

        public List<Categoria> ObtenerCategoriasUsuario(int identificadorUsuario)
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
                    UsuarioCreador = Convert.ToInt32(columna["IdentificadorUsuarioCreador"])
                });
            }
            return categorias;
        }

        public List<Tarea> ObtenerTareasUsuario(int identificadorUsuario)
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
                        Id = Convert.ToInt32(columna["IdentificadorTarea"]),
                        Titulo = Convert.ToString(columna["Titulo"]),
                        Descripcion = Convert.ToString(columna["Descripcion"]),
                        FechaInicial = Convert.ToDateTime(columna["FechaInicial"]),
                        FechaFinal = Convert.ToDateTime(columna["FechaFinal"]),
                        Dificultad = Convert.ToInt16(columna["Dificultad"]),
                        Prioridad = Convert.ToInt16(columna["Prioridad"]),
                        UsuarioCreador = Convert.ToInt32(columna["IdentificadorUsuarioCreador"]),
                        Categoria = Convert.ToInt32(columna["IdentificadorCategoria"]),
                        Estado = Convert.ToInt32(columna["IdentificadorEstado"])
                    });
                }
                return tareas;
            }
        }

    }
}

