CREATE PROCEDURE sp_ActualizarCliente
    @idCliente INT,
    @idUsuario INT,
    @nombres NVARCHAR(100),
    @apellidoPaterno NVARCHAR(100),
    @apellidoMaterno NVARCHAR(100),
    @tipoDocumento NVARCHAR(20),
    @numeroDocumento NVARCHAR(20),
    @celular NVARCHAR(20),
    @direccion NVARCHAR(150),
    @estado BIT
AS
BEGIN
    SET NOCOUNT ON;

    UPDATE Clientes
    SET 
        idUsuario = @idUsuario,
        nombres = @nombres,
        apellidoPaterno = @apellidoPaterno,
        apellidoMaterno = @apellidoMaterno,
        tipoDocumento = @tipoDocumento,
        numeroDocumento = @numeroDocumento,
        celular = @celular,
        direccion = @direccion,
        estado = @estado
    WHERE idCliente = @idCliente;

    SELECT @@ROWCOUNT AS RowsAffected;
END
GO