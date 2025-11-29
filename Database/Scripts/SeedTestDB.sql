PRINT 'Seeding test data for CustomInteriorsTest...';

BEGIN TRY
    BEGIN TRANSACTION;

    DELETE FROM [dbo].[Emails];
    DELETE FROM [dbo].[Addresses];
    DELETE FROM [dbo].[Customers];

    INSERT INTO [dbo].[Customers]
        (FirstName, LastName, CustomerType, PrefferedContactMethod, CompanyName, Status, CustomerNotes)
    VALUES
        ('Alice', 'Smith', 'Residential', 'Email', 'Smith Home Services', 'Active', 'VIP client - prefers email.'),
        ('Cameron', 'Scott', 'Commercial', 'Email', 'Google', 'Active', 'Frequent customer.');

    INSERT INTO [dbo].[Addresses]
        (CustomerId, Street, City, State, PostalCode, Country, AddressType)
    VALUES
        (1, '123 Maple St', 'Menomonie', 'WI', 54751, 'USA', 'Home'),
        (2, '456 Oak Ave', 'Eau Claire', 'WI', 54701, 'USA', 'Business');

    INSERT INTO [dbo].[Emails]
        (CustomerId, EmailAddress, EmailType, CreatedOn)
    VALUES
        (1, 'alice@example.com', 'Personal', '2024-11-05'),
        (2, 'cscott@gmail.com', 'Business', '2024-12-01');

    COMMIT TRANSACTION;
    PRINT 'Test data seeded successfully.';
END TRY
BEGIN CATCH
    ROLLBACK TRANSACTION;
    PRINT 'Error seeding test data: ' + ERROR_MESSAGE();
END CATCH;