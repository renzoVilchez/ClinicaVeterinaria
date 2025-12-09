CREATE PROCEDURE sp_ListarClientes
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
    ORDER BY idCliente;
END
GO