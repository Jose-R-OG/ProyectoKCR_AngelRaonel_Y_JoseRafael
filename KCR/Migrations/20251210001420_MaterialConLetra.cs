using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace KCR.Migrations
{
    /// <inheritdoc />
    public partial class MaterialConLetra : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "materiales",
                keyColumn: "IdMaterial",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "materiales",
                keyColumn: "IdMaterial",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "materiales",
                keyColumn: "IdMaterial",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "materiales",
                keyColumn: "IdMaterial",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "materiales",
                keyColumn: "IdMaterial",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "servicios",
                keyColumn: "IdServicio",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "servicios",
                keyColumn: "IdServicio",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "servicios",
                keyColumn: "IdServicio",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "servicios",
                keyColumn: "IdServicio",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "servicios",
                keyColumn: "IdServicio",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "servicios",
                keyColumn: "IdServicio",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "servicios",
                keyColumn: "IdServicio",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "servicios",
                keyColumn: "IdServicio",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "servicios",
                keyColumn: "IdServicio",
                keyValue: 11);

            migrationBuilder.DeleteData(
                table: "servicios",
                keyColumn: "IdServicio",
                keyValue: 12);

            migrationBuilder.DeleteData(
                table: "servicios",
                keyColumn: "IdServicio",
                keyValue: 13);

            migrationBuilder.DeleteData(
                table: "servicios",
                keyColumn: "IdServicio",
                keyValue: 14);

            migrationBuilder.DeleteData(
                table: "servicios",
                keyColumn: "IdServicio",
                keyValue: 15);

            migrationBuilder.DeleteData(
                table: "servicios",
                keyColumn: "IdServicio",
                keyValue: 16);

            migrationBuilder.DeleteData(
                table: "servicios",
                keyColumn: "IdServicio",
                keyValue: 17);

            migrationBuilder.DeleteData(
                table: "servicios",
                keyColumn: "IdServicio",
                keyValue: 18);

            migrationBuilder.DeleteData(
                table: "servicios",
                keyColumn: "IdServicio",
                keyValue: 19);

            migrationBuilder.AddColumn<bool>(
                name: "Activo",
                table: "materiales",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AlterColumn<string>(
                name: "Cargo",
                table: "empleados",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.UpdateData(
                table: "servicios",
                keyColumn: "IdServicio",
                keyValue: 1,
                columns: new[] { "Nombre", "Precio" },
                values: new object[] { "SERVICIO EXPRESS", 0.0 });

            migrationBuilder.UpdateData(
                table: "servicios",
                keyColumn: "IdServicio",
                keyValue: 2,
                columns: new[] { "Nombre", "Precio" },
                values: new object[] { "DISEÑO Y EDICIÓN", 0.0 });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Activo",
                table: "materiales");

            migrationBuilder.AlterColumn<string>(
                name: "Cargo",
                table: "empleados",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.InsertData(
                table: "materiales",
                columns: new[] { "IdMaterial", "Existencia", "Nombre", "PrecioUnitario" },
                values: new object[,]
                {
                    { 1, 500, "Papel Bond 8.5x11", 1.0 },
                    { 2, 500, "Papel Bond 8.5x14", 1.5 },
                    { 3, 500, "Papel Bond 11x17", 2.0 },
                    { 4, 500, "Cartonite 11x17", 10.0 },
                    { 5, 500, "Opalina 11x17", 15.0 }
                });

            migrationBuilder.UpdateData(
                table: "servicios",
                keyColumn: "IdServicio",
                keyValue: 1,
                columns: new[] { "Nombre", "Precio" },
                values: new object[] { "COPIA B/N 8.5x11 (Bond)", 5.0 });

            migrationBuilder.UpdateData(
                table: "servicios",
                keyColumn: "IdServicio",
                keyValue: 2,
                columns: new[] { "Nombre", "Precio" },
                values: new object[] { "COPIA B/N 8.5x14 (Bond)", 10.0 });

            migrationBuilder.InsertData(
                table: "servicios",
                columns: new[] { "IdServicio", "Nombre", "Precio", "Tipo" },
                values: new object[,]
                {
                    { 3, "COPIA B/N 11x17 (Bond)", 15.0, null },
                    { 4, "COPIA COLOR 8.5x11 (Bond)", 15.0, null },
                    { 5, "IMPRESION B/N 8.5x11 (Bond)", 5.0, null },
                    { 6, "IMPRESION COLOR 8.5x11 (Bond)", 20.0, null },
                    { 7, "IMPRESION COLOR 8.5x14 (Bond)", 25.0, null },
                    { 8, "IMPRESION COLOR 11x17 (Bond)", 40.0, null },
                    { 9, "IMPRESION COLOR 11x17 (Cartonité)", 75.0, null },
                    { 10, "IMPRESION COLOR 11x17 (Opalina)", 85.0, null },
                    { 11, "IMPRESION PLANO 24x36", 50.0, null },
                    { 12, "IMPRESION PLANO 18x24", 30.0, null },
                    { 13, "ENCUADERNADO (Pequeño/Carta)", 50.0, null },
                    { 14, "ENCUADERNADO (Mediano/Oficio)", 75.0, null },
                    { 15, "ENCUADERNADO (Grande/Doble Carta)", 100.0, null },
                    { 16, "ESCANER", 15.0, null },
                    { 17, "DISEÑO", 500.0, null },
                    { 18, "SERVICIO EXPRESS", 0.0, null },
                    { 19, "DISEÑO Y EDICIÓN", 0.0, null }
                });
        }
    }
}
