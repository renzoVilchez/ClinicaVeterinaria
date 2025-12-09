CREATE PROCEDURE sp_EliminarTicket
    @idTicket INT
AS
BEGIN
    SET NOCOUNT ON;

    DELETE FROM Tickets 
    WHERE idTicket = @idTicket;

    SELECT @@ROWCOUNT AS RowsAffected;
END
GO