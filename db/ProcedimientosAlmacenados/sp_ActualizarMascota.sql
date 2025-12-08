CREATE PROCEDURE sp_ActualizarMascota
    @idMascota INT,
    @idCliente INT,
    @idEspecie INT,
    @nombre NVARCHAR(100),
    @raza NVARCHAR(100),
    @edad INT,
    @peso DECIMAL(5,2),
    @sexo NVARCHAR(10),
    @estado BIT
AS
BEGIN
    SET NOCOUNT ON;

    UPDATE Mascotas
    SET idCliente = @idCliente,
        idEspecie = @idEspecie,
        nombre = @nombre,
        raza = @raza,
        edad = @edad,
        peso = @peso,
        sexo = @sexo,
        estado = @estado
    WHERE idMascota = @idMascota;

    SELECT @@ROWCOUNT AS RowsAffected;
END
GO
