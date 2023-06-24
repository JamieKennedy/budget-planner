using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace API.Migrations
{
    /// <inheritdoc />
    public partial class AddedClerkIdToUser : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SavingsBalance");

            migrationBuilder.AddColumn<string>(
                name: "ClerkId",
                table: "Users",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "SavingsBalanceModel",
                columns: table => new
                {
                    SavingsBalanceId = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SavingsId = table.Column<long>(type: "bigint", nullable: false),
                    Balance = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    SavingsModelSavingsId = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SavingsBalanceModel", x => x.SavingsBalanceId);
                    table.ForeignKey(
                        name: "FK_SavingsBalanceModel_Savings_SavingsModelSavingsId",
                        column: x => x.SavingsModelSavingsId,
                        principalTable: "Savings",
                        principalColumn: "SavingsId");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Users_ClerkId",
                table: "Users",
                column: "ClerkId",
                unique: true,
                filter: "[ClerkId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_SavingsBalanceModel_SavingsModelSavingsId",
                table: "SavingsBalanceModel",
                column: "SavingsModelSavingsId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SavingsBalanceModel");

            migrationBuilder.DropIndex(
                name: "IX_Users_ClerkId",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "ClerkId",
                table: "Users");

            migrationBuilder.CreateTable(
                name: "SavingsBalance",
                columns: table => new
                {
                    SavingsBalanceId = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Balance = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    SavingsId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SavingsBalance", x => x.SavingsBalanceId);
                    table.ForeignKey(
                        name: "FK_SavingsBalance_Savings_SavingsId",
                        column: x => x.SavingsId,
                        principalTable: "Savings",
                        principalColumn: "SavingsId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_SavingsBalance_SavingsId",
                table: "SavingsBalance",
                column: "SavingsId");
        }
    }
}
