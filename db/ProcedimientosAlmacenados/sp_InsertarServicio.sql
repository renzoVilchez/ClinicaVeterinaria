CREATE PROCEDURE sp_InsertarServicio
    @nombreServicio NVARCHAR(100),
    @descripcion NVARCHAR(250),
    @precio DECIMAL(10,2)
AS
BEGIN
    SET NOCOUNT ON;

    INSERT INTO Servicios(nombreServicio, descripcion, precio)
    VALUES(@nombreServicio,@descripcion,@precio);

    SELECT CAST(SCOPE_IDENTITY() AS INT) AS NewId;
END
GO