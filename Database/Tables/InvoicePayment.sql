-- Junction table to link Invoice and Payment (1-to-many relationship)
CREATE TABLE [dbo].[InvoicePayment] (
    [InvoiceID] INT NOT NULL,
    [PaymentID] INT NOT NULL,
    CONSTRAINT PK_InvoicePayment PRIMARY KEY ([InvoiceID], [PaymentID]),
    CONSTRAINT FK_InvoicePayment_Invoice FOREIGN KEY ([InvoiceID]) REFERENCES [dbo].[Invoice]([InvoiceID]),
    CONSTRAINT FK_InvoicePayment_Payment FOREIGN KEY ([PaymentID]) REFERENCES [dbo].[Payment]([PaymentID])
);
GO
