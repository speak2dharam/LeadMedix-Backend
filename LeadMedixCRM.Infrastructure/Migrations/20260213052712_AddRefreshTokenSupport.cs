using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LeadMedixCRM.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddRefreshTokenSupport : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Token",
                table: "UserTokens",
                newName: "RefreshToken");

            migrationBuilder.RenameColumn(
                name: "ExpiresAt",
                table: "UserTokens",
                newName: "RefreshTokenExpiresAt");

            migrationBuilder.AddColumn<string>(
                name: "AccessToken",
                table: "UserTokens",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateTime>(
                name: "AccessTokenExpiresAt",
                table: "UserTokens",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AccessToken",
                table: "UserTokens");

            migrationBuilder.DropColumn(
                name: "AccessTokenExpiresAt",
                table: "UserTokens");

            migrationBuilder.RenameColumn(
                name: "RefreshTokenExpiresAt",
                table: "UserTokens",
                newName: "ExpiresAt");

            migrationBuilder.RenameColumn(
                name: "RefreshToken",
                table: "UserTokens",
                newName: "Token");
        }
    }
}
