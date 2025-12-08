CREATE PROCEDURE sp_EliminarMascota
    @idMascota INT
AS
BEGIN
    SET NOCOUNT ON;

    DELETE FROM Mascotas WHERE idMascota = @idMascota;

    SELECT @@ROWCOUNT AS RowsAffected;
END
GO
