using System.Data;
using System.Data.SqlClient;


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
    }
}
