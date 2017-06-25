CREATE TABLE [dbo].[NegotiationCulture]
(
	NegotiationCultureID uniqueidentifier primary key, 
	NegotiationID uniqueidentifier,
	NegotiationCultureType tinyint not null,
	DefaultCultureID int references CultureFiveDimension(CultureID),
	Deleted bit,
	DeletedBy uniqueidentifier,
	DeletedOn datetime
)
