using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Library.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class ELoginModified : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Logins_Staffs_StaffId",
                table: "Logins");

            migrationBuilder.DropIndex(
                name: "IX_Logins_StaffId",
                table: "Logins");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
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
    }
}
