CREATE TABLE [dbo].[Invoices]
(
    [InvoiceId] INT NOT NULL PRIMARY KEY,
    [DateIssued] DATE,
    [Method] NVARCHAR(100),
    [SellerDetails] NVARCHAR(255)
)