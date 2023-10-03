using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BookingManagementApp.Migrations
{
    public partial class NIK : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Name",
                table: "tb_m_universities",
                newName: "name");

            migrationBuilder.AlterColumn<string>(
                name: "nik",
                table: "tb_m_employees",
                type: "nvarchar(100)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(1)");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "name",
                table: "tb_m_universities",
                newName: "Name");

            migrationBuilder.AlterColumn<string>(
                name: "nik",
                table: "tb_m_employees",
                type: "nvarchar(1)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(100)");
        }
    }
}
