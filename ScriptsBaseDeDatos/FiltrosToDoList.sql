use ProyectoToDoList

GO 
CREATE PROCEDURE FiltroPorDificultad (
	@IdentificadorUsuario UNIQUEIDENTIFIER,
	@NivelDificultad SMALLINT
) AS
BEGIN
	IF EXISTS (SELECT * 
			  FROM Usuario
			  WHERE Usuario.IdentificadorUsuario = @IdentificadorUsuario)
		BEGIN
			SELECT * 
			FROM Tarea
			WHERE Tarea.IdentificadorUsuarioCreador = @IdentificadorUsuario AND Tarea.Dificultad = @NivelDificultad;
		END;
END;

EXEC FiltroPorDificultad @IdentificadorUsuario = 'D156E9CE-405A-41E3-BBD7-A21FA5E5FE53', @NivelDificultad = 5

select * from Usuario
select * from Tarea


GO 
CREATE PROCEDURE FiltroPorCategoria (
	@IdentificadorUsuario UNIQUEIDENTIFIER,
	@IdentificadorCategoria INT
) AS
BEGIN
	IF EXISTS (SELECT * 
			  FROM Usuario
			  WHERE Usuario.IdentificadorUsuario = @IdentificadorUsuario)
		BEGIN
			IF EXISTS (SELECT * 
				FROM Categoria
				WHERE Categoria.IdentificadorCategoria = @IdentificadorCategoria)
			BEGIN
				SELECT * 
				FROM Tarea
				WHERE Tarea.IdentificadorUsuarioCreador = @IdentificadorUsuario AND Tarea.IdentificadorCategoria = @IdentificadorCategoria;
			END;
		END;
END;


EXEC FiltroPorCategoria @IdentificadorUsuario = '1C7F47AA-7A43-4A80-BC85-BF91621A7E65', @IdentificadorCategoria = 1

select * from Usuario
select * from Tarea
select * from Categoria

GO
CREATE PROCEDURE FiltroPorCategoriaDificultad(
	@IdentificadorUsuario UNIQUEIDENTIFIER,
	@IdentificadorCategoria INT,
	@NivelDificultad SMALLINT
) AS
BEGIN
	IF EXISTS (SELECT * 
			  FROM Usuario
			  WHERE Usuario.IdentificadorUsuario = @IdentificadorUsuario)
		BEGIN
			IF EXISTS (SELECT * 
				FROM Categoria
				WHERE Categoria.IdentificadorCategoria = @IdentificadorCategoria)
			BEGIN
				SELECT * 
				FROM Tarea
				WHERE Tarea.IdentificadorUsuarioCreador = @IdentificadorUsuario AND Tarea.IdentificadorCategoria = @IdentificadorCategoria AND Tarea.Dificultad = @NivelDificultad;
			END;
		END;
END;


EXEC FiltroPorCategoriaDificultad @IdentificadorUsuario = '1C7F47AA-7A43-4A80-BC85-BF91621A7E65', @IdentificadorCategoria = 1, @NivelDificultad = 4;

select * from Usuario
select * from Tarea