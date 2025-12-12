ALTER PROCEDURE sp_InsertarCita
    @idMascota INT,
    @idServicio INT,
    @idVeterinario INT,
    @fechaCita DATE,
    @horaCita TIME,
    @estado NVARCHAR(30)
AS
BEGIN
    SET NOCOUNT ON;

    INSERT INTO Citas(
        idMascota, idServicio, idVeterinario,
        fechaCita, horaCita, estado, fechaRegistro
    )
    VALUES(
        @idMascota, @idServicio, @idVeterinario,
        @fechaCita, @horaCita, @estado, GETDATE()
    );

    SELECT SCOPE_IDENTITY() AS idCitaCreada;
END
GO