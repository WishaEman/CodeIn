using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Code.Migrations
{
    /// <inheritdoc />
    public partial class Create_Table : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            //migrationBuilder.CreateTable(
            //    name: "Codebase",
            //    columns: table => new
            //    {
            //        Id = table.Column<int>(type: "int", nullable: false)
            //            .Annotation("SqlServer:Identity", "1, 1"),
            //        Name = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false),
            //        UserID = table.Column<int>(type: "int", nullable: false)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK__Codebase__3214EC07846A455D", x => x.Id);
            //    });

            migrationBuilder.CreateTable(
                name: "ProblemSolvings",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Type = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProblemSolvings", x => x.Id);
                });

            //migrationBuilder.CreateTable(
            //    name: "User",
            //    columns: table => new
            //    {
            //        Id = table.Column<int>(type: "int", nullable: false)
            //            .Annotation("SqlServer:Identity", "1, 1"),
            //        Name = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false),
            //        Email = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false),
            //        Password = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK__User__3214EC074DD2CA2F", x => x.Id);
            //    });

            //migrationBuilder.CreateIndex(
            //    name: "UQ__Codebase__737584F667128505",
            //    table: "Codebase",
            //    column: "Name",
            //    unique: true);

            //migrationBuilder.CreateIndex(
            //    name: "UQ__User__A9D10534BC813957",
            //    table: "User",
            //    column: "Email",
            //    unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            //migrationBuilder.DropTable(
            //    name: "Codebase");

            migrationBuilder.DropTable(
                name: "ProblemSolvings");

            //migrationBuilder.DropTable(
            //    name: "User");
        }
    }
}
