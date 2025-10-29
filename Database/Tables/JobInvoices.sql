CREATE TABLE [dbo].[JobInvoices]
(
    [JobId]               INT NOT NULL,
    [InvoiceId]           INT NOT NULL,
    [CreatedId]           DATE,
    [PercentageOfInvoice] DECIMAL,
    CONSTRAINT PK_JobInvoices PRIMARY KEY (JobId, InvoiceId),
    FOREIGN KEY (JobId) REFERENCES Jobs (JobId) ON DELETE CASCADE ON UPDATE CASCADE,
    FOREIGN KEY (InvoiceId) REFERENCES Invoices (InvoiceId) ON DELETE CASCADE ON UPDATE CASCADE
)