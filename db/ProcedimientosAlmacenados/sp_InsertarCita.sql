CREATE PROCEDURE sp_InsertarCita
    @idMascota INT,
    @idServicio INT,
    @idVeterinario INT,
    @fechaCita DATE,
    @horaCita TIME,
    @estado NVARCHAR(30)
AS
BEGIN
    SET NOCOUNT ON;

    INSERT INTO Citas(idMascota, idServicio, idVeterinario, fechaCita, horaCita, estado)
    VALUES(@idMascota, @idServicio, @idVeterinario, @fechaCita, @horaCita, @estado);

    SELECT CAST(SCOPE_IDENTITY() AS INT) AS NewId;
END
GO
