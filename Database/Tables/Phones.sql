CREATE TABLE [dbo].[Phones]
(
    [PhoneId]     INT IDENTITY (1,1) NOT NULL PRIMARY KEY,
    [CustomerId]  INT                NOT NULL,
    [PhoneNumber] NVARCHAR(100),
    [PhoneType]   NVARCHAR(100),
    FOREIGN KEY (CustomerId) REFERENCES Customers (CustomerId) ON DELETE CASCADE ON UPDATE CASCADE
)