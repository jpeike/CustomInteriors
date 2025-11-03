CREATE TABLE [dbo].[Employees]
(
    [EmployeeId] INT IDENTITY (1,1) NOT NULL PRIMARY KEY,
    [AccountId]  INT                NOT NULL,
    [EmailId]    INT                NOT NULL,
    [Name]       NVARCHAR(255)      NOT NULL,
    [Role]       NVARCHAR(255)      NOT NULL,
    --FOREIGN KEY (AccountId) REFERENCES Accounts (AccountId) ON DELETE CASCADE ON UPDATE CASCADE,
    FOREIGN KEY (EmailId) REFERENCES Emails (EmailId) ON DELETE CASCADE ON UPDATE CASCADE,
);
GO
