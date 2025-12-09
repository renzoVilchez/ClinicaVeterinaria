CREATE PROCEDURE sp_InsertarTicket
    @idCita INT,
    @montoTotal DECIMAL(10,2),
    @metodoPago NVARCHAR(20),
    @estado NVARCHAR(20),
    @fechaPago DATETIME = NULL,
    @observaciones NVARCHAR(200) = NULL
AS
BEGIN
    SET NOCOUNT ON;

    INSERT INTO Tickets(idCita, montoTotal, metodoPago, estado, fechaPago, observaciones)
    VALUES(@idCita, @montoTotal, @metodoPago, @estado, @fechaPago, @observaciones);

    SELECT CAST(SCOPE_IDENTITY() AS INT) AS NewId;
END
GO