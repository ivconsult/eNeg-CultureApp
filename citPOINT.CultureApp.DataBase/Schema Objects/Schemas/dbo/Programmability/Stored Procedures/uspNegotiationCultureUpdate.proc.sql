CREATE PROCEDURE [dbo].[uspNegotiationCultureUpdate]
	@NegotiationCultureID uniqueidentifier,
    @NegotiationID uniqueidentifier,
    @NegotiationCultureType tinyint,
    @DefaultCultureID int,
    @Deleted bit,
    @DeletedBy uniqueidentifier,
    @DeletedOn datetime
AS 
	SET NOCOUNT ON 
	SET XACT_ABORT ON  
	
	BEGIN TRAN

	UPDATE [dbo].[NegotiationCulture]
	SET    [NegotiationCultureID] = @NegotiationCultureID, [NegotiationID] = @NegotiationID, [NegotiationCultureType] = @NegotiationCultureType, [DefaultCultureID] = @DefaultCultureID, [Deleted] = @Deleted, [DeletedBy] = @DeletedBy, [DeletedOn] = @DeletedOn
	WHERE  [NegotiationCultureID] = @NegotiationCultureID
	
	-- Begin Return Select <- do not remove
	SELECT [NegotiationCultureID], [NegotiationID], [NegotiationCultureType], [DefaultCultureID], [Deleted], [DeletedBy], [DeletedOn]
	FROM   [dbo].[NegotiationCulture]
	WHERE  [NegotiationCultureID] = @NegotiationCultureID	
	-- End Return Select <- do not remove

	COMMIT TRAN