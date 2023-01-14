# EFPlayground
Entity Framework Playground

## Migration
[Separate Migrations Project](https://learn.microsoft.com/en-us/ef/core/managing-schemas/migrations/projects?tabs=dotnet-core-cli)

### Commit 
- dotnet ef migrations add InitializeDatabase
- dotnet ef database update InitializeDatabase

### Rollback
- dotnet ef database update "last success migration"