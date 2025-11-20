CREATE PROCEDURE sp_ActualizarUsuario
    @idUsuario INT,
    @nombreUsuario NVARCHAR(100),
    @contrasena NVARCHAR(100),
    @rol NVARCHAR(30),
    @estado BIT
AS
BEGIN
    SET NOCOUNT ON;

    UPDATE Usuarios
    SET nombreUsuario = @nombreUsuario,
        contrasena = @contrasena,
        rol = @rol,
        estado = @estado
    WHERE idUsuario = @idUsuario;

    SELECT @@ROWCOUNT AS RowsAffected;
END
GO
