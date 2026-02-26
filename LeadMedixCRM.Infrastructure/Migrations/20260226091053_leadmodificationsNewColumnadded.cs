using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LeadMedixCRM.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class leadmodificationsNewColumnadded : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "StatusBeforeClose",
                table: "Leads",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "StatusBeforeDiscard",
                table: "Leads",
                type: "int",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "StatusBeforeClose",
                table: "Leads");

            migrationBuilder.DropColumn(
                name: "StatusBeforeDiscard",
                table: "Leads");
        }
    }
}
