using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LeadMedixCRM.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class leadmodifications : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Leads_Status_Temperature",
                table: "Leads");

            migrationBuilder.DropColumn(
                name: "CreatedByUserId",
                table: "LeadActivities");

            migrationBuilder.RenameColumn(
                name: "CategoryId",
                table: "Treatments",
                newName: "TreatmentCategoryId");

            migrationBuilder.RenameColumn(
                name: "SourceId",
                table: "Leads",
                newName: "TreatmentCategoryId");

            migrationBuilder.RenameColumn(
                name: "Type",
                table: "LeadActivities",
                newName: "ActivityType");

            migrationBuilder.RenameColumn(
                name: "Notes",
                table: "LeadActivities",
                newName: "Title");

            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                table: "Treatments",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<int>(
                name: "SortOrder",
                table: "Treatments",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                table: "TreatmentCategories",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<int>(
                name: "SortOrder",
                table: "TreatmentCategories",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<string>(
                name: "PhoneNormalized",
                table: "Leads",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AlterColumn<string>(
                name: "Phone",
                table: "Leads",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddColumn<int>(
                name: "CityId",
                table: "Leads",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CloseRemarks",
                table: "Leads",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "DiscardRemarks",
                table: "Leads",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Enquiry",
                table: "Leads",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsClosed",
                table: "Leads",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsDiscarded",
                table: "Leads",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "LastActivityAt",
                table: "Leads",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "LeadSourceId",
                table: "Leads",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Notes",
                table: "Leads",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "HospitalId",
                table: "LeadActivities",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "HospitalReviewId",
                table: "LeadActivities",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsImportant",
                table: "LeadActivities",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<int>(
                name: "MediaId",
                table: "LeadActivities",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "PerformedByUserId",
                table: "LeadActivities",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "QuotationId",
                table: "LeadActivities",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "RequirementId",
                table: "LeadActivities",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Summary",
                table: "LeadActivities",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "VILId",
                table: "LeadActivities",
                type: "int",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsActive",
                table: "Treatments");

            migrationBuilder.DropColumn(
                name: "SortOrder",
                table: "Treatments");

            migrationBuilder.DropColumn(
                name: "IsActive",
                table: "TreatmentCategories");

            migrationBuilder.DropColumn(
                name: "SortOrder",
                table: "TreatmentCategories");

            migrationBuilder.DropColumn(
                name: "CityId",
                table: "Leads");

            migrationBuilder.DropColumn(
                name: "CloseRemarks",
                table: "Leads");

            migrationBuilder.DropColumn(
                name: "DiscardRemarks",
                table: "Leads");

            migrationBuilder.DropColumn(
                name: "Enquiry",
                table: "Leads");

            migrationBuilder.DropColumn(
                name: "IsClosed",
                table: "Leads");

            migrationBuilder.DropColumn(
                name: "IsDiscarded",
                table: "Leads");

            migrationBuilder.DropColumn(
                name: "LastActivityAt",
                table: "Leads");

            migrationBuilder.DropColumn(
                name: "LeadSourceId",
                table: "Leads");

            migrationBuilder.DropColumn(
                name: "Notes",
                table: "Leads");

            migrationBuilder.DropColumn(
                name: "HospitalId",
                table: "LeadActivities");

            migrationBuilder.DropColumn(
                name: "HospitalReviewId",
                table: "LeadActivities");

            migrationBuilder.DropColumn(
                name: "IsImportant",
                table: "LeadActivities");

            migrationBuilder.DropColumn(
                name: "MediaId",
                table: "LeadActivities");

            migrationBuilder.DropColumn(
                name: "PerformedByUserId",
                table: "LeadActivities");

            migrationBuilder.DropColumn(
                name: "QuotationId",
                table: "LeadActivities");

            migrationBuilder.DropColumn(
                name: "RequirementId",
                table: "LeadActivities");

            migrationBuilder.DropColumn(
                name: "Summary",
                table: "LeadActivities");

            migrationBuilder.DropColumn(
                name: "VILId",
                table: "LeadActivities");

            migrationBuilder.RenameColumn(
                name: "TreatmentCategoryId",
                table: "Treatments",
                newName: "CategoryId");

            migrationBuilder.RenameColumn(
                name: "TreatmentCategoryId",
                table: "Leads",
                newName: "SourceId");

            migrationBuilder.RenameColumn(
                name: "Title",
                table: "LeadActivities",
                newName: "Notes");

            migrationBuilder.RenameColumn(
                name: "ActivityType",
                table: "LeadActivities",
                newName: "Type");

            migrationBuilder.AlterColumn<string>(
                name: "PhoneNormalized",
                table: "Leads",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Phone",
                table: "Leads",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "CreatedByUserId",
                table: "LeadActivities",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Leads_Status_Temperature",
                table: "Leads",
                columns: new[] { "Status", "Temperature" });
        }
    }
}
