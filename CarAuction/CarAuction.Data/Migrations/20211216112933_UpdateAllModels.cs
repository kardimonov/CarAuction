using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CarAuction.Data.Migrations
{
    public partial class UpdateAllModels : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AuctionCar_Auctions_AuctionId",
                table: "AuctionCar");

            migrationBuilder.DropForeignKey(
                name: "FK_AuctionCar_Cars_CarId",
                table: "AuctionCar");

            migrationBuilder.DropForeignKey(
                name: "FK_Bids_Auctions_AuctionId",
                table: "Bids");

            migrationBuilder.DropForeignKey(
                name: "FK_Bids_Cars_CarId",
                table: "Bids");

            migrationBuilder.DropPrimaryKey(
                name: "PK_AuctionCar",
                table: "AuctionCar");

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DropColumn(
                name: "AuctionPrice",
                table: "Cars");

            migrationBuilder.RenameTable(
                name: "AuctionCar",
                newName: "AuctionCars");

            migrationBuilder.RenameColumn(
                name: "AuctionId",
                table: "Bids",
                newName: "AuctionCarId");

            migrationBuilder.RenameIndex(
                name: "IX_Bids_AuctionId",
                table: "Bids",
                newName: "IX_Bids_AuctionCarId");

            migrationBuilder.RenameIndex(
                name: "IX_AuctionCar_CarId",
                table: "AuctionCars",
                newName: "IX_AuctionCars_CarId");

            migrationBuilder.AddColumn<string>(
                name: "Manufacture",
                table: "Cars",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Model",
                table: "Cars",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AlterColumn<int>(
                name: "CarId",
                table: "Bids",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<DateTime>(
                name: "Time",
                table: "Bids",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "AuctionCars",
                type: "int",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddColumn<int>(
                name: "AuctionPrice",
                table: "AuctionCars",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddPrimaryKey(
                name: "PK_AuctionCars",
                table: "AuctionCars",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_AuctionCars_AuctionId",
                table: "AuctionCars",
                column: "AuctionId");

            migrationBuilder.AddForeignKey(
                name: "FK_AuctionCars_Auctions_AuctionId",
                table: "AuctionCars",
                column: "AuctionId",
                principalTable: "Auctions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AuctionCars_Cars_CarId",
                table: "AuctionCars",
                column: "CarId",
                principalTable: "Cars",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Bids_AuctionCars_AuctionCarId",
                table: "Bids",
                column: "AuctionCarId",
                principalTable: "AuctionCars",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Bids_Cars_CarId",
                table: "Bids",
                column: "CarId",
                principalTable: "Cars",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AuctionCars_Auctions_AuctionId",
                table: "AuctionCars");

            migrationBuilder.DropForeignKey(
                name: "FK_AuctionCars_Cars_CarId",
                table: "AuctionCars");

            migrationBuilder.DropForeignKey(
                name: "FK_Bids_AuctionCars_AuctionCarId",
                table: "Bids");

            migrationBuilder.DropForeignKey(
                name: "FK_Bids_Cars_CarId",
                table: "Bids");

            migrationBuilder.DropPrimaryKey(
                name: "PK_AuctionCars",
                table: "AuctionCars");

            migrationBuilder.DropIndex(
                name: "IX_AuctionCars_AuctionId",
                table: "AuctionCars");

            migrationBuilder.DropColumn(
                name: "Manufacture",
                table: "Cars");

            migrationBuilder.DropColumn(
                name: "Model",
                table: "Cars");

            migrationBuilder.DropColumn(
                name: "Time",
                table: "Bids");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "AuctionCars");

            migrationBuilder.DropColumn(
                name: "AuctionPrice",
                table: "AuctionCars");

            migrationBuilder.RenameTable(
                name: "AuctionCars",
                newName: "AuctionCar");

            migrationBuilder.RenameColumn(
                name: "AuctionCarId",
                table: "Bids",
                newName: "AuctionId");

            migrationBuilder.RenameIndex(
                name: "IX_Bids_AuctionCarId",
                table: "Bids",
                newName: "IX_Bids_AuctionId");

            migrationBuilder.RenameIndex(
                name: "IX_AuctionCars_CarId",
                table: "AuctionCar",
                newName: "IX_AuctionCar_CarId");

            migrationBuilder.AddColumn<int>(
                name: "AuctionPrice",
                table: "Cars",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<int>(
                name: "CarId",
                table: "Bids",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_AuctionCar",
                table: "AuctionCar",
                columns: new[] { "AuctionId", "CarId" });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Password", "Role", "UserName" },
                values: new object[] { 1, "adm123", "admin", "admin" });

            migrationBuilder.AddForeignKey(
                name: "FK_AuctionCar_Auctions_AuctionId",
                table: "AuctionCar",
                column: "AuctionId",
                principalTable: "Auctions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AuctionCar_Cars_CarId",
                table: "AuctionCar",
                column: "CarId",
                principalTable: "Cars",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Bids_Auctions_AuctionId",
                table: "Bids",
                column: "AuctionId",
                principalTable: "Auctions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Bids_Cars_CarId",
                table: "Bids",
                column: "CarId",
                principalTable: "Cars",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
