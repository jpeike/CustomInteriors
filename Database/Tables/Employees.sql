CREATE TABLE [dbo].[Employees]
(
    [EmployeeId] INT IDENTITY (1,1) NOT NULL PRIMARY KEY,
    [AccountId]  INT                NOT NULL,
    [EmailId]    INT                NOT NULL,
    [Name]       NVARCHAR(255)      NOT NULL,
    [Role]       NVARCHAR(255)      NOT NULL,
    FOREIGN KEY (AccountId) REFERENCES Accounts (AccountId) ON DELETE CASCADE ON UPDATE CASCADE,
    FOREIGN KEY (EmployeeId) REFERENCES Emails (EmailId) ON DELETE SET NULL ON UPDATE CASCADE,
);
GO
