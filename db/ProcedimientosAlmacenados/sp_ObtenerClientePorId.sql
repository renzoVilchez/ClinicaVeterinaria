CREATE PROCEDURE sp_ObtenerClientePorId
    @idCliente INT
AS
BEGIN
    SET NOCOUNT ON;

    SELECT 
        idCliente,
        idUsuario,
        nombres,
        apellidoPaterno,
        apellidoMaterno,
        tipoDocumento,
        numeroDocumento,
        celular,
        direccion,
        estado
    FROM Clientes
    WHERE idCliente = @idCliente;
END
GO