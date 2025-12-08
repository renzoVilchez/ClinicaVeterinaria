CREATE PROCEDURE sp_EliminarChatBot
    @idPregunta INT
AS
BEGIN
    SET NOCOUNT ON;

    DELETE FROM ChatBot WHERE idPregunta = @idPregunta;

    SELECT @@ROWCOUNT AS RowsAffected;
END
GO
