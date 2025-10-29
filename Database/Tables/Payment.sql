CREATE TABLE [dbo].[Payment] (
    [PaymentID] INT IDENTITY(1,1) NOT NULL PRIMARY KEY,
    [PaymentDate] DATE NOT NULL,
    [AmountPaid] DECIMAL(18,2) NOT NULL,
    [Method] NVARCHAR(100) NOT NULL
);
GO
