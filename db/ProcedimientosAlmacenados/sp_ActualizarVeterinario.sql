CREATE PROCEDURE sp_ActualizarVeterinario
    @idVeterinario INT,
    @idUsuario INT,
    @especialidad NVARCHAR(100),
    @estado BIT
AS
BEGIN
    SET NOCOUNT ON;

    UPDATE Veterinarios
    SET 
        idUsuario = @idUsuario,
        especialidad = @especialidad,
        estado = @estado
    WHERE idVeterinario = @idVeterinario;

    SELECT @@ROWCOUNT AS RowsAffected;
END
GO