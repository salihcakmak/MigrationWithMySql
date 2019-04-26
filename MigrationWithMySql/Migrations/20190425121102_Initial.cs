using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MigrationWithMySql.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AddressType",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("MySQL:AutoIncrement", true),
                    AddressTitle = table.Column<string>(maxLength: 120, nullable: true),
                    Address = table.Column<string>(maxLength: 60, nullable: true),
                    City = table.Column<string>(maxLength: 30, nullable: true),
                    Region = table.Column<string>(maxLength: 20, nullable: true),
                    PostalCode = table.Column<string>(maxLength: 15, nullable: true),
                    Country = table.Column<string>(maxLength: 20, nullable: true),
                    Phone = table.Column<string>(maxLength: 25, nullable: true),
                    Fax = table.Column<string>(maxLength: 25, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AddressType", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ProductCategory",
                columns: table => new
                {
                    CategoryID = table.Column<long>(nullable: false)
                        .Annotation("MySQL:AutoIncrement", true),
                    CategoryName = table.Column<string>(maxLength: 20, nullable: false),
                    ParentCategoryCategoryID = table.Column<long>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductCategory", x => x.CategoryID);
                    table.ForeignKey(
                        name: "FK_ProductCategory_ProductCategory_ParentCategoryCategoryID",
                        column: x => x.ParentCategoryCategoryID,
                        principalTable: "ProductCategory",
                        principalColumn: "CategoryID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Product",
                columns: table => new
                {
                    ProductID = table.Column<long>(nullable: false)
                        .Annotation("MySQL:AutoIncrement", true),
                    ProductName = table.Column<string>(maxLength: 50, nullable: false),
                    UnitName = table.Column<string>(nullable: true),
                    UnitScale = table.Column<int>(nullable: false),
                    InStock = table.Column<long>(nullable: false),
                    Price = table.Column<double>(nullable: false),
                    DiscontinuedPrice = table.Column<double>(nullable: false),
                    CategoryID = table.Column<long>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Product", x => x.ProductID);
                    table.ForeignKey(
                        name: "FK_Product_ProductCategory_CategoryID",
                        column: x => x.CategoryID,
                        principalTable: "ProductCategory",
                        principalColumn: "CategoryID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Company",
                columns: table => new
                {
                    CompanyID = table.Column<long>(nullable: false)
                        .Annotation("MySQL:AutoIncrement", true),
                    CompanyName = table.Column<string>(maxLength: 40, nullable: false),
                    Web = table.Column<string>(maxLength: 100, nullable: true),
                    Email = table.Column<string>(maxLength: 50, nullable: true),
                    AddressId = table.Column<long>(nullable: true),
                    PrimaryContactContactID = table.Column<long>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Company", x => x.CompanyID);
                    table.ForeignKey(
                        name: "FK_Company_AddressType_AddressId",
                        column: x => x.AddressId,
                        principalTable: "AddressType",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Person Contact",
                columns: table => new
                {
                    ContactID = table.Column<long>(nullable: false)
                        .Annotation("MySQL:AutoIncrement", true),
                    Title = table.Column<string>(maxLength: 8, nullable: true),
                    FirstName = table.Column<string>(maxLength: 50, nullable: true),
                    MiddleName = table.Column<string>(maxLength: 50, nullable: true),
                    LastName = table.Column<string>(maxLength: 50, nullable: true),
                    HomePhone = table.Column<string>(maxLength: 25, nullable: true),
                    MobilPhone = table.Column<string>(maxLength: 25, nullable: true),
                    AddressId = table.Column<long>(nullable: true),
                    CompanyID = table.Column<long>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Person Contact", x => x.ContactID);
                    table.ForeignKey(
                        name: "FK_Person Contact_AddressType_AddressId",
                        column: x => x.AddressId,
                        principalTable: "AddressType",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Person Contact_Company_CompanyID",
                        column: x => x.CompanyID,
                        principalTable: "Company",
                        principalColumn: "CompanyID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Orders",
                columns: table => new
                {
                    OrderID = table.Column<long>(nullable: false)
                        .Annotation("MySQL:AutoIncrement", true),
                    OrderDate = table.Column<DateTime>(nullable: false),
                    Freight = table.Column<double>(nullable: false),
                    ShipDate = table.Column<DateTime>(nullable: true),
                    Discount = table.Column<double>(nullable: false),
                    PersonContactContactID = table.Column<long>(nullable: true),
                    CompanyID = table.Column<long>(nullable: true),
                    ShipCompanyCompanyID = table.Column<long>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Orders", x => x.OrderID);
                    table.ForeignKey(
                        name: "FK_Orders_Company_CompanyID",
                        column: x => x.CompanyID,
                        principalTable: "Company",
                        principalColumn: "CompanyID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Orders_Person Contact_PersonContactContactID",
                        column: x => x.PersonContactContactID,
                        principalTable: "Person Contact",
                        principalColumn: "ContactID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Orders_Company_ShipCompanyCompanyID",
                        column: x => x.ShipCompanyCompanyID,
                        principalTable: "Company",
                        principalColumn: "CompanyID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Order Details",
                columns: table => new
                {
                    OrderID = table.Column<long>(nullable: false),
                    ProductID = table.Column<long>(nullable: false),
                    Price = table.Column<double>(nullable: false),
                    Quantity = table.Column<double>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Order Details", x => new { x.OrderID, x.ProductID });
                    table.ForeignKey(
                        name: "FK_Order Details_Orders_OrderID",
                        column: x => x.OrderID,
                        principalTable: "Orders",
                        principalColumn: "OrderID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Order Details_Product_ProductID",
                        column: x => x.ProductID,
                        principalTable: "Product",
                        principalColumn: "ProductID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Company_AddressId",
                table: "Company",
                column: "AddressId");

            migrationBuilder.CreateIndex(
                name: "IX_Company_PrimaryContactContactID",
                table: "Company",
                column: "PrimaryContactContactID");

            migrationBuilder.CreateIndex(
                name: "IX_Order Details_ProductID",
                table: "Order Details",
                column: "ProductID");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_CompanyID",
                table: "Orders",
                column: "CompanyID");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_PersonContactContactID",
                table: "Orders",
                column: "PersonContactContactID");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_ShipCompanyCompanyID",
                table: "Orders",
                column: "ShipCompanyCompanyID");

            migrationBuilder.CreateIndex(
                name: "IX_Person Contact_AddressId",
                table: "Person Contact",
                column: "AddressId");

            migrationBuilder.CreateIndex(
                name: "IX_Person Contact_CompanyID",
                table: "Person Contact",
                column: "CompanyID");

            migrationBuilder.CreateIndex(
                name: "IX_Product_CategoryID",
                table: "Product",
                column: "CategoryID");

            migrationBuilder.CreateIndex(
                name: "IX_ProductCategory_ParentCategoryCategoryID",
                table: "ProductCategory",
                column: "ParentCategoryCategoryID");

            migrationBuilder.AddForeignKey(
                name: "FK_Company_Person Contact_PrimaryContactContactID",
                table: "Company",
                column: "PrimaryContactContactID",
                principalTable: "Person Contact",
                principalColumn: "ContactID",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Company_AddressType_AddressId",
                table: "Company");

            migrationBuilder.DropForeignKey(
                name: "FK_Person Contact_AddressType_AddressId",
                table: "Person Contact");

            migrationBuilder.DropForeignKey(
                name: "FK_Company_Person Contact_PrimaryContactContactID",
                table: "Company");

            migrationBuilder.DropTable(
                name: "Order Details");

            migrationBuilder.DropTable(
                name: "Orders");

            migrationBuilder.DropTable(
                name: "Product");

            migrationBuilder.DropTable(
                name: "ProductCategory");

            migrationBuilder.DropTable(
                name: "AddressType");

            migrationBuilder.DropTable(
                name: "Person Contact");

            migrationBuilder.DropTable(
                name: "Company");
        }
    }
}
