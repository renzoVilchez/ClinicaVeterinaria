CREATE PROCEDURE sp_EliminarUsuario
    @idUsuario INT
AS
BEGIN
    SET NOCOUNT ON;

    DELETE FROM Usuarios WHERE idUsuario = @idUsuario;

    SELECT @@ROWCOUNT AS RowsAffected;
END
GO
