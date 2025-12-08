CREATE PROCEDURE sp_InsertarMascota
    @idCliente INT,
    @idEspecie INT,
    @nombre NVARCHAR(100),
    @raza NVARCHAR(100),
    @edad INT,
    @peso DECIMAL(5,2),
    @sexo NVARCHAR(10)
AS
BEGIN
    SET NOCOUNT ON;

    INSERT INTO Mascotas(idCliente, idEspecie, nombre, raza, edad, peso, sexo)
    VALUES(@idCliente, @idEspecie, @nombre, @raza, @edad, @peso, @sexo);

    SELECT CAST(SCOPE_IDENTITY() AS INT) AS NewId;
END
GO
