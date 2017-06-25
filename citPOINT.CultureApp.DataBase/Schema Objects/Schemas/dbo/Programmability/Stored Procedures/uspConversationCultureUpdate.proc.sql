CREATE PROCEDURE [dbo].[uspConversationCultureUpdate]
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

	UPDATE [dbo].[ConversationCulture]
	SET    [ConversationCultureID] = @ConversationCultureID, [ConversationID] = @ConversationID, [PartnerCultureID] = @PartnerCultureID, [Deleted] = @Deleted, [DeletedBy] = @DeletedBy, [DeletedOn] = @DeletedOn
	WHERE  [ConversationCultureID] = @ConversationCultureID
	
	-- Begin Return Select <- do not remove
	SELECT [ConversationCultureID], [ConversationID], [PartnerCultureID], [Deleted], [DeletedBy], [DeletedOn]
	FROM   [dbo].[ConversationCulture]
	WHERE  [ConversationCultureID] = @ConversationCultureID	
	-- End Return Select <- do not remove

	COMMIT TRAN