using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CareTrack.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class RebuildEntity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Medicines_RecipeMedicines_RecipeMedicineRecipeId_RecipeMedi~",
                table: "Medicines");

            migrationBuilder.DropForeignKey(
                name: "FK_Recipes_PatientRecipes_PatientRecipePatientId_PatientRecipe~",
                table: "Recipes");

            migrationBuilder.DropIndex(
                name: "IX_Recipes_PatientRecipePatientId_PatientRecipeRecipeId",
                table: "Recipes");

            migrationBuilder.DropIndex(
                name: "IX_Medicines_RecipeMedicineRecipeId_RecipeMedicineMedicineId",
                table: "Medicines");

            migrationBuilder.DropColumn(
                name: "PatientRecipePatientId",
                table: "Recipes");

            migrationBuilder.DropColumn(
                name: "PatientRecipeRecipeId",
                table: "Recipes");

            migrationBuilder.DropColumn(
                name: "RecipeMedicineMedicineId",
                table: "Medicines");

            migrationBuilder.DropColumn(
                name: "RecipeMedicineRecipeId",
                table: "Medicines");

            migrationBuilder.CreateIndex(
                name: "IX_RecipeMedicines_MedicineId",
                table: "RecipeMedicines",
                column: "MedicineId");

            migrationBuilder.CreateIndex(
                name: "IX_PatientRecipes_RecipeId",
                table: "PatientRecipes",
                column: "RecipeId");

            migrationBuilder.AddForeignKey(
                name: "FK_PatientRecipes_Recipes_RecipeId",
                table: "PatientRecipes",
                column: "RecipeId",
                principalTable: "Recipes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_RecipeMedicines_Medicines_MedicineId",
                table: "RecipeMedicines",
                column: "MedicineId",
                principalTable: "Medicines",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PatientRecipes_Recipes_RecipeId",
                table: "PatientRecipes");

            migrationBuilder.DropForeignKey(
                name: "FK_RecipeMedicines_Medicines_MedicineId",
                table: "RecipeMedicines");

            migrationBuilder.DropIndex(
                name: "IX_RecipeMedicines_MedicineId",
                table: "RecipeMedicines");

            migrationBuilder.DropIndex(
                name: "IX_PatientRecipes_RecipeId",
                table: "PatientRecipes");

            migrationBuilder.AddColumn<int>(
                name: "PatientRecipePatientId",
                table: "Recipes",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "PatientRecipeRecipeId",
                table: "Recipes",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "RecipeMedicineMedicineId",
                table: "Medicines",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "RecipeMedicineRecipeId",
                table: "Medicines",
                type: "integer",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Recipes_PatientRecipePatientId_PatientRecipeRecipeId",
                table: "Recipes",
                columns: new[] { "PatientRecipePatientId", "PatientRecipeRecipeId" });

            migrationBuilder.CreateIndex(
                name: "IX_Medicines_RecipeMedicineRecipeId_RecipeMedicineMedicineId",
                table: "Medicines",
                columns: new[] { "RecipeMedicineRecipeId", "RecipeMedicineMedicineId" });

            migrationBuilder.AddForeignKey(
                name: "FK_Medicines_RecipeMedicines_RecipeMedicineRecipeId_RecipeMedi~",
                table: "Medicines",
                columns: new[] { "RecipeMedicineRecipeId", "RecipeMedicineMedicineId" },
                principalTable: "RecipeMedicines",
                principalColumns: new[] { "RecipeId", "MedicineId" });

            migrationBuilder.AddForeignKey(
                name: "FK_Recipes_PatientRecipes_PatientRecipePatientId_PatientRecipe~",
                table: "Recipes",
                columns: new[] { "PatientRecipePatientId", "PatientRecipeRecipeId" },
                principalTable: "PatientRecipes",
                principalColumns: new[] { "PatientId", "RecipeId" });
        }
    }
}
