CREATE TABLE [dbo].[QuoteRequests]
(
    [QuoteId]           INT NOT NULL PRIMARY KEY,
    [CustomerId]        INT NOT NULL, -- FOREIGN KEY REFERENCES [dbo].[Customers].[CustomerId],
    [JobId]             INT NOT NULL, -- FOREIGN KEY REFERENCES [dbo].[Jobs].[JobId],
    [RequestDate]       DATE,
    [DescriptionOfWork] NVARCHAR(MAX),
    [EstimatedPrice]    DECIMAL
)