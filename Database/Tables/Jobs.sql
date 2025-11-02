CREATE TABLE [dbo].[Jobs]
(
    [JobId]          INT IDENTITY (1,1) NOT NULL PRIMARY KEY,
    [CustomerId]     INT                NOT NULL,
    [JobDescription] NVARCHAR(255),
    [StartDate]      DATE,
    [EndDate]        DATE,
    [Status]         NVARCHAR(100),
    FOREIGN KEY (CustomerId) REFERENCES Customers (CustomerId) ON DELETE CASCADE ON UPDATE CASCADE
)