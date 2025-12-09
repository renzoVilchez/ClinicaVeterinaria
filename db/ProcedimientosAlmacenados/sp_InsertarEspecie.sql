CREATE PROCEDURE sp_InsertarEspecie
    @nombreEspecie NVARCHAR(50)
AS
BEGIN
    SET NOCOUNT ON;

    INSERT INTO Especies(nombreEspecie)
    VALUES(@nombreEspecie);

    SELECT CAST(SCOPE_IDENTITY() AS INT) AS NewId;
END
GO