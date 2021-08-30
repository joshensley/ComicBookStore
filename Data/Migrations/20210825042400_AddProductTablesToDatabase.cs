using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ComicBookStore.Data.Migrations
{
    public partial class AddProductTablesToDatabase : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Products",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Description = table.Column<string>(type: "nvarchar(3000)", maxLength: 3000, nullable: false),
                    RegularPrice = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    DiscountPrice = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ProductTypeID = table.Column<int>(type: "int", nullable: false),
                    CategoryTypeID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Products", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Products_CategoryTypes_CategoryTypeID",
                        column: x => x.CategoryTypeID,
                        principalTable: "CategoryTypes",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Products_ProductTypes_ProductTypeID",
                        column: x => x.ProductTypeID,
                        principalTable: "ProductTypes",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ProductSpecification",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    ProductTypeID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductSpecification", x => x.ID);
                    table.ForeignKey(
                        name: "FK_ProductSpecification_ProductTypes_ProductTypeID",
                        column: x => x.ProductTypeID,
                        principalTable: "ProductTypes",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ProductSpecificationValues",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Value = table.Column<string>(type: "nvarchar(2000)", maxLength: 2000, nullable: false),
                    ProductSpecificationID = table.Column<int>(type: "int", nullable: true),
                    ProductID = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductSpecificationValues", x => x.ID);
                    table.ForeignKey(
                        name: "FK_ProductSpecificationValues_Products_ProductID",
                        column: x => x.ProductID,
                        principalTable: "Products",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ProductSpecificationValues_ProductSpecification_ProductSpecificationID",
                        column: x => x.ProductSpecificationID,
                        principalTable: "ProductSpecification",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Products_CategoryTypeID",
                table: "Products",
                column: "CategoryTypeID");

            migrationBuilder.CreateIndex(
                name: "IX_Products_ProductTypeID",
                table: "Products",
                column: "ProductTypeID");

            migrationBuilder.CreateIndex(
                name: "IX_ProductSpecification_ProductTypeID",
                table: "ProductSpecification",
                column: "ProductTypeID");

            migrationBuilder.CreateIndex(
                name: "IX_ProductSpecificationValues_ProductID",
                table: "ProductSpecificationValues",
                column: "ProductID");

            migrationBuilder.CreateIndex(
                name: "IX_ProductSpecificationValues_ProductSpecificationID",
                table: "ProductSpecificationValues",
                column: "ProductSpecificationID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ProductSpecificationValues");

            migrationBuilder.DropTable(
                name: "Products");

            migrationBuilder.DropTable(
                name: "ProductSpecification");
        }
    }
}
