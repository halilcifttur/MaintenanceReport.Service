using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TrackerApp.Migrations
{
    public partial class UpdateAnomalyTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "AnomalyEndDate",
                table: "Anomalies",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "AnomalyStartDate",
                table: "Anomalies",
                type: "datetime2",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AnomalyEndDate",
                table: "Anomalies");

            migrationBuilder.DropColumn(
                name: "AnomalyStartDate",
                table: "Anomalies");
        }
    }
}
