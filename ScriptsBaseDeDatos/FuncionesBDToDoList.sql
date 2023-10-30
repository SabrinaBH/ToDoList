drop function ObtenerTareasUsuario;
use ProyectoToDoList

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


CREATE PROCEDURE ObtenerIDUsuario(
	@EmailUsuario VARCHAR(100),
	@IDUsuario UNIQUEIDENTIFIER OUTPUT
) AS 
BEGIN 
	SELECT @IDUsuario = IdentificadorUsuario
	FROM Usuario
	WHERE Usuario.Email = @EmailUsuario;
END;


DECLARE @Usuario UNIQUEIDENTIFIER;

EXEC ObtenerIDUsuario @EmailUsuario = 'sabry.brenes@outlook.es', @IDUsuario = @Usuario output;

select @Usuario;
