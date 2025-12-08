CREATE PROCEDURE sp_InsertarChatBot	    	 
	@pregunta NVARCHAR(200),
	@respuesta NVARCHAR(500)
AS
BEGIN
	SET NOCOUNT ON;

	INSERT INTO ChatBot(pregunta,respuesta)		
	VALUES(@pregunta,@respuesta);

	SELECT CAST(SCOPE_IDENTITY() AS INT) AS NewId;

END
GO

