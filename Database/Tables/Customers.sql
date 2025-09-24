CREATE TABLE [dbo].[Customers] (
	[CustomerId] INT IDENTITY (1,1) NOT NULL PRIMARY KEY,
	[FirstName] NVARCHAR(100) NOT NULL,
	[LastName]	NVARCHAR(100) NOT NULL,
	[CustomerType] NVARCHAR(100) NOT NULL,
	[PrefferedContactMethod] NVARCHAR(100),
	[CompanyName] NVARCHAR(100),
	[Status] NVARCHAR(100),
	[CustomerNotes] TEXT
);
GO

