using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace polisync.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreateWithSeededTables : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Policies",
                columns: table => new
                {
                    PolicyId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    PolicyNumber = table.Column<string>(type: "TEXT", nullable: false),
                    PolicyType = table.Column<string>(type: "TEXT", nullable: false),
                    StartDate = table.Column<DateTime>(type: "TEXT", nullable: false),
                    EndDate = table.Column<DateTime>(type: "TEXT", nullable: false),
                    PolicyLimit = table.Column<decimal>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Policies", x => x.PolicyId);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    UserId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", nullable: false),
                    Email = table.Column<string>(type: "TEXT", nullable: false),
                    Password = table.Column<string>(type: "TEXT", nullable: false),
                    Role = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.UserId);
                });

            migrationBuilder.CreateTable(
                name: "Claims",
                columns: table => new
                {
                    ClaimId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    UserId = table.Column<int>(type: "INTEGER", nullable: false),
                    PolicyId = table.Column<int>(type: "INTEGER", nullable: false),
                    IncidentDescription = table.Column<string>(type: "TEXT", nullable: false),
                    IncidentDate = table.Column<DateTime>(type: "TEXT", nullable: false),
                    ClaimAmount = table.Column<decimal>(type: "TEXT", nullable: false),
                    Status = table.Column<int>(type: "INTEGER", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Claims", x => x.ClaimId);
                    table.ForeignKey(
                        name: "FK_Claims_Policies_PolicyId",
                        column: x => x.PolicyId,
                        principalTable: "Policies",
                        principalColumn: "PolicyId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Claims_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PolicyUserModel",
                columns: table => new
                {
                    PoliciesPolicyId = table.Column<int>(type: "INTEGER", nullable: false),
                    UsersUserId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PolicyUserModel", x => new { x.PoliciesPolicyId, x.UsersUserId });
                    table.ForeignKey(
                        name: "FK_PolicyUserModel_Policies_PoliciesPolicyId",
                        column: x => x.PoliciesPolicyId,
                        principalTable: "Policies",
                        principalColumn: "PolicyId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PolicyUserModel_Users_UsersUserId",
                        column: x => x.UsersUserId,
                        principalTable: "Users",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Policies",
                columns: new[] { "PolicyId", "EndDate", "PolicyLimit", "PolicyNumber", "PolicyType", "StartDate" },
                values: new object[,]
                {
                    { 1, new DateTime(2027, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 15000000m, "HEALTH/4/2023", "Health", new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 2, new DateTime(2027, 2, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 25000000m, "MOTOR/1/2024", "Motor", new DateTime(2026, 2, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 3, new DateTime(2026, 7, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 7500000m, "PROPERTY/2/2023", "Property", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 4, new DateTime(2026, 7, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 2500000m, "TRAVEL/4/2022", "Travel", new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 5, new DateTime(2026, 12, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 10000000m, "WIBA/1/2025", "WIBA", new DateTime(2025, 12, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "UserId", "Email", "Name", "Password", "Role" },
                values: new object[,]
                {
                    { 1, "allan.doe@email.com", "Allan Doe", "Allan@123!", "Customer" },
                    { 2, "beth.doe@email.com", "Beth Doe", "Beth@123!", "Customer" },
                    { 3, "charles.doe@email.com", "Charles Doe", "Charles@123!", "Customer" },
                    { 4, "diana.doe@email.com", "Diana Doe", "Diana@123!", "Customer" },
                    { 5, "edward.doe@email.com", "Edward Doe", "Edward@123!", "Customer" },
                    { 6, "faith.doe@email.com", "Faith Doe", "Faith@123!", "Customer" },
                    { 7, "gerald.doe@email.com", "Gerald Doe", "Gerald@123!", "Customer" },
                    { 8, "hannah.doe@email.com", "Hannah Doe", "Hannah@123!", "Customer" },
                    { 9, "ivy.doe@email.com", "Ivy Doe", "Ivy@123!", "Customer" },
                    { 10, "john.doe@email.com", "John Doe", "John@123!", "Customer" },
                    { 11, "kellen.smith@polisync.com", "Kellen Smith", "Kellen@123!", "Administrator" },
                    { 12, "larry.smith@polisync.com", "Larry Smith", "Larry@123!", "Administrator" }
                });

            migrationBuilder.InsertData(
                table: "Claims",
                columns: new[] { "ClaimId", "ClaimAmount", "CreatedAt", "IncidentDate", "IncidentDescription", "PolicyId", "Status", "UserId" },
                values: new object[,]
                {
                    { 1, 250000m, new DateTime(2026, 7, 7, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2026, 4, 13, 22, 50, 0, 0, DateTimeKind.Unspecified), "Laptop Theft", 3, 1, 3 },
                    { 2, 120000m, new DateTime(2026, 7, 7, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2026, 5, 29, 15, 3, 0, 0, DateTimeKind.Unspecified), "Bodily Injury At Work", 5, 1, 1 },
                    { 3, 1000000m, new DateTime(2026, 7, 7, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2026, 7, 1, 10, 32, 0, 0, DateTimeKind.Unspecified), "Car Accident", 2, 1, 2 },
                    { 4, 1000000m, new DateTime(2026, 7, 7, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2026, 7, 5, 10, 32, 0, 0, DateTimeKind.Unspecified), "Baggage Loss", 4, 1, 8 },
                    { 5, 50000m, new DateTime(2026, 7, 7, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2026, 5, 26, 12, 8, 0, 0, DateTimeKind.Unspecified), "Motor Windshield Replacement", 2, 1, 2 },
                    { 6, 200000m, new DateTime(2026, 7, 7, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2026, 4, 7, 21, 33, 0, 0, DateTimeKind.Unspecified), "Medical Surgery", 1, 1, 4 },
                    { 7, 100000m, new DateTime(2026, 7, 7, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2026, 5, 29, 14, 17, 0, 0, DateTimeKind.Unspecified), "Equipment Damage", 3, 1, 1 },
                    { 8, 75000m, new DateTime(2026, 7, 7, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2026, 6, 29, 20, 4, 0, 0, DateTimeKind.Unspecified), "Cancelled Flight", 4, 1, 8 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Claims_PolicyId",
                table: "Claims",
                column: "PolicyId");

            migrationBuilder.CreateIndex(
                name: "IX_Claims_UserId",
                table: "Claims",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_PolicyUserModel_UsersUserId",
                table: "PolicyUserModel",
                column: "UsersUserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Claims");

            migrationBuilder.DropTable(
                name: "PolicyUserModel");

            migrationBuilder.DropTable(
                name: "Policies");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
