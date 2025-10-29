CREATE TABLE [dbo].[QuoteRequest] (
    [QuoteID] INT IDENTITY(1,1) NOT NULL PRIMARY KEY,
    [CustomerID] INT NOT NULL,
    [JobID] INT NOT NULL,
    [RequestDate] DATE NOT NULL,
    [DescriptionOfWork] NVARCHAR(255) NOT NULL,
    [EstimatedPrice] DECIMAL(18,2) NULL,
    CONSTRAINT FK_QuoteRequest_Customer FOREIGN KEY ([CustomerID]) REFERENCES [dbo].[Customer]([CustomerID]),
    CONSTRAINT FK_QuoteRequest_Job FOREIGN KEY ([JobID]) REFERENCES [dbo].[Job]([JobID])
);
GO
