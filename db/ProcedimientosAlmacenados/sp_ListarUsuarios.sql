CREATE PROCEDURE sp_ListarUsuarios
AS
BEGIN
    SET NOCOUNT ON;

    SELECT idUsuario, nombreUsuario, contrasena, rol, estado
    FROM Usuarios ORDER BY idUsuario;
END
GO
