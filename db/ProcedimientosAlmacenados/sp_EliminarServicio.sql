CREATE PROCEDURE sp_EliminarServicio
    @idServicio INT
AS
BEGIN
    SET NOCOUNT ON;

    DELETE FROM Servicios WHERE idServicio = @idServicio;

    SELECT @@ROWCOUNT AS RowsAffected;
END
GO
