CREATE PROCEDURE sp_EliminarHorarioVeterinario
    @idHorario INT
AS
BEGIN
    SET NOCOUNT ON;

    DELETE FROM HorariosVeterinarios 
    WHERE idHorario = @idHorario;

    SELECT @@ROWCOUNT AS RowsAffected;
END
GO