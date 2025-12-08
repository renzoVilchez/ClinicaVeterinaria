CREATE PROCEDURE sp_ListarServicios
AS
BEGIN
    SET NOCOUNT ON;

    SELECT idServicio, nombreServicio, descripcion, precio
    FROM Servicios ORDER BY idServicio;
END
GO

