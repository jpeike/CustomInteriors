CREATE TABLE [dbo].[JobInvoices]
(
    [JobId]               INT NOT NULL PRIMARY KEY, -- todo make pk and fk
    [InvoiceId]           INT NOT NULL PRIMARY KEY,
    [CreatedId]           DATE,
    [PercentageOfInvoice] DECIMAL
)