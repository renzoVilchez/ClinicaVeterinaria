CREATE PROCEDURE sp_ObtenerTicketPorId
    @idTicket INT
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
    WHERE idTicket = @idTicket;
END
GO