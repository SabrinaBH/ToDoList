
use ProyectoToDoList

GO
CREATE PROCEDURE ObtenerIDUsuarioAdmin(
	@IDAdmin UNIQUEIDENTIFIER OUTPUT
) AS 
BEGIN 
	SELECT @IDAdmin = IdentificadorUsuario
	FROM Usuario
	WHERE Usuario.Nombre = 'Admin';
	
END;

DECLARE @Admin UNIQUEIDENTIFIER;
EXEC ObtenerIDUsuarioAdmin @IDAdmin = @Admin output;

select @Admin;

GO
CREATE PROCEDURE ObtenerIDUsuario(
	@EmailUsuario VARCHAR(100),
	@IDUsuario UNIQUEIDENTIFIER OUTPUT
) AS 
BEGIN 

	IF EXISTS (SELECT Email
			   FROM Usuario
			   WHERE @EmailUsuario = Usuario.Email)
		BEGIN
			SELECT @IDUsuario = IdentificadorUsuario
			FROM Usuario
			WHERE Usuario.Email = @EmailUsuario;
		END;
	ELSE 
		BEGIN 
			SET @IDUsuario = NULL
		END;
END;


DECLARE @Usuario UNIQUEIDENTIFIER;

EXEC ObtenerIDUsuario @EmailUsuario = 'sabry.brenes@outlook.es', @IDUsuario = @Usuario output;

select @Usuario;



GO
CREATE PROCEDURE ObtenerEsJuego(
	@EmailUsuario VARCHAR(100),
	@EsJuego BIT OUTPUT
) AS 
BEGIN 

	IF EXISTS (SELECT Email
			   FROM Usuario
			   WHERE @EmailUsuario = Usuario.Email)
		BEGIN
			SELECT @EsJuego = EsUsuarioDeJuego
			FROM Usuario
			WHERE Usuario.Email = @EmailUsuario;
		END;

END;

SELECT * FROM Usuario

DECLARE @EsUsuarioJuego BIT;

EXEC ObtenerEsJuego @EmailUsuario = 'pablo.rodrigueznavarro@ucr.ac.cr', @EsJuego = @EsUsuarioJuego output;

select @EsUsuarioJuego;

GO
CREATE PROCEDURE InsertarNuevoUsuario(
	@NombreNuevo VARCHAR(50),
	@PrimerApellidoNuevo VARCHAR(50),
	@SegundoApellidoNuevo VARCHAR(50),
	@EmailNuevo VARCHAR(100),
	@EsUsuarioDeJuego BIT, 
	@InsertCompletado BIT OUTPUT
)AS
BEGIN 

	SET @InsertCompletado = 0;

	IF NOT EXISTS (SELECT Email
				   FROM Usuario
				   WHERE Usuario.Email = @EmailNuevo)
		BEGIN
			INSERT INTO Usuario (Nombre, PrimerApellido, SegundoApellido, Email, EsUsuarioDeJuego)
			VALUES (@NombreNuevo, @PrimerApellidoNuevo, @SegundoApellidoNuevo, @EmailNuevo, @EsUsuarioDeJuego);

			SET @InsertCompletado = 1;
		END;
END;

DECLARE @Completado BIT;

EXEC InsertarNuevoUsuario @NombreNuevo = 'Prueba', @PrimerApellidoNuevo = 'Prueba', @SegundoApellidoNuevo = 'Prueba', @EmailNuevo = 'prueba@gmail.com', @EsUsuarioDeJuego = 0,  @InsertCompletado = @Completado output;

SELECT @Completado;

SELECT * FROM Usuario;

GO
CREATE PROCEDURE InsertarNuevaCategoria(
	@NombreNuevo VARCHAR(20),
	@IdentificadorCreador UNIQUEIDENTIFIER,
	@InsertCompletado BIT OUTPUT
) AS
BEGIN 
	SET @InsertCompletado = 0;

	IF EXISTS (SELECT IdentificadorUsuario
			   FROM Usuario
			   WHERE Usuario.IdentificadorUsuario = @IdentificadorCreador)
		BEGIN
			IF NOT EXISTS (SELECT NombreCategoria
						   FROM Categoria
				           WHERE Categoria.NombreCategoria = @NombreNuevo 
				           AND Categoria.IdentificadorUsuarioCreador = @IdentificadorCreador)
				BEGIN
					INSERT INTO Categoria (NombreCategoria, IdentificadorUsuarioCreador)
					VALUES (@NombreNuevo, @IdentificadorCreador);

					SET @InsertCompletado = 1;
				END;
		END;
END;


DECLARE @Completado BIT;

EXEC InsertarNuevaCategoria @NombreNuevo = 'Juegos', @IdentificadorCreador = 'FA4C6D77-3C5F-4632-BE66-C80636EEE7C2', @InsertCompletado = @Completado output;

SELECT @Completado;

SELECT * FROM Categoria;

DELETE Categoria
WHERE Categoria.IdentificadorUsuarioCreador = '54B7179A-C237-491C-842E-AA3ED2A70AE9';



GO
CREATE PROCEDURE InsertarNuevoEstado(
	@NombreNuevo VARCHAR(20),
	@IdentificadorCreador UNIQUEIDENTIFIER,
	@InsertCompletado BIT OUTPUT
) AS
BEGIN 
	SET @InsertCompletado = 0;

	IF EXISTS (SELECT IdentificadorUsuario
			   FROM Usuario
			   WHERE Usuario.IdentificadorUsuario = @IdentificadorCreador)
		BEGIN
			IF NOT EXISTS (SELECT NombreEstado
						   FROM Estado
				           WHERE Estado.NombreEstado = @NombreNuevo 
				           AND Estado.IdentificadorUsuarioCreador = @IdentificadorCreador)
				BEGIN
					INSERT INTO Estado(NombreEstado, IdentificadorUsuarioCreador)
					VALUES (@NombreNuevo, @IdentificadorCreador);

					SET @InsertCompletado = 1;
				END;
		END;
END;

DECLARE @Completado BIT;

EXEC InsertarNuevoEstado @NombreNuevo = 'Casi Completados', @IdentificadorCreador = 'FA4C6D77-3C5F-4632-BE66-C80636EEE7C2', @InsertCompletado = @Completado output;

SELECT @Completado;

SELECT * FROM Estado;

DELETE Estado
WHERE Estado.IdentificadorUsuarioCreador = '54B7179A-C237-491C-842E-AA3ED2A70AE9';


GO
CREATE PROCEDURE InsertarNuevaTarea(
	@TituloNuevo VARCHAR(20),
	@DescripcionNueva VARCHAR(1000),
	@FechaInicialNueva DATE,
	@FechaFinalNueva DATE,
	@Dificultad SMALLINT,
	@Prioridad SMALLINT,
	@IdentificadorCreador UNIQUEIDENTIFIER,
	@IdentificadorCategoria INT,
	@IdentificadorEstado INT,
	@InsertCompletado BIT OUTPUT
) AS
BEGIN 
	SET @InsertCompletado = 0;

	IF EXISTS (SELECT IdentificadorUsuario
			   FROM Usuario
			   WHERE Usuario.IdentificadorUsuario = @IdentificadorCreador)
		BEGIN
			IF EXISTS (SELECT IdentificadorCategoria
					   FROM Categoria
					   WHERE Categoria.IdentificadorCategoria = @IdentificadorCategoria)
				BEGIN
					IF EXISTS (SELECT IdentificadorEstado
					           FROM Estado
					           WHERE Estado.IdentificadorEstado = @IdentificadorEstado)
						BEGIN
							IF @TituloNuevo = NULL
								BEGIN 
									SET @TituloNuevo = 'Sin Título';
								END;

							IF @FechaInicialNueva = NULL
								BEGIN 
									SET @FechaInicialNueva = CONVERT(DATE, GETDATE());
								END;

							INSERT INTO Tarea (Titulo, Descripcion, FechaInicial, FechaFinal, Dificultad, Prioridad, IdentificadorUsuarioCreador, IdentificadorCategoria, IdentificadorEstado)
							VALUES (@TituloNuevo, @DescripcionNueva, @FechaInicialNueva, @FechaFinalNueva, @Dificultad, @Prioridad, @IdentificadorCreador, @IdentificadorCategoria, @IdentificadorEstado);
							
							SET @InsertCompletado = 1;
						END;

				END;
		END;
END;

DECLARE @Completado BIT;

EXEC InsertarNuevaTarea @TituloNuevo ='Proyecto de software', @DescripcionNueva = 'hacer metodos de base de datos', @FechaInicialNueva = '2023-11-01', @FechaFinalNueva = '',
	@Dificultad = '5', @Prioridad = '3', @IdentificadorCreador = 'FA4C6D77-3C5F-4632-BE66-C80636EEE7C2', @IdentificadorCategoria = '3', @IdentificadorEstado = '2', @InsertCompletado = @Completado OUTPUT;

SELECT @Completado;

select * from usuario
SELECT * FROM Tarea;

DELETE Tarea 
WHERE Tarea.IdentificadorUsuarioCreador = '54B7179A-C237-491C-842E-AA3ED2A70AE9';


DELETE Usuario
WHERE Usuario.IdentificadorUsuario = '4230AF98-1726-4ED2-8D68-9CE656A3F546';


GO
CREATE PROCEDURE ActualizarTarea(
	@TituloCambio VARCHAR(20),
	@DescripcionCambio VARCHAR(1000),
	@FechaInicialCambio DATE,
	@FechaFinalCambio DATE,
	@DificultadCambio SMALLINT,
	@PrioridadCambio SMALLINT,
	@IdentificadorCreador UNIQUEIDENTIFIER,
	@IdentificadorTarea UNIQUEIDENTIFIER,
	@IdentificadorCategoriaCambio INT,
	@IdentificadorEstadoCambio INT,
	@ActualizadoCompletado BIT OUTPUT
) AS
BEGIN 

	SET @ActualizadoCompletado = 0;

	IF EXISTS (SELECT *
			   FROM Tarea
			   WHERE Tarea.IdentificadorUsuarioCreador = @IdentificadorCreador AND Tarea.IdentificadorTarea = @IdentificadorTarea)
		BEGIN 
			IF EXISTS (SELECT * 
			           FROM Categoria 
					   WHERE Categoria.IdentificadorCategoria = @IdentificadorCategoriaCambio)
				BEGIN 
					IF EXISTS (SELECT * 
			           FROM Estado 
					   WHERE Estado.IdentificadorEstado = @IdentificadorEstadoCambio)
						BEGIN 
							UPDATE Tarea 
							SET Tarea.Titulo = @TituloCambio,
								Tarea.Descripcion = @DescripcionCambio,
								Tarea.FechaInicial = @FechaInicialCambio,
								Tarea.FechaFinal = @FechaFinalCambio,
								Tarea.Dificultad = @DificultadCambio,
								Tarea.Prioridad = @PrioridadCambio,
								Tarea.IdentificadorCategoria = @IdentificadorCategoriaCambio,
								Tarea.IdentificadorEstado = @IdentificadorEstadoCambio
							WHERE Tarea.IdentificadorTarea = @IdentificadorTarea AND Tarea.IdentificadorUsuarioCreador = @IdentificadorCreador;

							SET @ActualizadoCompletado = 1;
						END;
				END;
		END;
END;

DECLARE @Completado BIT;

EXEC ActualizarTarea @TituloCambio ='Proyecto de Calidad', @DescripcionCambio = 'Buscar herramientas de analisis', @FechaInicialCambio = '2023-10-05', @FechaFinalCambio = '2023-10-12',
	@DificultadCambio = '4', @PrioridadCambio = '3', @IdentificadorCreador = 'D156E9CE-405A-41E3-BBD7-A21FA5E5FE53', @IdentificadorTarea = '4EAE4A1B-7574-4DF1-9A8F-24504CAE5941', 
	@IdentificadorCategoriaCambio = '3', @IdentificadorEstadoCambio = '2', @ActualizadoCompletado = @Completado OUTPUT;

SELECT @Completado;

SELECT * FROM Tarea;

SELECT * FROM Categoria;

SELECT * FROM Estado;


GO
CREATE PROCEDURE ActualizarUsuario(
	@IdentificadorUsuario UNIQUEIDENTIFIER,
	@NombreCambio VARCHAR(50),
	@PrimerApellidoCambio VARCHAR(50),
	@SegundoApellidoCambio VARCHAR(50),
	@EmailCambio VARCHAR(100),
	@ActualizadoCompletado BIT OUTPUT
) AS
BEGIN 

	SET @ActualizadoCompletado = 0;

	IF EXISTS (SELECT *
			   FROM Usuario
			   WHERE Usuario.IdentificadorUsuario = @IdentificadorUsuario)
		BEGIN 
			UPDATE Usuario
			SET Usuario.Nombre = @NombreCambio,
				Usuario.PrimerApellido = @PrimerApellidoCambio,
				Usuario.SegundoApellido = @SegundoApellidoCambio,
				Usuario.Email = @EmailCambio
			WHERE Usuario.IdentificadorUsuario = @IdentificadorUsuario;

			SET @ActualizadoCompletado = 1;
		END;
END;

DECLARE @Completado BIT;

EXEC ActualizarUsuario @IdentificadorUsuario = 'B681430E-F422-441C-81AB-D8F015A26909', @NombreCambio = 'Allen', @PrimerApellidoCambio = 'Rivera', @SegundoApellidoCambio= 'Hernandez', @EmailCambio = 'AlQB25@gmail.com',  @ActualizadoCompletado = @Completado output;

SELECT @Completado;

SELECT * FROM Usuario;

GO
CREATE PROCEDURE BorrarTarea(
	@IdentificadorTarea UNIQUEIDENTIFIER,
	@IdentificadorUsuarioCreador UNIQUEIDENTIFIER,
	@BorradoCompletado BIT OUTPUT
) AS
BEGIN  
	SET @BorradoCompletado = 0;

	IF EXISTS (SELECT *
			   FROM Tarea
			   WHERE Tarea.IdentificadorUsuarioCreador = @IdentificadorUsuarioCreador AND Tarea.IdentificadorTarea = @IdentificadorTarea)
		BEGIN 
			DELETE Tarea
			WHERE Tarea.IdentificadorTarea = @IdentificadorTarea AND Tarea.IdentificadorUsuarioCreador = @IdentificadorUsuarioCreador;

			SET @BorradoCompletado = 1;
		END;
END;


DECLARE @Completado BIT;

EXEC BorrarTarea @IdentificadorUsuarioCreador = 'D156E9CE-405A-41E3-BBD7-A21FA5E5FE53', @IdentificadorTarea = '4EAE4A1B-7574-4DF1-9A8F-24504CAE5940', @BorradoCompletado = @Completado OUTPUT;

SELECT @Completado;

SELECT * FROM Tarea;

GO
CREATE PROCEDURE BorrarCategoria(
	@IdentificadorCategoria INT,
	@IdentificadorUsuarioCreador UNIQUEIDENTIFIER,
	@BorradoCompletado BIT OUTPUT
) AS
BEGIN  
	SET @BorradoCompletado = 0;

	IF EXISTS (SELECT *
			   FROM Categoria
			   WHERE Categoria.IdentificadorUsuarioCreador = @IdentificadorUsuarioCreador AND Categoria.IdentificadorCategoria = @IdentificadorCategoria)
		BEGIN 
			DELETE Categoria
			WHERE Categoria.IdentificadorCategoria = @IdentificadorCategoria AND Categoria.IdentificadorUsuarioCreador = @IdentificadorUsuarioCreador;

			SET @BorradoCompletado = 1;
		END;
END;


DECLARE @Completado BIT;

EXEC BorrarCategoria @IdentificadorUsuarioCreador = '54B7179A-C237-491C-842E-AA3ED2A70AE9', @IdentificadorCategoria = '12', @BorradoCompletado = @Completado OUTPUT;

SELECT @Completado;

SELECT * FROM Categoria;

GO
CREATE PROCEDURE BorrarEstado(
	@IdentificadorEstado INT,
	@IdentificadorUsuarioCreador UNIQUEIDENTIFIER,
	@BorradoCompletado BIT OUTPUT
) AS
BEGIN  
	SET @BorradoCompletado = 0;

	IF EXISTS (SELECT *
			   FROM Estado
			   WHERE Estado.IdentificadorUsuarioCreador = @IdentificadorUsuarioCreador AND Estado.IdentificadorEstado = @IdentificadorEstado)
		BEGIN 
			DELETE Estado
			WHERE Estado.IdentificadorEstado = @IdentificadorEstado AND Estado.IdentificadorUsuarioCreador = @IdentificadorUsuarioCreador;

			SET @BorradoCompletado = 1;
		END;
END;

DECLARE @Completado BIT;

EXEC BorrarEstado @IdentificadorUsuarioCreador = '54B7179A-C237-491C-842E-AA3ED2A70AE9', @IdentificadorEstado = '5', @BorradoCompletado = @Completado OUTPUT;

SELECT @Completado;

SELECT * FROM Estado;

GO
CREATE PROCEDURE BorrarUsuario(
	@IdentificadorUsuario UNIQUEIDENTIFIER,
	@BorradoCompletado BIT OUTPUT
) AS
BEGIN  
	SET @BorradoCompletado = 0;

	IF EXISTS (SELECT *
			   FROM Usuario
			   WHERE Usuario.IdentificadorUsuario = @IdentificadorUsuario)
		BEGIN 

			DELETE Tarea
			WHERE Tarea.IdentificadorUsuarioCreador = @IdentificadorUsuario;

			DELETE Categoria
			WHERE Categoria.IdentificadorUsuarioCreador = @IdentificadorUsuario;

			DELETE Estado
			WHERE Estado.IdentificadorUsuarioCreador = @IdentificadorUsuario;

			DELETE Usuario
			WHERE Usuario.IdentificadorUsuario = @IdentificadorUsuario;

			SET @BorradoCompletado = 1;
		END;
END;

DECLARE @Completado BIT;

EXEC BorrarUsuario @IdentificadorUsuario = 'FA4C6D77-3C5F-4632-BE66-C80636EEE7C2', @BorradoCompletado = @Completado OUTPUT;

SELECT @Completado;

SELECT * FROM Usuario;
SELECT * FROM Tarea;
SELECT * FROM Categoria;
SELECT * FROM Estado;


GO
CREATE PROCEDURE ObtenerTareas(
	@IdentificadorUsuario UNIQUEIDENTIFIER
)AS
BEGIN 
	IF EXISTS (SELECT * 
			  FROM Usuario
			  WHERE Usuario.IdentificadorUsuario = @IdentificadorUsuario)
		BEGIN
			SELECT * 
			FROM Tarea
			WHERE Tarea.IdentificadorUsuarioCreador = @IdentificadorUsuario;
		END;
END;


EXEC ObtenerTareas @IdentificadorUsuario = 'D156E9CE-405A-41E3-BBD7-A21FA5E5FE53'



GO
CREATE PROCEDURE ObtenerInformacionUsuario(
	@IdentificadorUsuario UNIQUEIDENTIFIER
)AS
BEGIN 
	IF EXISTS (SELECT * 
			  FROM Usuario
			  WHERE Usuario.IdentificadorUsuario = @IdentificadorUsuario)
		BEGIN
			SELECT * 
			FROM Usuario
			WHERE Usuario.IdentificadorUsuario = @IdentificadorUsuario;
		END;
END;


EXEC ObtenerInformacionUsuario @IdentificadorUsuario = 'D156E9CE-405A-41E3-BBD7-A21FA5E5FE53'

GO
CREATE PROCEDURE ObtenerEstadosUsuario(
	@IdentificadorUsuario UNIQUEIDENTIFIER
)AS
BEGIN 
	IF EXISTS (SELECT * 
			  FROM Usuario
			  WHERE Usuario.IdentificadorUsuario = @IdentificadorUsuario)
		BEGIN
			SELECT * 
			FROM Estado
			WHERE Estado.IdentificadorUsuarioCreador = @IdentificadorUsuario;
		END;
END;


EXEC ObtenerEstadosUsuario @IdentificadorUsuario = 'F547EF79-DB4A-4803-B74C-0BF7EC66D439'


GO
CREATE PROCEDURE ObtenerCategoriasUsuario(
	@IdentificadorUsuario UNIQUEIDENTIFIER
)AS
BEGIN 
	IF EXISTS (SELECT * 
			  FROM Usuario
			  WHERE Usuario.IdentificadorUsuario = @IdentificadorUsuario)
		BEGIN
			SELECT * 
			FROM Categoria
			WHERE Categoria.IdentificadorUsuarioCreador = @IdentificadorUsuario;
		END;
END;


EXEC ObtenerCategoriasUsuario @IdentificadorUsuario = 'F547EF79-DB4A-4803-B74C-0BF7EC66D439'


