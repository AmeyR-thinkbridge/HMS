using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HMS.Data.Migrations
{
    public partial class InvoiceRecordConfigurationChanges : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_InvoiceRecords_DishId",
                table: "InvoiceRecords");

            migrationBuilder.CreateIndex(
                name: "IX_InvoiceRecords_DishId",
                table: "InvoiceRecords",
                column: "DishId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_InvoiceRecords_DishId",
                table: "InvoiceRecords");

            migrationBuilder.CreateIndex(
                name: "IX_InvoiceRecords_DishId",
                table: "InvoiceRecords",
                column: "DishId",
                unique: true,
                filter: "[DishId] IS NOT NULL");
        }
    }
}
