CREATE PROCEDURE dbo.sp_RegistrarVeterinarioCompleto
(
    @nombreUsuario NVARCHAR(100),
    @contrasena NVARCHAR(200),
    @nombres NVARCHAR(100),
    @apellidoPaterno NVARCHAR(100),
    @apellidoMaterno NVARCHAR(100),
    @tipoDocumento NVARCHAR(20),
    @numeroDocumento NVARCHAR(20),
    @celular NVARCHAR(20) = NULL,
    @direccion NVARCHAR(200) = NULL,
    @especialidad NVARCHAR(100) = NULL,
    @estado BIT = 1
)
AS
BEGIN
    SET NOCOUNT ON;

    BEGIN TRY
        BEGIN TRANSACTION;

        -- Inserta usuario (rol será 'Veterinario')
        INSERT INTO Usuarios (nombreUsuario, contrasena, rol, estado)
        VALUES (@nombreUsuario, @contrasena, 'Veterinario', 1);

        DECLARE @idUsuario INT = SCOPE_IDENTITY();

        -- Inserta veterinario
        INSERT INTO Veterinarios (
            idUsuario, nombres, apellidoPaterno, apellidoMaterno,
            tipoDocumento, numeroDocumento, celular, direccion,
            especialidad, estado
        )
        VALUES (
            @idUsuario, @nombres, @apellidoPaterno, @apellidoMaterno,
            @tipoDocumento, @numeroDocumento, @celular, @direccion,
            @especialidad, @estado
        );

        COMMIT TRANSACTION;

        SELECT @idUsuario AS idUsuarioCreado;
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION;
        DECLARE @ErrMsg NVARCHAR(4000) = ERROR_MESSAGE();
        RAISERROR(@ErrMsg, 16, 1);
    END CATCH
END
GO
