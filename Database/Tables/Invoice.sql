CREATE TABLE [dbo].[Invoice] (
    [InvoiceID] INT IDENTITY(1,1) NOT NULL PRIMARY KEY,
    [DateIssued] NVARCHAR(100) NOT NULL,
    [Method] NVARCHAR(100) NOT NULL,
    [SellerDetails] NVARCHAR(255) NOT NULL
);
GO
