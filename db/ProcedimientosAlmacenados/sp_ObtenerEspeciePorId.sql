CREATE PROCEDURE sp_ObtenerEspeciePorId
    @idEspecie INT
AS
BEGIN
    SET NOCOUNT ON;

    SELECT idEspecie, nombreEspecie
    FROM Especies
    WHERE idEspecie = @idEspecie;
END
GO