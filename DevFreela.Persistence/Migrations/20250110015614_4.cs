using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DevFreela.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class _4 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Project_User_IdClient",
                table: "Project");

            migrationBuilder.DropForeignKey(
                name: "FK_Project_User_IdFreelancer",
                table: "Project");

            migrationBuilder.DropForeignKey(
                name: "FK_ProjectComment_Project_IdProject",
                table: "ProjectComment");

            migrationBuilder.DropForeignKey(
                name: "FK_ProjectComment_User_IdUser",
                table: "ProjectComment");

            migrationBuilder.DropForeignKey(
                name: "FK_UserSkill_Skill_IdSkill",
                table: "UserSkill");

            migrationBuilder.DropForeignKey(
                name: "FK_UserSkill_User_IdUser",
                table: "UserSkill");

            migrationBuilder.RenameColumn(
                name: "IdUser",
                table: "UserSkill",
                newName: "UserId");

            migrationBuilder.RenameColumn(
                name: "IdSkill",
                table: "UserSkill",
                newName: "SkillId");

            migrationBuilder.RenameIndex(
                name: "IX_UserSkill_IdUser",
                table: "UserSkill",
                newName: "IX_UserSkill_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_UserSkill_IdSkill",
                table: "UserSkill",
                newName: "IX_UserSkill_SkillId");

            migrationBuilder.RenameColumn(
                name: "IdUser",
                table: "ProjectComment",
                newName: "UserId");

            migrationBuilder.RenameColumn(
                name: "IdProject",
                table: "ProjectComment",
                newName: "ProjectId");

            migrationBuilder.RenameIndex(
                name: "IX_ProjectComment_IdUser",
                table: "ProjectComment",
                newName: "IX_ProjectComment_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_ProjectComment_IdProject",
                table: "ProjectComment",
                newName: "IX_ProjectComment_ProjectId");

            migrationBuilder.RenameColumn(
                name: "IdFreelancer",
                table: "Project",
                newName: "FreelancerId");

            migrationBuilder.RenameColumn(
                name: "IdClient",
                table: "Project",
                newName: "ClientId");

            migrationBuilder.RenameIndex(
                name: "IX_Project_IdFreelancer",
                table: "Project",
                newName: "IX_Project_FreelancerId");

            migrationBuilder.RenameIndex(
                name: "IX_Project_IdClient",
                table: "Project",
                newName: "IX_Project_ClientId");

            migrationBuilder.AddForeignKey(
                name: "FK_Project_User_ClientId",
                table: "Project",
                column: "ClientId",
                principalTable: "User",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Project_User_FreelancerId",
                table: "Project",
                column: "FreelancerId",
                principalTable: "User",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ProjectComment_Project_ProjectId",
                table: "ProjectComment",
                column: "ProjectId",
                principalTable: "Project",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ProjectComment_User_UserId",
                table: "ProjectComment",
                column: "UserId",
                principalTable: "User",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_UserSkill_Skill_SkillId",
                table: "UserSkill",
                column: "SkillId",
                principalTable: "Skill",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_UserSkill_User_UserId",
                table: "UserSkill",
                column: "UserId",
                principalTable: "User",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Project_User_ClientId",
                table: "Project");

            migrationBuilder.DropForeignKey(
                name: "FK_Project_User_FreelancerId",
                table: "Project");

            migrationBuilder.DropForeignKey(
                name: "FK_ProjectComment_Project_ProjectId",
                table: "ProjectComment");

            migrationBuilder.DropForeignKey(
                name: "FK_ProjectComment_User_UserId",
                table: "ProjectComment");

            migrationBuilder.DropForeignKey(
                name: "FK_UserSkill_Skill_SkillId",
                table: "UserSkill");

            migrationBuilder.DropForeignKey(
                name: "FK_UserSkill_User_UserId",
                table: "UserSkill");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "UserSkill",
                newName: "IdUser");

            migrationBuilder.RenameColumn(
                name: "SkillId",
                table: "UserSkill",
                newName: "IdSkill");

            migrationBuilder.RenameIndex(
                name: "IX_UserSkill_UserId",
                table: "UserSkill",
                newName: "IX_UserSkill_IdUser");

            migrationBuilder.RenameIndex(
                name: "IX_UserSkill_SkillId",
                table: "UserSkill",
                newName: "IX_UserSkill_IdSkill");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "ProjectComment",
                newName: "IdUser");

            migrationBuilder.RenameColumn(
                name: "ProjectId",
                table: "ProjectComment",
                newName: "IdProject");

            migrationBuilder.RenameIndex(
                name: "IX_ProjectComment_UserId",
                table: "ProjectComment",
                newName: "IX_ProjectComment_IdUser");

            migrationBuilder.RenameIndex(
                name: "IX_ProjectComment_ProjectId",
                table: "ProjectComment",
                newName: "IX_ProjectComment_IdProject");

            migrationBuilder.RenameColumn(
                name: "FreelancerId",
                table: "Project",
                newName: "IdFreelancer");

            migrationBuilder.RenameColumn(
                name: "ClientId",
                table: "Project",
                newName: "IdClient");

            migrationBuilder.RenameIndex(
                name: "IX_Project_FreelancerId",
                table: "Project",
                newName: "IX_Project_IdFreelancer");

            migrationBuilder.RenameIndex(
                name: "IX_Project_ClientId",
                table: "Project",
                newName: "IX_Project_IdClient");

            migrationBuilder.AddForeignKey(
                name: "FK_Project_User_IdClient",
                table: "Project",
                column: "IdClient",
                principalTable: "User",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Project_User_IdFreelancer",
                table: "Project",
                column: "IdFreelancer",
                principalTable: "User",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ProjectComment_Project_IdProject",
                table: "ProjectComment",
                column: "IdProject",
                principalTable: "Project",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ProjectComment_User_IdUser",
                table: "ProjectComment",
                column: "IdUser",
                principalTable: "User",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_UserSkill_Skill_IdSkill",
                table: "UserSkill",
                column: "IdSkill",
                principalTable: "Skill",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_UserSkill_User_IdUser",
                table: "UserSkill",
                column: "IdUser",
                principalTable: "User",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
