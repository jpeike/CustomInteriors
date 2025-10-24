CREATE TABLE [dbo].[InvoiceItems]
(
    [ItemId]      INT NOT NULL PRIMARY KEY,
    [InvoiceId]   INT, -- FOREIGN KEY REFERENCES [dbo].[Invoices].[InvoiceId],
    [Description] NVARCHAR(255),
    [Amount]      INT,
    [Price]       DECIMAL
)