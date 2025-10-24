CREATE TABLE [dbo].[Payments]
(
    [PaymentId]   INT NOT NULL PRIMARY KEY,
    [InvoiceId]   INT, -- FOREIGN KEY REFERENCES [dbo].[Invoices].[InvoiceId],
    [PaymentDate] DATE,
    [AmountPaid]  DECIMAL,
    [Method]      NVARCHAR(100)
)