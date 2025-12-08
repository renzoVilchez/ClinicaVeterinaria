CREATE PROCEDURE sp_ObtenerServicioPorId
    @idServicio INT
AS
BEGIN
    SET NOCOUNT ON;

    SELECT idServicio, nombreServicio, descripcion, precio
    FROM Servicios WHERE idServicio = @idServicio;
END
GO
