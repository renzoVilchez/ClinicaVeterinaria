CREATE PROCEDURE sp_ActualizarChatBot
    @idPregunta INT,
    @pregunta NVARCHAR(200),
	@respuesta NVARCHAR(500)
AS
BEGIN
    SET NOCOUNT ON;

    UPDATE ChatBot
    SET pregunta = @pregunta,
        respuesta = @respuesta
    WHERE idPregunta = @idPregunta;

    SELECT @@ROWCOUNT AS RowsAffected;
END
GO
