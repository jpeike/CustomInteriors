CREATE TABLE [dbo].[Employees]
(
    [EmployeeId] INT IDENTITY (1,1) NOT NULL PRIMARY KEY,
    [AccountId]  INT                NOT NULL, -- FOREIGN KEY REFERENCES [dbo].[Users].[UserId],
    [EmailId]    INT                NOT NULL, -- FOREIGN KEY REFERENCES [dbo].[Emails].[EmailId], 
    [Name]       NVARCHAR(255)      NOT NULL,
    [Role]       NVARCHAR(255)      NOT NULL
);
GO
