CREATE TABLE [dbo].[Email] (
    [EmailID] INT IDENTITY(1,1) NOT NULL PRIMARY KEY,
    [CustomerID] INT NOT NULL,
    [EmailAddress] NVARCHAR(255) NOT NULL,
    [EmailType] NVARCHAR(100) NOT NULL,
    CONSTRAINT FK_Email_Customer FOREIGN KEY ([CustomerID]) REFERENCES [dbo].[Customer]([CustomerID]) ON DELETE CASCADE
);
GO

-- Unique index to prevent duplicate email addresses for the same customer
CREATE UNIQUE INDEX IX_Email_Customer_EmailAddress 
    ON [dbo].[Email]([CustomerID], [EmailAddress]);
GO
