using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MyPharmacy.Migrations
{
    /// <inheritdoc />
    public partial class addrequiredremovedfrompasswordHash : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "PasswordHash",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PasswordHash",
                table: "AspNetUsers");
        }
    }
}
