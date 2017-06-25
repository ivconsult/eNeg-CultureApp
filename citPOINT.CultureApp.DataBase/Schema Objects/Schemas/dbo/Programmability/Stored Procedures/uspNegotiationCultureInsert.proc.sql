CREATE PROCEDURE [dbo].[uspNegotiationCultureInsert]
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
	
	INSERT INTO [dbo].[NegotiationCulture] ([NegotiationCultureID], [NegotiationID], [NegotiationCultureType], [DefaultCultureID], [Deleted], [DeletedBy], [DeletedOn])
	SELECT @NegotiationCultureID, @NegotiationID, @NegotiationCultureType, @DefaultCultureID, @Deleted, @DeletedBy, @DeletedOn
	
	-- Begin Return Select <- do not remove
	SELECT [NegotiationCultureID], [NegotiationID], [NegotiationCultureType], [DefaultCultureID], [Deleted], [DeletedBy], [DeletedOn]
	FROM   [dbo].[NegotiationCulture]
	WHERE  [NegotiationCultureID] = @NegotiationCultureID
	-- End Return Select <- do not remove
               
	COMMIT