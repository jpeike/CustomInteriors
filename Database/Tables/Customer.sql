CREATE TABLE [dbo].[Customer] (
	[customerID] INT IDENTITY (1,1) NOT NULL PRIMARY KEY,
	[firstName] NVARCHAR(100) NOT NULL,
	[lastName]	NVARCHAR(100) NOT NULL,
	[customerType] NVARCHAR(100) NOT NULL,
	[prefferedContactMethod] NVARCHAR(100),
	[companyName] NVARCHAR(100),
	[status] NVARCHAR(100),
	[customerNotes] TEXT
);
GO

