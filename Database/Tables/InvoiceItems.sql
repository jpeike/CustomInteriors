CREATE TABLE [dbo].[InvoiceItems]
(
    [ItemId]      INT IDENTITY (1,1) NOT NULL PRIMARY KEY,
    [InvoiceId]   INT                NOT NULL,
    [Description] NVARCHAR(255),
    [Amount]      INT,
    [Price]       DECIMAL,
    FOREIGN KEY (InvoiceId) REFERENCES Invoices (InvoiceId) ON DELETE CASCADE ON UPDATE CASCADE
)