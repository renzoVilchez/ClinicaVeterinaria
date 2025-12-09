CREATE PROCEDURE sp_ActualizarTicket
    @idTicket INT,
    @idCita INT,
    @montoTotal DECIMAL(10,2),
    @metodoPago NVARCHAR(20),
    @estado NVARCHAR(20),
    @fechaPago DATETIME = NULL,
    @observaciones NVARCHAR(200) = NULL
AS
BEGIN
    SET NOCOUNT ON;

    UPDATE Tickets
    SET 
        idCita = @idCita,
        montoTotal = @montoTotal,
        metodoPago = @metodoPago,
        estado = @estado,
        fechaPago = @fechaPago,
        observaciones = @observaciones
    WHERE idTicket = @idTicket;

    SELECT @@ROWCOUNT AS RowsAffected;
END
GO