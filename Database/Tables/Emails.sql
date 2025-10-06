CREATE TABLE [dbo].[Emails] (
    [EmailID] INT IDENTITY(1,1) NOT NULL PRIMARY KEY,
    [CustomerId] INT NOT NULL,
    [EmailAddress] NVARCHAR(255) NOT NULL,
    [EmailType] NVARCHAR(100) NOT NULL,
    [CreatedOn] DATETIME2 NOT NULL DEFAULT GETDATE(),

    FOREIGN KEY (CustomerId) REFERENCES Customers(CustomerId) ON DELETE CASCADE
);
GO

-- Unique index to prevent duplicate email addresses for the same customer
CREATE UNIQUE INDEX IX_Email_Customer_EmailAddress 
    ON [dbo].[Emails]([CustomerId], [EmailAddress]);
GO
