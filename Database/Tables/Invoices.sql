CREATE TABLE [dbo].[Invoices]
(
    [InvoiceId]     INT IDENTITY (1,1) NOT NULL PRIMARY KEY,
    [DateIssued]    DATE,
    [Method]        NVARCHAR(100),
    [SellerDetails] NVARCHAR(255)
)