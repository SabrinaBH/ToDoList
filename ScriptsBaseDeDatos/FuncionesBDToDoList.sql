
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

EXEC InsertarNuevoUsuario @NombreNuevo = 'Sabrina', @PrimerApellidoNuevo = 'Brenes', @SegundoApellidoNuevo = 'Hernandez', @EmailNuevo = 'sab@gmail.com', @EsUsuarioDeJuego = 0,  @InsertCompletado = @Completado output;

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

EXEC InsertarNuevaCategoria @NombreNuevo = 'Juegos', @IdentificadorCreador = '54B7179A-C237-491C-842E-AA3ED2A70AE9', @InsertCompletado = @Completado output;

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

EXEC InsertarNuevoEstado @NombreNuevo = 'Casi Completados', @IdentificadorCreador = '54B7179A-C237-491C-842E-AA3ED2A70AE9', @InsertCompletado = @Completado output;

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
	@Dificultad = '5', @Prioridad = '3', @IdentificadorCreador = '54B7179A-C237-491C-842E-AA3ED2A70AE9', @IdentificadorCategoria = '3', @IdentificadorEstado = '2', @InsertCompletado = @Completado OUTPUT;

SELECT @Completado;


SELECT * FROM Tarea;

DELETE Tarea 
WHERE Tarea.IdentificadorUsuarioCreador = '54B7179A-C237-491C-842E-AA3ED2A70AE9';