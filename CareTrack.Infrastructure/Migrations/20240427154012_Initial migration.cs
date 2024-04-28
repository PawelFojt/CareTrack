using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace CareTrack.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Initialmigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Patients",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    FirstName = table.Column<string>(type: "text", nullable: false),
                    LastName = table.Column<string>(type: "text", nullable: false),
                    Age = table.Column<int>(type: "integer", nullable: false),
                    Weight = table.Column<int>(type: "integer", nullable: false),
                    PhoneNumber = table.Column<string>(type: "text", nullable: false),
                    Admission = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Discharge = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Patients", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PatientRecipes",
                columns: table => new
                {
                    RecipeId = table.Column<int>(type: "integer", nullable: false),
                    PatientId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PatientRecipes", x => new { x.PatientId, x.RecipeId });
                    table.ForeignKey(
                        name: "FK_PatientRecipes_Patients_PatientId",
                        column: x => x.PatientId,
                        principalTable: "Patients",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Recipes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Quantity = table.Column<int>(type: "integer", nullable: false),
                    DosingTime = table.Column<TimeOnly[]>(type: "time without time zone[]", nullable: false),
                    PatientRecipePatientId = table.Column<int>(type: "integer", nullable: true),
                    PatientRecipeRecipeId = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Recipes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Recipes_PatientRecipes_PatientRecipePatientId_PatientRecipe~",
                        columns: x => new { x.PatientRecipePatientId, x.PatientRecipeRecipeId },
                        principalTable: "PatientRecipes",
                        principalColumns: new[] { "PatientId", "RecipeId" });
                });

            migrationBuilder.CreateTable(
                name: "RecipeMedicines",
                columns: table => new
                {
                    MedicineId = table.Column<int>(type: "integer", nullable: false),
                    RecipeId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RecipeMedicines", x => new { x.RecipeId, x.MedicineId });
                    table.ForeignKey(
                        name: "FK_RecipeMedicines_Recipes_RecipeId",
                        column: x => x.RecipeId,
                        principalTable: "Recipes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Medicines",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: true),
                    Quantity = table.Column<string>(type: "text", nullable: true),
                    ExpirationDate = table.Column<DateOnly>(type: "date", nullable: true),
                    RecipeMedicineMedicineId = table.Column<int>(type: "integer", nullable: true),
                    RecipeMedicineRecipeId = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Medicines", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Medicines_RecipeMedicines_RecipeMedicineRecipeId_RecipeMedi~",
                        columns: x => new { x.RecipeMedicineRecipeId, x.RecipeMedicineMedicineId },
                        principalTable: "RecipeMedicines",
                        principalColumns: new[] { "RecipeId", "MedicineId" });
                });

            migrationBuilder.CreateIndex(
                name: "IX_Medicines_RecipeMedicineRecipeId_RecipeMedicineMedicineId",
                table: "Medicines",
                columns: new[] { "RecipeMedicineRecipeId", "RecipeMedicineMedicineId" });

            migrationBuilder.CreateIndex(
                name: "IX_Recipes_PatientRecipePatientId_PatientRecipeRecipeId",
                table: "Recipes",
                columns: new[] { "PatientRecipePatientId", "PatientRecipeRecipeId" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Medicines");

            migrationBuilder.DropTable(
                name: "RecipeMedicines");

            migrationBuilder.DropTable(
                name: "Recipes");

            migrationBuilder.DropTable(
                name: "PatientRecipes");

            migrationBuilder.DropTable(
                name: "Patients");
        }
    }
}
