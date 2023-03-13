using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MyTasks.Migrations
{
    /// <inheritdoc />
    public partial class AddedSubtaskRelation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "HasSubTasks",
                table: "Task",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<Guid>(
                name: "ParentTaskId",
                table: "Task",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Task_ParentTaskId",
                table: "Task",
                column: "ParentTaskId");

            migrationBuilder.AddForeignKey(
                name: "FK_Task_Task_ParentTaskId",
                table: "Task",
                column: "ParentTaskId",
                principalTable: "Task",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Task_Task_ParentTaskId",
                table: "Task");

            migrationBuilder.DropIndex(
                name: "IX_Task_ParentTaskId",
                table: "Task");

            migrationBuilder.DropColumn(
                name: "HasSubTasks",
                table: "Task");

            migrationBuilder.DropColumn(
                name: "ParentTaskId",
                table: "Task");
        }
    }
}
