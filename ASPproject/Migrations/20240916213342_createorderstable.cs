using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ASPproject.Migrations
{
    /// <inheritdoc />
    public partial class createorderstable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
               name: "Orders",
               columns: table => new
               {
                   Id = table.Column<int>(type: "int", nullable: false)
                       .Annotation("SqlServer:Identity", "1, 1"),
                   Address = table.Column<string>(type: "nvarchar(max)", nullable: false),
                   InstallationDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                   OrderNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                   PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                   TotalPrice = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                   UserName = table.Column<string>(type: "nvarchar(max)", nullable: false)
               },
               constraints: table =>
               {
                   table.PrimaryKey("PK_Orders", x => x.Id);
               });
        }
           

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
               name: "Orders");
        }
    
    }
}
