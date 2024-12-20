using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CareTrack.Server.Migrations
{
    /// <inheritdoc />
    public partial class AddKnowledgeItemAuthor : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Author",
                table: "KnowledgeItems",
                type: "text",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Author",
                table: "KnowledgeItems");
        }
    }
}
