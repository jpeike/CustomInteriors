CREATE TABLE [dbo].[Payments]
(
    [PaymentId]   INT IDENTITY (1,1) NOT NULL PRIMARY KEY,
    [InvoiceId]   INT                NOT NULL,
    [PaymentDate] DATE,
    [AmountPaid]  DECIMAL,
    [Method]      NVARCHAR(100),
    FOREIGN KEY (InvoiceId) REFERENCES Invoices (InvoiceId) ON DELETE CASCADE ON UPDATE CASCADE
)