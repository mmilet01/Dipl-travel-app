using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Persistance.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "userRelationshipStatus",
                columns: table => new
                {
                    statusId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    statusText = table.Column<string>(unicode: false, maxLength: 60, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__userRela__36257A187926388E", x => x.statusId);
                });

            migrationBuilder.CreateTable(
                name: "users",
                columns: table => new
                {
                    userId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    firstName = table.Column<string>(unicode: false, maxLength: 50, nullable: false),
                    lastName = table.Column<string>(unicode: false, maxLength: 50, nullable: false),
                    email = table.Column<string>(unicode: false, maxLength: 50, nullable: false),
                    passwordField = table.Column<string>(type: "text", nullable: false),
                    createdOn = table.Column<DateTime>(type: "date", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__users__CB9A1CFF19500154", x => x.userId);
                });

            migrationBuilder.CreateTable(
                name: "memories",
                columns: table => new
                {
                    memoryId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    title = table.Column<string>(unicode: false, maxLength: 50, nullable: false),
                    memoryDescription = table.Column<string>(unicode: false, maxLength: 500, nullable: false),
                    isPrivate = table.Column<bool>(nullable: false),
                    createdOn = table.Column<DateTime>(type: "date", nullable: false),
                    createdBy = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__memories__96A15C453954E06C", x => x.memoryId);
                    table.ForeignKey(
                        name: "FK_Memories_Users",
                        column: x => x.createdBy,
                        principalTable: "users",
                        principalColumn: "userId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "usersImages",
                columns: table => new
                {
                    userImageId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    photoPath = table.Column<string>(unicode: false, maxLength: 50, nullable: false),
                    belongsTo = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__usersIma__8480DF35A7CE6092", x => x.userImageId);
                    table.ForeignKey(
                        name: "FK_UserImages_Users",
                        column: x => x.belongsTo,
                        principalTable: "users",
                        principalColumn: "userId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "usersRelationship",
                columns: table => new
                {
                    relationShip = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    firstUserId = table.Column<int>(nullable: false),
                    secondUserId = table.Column<int>(nullable: false),
                    relationshipStatus = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__usersRel__711C4A15FCEEB959", x => x.relationShip);
                    table.ForeignKey(
                        name: "FK_FirstUser_Users",
                        column: x => x.firstUserId,
                        principalTable: "users",
                        principalColumn: "userId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_UserRelationshipStatus_Stauts",
                        column: x => x.relationshipStatus,
                        principalTable: "userRelationshipStatus",
                        principalColumn: "statusId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_SecondUser_Users",
                        column: x => x.secondUserId,
                        principalTable: "users",
                        principalColumn: "userId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "memoryImages",
                columns: table => new
                {
                    memoryImageId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    photoPath = table.Column<string>(unicode: false, maxLength: 50, nullable: false),
                    belongsTo = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__memoryIm__283B4828AC616ABB", x => x.memoryImageId);
                    table.ForeignKey(
                        name: "FK_MemoryImages_Users",
                        column: x => x.belongsTo,
                        principalTable: "memories",
                        principalColumn: "memoryId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "usersTaggedOnMemory",
                columns: table => new
                {
                    tagId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    memoryId = table.Column<int>(nullable: false),
                    userId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__usersTag__50FC015783197F91", x => x.tagId);
                    table.ForeignKey(
                        name: "FK_TaggedMemory_Users",
                        column: x => x.memoryId,
                        principalTable: "memories",
                        principalColumn: "memoryId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_TaggedUsers_Memory",
                        column: x => x.userId,
                        principalTable: "users",
                        principalColumn: "userId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_memories_createdBy",
                table: "memories",
                column: "createdBy");

            migrationBuilder.CreateIndex(
                name: "IX_memoryImages_belongsTo",
                table: "memoryImages",
                column: "belongsTo");

            migrationBuilder.CreateIndex(
                name: "IX_usersImages_belongsTo",
                table: "usersImages",
                column: "belongsTo");

            migrationBuilder.CreateIndex(
                name: "IX_usersRelationship_firstUserId",
                table: "usersRelationship",
                column: "firstUserId");

            migrationBuilder.CreateIndex(
                name: "IX_usersRelationship_relationshipStatus",
                table: "usersRelationship",
                column: "relationshipStatus");

            migrationBuilder.CreateIndex(
                name: "IX_usersRelationship_secondUserId",
                table: "usersRelationship",
                column: "secondUserId");

            migrationBuilder.CreateIndex(
                name: "IX_usersTaggedOnMemory_memoryId",
                table: "usersTaggedOnMemory",
                column: "memoryId");

            migrationBuilder.CreateIndex(
                name: "IX_usersTaggedOnMemory_userId",
                table: "usersTaggedOnMemory",
                column: "userId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "memoryImages");

            migrationBuilder.DropTable(
                name: "usersImages");

            migrationBuilder.DropTable(
                name: "usersRelationship");

            migrationBuilder.DropTable(
                name: "usersTaggedOnMemory");

            migrationBuilder.DropTable(
                name: "userRelationshipStatus");

            migrationBuilder.DropTable(
                name: "memories");

            migrationBuilder.DropTable(
                name: "users");
        }
    }
}
