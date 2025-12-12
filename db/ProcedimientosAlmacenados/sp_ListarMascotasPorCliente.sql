CREATE PROCEDURE sp_ListarMascotasPorCliente
 @idCliente INT
AS
BEGIN
    SELECT idMascota, nombre
    FROM Mascotas
    WHERE idCliente = @idCliente
END