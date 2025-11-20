CREATE PROCEDURE sp_Login
    @usuario NVARCHAR(100),
    @contrasena NVARCHAR(100)
AS
BEGIN
    SET NOCOUNT ON;

    SELECT idUsuario, nombreUsuario, rol
    FROM Usuarios
    WHERE nombreUsuario = @usuario
      AND contrasena = @contrasena
      AND estado = 1;
END
GO
