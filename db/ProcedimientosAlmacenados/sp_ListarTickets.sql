CREATE PROCEDURE sp_ListarTickets
AS
BEGIN
    SET NOCOUNT ON;

    SELECT 
        idTicket,
        idCita,
        codigoTicket,
        fechaEmision,
        montoTotal,
        metodoPago,
        estado,
        fechaPago,
        observaciones
    FROM Tickets
    ORDER BY idTicket;
END
GO