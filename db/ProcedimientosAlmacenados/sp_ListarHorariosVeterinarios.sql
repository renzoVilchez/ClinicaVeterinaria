CREATE PROCEDURE sp_ListarHorariosVeterinarios
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
    ORDER BY idHorario;
END
GO