using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SmartCampusMVC.Migrations
{
    /// <inheritdoc />
    public partial class AddRejectionReasonToConsultation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "RejectionReason",
                table: "Consultations",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "RejectionReason",
                table: "Consultations");
        }
    }
}
