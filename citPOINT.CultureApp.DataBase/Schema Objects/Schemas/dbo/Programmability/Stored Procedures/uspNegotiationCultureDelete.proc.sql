CREATE PROCEDURE [dbo].[uspNegotiationCultureDelete]
	@NegotiationCultureID uniqueidentifier
AS 
	SET NOCOUNT ON 
	SET XACT_ABORT ON  
	
	BEGIN TRAN

	UPDATE [dbo].[NegotiationCulture]
	SET    [Deleted] = 1, DeletedOn = GETDATE()
	WHERE  [NegotiationCultureID] = @NegotiationCultureID

	COMMIT