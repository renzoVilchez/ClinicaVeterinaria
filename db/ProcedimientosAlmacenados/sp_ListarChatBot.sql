CREATE PROCEDURE sp_ListarChatBot
AS
BEGIN
    SET NOCOUNT ON;

    SELECT idPregunta, pregunta, respuesta
    FROM ChatBot ORDER BY idPregunta;
END
GO
