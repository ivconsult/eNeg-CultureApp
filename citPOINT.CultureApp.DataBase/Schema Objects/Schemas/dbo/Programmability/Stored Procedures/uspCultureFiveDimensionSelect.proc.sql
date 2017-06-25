CREATE PROCEDURE [dbo].[uspCultureFiveDimensionSelect]
	@CultureID int
AS
	select [CultureID], [PDI] , [IDV], [MAS], [UAI], [LTO]
	from [dbo].[CultureFiveDimension]
	where [CultureID] = @CultureID