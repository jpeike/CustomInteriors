CREATE TABLE [dbo].[Phones]
(
    [Id]          INT NOT NULL PRIMARY KEY,
    [CustomerId]  INT NOT NULL, -- FOREIGN KEY REFERENCES [dbo].[Customers].[CustomerId],
    [PhoneNumber] NVARCHAR(100),
    [PhoneType]   NVARCHAR(100)
)