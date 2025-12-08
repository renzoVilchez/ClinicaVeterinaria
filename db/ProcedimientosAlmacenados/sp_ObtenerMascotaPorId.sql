CREATE PROCEDURE sp_ObtenerMascotaPorId
    @idMascota INT
AS
BEGIN
    SET NOCOUNT ON;

    SELECT idMascota, idCliente, idEspecie, nombre, raza, edad, peso, sexo, estado
    FROM Mascotas
    WHERE idMascota = @idMascota;
END
GO
