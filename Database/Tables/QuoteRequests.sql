CREATE TABLE [dbo].[QuoteRequests]
(
    [QuoteId]           INT IDENTITY (1,1) NOT NULL PRIMARY KEY,
    [CustomerId]        INT                NOT NULL,
    [JobId]             INT                NOT NULL,
    [RequestDate]       DATE,
    [DescriptionOfWork] NVARCHAR(MAX),
    [EstimatedPrice]    DECIMAL,
    FOREIGN KEY (CustomerId) REFERENCES Customer (CustomerId) ON DELETE CASCADE ON UPDATE CASCADE,
    FOREIGN KEY (JobId) REFERENCES Jobs (JobId) ON DELETE CASCADE ON UPDATE CASCADE
)