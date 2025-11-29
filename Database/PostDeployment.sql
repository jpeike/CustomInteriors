-- ================================
-- CustomInteriors Post-Deployment Script
-- ================================

PRINT 'Running post-deployment versioned scripts...';

-- Initialize version table if it doesn't exist
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Version]') AND type = 'U')
BEGIN
    CREATE TABLE [dbo].[Version] (
        [ScriptName] NVARCHAR(255) NOT NULL PRIMARY KEY,
        [AppliedOn] DATETIME NOT NULL
    );
END
GO

-- === Script Runner Template ===
:r .\Scripts\2025.001.sql
GO

IF DB_NAME() LIKE '%Test'
BEGIN
    PRINT 'Detected Test database – seeding test data...';
    :r .\Scripts\SeedTestDB.sql
END
GO


PRINT '========== PostDeployment.sql Complete =========='
GO
