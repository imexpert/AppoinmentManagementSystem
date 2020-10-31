using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace AppointmentSchedule.Api.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "appointment");

            migrationBuilder.CreateSequence(
                name: "appointmentseq",
                schema: "appointment",
                incrementBy: 10);

            migrationBuilder.CreateSequence(
                name: "citizenseq",
                schema: "appointment",
                incrementBy: 10);

            migrationBuilder.CreateSequence(
                name: "notificationseq",
                schema: "appointment",
                incrementBy: 10);

            migrationBuilder.CreateTable(
                name: "AppointmentTypes",
                schema: "appointment",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false, defaultValue: 1),
                    Name = table.Column<string>(maxLength: 200, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppointmentTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Citizens",
                schema: "appointment",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false),
                    Name = table.Column<string>(maxLength: 100, nullable: false),
                    Lastname = table.Column<string>(maxLength: 100, nullable: false),
                    TcIdentity = table.Column<string>(maxLength: 11, nullable: false),
                    PhoneNumber_CountryCode = table.Column<int>(nullable: true),
                    PhoneNumber_AreaCode = table.Column<int>(nullable: true),
                    PhoneNumber_Number = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Citizens", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Counties",
                schema: "appointment",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Counties", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "NotificationTypes",
                schema: "appointment",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false, defaultValue: 1),
                    Name = table.Column<string>(maxLength: 200, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NotificationTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "StatusTypes",
                schema: "appointment",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false, defaultValue: 1),
                    Name = table.Column<string>(maxLength: 200, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StatusTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Appointments",
                schema: "appointment",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false),
                    AppoinmentTime = table.Column<DateTime>(nullable: false),
                    CitizenId = table.Column<int>(nullable: false),
                    AppointmentTypeId = table.Column<int>(nullable: false),
                    StatusId = table.Column<int>(nullable: false),
                    CountyId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Appointments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Appointments_Citizens_CitizenId",
                        column: x => x.CitizenId,
                        principalSchema: "appointment",
                        principalTable: "Citizens",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Appointments_AppointmentTypes_AppointmentTypeId",
                        column: x => x.AppointmentTypeId,
                        principalSchema: "appointment",
                        principalTable: "AppointmentTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Appointments_Counties_CountyId",
                        column: x => x.CountyId,
                        principalSchema: "appointment",
                        principalTable: "Counties",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Appointments_StatusTypes_StatusId",
                        column: x => x.StatusId,
                        principalSchema: "appointment",
                        principalTable: "StatusTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Notifications",
                schema: "appointment",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false),
                    Content = table.Column<string>(maxLength: 100, nullable: false),
                    NotificationTypeId = table.Column<int>(nullable: false),
                    Source = table.Column<string>(maxLength: 100, nullable: false),
                    Destination_CountryCode = table.Column<int>(nullable: true),
                    Destination_AreaCode = table.Column<int>(nullable: true),
                    Destination_Number = table.Column<int>(nullable: true),
                    AppointmentId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Notifications", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Notifications_Appointments_AppointmentId",
                        column: x => x.AppointmentId,
                        principalSchema: "appointment",
                        principalTable: "Appointments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Notifications_NotificationTypes_NotificationTypeId",
                        column: x => x.NotificationTypeId,
                        principalSchema: "appointment",
                        principalTable: "NotificationTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Appointments_CitizenId",
                schema: "appointment",
                table: "Appointments",
                column: "CitizenId");

            migrationBuilder.CreateIndex(
                name: "IX_Appointments_AppointmentTypeId",
                schema: "appointment",
                table: "Appointments",
                column: "AppointmentTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Appointments_CountyId",
                schema: "appointment",
                table: "Appointments",
                column: "CountyId");

            migrationBuilder.CreateIndex(
                name: "IX_Appointments_StatusId",
                schema: "appointment",
                table: "Appointments",
                column: "StatusId");

            migrationBuilder.CreateIndex(
                name: "IX_Citizens_TcIdentity",
                schema: "appointment",
                table: "Citizens",
                column: "TcIdentity",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Notifications_AppointmentId",
                schema: "appointment",
                table: "Notifications",
                column: "AppointmentId");

            migrationBuilder.CreateIndex(
                name: "IX_Notifications_NotificationTypeId",
                schema: "appointment",
                table: "Notifications",
                column: "NotificationTypeId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Notifications",
                schema: "appointment");

            migrationBuilder.DropTable(
                name: "Appointments",
                schema: "appointment");

            migrationBuilder.DropTable(
                name: "NotificationTypes",
                schema: "appointment");

            migrationBuilder.DropTable(
                name: "Citizens",
                schema: "appointment");

            migrationBuilder.DropTable(
                name: "AppointmentTypes",
                schema: "appointment");

            migrationBuilder.DropTable(
                name: "Counties",
                schema: "appointment");

            migrationBuilder.DropTable(
                name: "StatusTypes",
                schema: "appointment");

            migrationBuilder.DropSequence(
                name: "appointmentseq",
                schema: "appointment");

            migrationBuilder.DropSequence(
                name: "citizenseq",
                schema: "appointment");

            migrationBuilder.DropSequence(
                name: "notificationseq",
                schema: "appointment");
        }
    }
}
