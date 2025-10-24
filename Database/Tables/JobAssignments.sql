CREATE TABLE [dbo].[JobAssignments]
(
    [JobId]          INT NOT NULL PRIMARY KEY,
    [UserId]         INT, -- FOREIGN KEY REFERENCES [dbo].[Users].[UserId],
    [AssignmentDate] DATE,
    [RoleOnJob]      NVARCHAR(100)
)