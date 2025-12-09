CREATE PROCEDURE sp_ListarEspecies
AS
BEGIN
    SET NOCOUNT ON;

    SELECT idEspecie, nombreEspecie
    FROM Especies
    ORDER BY idEspecie;
END
GO