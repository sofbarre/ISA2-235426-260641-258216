using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ArenaGestor.DataAccess.Migrations
{
    public partial class ticketSnack : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
         
            migrationBuilder.CreateTable(
                name: "TicketSnack",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    idTicket = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    idSnack = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TicketSnack", x => x.Id);
                });        
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
          
        }
    }
}
