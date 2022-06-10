using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace TaskManager.Migrations
{
    public partial class ProjectEditField : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DoneDate",
                table: "Project");

            migrationBuilder.RenameColumn(
                name: "InProgressDate",
                table: "Project",
                newName: "FinishDate");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "FinishDate",
                table: "Project",
                newName: "InProgressDate");

            migrationBuilder.AddColumn<DateTime>(
                name: "DoneDate",
                table: "Project",
                type: "timestamp without time zone",
                nullable: true);
        }
    }
}
