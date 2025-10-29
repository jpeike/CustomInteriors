-- Script: 2025.001
-- By: Joey Peikert

DECLARE @ScriptName NVARCHAR(255) = '2025.001';


IF EXISTS (SELECT 1 FROM [dbo].[Version] WHERE ScriptName = @ScriptName)
BEGIN
    PRINT CONCAT(@ScriptName, ' already applied.');
    RETURN;
END

BEGIN TRY
    BEGIN TRANSACTION;

    -- START YOUR SQL
    -- Insert sample customer
    INSERT INTO [dbo].[Customer] ([FirstName], [LastName], [CustomerType], [PreferredContactMethod], [Status])
    VALUES ('John', 'Doe', 'Residential', 'Email', 'Active');
    
    DECLARE @CustomerID INT = SCOPE_IDENTITY();
    
    -- Insert sample user
    INSERT INTO [dbo].[User] ([CustomerID], [Username], [Email], [PasswordHash], [Role])
    VALUES (@CustomerID, 'demoUser', 'demo@example.com', 'DEMO_HASHED_PASSWORD', 'Customer');
    -- END SQL


    -- Log this version
    INSERT INTO [dbo].[Version] ([ScriptName], [AppliedOn])
    VALUES (@ScriptName, GETDATE());

    COMMIT TRANSACTION;
    PRINT CONCAT('Successfully applied ', @ScriptName);
END TRY
BEGIN CATCH
    ROLLBACK TRANSACTION;
    DECLARE @ErrorMessage NVARCHAR(4000) = ERROR_MESSAGE();
    RAISERROR('Error in %s: %s', 16, 1, @ScriptName, @ErrorMessage);
END CATCH;