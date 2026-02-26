using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LeadMedixCRM.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class leadtblmodification : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "DuplicateOfLeadId",
                table: "Leads",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsDuplicate",
                table: "Leads",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DuplicateOfLeadId",
                table: "Leads");

            migrationBuilder.DropColumn(
                name: "IsDuplicate",
                table: "Leads");
        }
    }
}
