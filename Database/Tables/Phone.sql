CREATE TABLE [dbo].[Phone] (
    [PhoneNumber] NVARCHAR(100) NOT NULL PRIMARY KEY,
    [CustomerID] INT NOT NULL,
    [PhoneType] NVARCHAR(100) NOT NULL,
    CONSTRAINT FK_Phone_Customer FOREIGN KEY ([CustomerID]) REFERENCES [dbo].[Customer]([CustomerID]) ON DELETE CASCADE
);
GO
