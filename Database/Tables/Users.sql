CREATE TABLE [dbo].[User] (
    [UserID] INT IDENTITY(1,1) NOT NULL PRIMARY KEY,
    [CustomerID] INT NULL,
    [Username] NVARCHAR(100) NOT NULL,
    [Email] NVARCHAR(255) NOT NULL,
    [PasswordHash] NVARCHAR(512) NOT NULL,
    [Role] NVARCHAR(100) NOT NULL,
    [CreatedOn] DATETIME2(7) NOT NULL DEFAULT GETDATE(),
    CONSTRAINT FK_User_Customer FOREIGN KEY ([CustomerID]) REFERENCES [dbo].[Customer]([CustomerID])
);
GO

-- Unique indexes to enforce uniqueness on Username and Email
CREATE UNIQUE INDEX IX_User_Username ON [dbo].[User]([Username]);
GO

CREATE UNIQUE INDEX IX_User_Email ON [dbo].[User]([Email]);
GO
