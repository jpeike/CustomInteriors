CREATE TABLE [dbo].[JobAssignment] (
    [JobID] INT NOT NULL,
    [UserID] INT NOT NULL,
    [AssignmentDate] DATE NOT NULL,
    [RoleOnJob] NVARCHAR(100) NOT NULL,
    CONSTRAINT PK_JobAssignment PRIMARY KEY ([JobID], [UserID]),
    CONSTRAINT FK_JobAssignment_Job FOREIGN KEY ([JobID]) REFERENCES [dbo].[Job]([JobID]),
    CONSTRAINT FK_JobAssignment_User FOREIGN KEY ([UserID]) REFERENCES [dbo].[User]([UserID])
);
GO
