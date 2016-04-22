namespace Desafio.Repository.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Telefone",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Ddd = c.String(nullable: false, maxLength: 4000),
                        Numero = c.String(nullable: false, maxLength: 4000),
                        IdUsuario = c.Int(nullable: false),
                        DataCriacao = c.DateTime(nullable: false),
                        DataAtualizacao = c.DateTime(nullable: false),
                        Timestamp = c.Binary(nullable: false, fixedLength: true, timestamp: true, storeType: "rowversion"),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Usuario", t => t.IdUsuario, cascadeDelete: true)
                .Index(t => t.IdUsuario);
            
            CreateTable(
                "dbo.Usuario",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Nome = c.String(nullable: false, maxLength: 4000),
                        Email = c.String(nullable: false, maxLength: 4000),
                        Senha = c.String(nullable: false, maxLength: 4000),
                        UltimoLogin = c.DateTime(nullable: false),
                        Token = c.String(maxLength: 4000),
                        DataCriacao = c.DateTime(nullable: false),
                        DataAtualizacao = c.DateTime(nullable: false),
                        Timestamp = c.Binary(nullable: false, fixedLength: true, timestamp: true, storeType: "rowversion"),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Telefone", "IdUsuario", "dbo.Usuario");
            DropIndex("dbo.Telefone", new[] { "IdUsuario" });
            DropTable("dbo.Usuario");
            DropTable("dbo.Telefone");
        }
    }
}
