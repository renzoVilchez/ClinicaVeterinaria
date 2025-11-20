CREATE PROCEDURE sp_ObtenerUsuarioPorId
    @idUsuario INT
AS
BEGIN
    SET NOCOUNT ON;

    SELECT idUsuario, nombreUsuario, contrasena, rol, estado
    FROM Usuarios WHERE idUsuario = @idUsuario;
END
GO
