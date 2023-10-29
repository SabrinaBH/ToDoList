drop function ObtenerTareasUsuario;


CREATE PROCEDURE Obtener


CREATE FUNCTION ObtenerTareasUsuario
(
    @IdentificadorUsuario INT
)
RETURNS TABLE
AS
RETURN
(
    SELECT *
    FROM Tarea 
    WHERE Tarea.IdentificadorUsuarioCreador = @IdentificadorUsuario 
)

CREATE FUNCTION ObtenerEstados 
(
	@identificadorUsuario INT
)
RETURNS TABLE
AS 
RETURN
(
	SELECT * 
	FROM Estado
	WHERE Estado.IdentificadorUsuarioCreador = @identificadorUsuario
)

CREATE FUNCTION ObtenerCategorias 
(
	@identificadorUsuario INT
)
RETURNS TABLE
AS 
RETURN
(
	SELECT * 
	FROM Categoria
	WHERE Categoria.IdentificadorUsuarioCreador = @identificadorUsuario
)

CREATE FUNCTION ObtenerUsuario 
(
	@identificadorUsuario INT
)
RETURNS TABLE
AS 
RETURN
(
	SELECT * 
	FROM Usuario
	WHERE Usuario.IdentificadorUsuario = @identificadorUsuario
)

SELECT * FROM dbo.ObtenerCategorias('1')
SELECT * FROM dbo.ObtenerEstados('1')
SELECT * FROM dbo.ObtenerTareasUsuario('2')
SELECT * FROM dbo.ObtenerUsuario('2')