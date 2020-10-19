using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace RazorPages.Migrations
{
    public partial class ClientAndAnnotations : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Clients",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FirstName = table.Column<string>(name: "First Name", nullable: false),
                    LastName = table.Column<string>(name: "Last Name", nullable: false),
                    Address = table.Column<string>(nullable: true),
                    City = table.Column<string>(nullable: true),
                    State = table.Column<string>(nullable: true),
                    Zipcode = table.Column<string>(nullable: true),
                    Country = table.Column<string>(nullable: true),
                    Phone = table.Column<string>(nullable: true),
                    Email = table.Column<string>(nullable: false),
                    Username = table.Column<string>(nullable: false),
                    Password = table.Column<string>(nullable: false),
                    ConfirmPassword = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Clients", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Services",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: true),
                    Classification = table.Column<string>(nullable: true),
                    Fee = table.Column<double>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Services", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "ClientServices",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ClientID = table.Column<int>(nullable: false),
                    ServicesID = table.Column<int>(nullable: false),
                    Date = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ClientServices", x => x.ID);
                    table.ForeignKey(
                        name: "FK_ClientServices_Clients_ClientID",
                        column: x => x.ClientID,
                        principalTable: "Clients",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ClientServices_Services_ServicesID",
                        column: x => x.ServicesID,
                        principalTable: "Services",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Contacts",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: false),
                    ServicesID = table.Column<int>(nullable: false),
                    ContactEmail = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Contacts", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Contacts_Services_ServicesID",
                        column: x => x.ServicesID,
                        principalTable: "Services",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Clients",
                columns: new[] { "ID", "Address", "City", "ConfirmPassword", "Country", "Email", "First Name", "Last Name", "Password", "Phone", "Zipcode", "State", "Username" },
                values: new object[,]
                {
                    { 1, null, null, null, null, "flo@schmoe.net", "Flo", "Schmoe", "FloSchmoe1234*", null, null, null, "Flo" },
                    { 2, null, null, null, null, "jojo@schmoe.net", "Jo", "Schmoe", "JoJoSchmoe1234?", null, null, null, "JoJo" },
                    { 3, null, null, null, null, "truly@schmoe.net", "Truly", "Schmoe", "Truly9876**", null, null, null, "Truly" }
                });

            migrationBuilder.InsertData(
                table: "Services",
                columns: new[] { "ID", "Classification", "Fee", "Name" },
                values: new object[,]
                {
                    { 1, "Full", 450.0, "Full-Day Treatment Package" },
                    { 2, "Half", 300.0, "Half-Day Treatment Package" },
                    { 3, "Two", 225.0, "Two-Hour Treatment Package" },
                    { 4, "One", 100.0, "One-Hour Treatment Package" },
                    { 5, "Other", 200.0, "Custom Treatment Package" },
                    { 6, "Cut", 45.0, "Haircut or Trim" },
                    { 7, "Color", 100.0, "Full Foil Color" }
                });

            migrationBuilder.InsertData(
                table: "ClientServices",
                columns: new[] { "ID", "ClientID", "Date", "ServicesID" },
                values: new object[,]
                {
                    { 1, 1, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 1 },
                    { 3, 1, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 5 },
                    { 2, 2, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 7 }
                });

            migrationBuilder.InsertData(
                table: "Contacts",
                columns: new[] { "ID", "ContactEmail", "Name", "ServicesID" },
                values: new object[,]
                {
                    { 1, "wilma@flint.net", "Wilma Flintstone", 1 },
                    { 3, "betts@rubb.com", "Betty Rubble", 5 },
                    { 2, "Barn@rubb.com", "Barney Rubble", 7 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_ClientServices_ClientID",
                table: "ClientServices",
                column: "ClientID");

            migrationBuilder.CreateIndex(
                name: "IX_ClientServices_ServicesID",
                table: "ClientServices",
                column: "ServicesID");

            migrationBuilder.CreateIndex(
                name: "IX_Contacts_ServicesID",
                table: "Contacts",
                column: "ServicesID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ClientServices");

            migrationBuilder.DropTable(
                name: "Contacts");

            migrationBuilder.DropTable(
                name: "Clients");

            migrationBuilder.DropTable(
                name: "Services");
        }
    }
}
