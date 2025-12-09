CREATE PROCEDURE sp_ObtenerVeterinarioPorId
    @idVeterinario INT
AS
BEGIN
    SET NOCOUNT ON;

    SELECT 
        idVeterinario,
        idUsuario,
        especialidad,
        estado
    FROM Veterinarios
    WHERE idVeterinario = @idVeterinario;
END
GO