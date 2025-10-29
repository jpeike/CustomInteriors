CREATE TABLE [dbo].[Job] (
    [JobID] INT IDENTITY(1,1) NOT NULL PRIMARY KEY,
    [CustomerID] INT NOT NULL,
    [JobDescription] NVARCHAR(255) NOT NULL,
    [StartDate] DATE NOT NULL,
    [EndDate] DATE NULL,
    [Status] NVARCHAR(100) NOT NULL,
    CONSTRAINT FK_Job_Customer FOREIGN KEY ([CustomerID]) REFERENCES [dbo].[Customer]([CustomerID])
);
GO
