using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Bloggie.Web.Migrations.AuthDb
{
    /// <inheritdoc />
    public partial class CreatingAuthDb1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "7CF690F7-5580-4D8A-8D22-3BF043195BAC",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "bb3d9776-14c3-4eac-84e6-df39ecb89228", "AQAAAAIAAYagAAAAEDLWu6y/dauoJSDpcTq6FnyLv7sh4PRA/6b83swXEo6v2NO5PgK0e7t+qpc0DrSdLQ==", "0101f54d-a8e7-42c9-bc6b-336b4c2d8222" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "7CF690F7-5580-4D8A-8D22-3BF043195BAC",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "59010219-64ff-4e15-a39b-98890257fcc5", "AQAAAAIAAYagAAAAEDrsqxA6jauKkmBNOxYpRpEZrMy7qFV2mGAuFEBaZQRGOBNU88nNe9ntTWDPM9pdcg==", "4c78a682-a249-47ce-a7f4-e1758f53017a" });
        }
    }
}
