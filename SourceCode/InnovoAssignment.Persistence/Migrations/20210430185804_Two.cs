using Microsoft.EntityFrameworkCore.Migrations;

namespace InnovoAssignment.Persistence.Migrations
{
    public partial class Two : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Boards_Organizations_UserId",
                table: "Boards");

            migrationBuilder.DropForeignKey(
                name: "FK_Standards_Organizations_UserId",
                table: "Standards");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Standards",
                table: "Standards");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Organizations",
                table: "Organizations");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Boards",
                table: "Boards");

            migrationBuilder.RenameTable(
                name: "Standards",
                newName: "UserAddresses");

            migrationBuilder.RenameTable(
                name: "Organizations",
                newName: "Users");

            migrationBuilder.RenameTable(
                name: "Boards",
                newName: "UserPreferences");

            migrationBuilder.RenameIndex(
                name: "IX_Standards_UserId",
                table: "UserAddresses",
                newName: "IX_UserAddresses_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_Boards_UserId",
                table: "UserPreferences",
                newName: "IX_UserPreferences_UserId");

            migrationBuilder.AlterColumn<string>(
                name: "FullName",
                table: "Users",
                type: "nvarchar(60)",
                maxLength: 60,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(60)",
                oldMaxLength: 60);

            migrationBuilder.AddPrimaryKey(
                name: "PK_UserAddresses",
                table: "UserAddresses",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Users",
                table: "Users",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_UserPreferences",
                table: "UserPreferences",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_UserAddresses_Users_UserId",
                table: "UserAddresses",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UserPreferences_Users_UserId",
                table: "UserPreferences",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserAddresses_Users_UserId",
                table: "UserAddresses");

            migrationBuilder.DropForeignKey(
                name: "FK_UserPreferences_Users_UserId",
                table: "UserPreferences");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Users",
                table: "Users");

            migrationBuilder.DropPrimaryKey(
                name: "PK_UserPreferences",
                table: "UserPreferences");

            migrationBuilder.DropPrimaryKey(
                name: "PK_UserAddresses",
                table: "UserAddresses");

            migrationBuilder.RenameTable(
                name: "Users",
                newName: "Organizations");

            migrationBuilder.RenameTable(
                name: "UserPreferences",
                newName: "Boards");

            migrationBuilder.RenameTable(
                name: "UserAddresses",
                newName: "Standards");

            migrationBuilder.RenameIndex(
                name: "IX_UserPreferences_UserId",
                table: "Boards",
                newName: "IX_Boards_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_UserAddresses_UserId",
                table: "Standards",
                newName: "IX_Standards_UserId");

            migrationBuilder.AlterColumn<string>(
                name: "FullName",
                table: "Organizations",
                type: "nvarchar(60)",
                maxLength: 60,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(60)",
                oldMaxLength: 60,
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Organizations",
                table: "Organizations",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Boards",
                table: "Boards",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Standards",
                table: "Standards",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Boards_Organizations_UserId",
                table: "Boards",
                column: "UserId",
                principalTable: "Organizations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Standards_Organizations_UserId",
                table: "Standards",
                column: "UserId",
                principalTable: "Organizations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
