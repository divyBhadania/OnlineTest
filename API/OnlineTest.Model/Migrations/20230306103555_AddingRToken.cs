using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OnlineTest.Migrations
{
    /// <inheritdoc />
    public partial class AddingRToken : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserRoles_Roles_RoleId",
                table: "UserRoles");

            migrationBuilder.DropForeignKey(
                name: "FK_UserRoles_Users_UserId",
                table: "UserRoles");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "UserRoles",
                newName: "User_Id");

            migrationBuilder.RenameColumn(
                name: "RoleId",
                table: "UserRoles",
                newName: "Role_Id");

            migrationBuilder.RenameIndex(
                name: "IX_UserRoles_UserId",
                table: "UserRoles",
                newName: "IX_UserRoles_User_Id");

            migrationBuilder.RenameIndex(
                name: "IX_UserRoles_RoleId",
                table: "UserRoles",
                newName: "IX_UserRoles_Role_Id");

            migrationBuilder.RenameColumn(
                name: "RoleName",
                table: "Roles",
                newName: "Role_Name");

            migrationBuilder.CreateTable(
                name: "RToken",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Refresh_Token = table.Column<string>(type: "varchar(150)", nullable: false),
                    Is_Stop = table.Column<int>(type: "int", nullable: false),
                    Created_Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    User_Id = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RToken", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RToken_Users_User_Id",
                        column: x => x.User_Id,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_RToken_User_Id",
                table: "RToken",
                column: "User_Id");

            migrationBuilder.AddForeignKey(
                name: "FK_UserRoles_Roles_Role_Id",
                table: "UserRoles",
                column: "Role_Id",
                principalTable: "Roles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UserRoles_Users_User_Id",
                table: "UserRoles",
                column: "User_Id",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserRoles_Roles_Role_Id",
                table: "UserRoles");

            migrationBuilder.DropForeignKey(
                name: "FK_UserRoles_Users_User_Id",
                table: "UserRoles");

            migrationBuilder.DropTable(
                name: "RToken");

            migrationBuilder.RenameColumn(
                name: "User_Id",
                table: "UserRoles",
                newName: "UserId");

            migrationBuilder.RenameColumn(
                name: "Role_Id",
                table: "UserRoles",
                newName: "RoleId");

            migrationBuilder.RenameIndex(
                name: "IX_UserRoles_User_Id",
                table: "UserRoles",
                newName: "IX_UserRoles_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_UserRoles_Role_Id",
                table: "UserRoles",
                newName: "IX_UserRoles_RoleId");

            migrationBuilder.RenameColumn(
                name: "Role_Name",
                table: "Roles",
                newName: "RoleName");

            migrationBuilder.AddForeignKey(
                name: "FK_UserRoles_Roles_RoleId",
                table: "UserRoles",
                column: "RoleId",
                principalTable: "Roles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UserRoles_Users_UserId",
                table: "UserRoles",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
