CREATE PROCEDURE sp_EliminarCliente
    @idCliente INT
AS
BEGIN
    SET NOCOUNT ON;

    DELETE FROM Clientes WHERE idCliente = @idCliente;

    SELECT @@ROWCOUNT AS RowsAffected;
END
GO