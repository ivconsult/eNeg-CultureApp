CREATE PROCEDURE [dbo].[uspNegotiationCultureSelect]
	@NegotiationCultureID UNIQUEIDENTIFIER
AS 
	SET NOCOUNT ON 
	SET XACT_ABORT ON  

	BEGIN TRAN

	SELECT [NegotiationCultureID], [NegotiationID], [NegotiationCultureType], [DefaultCultureID], [Deleted], [DeletedBy], [DeletedOn] 
	FROM   [dbo].[NegotiationCulture] 
	WHERE  ([NegotiationCultureID] = @NegotiationCultureID OR @NegotiationCultureID IS NULL) 

	COMMIT