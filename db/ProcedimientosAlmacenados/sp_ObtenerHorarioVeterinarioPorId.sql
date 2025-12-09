CREATE PROCEDURE sp_ObtenerHorarioVeterinarioPorId
    @idHorario INT
AS
BEGIN
    SET NOCOUNT ON;

    SELECT 
        idHorario,
        idVeterinario,
        diaSemana,
        horaInicio,
        horaFin,
        disponible
    FROM HorariosVeterinarios
    WHERE idHorario = @idHorario;
END
GO