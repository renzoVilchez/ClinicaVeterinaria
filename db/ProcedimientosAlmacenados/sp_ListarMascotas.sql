CREATE PROCEDURE sp_ListarMascotas
AS
BEGIN
    SET NOCOUNT ON;

    SELECT idMascota, idCliente, idEspecie, nombre, raza, edad, peso, sexo, estado
    FROM Mascotas
    ORDER BY idMascota;
END
GO
