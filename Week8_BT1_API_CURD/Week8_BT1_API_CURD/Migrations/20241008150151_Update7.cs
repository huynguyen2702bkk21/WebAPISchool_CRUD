using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Week8_BT1_API_CURD.Migrations
{
    /// <inheritdoc />
    public partial class Update7 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Users",
                table: "Users");

            // Xóa cột Id cũ
            migrationBuilder.DropColumn(
                name: "Id",
                table: "Users");

            // Thêm cột Id mới với thuộc tính IDENTITY
            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "Users",
                type: "int",
                nullable: false,
                defaultValue: 0) // Đặt giá trị mặc định là 0
                .Annotation("SqlServer:Identity", "1, 1"); // Đặt thuộc tính IDENTITY

            // Thêm cột ClassName vào bảng Students
            migrationBuilder.AddColumn<string>(
                name: "ClassName",
                table: "Students",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            // Thêm lại khóa chính cho bảng Users
            migrationBuilder.AddPrimaryKey(
                name: "PK_Users",
                table: "Users",
                column: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Users",
                table: "Users");

            // Xóa cột ClassName khỏi bảng Students
            migrationBuilder.DropColumn(
                name: "ClassName",
                table: "Students");

            // Xóa cột Id mới
            migrationBuilder.DropColumn(
                name: "Id",
                table: "Users");

            // Thêm cột Id cũ (nếu có) lại mà không có thuộc tính IDENTITY
            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "Users",
                type: "int",
                nullable: false,
                defaultValue: 0); // Đặt giá trị mặc định là 0

            // Thêm lại khóa chính cho bảng Users với cột Username
            migrationBuilder.AddPrimaryKey(
                name: "PK_Users",
                table: "Users",
                column: "Username");
        }
    }
}
