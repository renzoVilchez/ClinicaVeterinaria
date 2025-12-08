CREATE PROCEDURE sp_EliminarCita
    @idCita INT
AS
BEGIN
    SET NOCOUNT ON;

    DELETE FROM Citas WHERE idCita = @idCita;

    SELECT @@ROWCOUNT AS RowsAffected;
END
GO
