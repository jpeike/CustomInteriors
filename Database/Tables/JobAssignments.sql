CREATE TABLE [dbo].[JobAssignments]
(
    [JobId]          INT NOT NULL,
    [UserId]         INT NOT NULL,
    [AssignmentDate] DATE,
    [RoleOnJob]      NVARCHAR(100),
    CONSTRAINT PK_JobAssignments PRIMARY KEY (JobId, UserId),
    FOREIGN KEY (JobId) REFERENCES Jobs (JobId) ON DELETE CASCADE ON UPDATE CASCADE,
    FOREIGN KEY (UserId) REFERENCES Users (Id) ON DELETE CASCADE ON UPDATE CASCADE
)