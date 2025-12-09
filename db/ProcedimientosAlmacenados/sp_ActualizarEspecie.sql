CREATE PROCEDURE sp_ActualizarEspecie
    @idEspecie INT,
    @nombreEspecie NVARCHAR(50)
AS
BEGIN
    SET NOCOUNT ON;

    UPDATE Especies
    SET nombreEspecie = @nombreEspecie
    WHERE idEspecie = @idEspecie;

    SELECT @@ROWCOUNT AS RowsAffected;
END
GO