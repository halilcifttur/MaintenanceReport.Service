using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TrackerApp.Migrations
{
    public partial class UpdateMaintenanceTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "MaintenanceType",
                table: "MaintenanceEntities");

            migrationBuilder.AddColumn<int>(
                name: "MaintenanceType",
                table: "Maintenances",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Status",
                table: "Maintenances",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<Guid>(
                name: "CheckinId",
                table: "MaintenanceEntities",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "MaintenanceType",
                table: "Maintenances");

            migrationBuilder.DropColumn(
                name: "Status",
                table: "Maintenances");

            migrationBuilder.DropColumn(
                name: "CheckinId",
                table: "MaintenanceEntities");

            migrationBuilder.AddColumn<int>(
                name: "MaintenanceType",
                table: "MaintenanceEntities",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
