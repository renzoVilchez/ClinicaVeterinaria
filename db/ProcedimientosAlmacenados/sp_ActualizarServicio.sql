CREATE PROCEDURE sp_ActualizarServicio
    @idServicio INT,
    @nombreServicio NVARCHAR(100),
    @descripcion NVARCHAR(250),
    @precio DECIMAL(10,2)
AS
BEGIN
    SET NOCOUNT ON;

    UPDATE Servicios
    SET nombreServicio = @nombreServicio,
        descripcion = @descripcion,
		precio = @precio
    WHERE idServicio = @idServicio;

    SELECT @@ROWCOUNT AS RowsAffected;
END
GO
