using Microsoft.EntityFrameworkCore.Migrations;

namespace Demo.DAL.Migrations
{
    public partial class EmployeeDepartmentRelationships : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "DepartmentsId",
                table: "employees",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_employees_DepartmentsId",
                table: "employees",
                column: "DepartmentsId");

            migrationBuilder.AddForeignKey(
                name: "FK_employees_departments_DepartmentsId",
                table: "employees",
                column: "DepartmentsId",
                principalTable: "departments",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_employees_departments_DepartmentsId",
                table: "employees");

            migrationBuilder.DropIndex(
                name: "IX_employees_DepartmentsId",
                table: "employees");

            migrationBuilder.DropColumn(
                name: "DepartmentsId",
                table: "employees");
        }
    }
}
