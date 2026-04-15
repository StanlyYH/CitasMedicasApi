using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CitasMedicasApi.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "health_centers",
                columns: table => new
                {
                    id = table.Column<string>(type: "TEXT", nullable: false),
                    name = table.Column<string>(type: "TEXT", maxLength: 150, nullable: false),
                    address = table.Column<string>(type: "TEXT", maxLength: 250, nullable: false),
                    municipality = table.Column<string>(type: "TEXT", maxLength: 100, nullable: false),
                    department = table.Column<string>(type: "TEXT", maxLength: 100, nullable: false),
                    phone_number = table.Column<string>(type: "TEXT", maxLength: 20, nullable: false),
                    status = table.Column<string>(type: "TEXT", maxLength: 20, nullable: false),
                    created_by_id = table.Column<string>(type: "TEXT", nullable: true),
                    created_date = table.Column<DateTime>(type: "TEXT", nullable: false),
                    updated_by_id = table.Column<string>(type: "TEXT", nullable: true),
                    updated_date = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_health_centers", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "patients",
                columns: table => new
                {
                    id = table.Column<string>(type: "TEXT", nullable: false),
                    dni = table.Column<string>(type: "TEXT", maxLength: 13, nullable: false),
                    first_name = table.Column<string>(type: "TEXT", maxLength: 40, nullable: false),
                    last_name = table.Column<string>(type: "TEXT", maxLength: 40, nullable: false),
                    birth_date = table.Column<DateTime>(type: "TEXT", nullable: false),
                    gender = table.Column<string>(type: "TEXT", nullable: true),
                    phone = table.Column<string>(type: "TEXT", nullable: true),
                    address = table.Column<string>(type: "TEXT", nullable: true),
                    created_by_id = table.Column<string>(type: "TEXT", nullable: true),
                    created_date = table.Column<DateTime>(type: "TEXT", nullable: false),
                    updated_by_id = table.Column<string>(type: "TEXT", nullable: true),
                    updated_date = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_patients", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "doctors",
                columns: table => new
                {
                    id = table.Column<string>(type: "TEXT", nullable: false),
                    first_name = table.Column<string>(type: "TEXT", maxLength: 40, nullable: false),
                    last_name = table.Column<string>(type: "TEXT", maxLength: 40, nullable: false),
                    phone_number = table.Column<string>(type: "TEXT", maxLength: 20, nullable: false),
                    email = table.Column<string>(type: "TEXT", maxLength: 100, nullable: false),
                    specialty = table.Column<string>(type: "TEXT", maxLength: 100, nullable: false),
                    health_center_id = table.Column<string>(type: "TEXT", nullable: false),
                    status = table.Column<string>(type: "TEXT", maxLength: 20, nullable: false),
                    created_by_id = table.Column<string>(type: "TEXT", nullable: true),
                    created_date = table.Column<DateTime>(type: "TEXT", nullable: false),
                    updated_by_id = table.Column<string>(type: "TEXT", nullable: true),
                    updated_date = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_doctors", x => x.id);
                    table.ForeignKey(
                        name: "FK_doctors_health_centers_health_center_id",
                        column: x => x.health_center_id,
                        principalTable: "health_centers",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "appointments",
                columns: table => new
                {
                    id = table.Column<string>(type: "TEXT", nullable: false),
                    patient_id = table.Column<string>(type: "TEXT", nullable: false),
                    doctor_id = table.Column<string>(type: "TEXT", nullable: false),
                    appointment_date = table.Column<DateTime>(type: "TEXT", nullable: false),
                    appointment_time = table.Column<string>(type: "TEXT", maxLength: 10, nullable: false),
                    reason = table.Column<string>(type: "TEXT", maxLength: 250, nullable: false),
                    status = table.Column<string>(type: "TEXT", maxLength: 20, nullable: false),
                    notes = table.Column<string>(type: "TEXT", maxLength: 500, nullable: true),
                    created_by_id = table.Column<string>(type: "TEXT", nullable: true),
                    created_date = table.Column<DateTime>(type: "TEXT", nullable: false),
                    updated_by_id = table.Column<string>(type: "TEXT", nullable: true),
                    updated_date = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_appointments", x => x.id);
                    table.ForeignKey(
                        name: "FK_appointments_doctors_doctor_id",
                        column: x => x.doctor_id,
                        principalTable: "doctors",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_appointments_patients_patient_id",
                        column: x => x.patient_id,
                        principalTable: "patients",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_appointments_doctor_id",
                table: "appointments",
                column: "doctor_id");

            migrationBuilder.CreateIndex(
                name: "IX_appointments_patient_id",
                table: "appointments",
                column: "patient_id");

            migrationBuilder.CreateIndex(
                name: "IX_doctors_health_center_id",
                table: "doctors",
                column: "health_center_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "appointments");

            migrationBuilder.DropTable(
                name: "doctors");

            migrationBuilder.DropTable(
                name: "patients");

            migrationBuilder.DropTable(
                name: "health_centers");
        }
    }
}
