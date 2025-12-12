ALTER PROCEDURE sp_ListarCitas
AS
BEGIN
    SET NOCOUNT ON;

    SELECT 
        C.idCita,

        CL.idCliente,
        CL.nombres + ' ' + CL.apellidoPaterno + ' ' + CL.apellidoMaterno AS clienteNombre,

        M.idMascota,
        M.nombre AS mascotaNombre,

        S.idServicio,
        S.nombreServicio AS servicioNombre,

        V.idVeterinario,
        V.nombres + ' ' + V.apellidoPaterno + ' ' + V.apellidoMaterno AS veterinarioNombre,

        C.fechaCita,
        C.horaCita,
        C.estado,
        C.fechaRegistro
    FROM Citas C
    INNER JOIN Mascotas M ON C.idMascota = M.idMascota
    INNER JOIN Clientes CL ON M.idCliente = CL.idCliente
    INNER JOIN Servicios S ON C.idServicio = S.idServicio
    INNER JOIN Veterinarios V ON C.idVeterinario = V.idVeterinario
    ORDER BY C.idCita DESC;
END
GO