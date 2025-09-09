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


PRINT '========== PostDeployment.sql Complete =========='
