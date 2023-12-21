using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebProje.Migrations
{
    public partial class removeInfoFromWorkingTimes : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_WorkingTimes_Clinics_ClinicId",
                table: "WorkingTimes");

            migrationBuilder.DropForeignKey(
                name: "FK_WorkingTimes_Hospitals_HospitalId",
                table: "WorkingTimes");

            migrationBuilder.DropIndex(
                name: "IX_WorkingTimes_ClinicId",
                table: "WorkingTimes");

            migrationBuilder.DropIndex(
                name: "IX_WorkingTimes_HospitalId",
                table: "WorkingTimes");

            migrationBuilder.DropColumn(
                name: "ClinicId",
                table: "WorkingTimes");

            migrationBuilder.DropColumn(
                name: "HospitalId",
                table: "WorkingTimes");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ClinicId",
                table: "WorkingTimes",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "HospitalId",
                table: "WorkingTimes",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_WorkingTimes_ClinicId",
                table: "WorkingTimes",
                column: "ClinicId");

            migrationBuilder.CreateIndex(
                name: "IX_WorkingTimes_HospitalId",
                table: "WorkingTimes",
                column: "HospitalId");

            migrationBuilder.AddForeignKey(
                name: "FK_WorkingTimes_Clinics_ClinicId",
                table: "WorkingTimes",
                column: "ClinicId",
                principalTable: "Clinics",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_WorkingTimes_Hospitals_HospitalId",
                table: "WorkingTimes",
                column: "HospitalId",
                principalTable: "Hospitals",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
