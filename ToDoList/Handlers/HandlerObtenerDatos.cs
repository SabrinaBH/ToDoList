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


    public void EjecutarComandoSQL(SqlCommand comando)
    {

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

    }


    public String ObtenerIDUsuarioAdmin()
    {
      string consulta = "ObtenerIDUsuarioAdmin";
      string resultado = " ";
      SqlCommand comando = new(consulta, conexion);
      comando.CommandType = CommandType.StoredProcedure;
      SqlParameter id = new("@IDAdmin", SqlDbType.UniqueIdentifier);
      id.Direction = ParameterDirection.Output;
      comando.Parameters.Add(id);

      EjecutarComandoSQL(comando);

      if (id.Value != null)
      {
        resultado = id.Value.ToString();
      }

      return resultado;
    }

    public String ObtenerIDUsuario(String email)
    {
      string consulta = "ObtenerIDUsuario";
      string resultado = " ";
      SqlCommand comando = new(consulta, conexion);
      comando.CommandType = CommandType.StoredProcedure;
      comando.Parameters.AddWithValue("@EmailUsuario", email);
      SqlParameter id = new("@IDUsuario", SqlDbType.UniqueIdentifier);
      id.Direction = ParameterDirection.Output;
      comando.Parameters.Add(id);

      EjecutarComandoSQL(comando);

      if (id.Value != null)
      {
        resultado = id.Value.ToString();
      }

      return resultado;
    }


    public bool ObtenerEsJuego(string email)
    {
      string consulta = "ObtenerEsJuego";
      bool resultado = false;
      SqlCommand comando = new(consulta, conexion);
      comando.CommandType = CommandType.StoredProcedure;
      comando.Parameters.AddWithValue("@EmailUsuario", email);
      SqlParameter esJuego = new("@EsJuego", SqlDbType.Bit);
      esJuego.Direction = ParameterDirection.Output;
      comando.Parameters.Add(esJuego);

      EjecutarComandoSQL(comando);

      if (esJuego.Value != null)
      {
        resultado = Convert.ToBoolean(esJuego.Value);
      }

      return resultado;
    }

    public Usuario ObtenerUsuario(String identificadorUsuario)
    {
      string consultaBaseDatos = "EXEC ObtenerInformacionUsuario @IdentificadorUsuario = '" + identificadorUsuario + "';";

      DataTable tablaDeDesglose = CrearTablaConsulta(consultaBaseDatos);
      DataRow columna = tablaDeDesglose.Rows[0];
      Usuario usuario = new()
      {
        Id = Convert.ToString(columna["IdentificadorUsuario"]),
        Nombre = Convert.ToString(columna["Nombre"]),
        PrimerApellido = Convert.ToString(columna["PrimerApellido"]),
        SegundoApellido = Convert.ToString(columna["SegundoApellido"]),
        Email = Convert.ToString(columna["Email"]),
        EsUsuarioDeJuego = Convert.ToBoolean(columna["EsUsuarioDeJuego"])
      };
      return usuario;
    }


    public List<Estado> ObtenerEstadosUsuario(String identificadorUsuario)
    {
      List<Estado> estados = new();
      if (identificadorUsuario != "" && identificadorUsuario != null)
      {

        string consultaBaseDatos = "EXEC ObtenerEstadosUsuario @IdentificadorUsuario = '" + identificadorUsuario + "';";

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
      }
      return estados;
    }

    public List<Categoria> ObtenerCategoriasUsuario(String identificadorUsuario)
    {
      List<Categoria> categorias = new();
      if (identificadorUsuario != "" && identificadorUsuario != null)
      {

        string consultaBaseDatos = "EXEC ObtenerCategoriasUsuario @IdentificadorUsuario = '" + identificadorUsuario + "';";

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
      }
      return categorias;
    }

    public List<Tarea> ObtenerTareasUsuario(String identificadorUsuario)
    {
      List<Tarea> tareas = new();

      if (identificadorUsuario != "" && identificadorUsuario != null)
      {

        string consultaBaseDatos = "EXEC ObtenerTareas @IdentificadorUsuario = '" + identificadorUsuario + "';";

        using (SqlCommand command = new(consultaBaseDatos, conexion))
        {


          DataTable tablaDeDesglose = CrearTablaConsulta(consultaBaseDatos);

          tareas = LlenarListaTareas(tablaDeDesglose);
          //foreach (DataRow columna in tablaDeDesglose.Rows)
          //{
          //    tareas.Add(
          //    new Tarea
          //    {
          //        Id = Convert.ToString(columna["IdentificadorTarea"]),
          //        Titulo = Convert.ToString(columna["Titulo"]),
          //        Descripcion = Convert.ToString(columna["Descripcion"]),
          //        FechaInicial = Convert.ToDateTime(columna["FechaInicial"]),
          //        FechaFinal = Convert.ToDateTime(columna["FechaFinal"]),
          //        Dificultad = Convert.ToInt16(columna["Dificultad"]),
          //        Prioridad = Convert.ToInt16(columna["Prioridad"]),
          //        UsuarioCreador = Convert.ToString(columna["IdentificadorUsuarioCreador"]),
          //        Categoria = Convert.ToInt32(columna["IdentificadorCategoria"]),
          //        Estado = Convert.ToInt32(columna["IdentificadorEstado"])
          //    });
          //}
        }
      }
      return tareas;
    }


    public bool InsertarNuevoUsuario(Usuario usuario)
    {
      bool completado = false;

      string consulta = "InsertarNuevoUsuario";
      SqlCommand comando = new(consulta, conexion);
      comando.CommandType = CommandType.StoredProcedure;

      comando.Parameters.AddWithValue("@NombreNuevo", usuario.Nombre);
      comando.Parameters.AddWithValue("@PrimerApellidoNuevo", usuario.PrimerApellido);
      comando.Parameters.AddWithValue("@SegundoApellidoNuevo", usuario.SegundoApellido);
      comando.Parameters.AddWithValue("@EmailNuevo", usuario.Email);
      comando.Parameters.AddWithValue("@EsUsuarioDeJuego", usuario.EsUsuarioDeJuego);
      SqlParameter completadoExito = new("@InsertCompletado", SqlDbType.Bit);
      completadoExito.Direction = ParameterDirection.Output;
      comando.Parameters.Add(completadoExito);

      EjecutarComandoSQL(comando);

      if (completadoExito.Value != null)
      {
        completado = Convert.ToBoolean(completadoExito.Value);
      }

      return completado;
    }


    public bool InsertarNuevaCategoria(Categoria categoria)
    {
      bool completado = false;

      string consulta = "InsertarNuevaCategoria";
      SqlCommand comando = new(consulta, conexion);
      comando.CommandType = CommandType.StoredProcedure;

      comando.Parameters.AddWithValue("@NombreNuevo", categoria.Nombre);
      comando.Parameters.AddWithValue("@IdentificadorCreador", categoria.UsuarioCreador);

      SqlParameter completadoExito = new("@InsertCompletado", SqlDbType.Bit);
      completadoExito.Direction = ParameterDirection.Output;
      comando.Parameters.Add(completadoExito);

      EjecutarComandoSQL(comando);

      if (completadoExito.Value != null)
      {
        completado = Convert.ToBoolean(completadoExito.Value);
      }

      return completado;
    }

    public bool InsertarNuevoEstado(Estado estado)
    {
      bool completado = false;

      string consulta = "InsertarNuevoEstado";
      SqlCommand comando = new(consulta, conexion);
      comando.CommandType = CommandType.StoredProcedure;

      comando.Parameters.AddWithValue("@NombreNuevo", estado.Nombre);
      comando.Parameters.AddWithValue("@IdentificadorCreador", estado.UsuarioCreador);

      SqlParameter completadoExito = new("@InsertCompletado", SqlDbType.Bit);
      completadoExito.Direction = ParameterDirection.Output;
      comando.Parameters.Add(completadoExito);

      EjecutarComandoSQL(comando);

      if (completadoExito.Value != null)
      {
        completado = Convert.ToBoolean(completadoExito.Value);
      }

      return completado;
    }

    public bool InsertarNuevaTarea(Tarea tarea)
    {
      bool completado = false;

      string consulta = "InsertarNuevaTarea";
      SqlCommand comando = new(consulta, conexion);
      comando.CommandType = CommandType.StoredProcedure;

      comando.Parameters.AddWithValue("@TituloNuevo", tarea.Titulo);
      comando.Parameters.AddWithValue("@DescripcionNueva", tarea.Descripcion);
      comando.Parameters.AddWithValue("@FechaInicialNueva", tarea.FechaInicial);
      comando.Parameters.AddWithValue("@FechaFinalNueva", tarea.FechaFinal);
      comando.Parameters.AddWithValue("@Dificultad", tarea.Dificultad);
      comando.Parameters.AddWithValue("@Prioridad", tarea.Prioridad);
      comando.Parameters.AddWithValue("@IdentificadorCreador", tarea.UsuarioCreador);
      comando.Parameters.AddWithValue("@IdentificadorCategoria", tarea.Categoria);
      comando.Parameters.AddWithValue("@IdentificadorEstado", tarea.Estado);
      SqlParameter completadoExito = new("@InsertCompletado", SqlDbType.Bit);
      completadoExito.Direction = ParameterDirection.Output;
      comando.Parameters.Add(completadoExito);

      EjecutarComandoSQL(comando);

      if (completadoExito.Value != null)
      {
        completado = Convert.ToBoolean(completadoExito.Value);
      }

      return completado;
    }

    public bool ActualizarTarea(Tarea tarea)
    {
      bool completado = false;

      string consulta = "ActualizarTarea";
      SqlCommand comando = new(consulta, conexion);
      comando.CommandType = CommandType.StoredProcedure;

      comando.Parameters.AddWithValue("@TituloCambio", tarea.Titulo);
      comando.Parameters.AddWithValue("@DescripcionCambio", tarea.Descripcion);
      comando.Parameters.AddWithValue("@FechaInicialCambio", tarea.FechaInicial);
      comando.Parameters.AddWithValue("@FechaFinalCambio", tarea.FechaFinal);
      comando.Parameters.AddWithValue("@DificultadCambio", tarea.Dificultad);
      comando.Parameters.AddWithValue("@PrioridadCambio", tarea.Prioridad);
      comando.Parameters.AddWithValue("@IdentificadorCreador", tarea.UsuarioCreador);
      comando.Parameters.AddWithValue("@IdentificadorTarea", tarea.Id);
      comando.Parameters.AddWithValue("@IdentificadorCategoriaCambio", tarea.Categoria);
      comando.Parameters.AddWithValue("@IdentificadorEstadoCambio", tarea.Estado);
      SqlParameter completadoExito = new("@ActualizadoCompletado", SqlDbType.Bit);
      completadoExito.Direction = ParameterDirection.Output;
      comando.Parameters.Add(completadoExito);

      EjecutarComandoSQL(comando);
      if (completadoExito.Value != null)
      {
        completado = Convert.ToBoolean(completadoExito.Value);
      }

      return completado;
    }

    public bool ActualizarUsuario(Usuario usuario)
    {
      bool completado = false;

      string consulta = "ActualizarUsuario";
      SqlCommand comando = new(consulta, conexion);
      comando.CommandType = CommandType.StoredProcedure;

      comando.Parameters.AddWithValue("@IdentificadorUsuario", usuario.Id);
      comando.Parameters.AddWithValue("@NombreCambio", usuario.Nombre);
      comando.Parameters.AddWithValue("@PrimerApellidoCambio", usuario.PrimerApellido);
      comando.Parameters.AddWithValue("@SegundoApellidoCambio", usuario.SegundoApellido);
      comando.Parameters.AddWithValue("@EmailCambio", usuario.Email);
      SqlParameter completadoExito = new("@ActualizadoCompletado", SqlDbType.Bit);
      completadoExito.Direction = ParameterDirection.Output;
      comando.Parameters.Add(completadoExito);

      EjecutarComandoSQL(comando);

      if (completadoExito.Value != null)
      {
        completado = Convert.ToBoolean(completadoExito.Value);
      }

      return completado;
    }

    public bool BorrarTarea(Tarea tarea)
    {
      bool completado = false;

      string consulta = "BorrarTarea";
      SqlCommand comando = new(consulta, conexion);
      comando.CommandType = CommandType.StoredProcedure;

      comando.Parameters.AddWithValue("@IdentificadorTarea", tarea.Id);
      comando.Parameters.AddWithValue("@IdentificadorUsuarioCreador", tarea.UsuarioCreador);
      SqlParameter completadoExito = new("@BorradoCompletado", SqlDbType.Bit);
      completadoExito.Direction = ParameterDirection.Output;
      comando.Parameters.Add(completadoExito);

      EjecutarComandoSQL(comando);

      if (completadoExito.Value != null)
      {
        completado = Convert.ToBoolean(completadoExito.Value);
      }

      return completado;
    }


    public bool BorrarCategoria(Categoria categoria)
    {
      bool completado = false;

      string consulta = "BorrarCategoria";
      SqlCommand comando = new(consulta, conexion);
      comando.CommandType = CommandType.StoredProcedure;

      comando.Parameters.AddWithValue("@IdentificadorCategoria", categoria.Id);
      comando.Parameters.AddWithValue("@IdentificadorUsuarioCreador", categoria.UsuarioCreador);
      SqlParameter completadoExito = new("@BorradoCompletado", SqlDbType.Bit);
      completadoExito.Direction = ParameterDirection.Output;
      comando.Parameters.Add(completadoExito);

      EjecutarComandoSQL(comando);

      if (completadoExito.Value != null)
      {
        completado = Convert.ToBoolean(completadoExito.Value);
      }

      return completado;
    }

    public bool BorrarEstado(Estado estado)
    {
      bool completado = false;

      string consulta = "BorrarEstado";
      SqlCommand comando = new(consulta, conexion);
      comando.CommandType = CommandType.StoredProcedure;

      comando.Parameters.AddWithValue("@IdentificadorEstado", estado.Id);
      comando.Parameters.AddWithValue("@IdentificadorUsuarioCreador", estado.UsuarioCreador);
      SqlParameter completadoExito = new("@BorradoCompletado", SqlDbType.Bit);
      completadoExito.Direction = ParameterDirection.Output;
      comando.Parameters.Add(completadoExito);

      EjecutarComandoSQL(comando);

      if (completadoExito.Value != null)
      {
        completado = Convert.ToBoolean(completadoExito.Value);
      }

      return completado;
    }

    public bool BorrarUsuario(Usuario usuario)
    {
      bool completado = false;

      string consulta = "BorrarUsuario";
      SqlCommand comando = new(consulta, conexion);
      comando.CommandType = CommandType.StoredProcedure;

      comando.Parameters.AddWithValue("@IdentificadorUsuario", usuario.Id);
      SqlParameter completadoExito = new("@BorradoCompletado", SqlDbType.Bit);
      completadoExito.Direction = ParameterDirection.Output;
      comando.Parameters.Add(completadoExito);

      EjecutarComandoSQL(comando);

      if (completadoExito.Value != null)
      {
        completado = Convert.ToBoolean(completadoExito.Value);
      }

      return completado;
    }
  }

}

