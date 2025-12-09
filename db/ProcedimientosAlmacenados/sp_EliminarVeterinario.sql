CREATE PROCEDURE sp_EliminarVeterinario
    @idVeterinario INT
AS
BEGIN
    SET NOCOUNT ON;

    DELETE FROM Veterinarios WHERE idVeterinario = @idVeterinario;

    SELECT @@ROWCOUNT AS RowsAffected;
END
GO