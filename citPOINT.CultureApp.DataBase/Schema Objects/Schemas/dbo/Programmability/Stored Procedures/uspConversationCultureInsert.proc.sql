CREATE PROCEDURE [dbo].[uspConversationCultureInsert]
	@ConversationCultureID uniqueidentifier,
    @ConversationID uniqueidentifier,
    @PartnerCultureID int,
    @Deleted bit,
    @DeletedBy uniqueidentifier,
    @DeletedOn datetime
AS 
	SET NOCOUNT ON 
	SET XACT_ABORT ON  
	
	BEGIN TRAN
	
	INSERT INTO [dbo].[ConversationCulture] ([ConversationCultureID], [ConversationID], [PartnerCultureID], [Deleted], [DeletedBy], [DeletedOn])
	SELECT @ConversationCultureID, @ConversationID, @PartnerCultureID, @Deleted, @DeletedBy, @DeletedOn
	
	-- Begin Return Select <- do not remove
	SELECT [ConversationCultureID], [ConversationID], [PartnerCultureID], [Deleted], [DeletedBy], [DeletedOn]
	FROM   [dbo].[ConversationCulture]
	WHERE  [ConversationCultureID] = @ConversationCultureID
	-- End Return Select <- do not remove
               
	COMMIT