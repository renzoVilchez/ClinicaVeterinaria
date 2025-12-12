ALTER PROCEDURE sp_HorariosDisponibles
    @idVeterinario INT,
    @fecha DATE
AS
BEGIN
    SET NOCOUNT ON;

    DECLARE @dia NVARCHAR(20);
    SET @dia = DATENAME(WEEKDAY, @fecha);

    -- Normalizar al español si tu SQL está en inglés
    SET @dia = CASE @dia
        WHEN 'Monday' THEN 'Lunes'
        WHEN 'Tuesday' THEN 'Martes'
        WHEN 'Wednesday' THEN 'Miercoles'
        WHEN 'Thursday' THEN 'Jueves'
        WHEN 'Friday' THEN 'Viernes'
        WHEN 'Saturday' THEN 'Sabado'
        WHEN 'Sunday' THEN 'Domingo'
        ELSE @dia
    END;

    -- Obtener horario del día
    DECLARE @horaInicio TIME;
    DECLARE @horaFin TIME;

    SELECT @horaInicio = horaInicio,
           @horaFin = horaFin
    FROM HorariosVeterinarios
    WHERE idVeterinario = @idVeterinario
      AND diaSemana = @dia
      AND disponible = 1;

    -- Si no trabaja este día
    IF @horaInicio IS NULL
    BEGIN
        SELECT 'SIN_HORARIO' AS hora;
        RETURN;
    END;

    -- Generar horas disponibles cada 1 hora
    DECLARE @hora TIME = @horaInicio;

    WHILE @hora <= @horaFin
    BEGIN
        SELECT CONVERT(VARCHAR(5), @hora, 108) AS hora;
        SET @hora = DATEADD(HOUR, 1, @hora);
    END;
END;