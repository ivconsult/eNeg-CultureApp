CREATE PROCEDURE [dbo].[uspConversationCultureDelete]
	@ConversationCultureID uniqueidentifier
AS 
	SET NOCOUNT ON 
	SET XACT_ABORT ON  
	
	BEGIN TRAN
	
	UPDATE [dbo].[ConversationCulture]
	SET    [Deleted] = 1, [DeletedOn] = GETDATE()
	WHERE  [ConversationCultureID] = @ConversationCultureID

	COMMIT