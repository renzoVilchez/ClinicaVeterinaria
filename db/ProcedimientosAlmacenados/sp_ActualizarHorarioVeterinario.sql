CREATE PROCEDURE sp_ActualizarHorarioVeterinario
    @idHorario INT,
    @idVeterinario INT,
    @diaSemana NVARCHAR(15),
    @horaInicio TIME,
    @horaFin TIME,
    @disponible BIT
AS
BEGIN
    SET NOCOUNT ON;

    UPDATE HorariosVeterinarios
    SET 
        idVeterinario = @idVeterinario,
        diaSemana = @diaSemana,
        horaInicio = @horaInicio,
        horaFin = @horaFin,
        disponible = @disponible
    WHERE idHorario = @idHorario;

    SELECT @@ROWCOUNT AS RowsAffected;
END
GO