using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EFDatabaseMigration.Migrations
{
    /// <inheritdoc />
    public partial class InitializeDatabase : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Company",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "TEXT", nullable: false),
                    name = table.Column<string>(type: "TEXT", maxLength: 30, nullable: false),
                    createat = table.Column<DateTime>(name: "create_at", type: "TEXT", nullable: false, defaultValueSql: "DATETIME('now')"),
                    isdelete = table.Column<bool>(name: "is_delete", type: "INTEGER", nullable: false, defaultValue: false),
                    updateat = table.Column<DateTime>(name: "update_at", type: "TEXT", nullable: false, defaultValueSql: "DATETIME('now')"),
                    version = table.Column<long>(type: "INTEGER", rowVersion: true, nullable: false, defaultValue: 0L)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Company", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "User",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "TEXT", nullable: false),
                    login = table.Column<string>(type: "TEXT", maxLength: 100, nullable: false),
                    password = table.Column<string>(type: "TEXT", maxLength: 50, nullable: false),
                    companyid = table.Column<Guid>(name: "company_id", type: "TEXT", nullable: false),
                    createat = table.Column<DateTime>(name: "create_at", type: "TEXT", nullable: false, defaultValueSql: "DATETIME('now')"),
                    updateat = table.Column<DateTime>(name: "update_at", type: "TEXT", nullable: false, defaultValueSql: "DATETIME('now')"),
                    version = table.Column<long>(type: "INTEGER", rowVersion: true, nullable: false, defaultValue: 0L)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_User", x => x.id);
                    table.ForeignKey(
                        name: "FK_User_Company_company_id",
                        column: x => x.companyid,
                        principalTable: "Company",
                        principalColumn: "id",
                        onDelete: ReferentialAction.SetNull);
                });

            migrationBuilder.CreateTable(
                name: "Profile",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "TEXT", nullable: false),
                    name = table.Column<string>(type: "TEXT", maxLength: 50, nullable: false),
                    age = table.Column<int>(type: "INTEGER", maxLength: 3, nullable: false),
                    passport = table.Column<string>(type: "TEXT", maxLength: 50, nullable: false),
                    phone = table.Column<string>(type: "TEXT", maxLength: 20, nullable: false),
                    userid = table.Column<Guid>(name: "user_id", type: "TEXT", nullable: false),
                    createat = table.Column<DateTime>(name: "create_at", type: "TEXT", nullable: false, defaultValueSql: "DATETIME('now')"),
                    updateat = table.Column<DateTime>(name: "update_at", type: "TEXT", nullable: false, defaultValueSql: "DATETIME('now')"),
                    version = table.Column<long>(type: "INTEGER", rowVersion: true, nullable: false, defaultValue: 0L)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Profile", x => x.id);
                    table.CheckConstraint("Age", "Age > 0 AND Age < 150");
                    table.ForeignKey(
                        name: "FK_Profile_User_user_id",
                        column: x => x.userid,
                        principalTable: "User",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Company_name",
                table: "Company",
                column: "name");

            migrationBuilder.CreateIndex(
                name: "IX_Profile_name",
                table: "Profile",
                column: "name");

            migrationBuilder.CreateIndex(
                name: "IX_Profile_passport",
                table: "Profile",
                column: "passport",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Profile_phone",
                table: "Profile",
                column: "phone",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Profile_user_id",
                table: "Profile",
                column: "user_id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_User_company_id",
                table: "User",
                column: "company_id");

            migrationBuilder.CreateIndex(
                name: "IX_User_login",
                table: "User",
                column: "login",
                unique: true);
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

        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Profile");

            migrationBuilder.DropTable(
                name: "User");

            migrationBuilder.DropTable(
                name: "Company");
        }
    }
}
