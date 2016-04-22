namespace Desafio.Repository.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UsuarioDataCriacaoNaoComputada : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Telefone", "DataCriacao", c => c.DateTime(nullable: false));
            AlterColumn("dbo.Usuario", "DataCriacao", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Usuario", "DataCriacao", c => c.DateTime(nullable: false));
            AlterColumn("dbo.Telefone", "DataCriacao", c => c.DateTime(nullable: false));
        }
    }
}
