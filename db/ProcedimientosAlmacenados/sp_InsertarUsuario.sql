CREATE PROCEDURE sp_InsertarUsuario
    @nombreUsuario NVARCHAR(100),
    @contrasena NVARCHAR(100),
    @rol NVARCHAR(30)
AS
BEGIN
    SET NOCOUNT ON;

    INSERT INTO Usuarios(nombreUsuario, contrasena, rol)
    VALUES(@nombreUsuario, @contrasena, @rol);

    SELECT CAST(SCOPE_IDENTITY() AS INT) AS NewId;
END
GO
