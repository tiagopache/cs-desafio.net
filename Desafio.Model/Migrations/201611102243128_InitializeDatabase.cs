namespace Desafio.Model.Migrations
{
    using System.Data.Entity.Migrations;

    public partial class InitializeDatabase : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Usuario",
                c => new
                {
                    Id = c.Int(nullable: false, identity: true),
                    Nome = c.String(nullable: false),
                    Email = c.String(nullable: false),
                    Senha = c.String(nullable: false),
                    UltimoLogin = c.DateTime(nullable: false),
                    Token = c.String(),
                    CreatedOn = c.DateTime(nullable: false, defaultValueSql: "GETDATE()"),
                    UpdatedOn = c.DateTime(nullable: false, defaultValueSql: "GETDATE()"),
                    DeletedOn = c.DateTime(),
                })
                .PrimaryKey(t => t.Id);

            CreateTable(
                "dbo.Telefone",
                c => new
                {
                    Id = c.Int(nullable: false, identity: true),
                    Ddd = c.String(nullable: false),
                    Numero = c.String(nullable: false),
                    IdUsuario = c.Int(nullable: false),
                    CreatedOn = c.DateTime(nullable: false, defaultValueSql: "GETDATE()"),
                    UpdatedOn = c.DateTime(nullable: false, defaultValueSql: "GETDATE()"),
                    DeletedOn = c.DateTime(),
                })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Usuario", t => t.IdUsuario, cascadeDelete: true)
                .Index(t => t.IdUsuario);

        }

        public override void Down()
        {
            DropForeignKey("dbo.Telefone", "IdUsuario", "dbo.Usuario");
            DropIndex("dbo.Telefone", new[] { "IdUsuario" });
            DropTable("dbo.Telefone");
            DropTable("dbo.Usuario");
        }
    }
}
