CREATE PROCEDURE sp_InsertarCliente
    @idUsuario INT,
    @nombres NVARCHAR(100),
    @apellidoPaterno NVARCHAR(100),
    @apellidoMaterno NVARCHAR(100),
    @tipoDocumento NVARCHAR(20),
    @numeroDocumento NVARCHAR(20),
    @celular NVARCHAR(20),
    @direccion NVARCHAR(150)
AS
BEGIN
    SET NOCOUNT ON;

    INSERT INTO Clientes(
        idUsuario, nombres, apellidoPaterno, apellidoMaterno, 
        tipoDocumento, numeroDocumento, celular, direccion
    )
    VALUES(
        @idUsuario, @nombres, @apellidoPaterno, @apellidoMaterno,
        @tipoDocumento, @numeroDocumento, @celular, @direccion
    );

    SELECT CAST(SCOPE_IDENTITY() AS INT) AS NewId;
END
GO