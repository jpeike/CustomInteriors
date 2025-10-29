CREATE TABLE [dbo].[InvoiceItem] (
    [ItemID] INT IDENTITY(1,1) NOT NULL PRIMARY KEY,
    [InvoiceID] INT NOT NULL,
    [Description] NVARCHAR(255) NOT NULL,
    [Amount] INT NOT NULL,
    [Price] DECIMAL(18,2) NOT NULL,
    CONSTRAINT FK_InvoiceItem_Invoice FOREIGN KEY ([InvoiceID]) REFERENCES [dbo].[Invoice]([InvoiceID]) ON DELETE CASCADE
);
GO
