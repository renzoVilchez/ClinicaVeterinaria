ALTER PROCEDURE sp_ActualizarCita
    @idCita INT,
    @idMascota INT,
    @idServicio INT,
    @idVeterinario INT,
    @fechaCita DATE,
    @horaCita TIME,
    @estado NVARCHAR(30)
AS
BEGIN
    SET NOCOUNT ON;

    UPDATE Citas
    SET idMascota = @idMascota,
        idServicio = @idServicio,
        idVeterinario = @idVeterinario,
        fechaCita = @fechaCita,
        horaCita = @horaCita,
        estado = @estado
    WHERE idCita = @idCita;

    SELECT @@ROWCOUNT AS RowsAffected;
END
GO