using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace InspireMind.Education.Identity.Migrations
{
    /// <inheritdoc />
    public partial class AddUserRole : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "1E25D52D-1C76-40C0-9CEA-5EA71A8088F2", null, "User", "USER" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "8e445865-a24d-4543-a6c6-9443d048cdb9",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "345cb003-2b2e-4b49-8f64-5e2372355646", "AQAAAAIAAYagAAAAEHyBcsOBQ1/o2dmfmq2FCLC1YvVSPFHZy9YMTllyOMqq5pram8OtjsBlZMNn/OMELA==", "f46a4cbe-2502-4e94-af9b-ecfaa743efba" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "9e224968-33e4-4652-b7b7-8574d048cdb9",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "c0bf990e-d456-4eee-9c60-57fa85d91111", "AQAAAAIAAYagAAAAEO7PBSx+uthX2HNZH6JgUd1/09IYchM0XwnsCLim/KaQ0AY9cEB7tQovPlflUi8gdw==", "e1ed9492-ffc1-420a-aa65-fe46daf58177" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "1E25D52D-1C76-40C0-9CEA-5EA71A8088F2");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "8e445865-a24d-4543-a6c6-9443d048cdb9",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "6dc31c6e-1c9f-46f4-b676-417c9ba90d2a", "AQAAAAIAAYagAAAAELggprrTuaFVPcv5EDjbHA6Z8omE/djt+ZT8E39aVn9bLGOhoJ/d2cC7BNEG34w1bw==", "be0f363d-57e7-40fb-b82b-df23493851fc" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "9e224968-33e4-4652-b7b7-8574d048cdb9",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "27a1aafd-11e1-46ca-b3a5-61ae0192c628", "AQAAAAIAAYagAAAAEFwbjVICywREqOHhAepoPcnnZCZTUDwueKSP6lfksU+tj1sgAGOWhWaGmrg0YLgSXA==", "c9b2e538-a64a-4410-bcc4-fcbbaae7c753" });
        }
    }
}
