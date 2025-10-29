CREATE TABLE [dbo].[JobInvoice] (
    [JobID] INT NOT NULL PRIMARY KEY,
    [InvoiceID] INT NOT NULL,
    [CreatedDate] DATE NOT NULL,
    [PercentageOfInvoice] DECIMAL(5,2) NOT NULL,
    CONSTRAINT FK_JobInvoice_Job FOREIGN KEY ([JobID]) REFERENCES [dbo].[Job]([JobID]),
    CONSTRAINT FK_JobInvoice_Invoice FOREIGN KEY ([InvoiceID]) REFERENCES [dbo].[Invoice]([InvoiceID])
);
GO
