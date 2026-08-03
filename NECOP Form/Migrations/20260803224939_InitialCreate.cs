using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace NECOP_Form.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Department",
                columns: table => new
                {
                    DepartmentId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    DepartmentName = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Department", x => x.DepartmentId);
                });

            migrationBuilder.CreateTable(
                name: "Designation",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    DepartmentId = table.Column<int>(type: "integer", nullable: false),
                    RefNo = table.Column<string>(type: "text", nullable: false),
                    RecordedBy = table.Column<string>(type: "text", nullable: true),
                    Officer = table.Column<string>(type: "text", nullable: false),
                    name_tasked = table.Column<string>(type: "text", nullable: false),
                    Designation = table.Column<string>(type: "text", nullable: false),
                    details = table.Column<string>(type: "text", nullable: true),
                    verify = table.Column<string>(type: "text", nullable: true),
                    estimated_cost = table.Column<decimal>(type: "numeric", nullable: false),
                    registration_type = table.Column<string>(type: "text", nullable: true),
                    sanction_type = table.Column<string>(type: "text", nullable: false),
                    OtherBudgetDetails = table.Column<string>(type: "text", nullable: true),
                    technical = table.Column<string>(type: "text", nullable: true),
                    execution_officer_incharge = table.Column<string>(type: "text", nullable: false),
                    deployed_staff = table.Column<string>(type: "text", nullable: true),
                    end_user_feedback = table.Column<string>(type: "text", nullable: true),
                    created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    ImagePath = table.Column<string>(type: "text", nullable: true),
                    FilePath = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Designation", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Designation_Department_DepartmentId",
                        column: x => x.DepartmentId,
                        principalTable: "Department",
                        principalColumn: "DepartmentId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "designations",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: false),
                    DepartmentId = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_designations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_designations_Department_DepartmentId",
                        column: x => x.DepartmentId,
                        principalTable: "Department",
                        principalColumn: "DepartmentId");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Designation_DepartmentId",
                table: "Designation",
                column: "DepartmentId");

            migrationBuilder.CreateIndex(
                name: "IX_designations_DepartmentId",
                table: "designations",
                column: "DepartmentId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Designation");

            migrationBuilder.DropTable(
                name: "designations");

            migrationBuilder.DropTable(
                name: "Department");
        }
    }
}
