using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Library.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddedStaffIdonStaffLoginTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Username",
                table: "Logins",
                newName: "Email");

            migrationBuilder.AddColumn<int>(
                name: "StaffId",
                table: "Logins",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Logins_StaffId",
                table: "Logins",
                column: "StaffId");

            migrationBuilder.AddForeignKey(
                name: "FK_Logins_Staffs_StaffId",
                table: "Logins",
                column: "StaffId",
                principalTable: "Staffs",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Logins_Staffs_StaffId",
                table: "Logins");

            migrationBuilder.DropIndex(
                name: "IX_Logins_StaffId",
                table: "Logins");

            migrationBuilder.DropColumn(
                name: "StaffId",
                table: "Logins");

            migrationBuilder.RenameColumn(
                name: "Email",
                table: "Logins",
                newName: "Username");
        }
    }
}
