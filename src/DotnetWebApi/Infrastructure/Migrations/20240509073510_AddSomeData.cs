using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddSomeData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(
                """
                INSERT INTO Tags (Name, Description) VALUES('Phone','');
                INSERT INTO Tags (Name, Description) VALUES('PC','');
                INSERT INTO Tags (Name, Description) VALUES('Mac','');
                """
            );
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(
                """
                DELETE FROM Tags;
                """
            );
        }
    }
}
