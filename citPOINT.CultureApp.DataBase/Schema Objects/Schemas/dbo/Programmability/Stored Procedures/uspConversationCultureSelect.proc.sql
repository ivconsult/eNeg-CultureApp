CREATE PROCEDURE [dbo].[uspConversationCultureSelect]
	@ConversationCultureID UNIQUEIDENTIFIER
AS 
	SET NOCOUNT ON 
	SET XACT_ABORT ON  

	BEGIN TRAN

	SELECT [ConversationCultureID], [ConversationID], [PartnerCultureID], [Deleted], [DeletedBy], [DeletedOn] 
	FROM   [dbo].[ConversationCulture] 
	WHERE  ([ConversationCultureID] = @ConversationCultureID OR @ConversationCultureID IS NULL) 

	COMMIT