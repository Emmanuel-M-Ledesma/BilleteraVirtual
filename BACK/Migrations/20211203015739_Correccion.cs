using Microsoft.EntityFrameworkCore.Migrations;

namespace PILpw.Migrations
{
    public partial class Correccion : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Cuentas",
                columns: table => new
                {
                    id_cuenta = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    id_usuario = table.Column<int>(type: "int", nullable: true),
                    tipo_cuenta = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true),
                    CVU = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true),
                    alias = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true),
                    saldo = table.Column<decimal>(type: "decimal(18,2)", unicode: false, maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cuentas", x => x.id_cuenta);
                });

            migrationBuilder.CreateTable(
                name: "Tipo_operacion",
                columns: table => new
                {
                    id_tipo_operacion = table.Column<int>(type: "int", nullable: false),
                    nombre_operacion = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tipo_operacion", x => x.id_tipo_operacion);
                });

            migrationBuilder.CreateTable(
                name: "Usuario",
                columns: table => new
                {
                    id_usuario = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    nombre_usuario = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true),
                    nombre = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true),
                    apelldio = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true),
                    dni = table.Column<int>(type: "int", nullable: true),
                    telefono = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true),
                    email = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true),
                    calle = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true),
                    ciudad = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true),
                    provincia = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true),
                    pais = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true),
                    password = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Usuario", x => x.id_usuario);
                });

            migrationBuilder.CreateTable(
                name: "Operaciones",
                columns: table => new
                {
                    id_operacion = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    id_cuenta = table.Column<int>(type: "int", nullable: true),
                    id_tipo_operacion = table.Column<int>(type: "int", nullable: true),
                    destinatario = table.Column<int>(type: "int", nullable: true),
                    monto = table.Column<decimal>(type: "decimal(18,2)", unicode: false, maxLength: 50, nullable: false),
                    fecha_operacion = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Operaciones", x => x.id_operacion);
                    table.ForeignKey(
                        name: "FK_Operaciones_Cuentas",
                        column: x => x.id_cuenta,
                        principalTable: "Cuentas",
                        principalColumn: "id_cuenta",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Operaciones_Tipo_operacion",
                        column: x => x.id_tipo_operacion,
                        principalTable: "Tipo_operacion",
                        principalColumn: "id_tipo_operacion",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Contactos",
                columns: table => new
                {
                    id_contacto = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    id_usuario = table.Column<int>(type: "int", nullable: true),
                    id_usuario_agendado = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Contactos", x => x.id_contacto);
                    table.ForeignKey(
                        name: "FK_Contactos_Usuario",
                        column: x => x.id_usuario,
                        principalTable: "Usuario",
                        principalColumn: "id_usuario",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Contactos_id_usuario",
                table: "Contactos",
                column: "id_usuario");

            migrationBuilder.CreateIndex(
                name: "IX_Operaciones_id_cuenta",
                table: "Operaciones",
                column: "id_cuenta");

            migrationBuilder.CreateIndex(
                name: "IX_Operaciones_id_tipo_operacion",
                table: "Operaciones",
                column: "id_tipo_operacion",
                filter: "[id_tipo_operacion] IS NOT NULL");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Contactos");

            migrationBuilder.DropTable(
                name: "Operaciones");

            migrationBuilder.DropTable(
                name: "Usuario");

            migrationBuilder.DropTable(
                name: "Cuentas");

            migrationBuilder.DropTable(
                name: "Tipo_operacion");
        }
    }
}
