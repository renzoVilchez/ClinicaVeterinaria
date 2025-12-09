CREATE PROCEDURE sp_ListarVeterinarios
AS
BEGIN
    SET NOCOUNT ON;

    SELECT 
        idVeterinario,
        idUsuario,
        especialidad,
        estado
    FROM Veterinarios
    ORDER BY idVeterinario;
END
GO