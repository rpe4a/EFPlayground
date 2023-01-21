# EFPlayground
Entity Framework Playground

## Migration
[Separate Migrations Project](https://learn.microsoft.com/en-us/ef/core/managing-schemas/migrations/projects?tabs=dotnet-core-cli)

### Commit 
- dotnet ef migrations add InitializeDatabase
- dotnet ef database update InitializeDatabase

### Rollback
- dotnet ef database update "last success migration"

### Optimistic lock - 
Add these triggers to Up method of migration. [See more information](https://www.bricelam.net/2020/08/07/sqlite-and-efcore-concurrency-tokens.html)
```SQL
CREATE TRIGGER UpdateUserVersion
AFTER UPDATE ON User
BEGIN
UPDATE User
SET 
    version = version + 1,
    update_at = DATETIME('now')
WHERE rowid = NEW.rowid;
END;

CREATE TRIGGER UpdateProfileVersion
    AFTER UPDATE ON Profile
BEGIN
    UPDATE Profile
    SET
        version = version + 1,
        update_at = DATETIME('now')
    WHERE rowid = NEW.rowid;
END;

CREATE TRIGGER UpdateCompanyVersion
    AFTER UPDATE ON Company
BEGIN
    UPDATE Company
    SET
        version = version + 1,
        update_at = DATETIME('now')
    WHERE rowid = NEW.rowid;
END;
```
```csharp
migrationBuilder.Sql(@"CREATE TRIGGER UpdateUserVersion
AFTER UPDATE ON User
BEGIN
UPDATE User
SET 
    version = version + 1,
    update_at = DATETIME('now')
WHERE rowid = NEW.rowid;
END;");

migrationBuilder.Sql(@"CREATE TRIGGER UpdateProfileVersion
    AFTER UPDATE ON Profile
    BEGIN
    UPDATE Profile
    SET
        version = version + 1,
        update_at = DATETIME('now')
    WHERE rowid = NEW.rowid;
END;");

migrationBuilder.Sql(@"CREATE TRIGGER UpdateCompanyVersion
    AFTER UPDATE ON Company
BEGIN
    UPDATE Company
    SET
        version = version + 1,
        update_at = DATETIME('now')
    WHERE rowid = NEW.rowid;
END;");
```
