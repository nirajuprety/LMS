using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Library.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class UpdateIssueTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "StudentId",
                table: "IssueTables",
                newName: "MemberId");

            migrationBuilder.AddColumn<double>(
                name: "FineAmount",
                table: "IssueTables",
                type: "double precision",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "FineRate",
                table: "IssueTables",
                type: "double precision",
                nullable: false,
                defaultValue: 0.0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FineAmount",
                table: "IssueTables");

            migrationBuilder.DropColumn(
                name: "FineRate",
                table: "IssueTables");

            migrationBuilder.RenameColumn(
                name: "MemberId",
                table: "IssueTables",
                newName: "StudentId");
        }
    }
}
