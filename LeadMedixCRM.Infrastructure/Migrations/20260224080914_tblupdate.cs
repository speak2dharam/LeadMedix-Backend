using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LeadMedixCRM.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class tblupdate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_vILStatusMasters",
                table: "vILStatusMasters");

            migrationBuilder.DropPrimaryKey(
                name: "PK_quotationStatusMasters",
                table: "quotationStatusMasters");

            migrationBuilder.DropPrimaryKey(
                name: "PK_leadStatusMasters",
                table: "leadStatusMasters");

            migrationBuilder.DropPrimaryKey(
                name: "PK_leadRequirementTypeMasters",
                table: "leadRequirementTypeMasters");

            migrationBuilder.DropPrimaryKey(
                name: "PK_leadRequirementStatusMasters",
                table: "leadRequirementStatusMasters");

            migrationBuilder.DropPrimaryKey(
                name: "PK_leadDiscardReasonMasters",
                table: "leadDiscardReasonMasters");

            migrationBuilder.DropPrimaryKey(
                name: "PK_leadCloseReasonMasters",
                table: "leadCloseReasonMasters");

            migrationBuilder.DropPrimaryKey(
                name: "PK_hospitalReviewStatusMasters",
                table: "hospitalReviewStatusMasters");

            migrationBuilder.DropColumn(
                name: "ChangedAt",
                table: "leadAssignmentHistories");

            migrationBuilder.DropColumn(
                name: "ChangedByUserId",
                table: "leadAssignmentHistories");

            migrationBuilder.RenameTable(
                name: "vILStatusMasters",
                newName: "VILStatusMasters");

            migrationBuilder.RenameTable(
                name: "quotationStatusMasters",
                newName: "QuotationStatusMasters");

            migrationBuilder.RenameTable(
                name: "leadStatusMasters",
                newName: "LeadStatusMasters");

            migrationBuilder.RenameTable(
                name: "leadRequirementTypeMasters",
                newName: "LeadRequirementTypeMasters");

            migrationBuilder.RenameTable(
                name: "leadRequirementStatusMasters",
                newName: "LeadRequirementStatusMasters");

            migrationBuilder.RenameTable(
                name: "leadDiscardReasonMasters",
                newName: "LeadDiscardReasonMasters");

            migrationBuilder.RenameTable(
                name: "leadCloseReasonMasters",
                newName: "LeadCloseReasonMasters");

            migrationBuilder.RenameTable(
                name: "hospitalReviewStatusMasters",
                newName: "HospitalReviewStatusMasters");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "VILStatusMasters",
                type: "nvarchar(200)",
                maxLength: 200,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "Code",
                table: "VILStatusMasters",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddColumn<int>(
                name: "UpdatedBy",
                table: "VILStatusMasters",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "UpdatedBy",
                table: "UserTokens",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "UpdatedBy",
                table: "Users",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "UpdatedBy",
                table: "UserRoles",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "UpdatedBy",
                table: "Treatments",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "UpdatedBy",
                table: "TreatmentCategories",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "UpdatedBy",
                table: "Roles",
                type: "int",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "QuotationStatusMasters",
                type: "nvarchar(200)",
                maxLength: 200,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "Code",
                table: "QuotationStatusMasters",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddColumn<int>(
                name: "UpdatedBy",
                table: "QuotationStatusMasters",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "UpdatedBy",
                table: "MediaFiles",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "UpdatedBy",
                table: "leadVILs",
                type: "int",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "LeadStatusMasters",
                type: "nvarchar(200)",
                maxLength: 200,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "Code",
                table: "LeadStatusMasters",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddColumn<int>(
                name: "UpdatedBy",
                table: "LeadStatusMasters",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "UpdatedBy",
                table: "LeadSources",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "UpdatedBy",
                table: "Leads",
                type: "int",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "LeadRequirementTypeMasters",
                type: "nvarchar(200)",
                maxLength: 200,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "Code",
                table: "LeadRequirementTypeMasters",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddColumn<int>(
                name: "UpdatedBy",
                table: "LeadRequirementTypeMasters",
                type: "int",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "LeadRequirementStatusMasters",
                type: "nvarchar(200)",
                maxLength: 200,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "Code",
                table: "LeadRequirementStatusMasters",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddColumn<int>(
                name: "UpdatedBy",
                table: "LeadRequirementStatusMasters",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "UpdatedBy",
                table: "leadRequirements",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "UpdatedBy",
                table: "leadQuotations",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "UpdatedBy",
                table: "leadHospitalReviews",
                type: "int",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "LeadDiscardReasonMasters",
                type: "nvarchar(200)",
                maxLength: 200,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "Code",
                table: "LeadDiscardReasonMasters",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddColumn<int>(
                name: "UpdatedBy",
                table: "LeadDiscardReasonMasters",
                type: "int",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "LeadCloseReasonMasters",
                type: "nvarchar(200)",
                maxLength: 200,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "Code",
                table: "LeadCloseReasonMasters",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddColumn<int>(
                name: "UpdatedBy",
                table: "LeadCloseReasonMasters",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "UpdatedBy",
                table: "leadAssignmentHistories",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "UpdatedBy",
                table: "LeadActivities",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "UpdatedBy",
                table: "Hospitals",
                type: "int",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "HospitalReviewStatusMasters",
                type: "nvarchar(200)",
                maxLength: 200,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "Code",
                table: "HospitalReviewStatusMasters",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddColumn<int>(
                name: "UpdatedBy",
                table: "HospitalReviewStatusMasters",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "UpdatedBy",
                table: "HospitalAccreditations",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "UpdatedBy",
                table: "DoctorSpecialization",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "UpdatedBy",
                table: "DoctorPublication",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "UpdatedBy",
                table: "DoctorMembership",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "UpdatedBy",
                table: "DoctorFellowship",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "UpdatedBy",
                table: "DoctorEducation",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "UpdatedBy",
                table: "DoctorAward",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "UpdatedBy",
                table: "Countries",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "UpdatedBy",
                table: "Cities",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "UpdatedBy",
                table: "Accreditations",
                type: "int",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_VILStatusMasters",
                table: "VILStatusMasters",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_QuotationStatusMasters",
                table: "QuotationStatusMasters",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_LeadStatusMasters",
                table: "LeadStatusMasters",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_LeadRequirementTypeMasters",
                table: "LeadRequirementTypeMasters",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_LeadRequirementStatusMasters",
                table: "LeadRequirementStatusMasters",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_LeadDiscardReasonMasters",
                table: "LeadDiscardReasonMasters",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_LeadCloseReasonMasters",
                table: "LeadCloseReasonMasters",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_HospitalReviewStatusMasters",
                table: "HospitalReviewStatusMasters",
                column: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_VILStatusMasters",
                table: "VILStatusMasters");

            migrationBuilder.DropPrimaryKey(
                name: "PK_QuotationStatusMasters",
                table: "QuotationStatusMasters");

            migrationBuilder.DropPrimaryKey(
                name: "PK_LeadStatusMasters",
                table: "LeadStatusMasters");

            migrationBuilder.DropPrimaryKey(
                name: "PK_LeadRequirementTypeMasters",
                table: "LeadRequirementTypeMasters");

            migrationBuilder.DropPrimaryKey(
                name: "PK_LeadRequirementStatusMasters",
                table: "LeadRequirementStatusMasters");

            migrationBuilder.DropPrimaryKey(
                name: "PK_LeadDiscardReasonMasters",
                table: "LeadDiscardReasonMasters");

            migrationBuilder.DropPrimaryKey(
                name: "PK_LeadCloseReasonMasters",
                table: "LeadCloseReasonMasters");

            migrationBuilder.DropPrimaryKey(
                name: "PK_HospitalReviewStatusMasters",
                table: "HospitalReviewStatusMasters");

            migrationBuilder.DropColumn(
                name: "UpdatedBy",
                table: "VILStatusMasters");

            migrationBuilder.DropColumn(
                name: "UpdatedBy",
                table: "UserTokens");

            migrationBuilder.DropColumn(
                name: "UpdatedBy",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "UpdatedBy",
                table: "UserRoles");

            migrationBuilder.DropColumn(
                name: "UpdatedBy",
                table: "Treatments");

            migrationBuilder.DropColumn(
                name: "UpdatedBy",
                table: "TreatmentCategories");

            migrationBuilder.DropColumn(
                name: "UpdatedBy",
                table: "Roles");

            migrationBuilder.DropColumn(
                name: "UpdatedBy",
                table: "QuotationStatusMasters");

            migrationBuilder.DropColumn(
                name: "UpdatedBy",
                table: "MediaFiles");

            migrationBuilder.DropColumn(
                name: "UpdatedBy",
                table: "leadVILs");

            migrationBuilder.DropColumn(
                name: "UpdatedBy",
                table: "LeadStatusMasters");

            migrationBuilder.DropColumn(
                name: "UpdatedBy",
                table: "LeadSources");

            migrationBuilder.DropColumn(
                name: "UpdatedBy",
                table: "Leads");

            migrationBuilder.DropColumn(
                name: "UpdatedBy",
                table: "LeadRequirementTypeMasters");

            migrationBuilder.DropColumn(
                name: "UpdatedBy",
                table: "LeadRequirementStatusMasters");

            migrationBuilder.DropColumn(
                name: "UpdatedBy",
                table: "leadRequirements");

            migrationBuilder.DropColumn(
                name: "UpdatedBy",
                table: "leadQuotations");

            migrationBuilder.DropColumn(
                name: "UpdatedBy",
                table: "leadHospitalReviews");

            migrationBuilder.DropColumn(
                name: "UpdatedBy",
                table: "LeadDiscardReasonMasters");

            migrationBuilder.DropColumn(
                name: "UpdatedBy",
                table: "LeadCloseReasonMasters");

            migrationBuilder.DropColumn(
                name: "UpdatedBy",
                table: "leadAssignmentHistories");

            migrationBuilder.DropColumn(
                name: "UpdatedBy",
                table: "LeadActivities");

            migrationBuilder.DropColumn(
                name: "UpdatedBy",
                table: "Hospitals");

            migrationBuilder.DropColumn(
                name: "UpdatedBy",
                table: "HospitalReviewStatusMasters");

            migrationBuilder.DropColumn(
                name: "UpdatedBy",
                table: "HospitalAccreditations");

            migrationBuilder.DropColumn(
                name: "UpdatedBy",
                table: "DoctorSpecialization");

            migrationBuilder.DropColumn(
                name: "UpdatedBy",
                table: "DoctorPublication");

            migrationBuilder.DropColumn(
                name: "UpdatedBy",
                table: "DoctorMembership");

            migrationBuilder.DropColumn(
                name: "UpdatedBy",
                table: "DoctorFellowship");

            migrationBuilder.DropColumn(
                name: "UpdatedBy",
                table: "DoctorEducation");

            migrationBuilder.DropColumn(
                name: "UpdatedBy",
                table: "DoctorAward");

            migrationBuilder.DropColumn(
                name: "UpdatedBy",
                table: "Countries");

            migrationBuilder.DropColumn(
                name: "UpdatedBy",
                table: "Cities");

            migrationBuilder.DropColumn(
                name: "UpdatedBy",
                table: "Accreditations");

            migrationBuilder.RenameTable(
                name: "VILStatusMasters",
                newName: "vILStatusMasters");

            migrationBuilder.RenameTable(
                name: "QuotationStatusMasters",
                newName: "quotationStatusMasters");

            migrationBuilder.RenameTable(
                name: "LeadStatusMasters",
                newName: "leadStatusMasters");

            migrationBuilder.RenameTable(
                name: "LeadRequirementTypeMasters",
                newName: "leadRequirementTypeMasters");

            migrationBuilder.RenameTable(
                name: "LeadRequirementStatusMasters",
                newName: "leadRequirementStatusMasters");

            migrationBuilder.RenameTable(
                name: "LeadDiscardReasonMasters",
                newName: "leadDiscardReasonMasters");

            migrationBuilder.RenameTable(
                name: "LeadCloseReasonMasters",
                newName: "leadCloseReasonMasters");

            migrationBuilder.RenameTable(
                name: "HospitalReviewStatusMasters",
                newName: "hospitalReviewStatusMasters");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "vILStatusMasters",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(200)",
                oldMaxLength: 200);

            migrationBuilder.AlterColumn<string>(
                name: "Code",
                table: "vILStatusMasters",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "quotationStatusMasters",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(200)",
                oldMaxLength: 200);

            migrationBuilder.AlterColumn<string>(
                name: "Code",
                table: "quotationStatusMasters",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "leadStatusMasters",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(200)",
                oldMaxLength: 200);

            migrationBuilder.AlterColumn<string>(
                name: "Code",
                table: "leadStatusMasters",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "leadRequirementTypeMasters",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(200)",
                oldMaxLength: 200);

            migrationBuilder.AlterColumn<string>(
                name: "Code",
                table: "leadRequirementTypeMasters",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "leadRequirementStatusMasters",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(200)",
                oldMaxLength: 200);

            migrationBuilder.AlterColumn<string>(
                name: "Code",
                table: "leadRequirementStatusMasters",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "leadDiscardReasonMasters",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(200)",
                oldMaxLength: 200);

            migrationBuilder.AlterColumn<string>(
                name: "Code",
                table: "leadDiscardReasonMasters",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "leadCloseReasonMasters",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(200)",
                oldMaxLength: 200);

            migrationBuilder.AlterColumn<string>(
                name: "Code",
                table: "leadCloseReasonMasters",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100);

            migrationBuilder.AddColumn<DateTime>(
                name: "ChangedAt",
                table: "leadAssignmentHistories",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<int>(
                name: "ChangedByUserId",
                table: "leadAssignmentHistories",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "hospitalReviewStatusMasters",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(200)",
                oldMaxLength: 200);

            migrationBuilder.AlterColumn<string>(
                name: "Code",
                table: "hospitalReviewStatusMasters",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100);

            migrationBuilder.AddPrimaryKey(
                name: "PK_vILStatusMasters",
                table: "vILStatusMasters",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_quotationStatusMasters",
                table: "quotationStatusMasters",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_leadStatusMasters",
                table: "leadStatusMasters",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_leadRequirementTypeMasters",
                table: "leadRequirementTypeMasters",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_leadRequirementStatusMasters",
                table: "leadRequirementStatusMasters",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_leadDiscardReasonMasters",
                table: "leadDiscardReasonMasters",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_leadCloseReasonMasters",
                table: "leadCloseReasonMasters",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_hospitalReviewStatusMasters",
                table: "hospitalReviewStatusMasters",
                column: "Id");
        }
    }
}
