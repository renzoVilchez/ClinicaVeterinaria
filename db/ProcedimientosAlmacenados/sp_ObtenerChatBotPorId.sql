CREATE PROCEDURE sp_ObtenerChatBotPorId
    @idPregunta INT
AS
BEGIN
    SET NOCOUNT ON;

    SELECT idPregunta, pregunta, respuesta
    FROM ChatBot WHERE idPregunta = @idPregunta;
END
GO
