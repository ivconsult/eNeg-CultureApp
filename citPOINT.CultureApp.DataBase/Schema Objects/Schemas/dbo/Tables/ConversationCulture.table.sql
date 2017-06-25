CREATE TABLE [dbo].[ConversationCulture]
(
	ConversationCultureID uniqueidentifier primary key, 
	ConversationID uniqueidentifier,
	PartnerCultureID int references CultureFiveDimension(CultureID),
	Deleted bit,
	DeletedBy uniqueidentifier,
	DeletedOn datetime
)
