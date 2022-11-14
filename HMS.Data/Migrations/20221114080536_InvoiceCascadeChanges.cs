using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HMS.Data.Migrations
{
    public partial class InvoiceCascadeChanges : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_InvoiceRecords_Invoice_InvoiceId",
                table: "InvoiceRecords");

            migrationBuilder.AddForeignKey(
                name: "FK_InvoiceRecords_Invoice_InvoiceId",
                table: "InvoiceRecords",
                column: "InvoiceId",
                principalTable: "Invoice",
                principalColumn: "InvoiceId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_InvoiceRecords_Invoice_InvoiceId",
                table: "InvoiceRecords");

            migrationBuilder.AddForeignKey(
                name: "FK_InvoiceRecords_Invoice_InvoiceId",
                table: "InvoiceRecords",
                column: "InvoiceId",
                principalTable: "Invoice",
                principalColumn: "InvoiceId");
        }
    }
}
