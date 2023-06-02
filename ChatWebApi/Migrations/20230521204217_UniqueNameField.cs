using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ChatWebApi.Migrations
{
    /// <inheritdoc />
    public partial class UniqueNameField : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Members_Name",
                table: "Members",
                column: "Name",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Members_Name",
                table: "Members");
        }
    }
}
