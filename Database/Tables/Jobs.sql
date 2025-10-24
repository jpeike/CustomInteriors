CREATE TABLE [dbo].[Jobs]
(
    [JobId]          INT NOT NULL PRIMARY KEY,
    [CustomerId]     INT NOT NULL, -- FOREIGN KEY REFERENCES [dbo].[Customers].[CustomerId],
    [JobDescription] NVARCHAR(255),
    [StartDate]      DATE,
    [EndDate]        DATE,
    [Status]         NVARCHAR(100)
)