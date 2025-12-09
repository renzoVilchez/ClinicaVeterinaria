CREATE PROCEDURE sp_EliminarEspecie
    @idEspecie INT
AS
BEGIN
    SET NOCOUNT ON;

    DELETE FROM Especies WHERE idEspecie = @idEspecie;

    SELECT @@ROWCOUNT AS RowsAffected;
END
GO