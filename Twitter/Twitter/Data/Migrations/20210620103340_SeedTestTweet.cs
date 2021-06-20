using Microsoft.EntityFrameworkCore.Migrations;

namespace Twitter.Data.Migrations
{
    public partial class SeedTestTweet : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
/*            migrationBuilder.InsertData(
                table: "Tweets",
                columns: new[] { "Id", "Content", "ParentId", "Time", "UserId" },
                values: new object[] { 2, "test", 2, "10-10-2020", "35606eb1-8062-420c-8eb2-fe8eaa26c73f" });

            migrationBuilder.InsertData(
                table: "Tweets",
                columns: new[] { "Id", "Content", "ParentId", "Time", "UserId" },
                values: new object[] { 1, "test", 2, "10-10-2020", "35606eb1-8062-420c-8eb2-fe8eaa26c73f" });*/
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Tweets",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Tweets",
                keyColumn: "Id",
                keyValue: 2);
        }
    }
}
