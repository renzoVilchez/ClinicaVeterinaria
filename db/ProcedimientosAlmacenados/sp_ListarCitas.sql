CREATE PROCEDURE sp_ListarCitas
AS
BEGIN
    SET NOCOUNT ON;

    SELECT 
        C.idCita,
        C.fechaCita,
        C.horaCita,
        C.estado,
        C.fechaRegistro,

        M.idMascota,
        M.nombre AS NombreMascota,

        S.idServicio,
        S.nombreServicio,

        V.idVeterinario,
        V.especialidad
    FROM Citas C
    INNER JOIN Mascotas M ON C.idMascota = M.idMascota
    INNER JOIN Servicios S ON C.idServicio = S.idServicio
    INNER JOIN Veterinarios V ON C.idVeterinario = V.idVeterinario
    ORDER BY C.idCita;
END
GO
