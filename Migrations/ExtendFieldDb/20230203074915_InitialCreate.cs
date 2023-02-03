using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ExtendFieldDemo.Migrations.ExtendFieldDb
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ExtendFieldModels",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    TableName = table.Column<string>(type: "TEXT", nullable: true),
                    ModelType = table.Column<string>(type: "TEXT", nullable: true),
                    FieldName = table.Column<string>(type: "TEXT", nullable: true),
                    FieldType = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ExtendFieldModels", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ExtendFieldModels");
        }
    }
}
