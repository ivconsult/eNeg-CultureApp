CREATE TABLE [dbo].[DomainCultureMapping]
(
	DomainCultureMappingID int Primary Key, 
	DomainExt nvarchar(10),
	CultureID int references CultureFiveDimension(CultureID)
)
