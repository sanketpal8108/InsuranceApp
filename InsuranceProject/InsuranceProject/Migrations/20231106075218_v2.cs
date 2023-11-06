using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace InsuranceProject.Migrations
{
    /// <inheritdoc />
    public partial class v2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "QueryDate",
                table: "Queries",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "PaidDate",
                table: "PolicyPayments",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "WithdrawalDate",
                table: "PolicyClaims",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "InsuranceCreationDate",
                table: "CustomerInsuranceAccounts",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "RequestDate",
                table: "CommisionWithdrawals",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "QueryDate",
                table: "Queries");

            migrationBuilder.DropColumn(
                name: "PaidDate",
                table: "PolicyPayments");

            migrationBuilder.DropColumn(
                name: "WithdrawalDate",
                table: "PolicyClaims");

            migrationBuilder.DropColumn(
                name: "InsuranceCreationDate",
                table: "CustomerInsuranceAccounts");

            migrationBuilder.DropColumn(
                name: "RequestDate",
                table: "CommisionWithdrawals");
        }
    }
}
