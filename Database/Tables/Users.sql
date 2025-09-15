CREATE TABLE [dbo].[Users] (
    [Id] INT IDENTITY(1,1) NOT NULL PRIMARY KEY,
    [Username] NVARCHAR(100) NOT NULL,
    [Email] NVARCHAR(255) NOT NULL,
    [PasswordHash] NVARCHAR(512) NOT NULL,
    [CreatedOn] DATETIME2 NOT NULL DEFAULT GETDATE()
);
GO

-- Unique indexes to enforce uniqueness on Username and Email
CREATE UNIQUE INDEX IX_Users_Username ON [dbo].[Users]([Username]);
GO

CREATE UNIQUE INDEX IX_Users_Email ON [dbo].[Users]([Email]);
GO
