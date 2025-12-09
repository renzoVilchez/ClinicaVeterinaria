CREATE PROCEDURE sp_InsertarVeterinario
    @idUsuario INT,
    @especialidad NVARCHAR(100)
AS
BEGIN
    SET NOCOUNT ON;

    INSERT INTO Veterinarios(idUsuario, especialidad)
    VALUES(@idUsuario, @especialidad);

    SELECT CAST(SCOPE_IDENTITY() AS INT) AS NewId;
END
GO