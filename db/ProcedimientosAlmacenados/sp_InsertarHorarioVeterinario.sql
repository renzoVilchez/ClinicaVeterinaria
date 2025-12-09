CREATE PROCEDURE sp_InsertarHorarioVeterinario
    @idVeterinario INT,
    @diaSemana NVARCHAR(15),
    @horaInicio TIME,
    @horaFin TIME,
    @disponible BIT
AS
BEGIN
    SET NOCOUNT ON;

    INSERT INTO HorariosVeterinarios(idVeterinario, diaSemana, horaInicio, horaFin, disponible)
    VALUES(@idVeterinario, @diaSemana, @horaInicio, @horaFin, @disponible);

    SELECT CAST(SCOPE_IDENTITY() AS INT) AS NewId;
END
GO